namespace BusTask.GraphLogic
{
	public class Graph
	{
		private readonly Timetable _timetable;
		private readonly WeightComparer _weightComparer = new WeightComparer();
		public List<Edge> Edges { get; set; }
		public List<BusStop> Vertices { get; set; }
		public Graph(Timetable timetable)
		{
			Vertices = new List<BusStop>();
			Edges = new List<Edge>();

			_timetable = timetable;
			foreach (var route in timetable.Routes)
			{
				List<Edge> edges = new List<Edge>();
				for (int i = 0; i < route.Stops.Count; i++)
				{
					Vertices.Add(new BusStop(route.Stops[i], route.Id));

					for (int j = 0; j < route.Stops.Count; j++)
					{
						if (i == j)
							continue;

						int time;
						if (i < j)
							time = route.TimeSpans.Skip(i).Take(j - i).Sum();
						else
							time = route.TimeSpans.Skip(i).Sum() + route.TimeSpans.Take(j).Sum();

						edges.Add(new Edge()
						{
							From = route.Stops[i],
							To = route.Stops[j],
							Time = new Weight() { Value = time },
							Price = new Weight() { Value = route.Price },
							RouteFrom = route.Id,
							RouteTo = route.Id
						});
					}

				}

				Edges.AddRange(edges);
			}
			// Добавляем ребра пересадок(значение Time указываем null), чтобы расчитать время передадки в зависимости от времени выезда
			foreach (var stop in Vertices)
			{
				var transitStops = Vertices.Where(x => x.StopId == stop.StopId && x.Route != stop.Route);
				foreach (var transitStop in transitStops)
				{
					Edges.Add(new Edge()
					{
						From = stop.StopId,
						To = stop.StopId,
						Time = new Weight() { Value = null },
						Price = new Weight() { Value = 0 },
						RouteFrom = stop.Route,
						RouteTo = transitStop.Route
					});
				}
			}
		}
		public Edge[] FindRoute(int from, int to, TimeOnly startTime, bool byPrice = false)
		{
			var allStops = _timetable.Routes.SelectMany(r => r.BusStops).Where(r => r.ArrivalTime >= startTime);
			var startStop = allStops.Where(x => x.StopId == from).MinBy(x => x.ArrivalTime);
			if (startStop == null)
				return null;
			var visited = new List<BusStop>() { startStop };
			var pathEdges = GetPathInitValues(startStop).ToArray();

			while (visited.Count != Vertices.Count)
			{
				var availableEdges = pathEdges.Where(x => !visited.Any(s => s.StopId == x.To && s.Route == x.RouteTo));
				if (availableEdges.Count() == 0)
					break;
				Edge currentEdge;

				currentEdge = byPrice ? availableEdges.MinBy(x => x.Price, _weightComparer) : availableEdges.MinBy(x => x.Time, _weightComparer);
				if (currentEdge != null)
				{
					var currentTime = currentEdge.Time.Value ?? 0;

					var relatedEdges = Edges.Where(x => x.From == currentEdge.To && x.RouteFrom == currentEdge.RouteTo);
					foreach (var edge in relatedEdges)
					{
						var actualPath = pathEdges.FirstOrDefault(x => x.To == edge.To && x.RouteTo == edge.RouteTo) ;
						Weight transitTime = new Weight() { Value = null };
						if (edge.RouteTo != edge.RouteFrom)
						{
							// расчет времени пересадки
							var nearestTransit = allStops.Where(x =>
								x.Route == edge.RouteTo
								&& x.StopId == edge.To
								&& x.ArrivalTime >= startStop.ArrivalTime.AddMinutes(currentTime)).MinBy(x => x.ArrivalTime);
							if (nearestTransit == null) // Нет точки пересадки по текущему времени
							{
								visited.Add(new BusStop(edge.To, edge.RouteTo));
								continue;
							}
							else
							{
								transitTime = new Weight() { Value = (int)(nearestTransit.ArrivalTime - startStop.ArrivalTime.AddMinutes(currentTime)).TotalMinutes };
								if(currentTime+transitTime.Value > 1440)
								{
									visited.Add(new BusStop(edge.To, edge.RouteTo));
									continue;
								}
							}
							edge.Time = transitTime;
						}

						if ((byPrice && (actualPath.Price > edge.Price + currentEdge.Price))
						|| (!byPrice && (actualPath.Time > edge.Time + currentEdge.Time)))
						{
							actualPath.From = edge.From;
							actualPath.Time = edge.Time + currentEdge.Time;
							actualPath.Price = edge.Price + currentEdge.Price;
							actualPath.RouteFrom = edge.RouteFrom;
						}
					}
					visited.Add(new BusStop(currentEdge.To, currentEdge.RouteTo));
				}

			}
			// restore path
			var resPath = new List<Edge>();
			var endPoint = byPrice ?
				pathEdges.Where(x => x.Price.Value != null && x.To == to).MinBy(x => x.Price, _weightComparer)
				: pathEdges.Where(x => x.Time.Value != null && x.To == to).MinBy(x => x.Time, _weightComparer);
			if (endPoint == null)
				return null;
			do
			{
				resPath.Add(endPoint);
				if(endPoint.From == null || endPoint.To == null || endPoint.Time.Value == null || endPoint.Price.Value == null)
					return null;

				endPoint = pathEdges.FirstOrDefault(x => x.To == endPoint.From && x.RouteTo == endPoint.RouteFrom);
			}
			while (endPoint.From != from);

			resPath.Add(endPoint);
			resPath.Reverse();
			return resPath.ToArray();
		}

		private IEnumerable<Edge> GetPathInitValues(BusStop start)
		{
			foreach (var vertex in Vertices)
			{
				if (vertex.StopId == start.StopId && vertex.Route == start.Route)
				{
					yield return new Edge()
					{
						From = start.StopId,
						To = start.StopId,
						RouteFrom = start.Route,
						RouteTo = start.Route,
						Price = new Weight() { Value = 0 },
						Time = new Weight() { Value = 0 }
					};
				}
				else
				{
					var foundMove = Edges.FirstOrDefault(x => x.From == start.StopId && x.RouteFrom == start.Route && x.To == vertex.StopId && x.RouteTo == vertex.Route);

					yield return new Edge()
					{
						From = start.StopId,
						To = vertex.StopId,
						RouteFrom = start.Route,
						RouteTo = vertex.Route,
						Price = new Weight() { Value = foundMove?.Price?.Value },
						Time = new Weight() { Value = foundMove?.Time?.Value }
					};
				}
			}
		}
	}
}
