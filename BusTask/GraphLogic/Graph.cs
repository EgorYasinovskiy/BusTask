namespace BusTask.GraphLogic
{
	public class Graph
	{
		public BusStop[] BusStops { get; set; }
		public Move[] Moves { get; set; }
		private WeightComparer _weightComparer = new WeightComparer();
		public Graph(Timetable timetable)
		{
			BusStops = timetable.Routes.SelectMany(x => x.BusStops).ToArray();
			List<Move> moves = new List<Move>();
			foreach (var route in timetable.Routes)
			{
				route.BusStops = route.BusStops.OrderBy(x => x.ArrivalTime).ToList();
				for (int i = 0; i < route.BusStops.Count - 1; i++)
				{
					for (int j = i+1; j < route.BusStops.Count; j++)
					{
						moves.Add(new Move
						{
							From = route.BusStops[i],
							To = route.BusStops[j],
							Time = new Weight { Value = (int)(route.BusStops[j].ArrivalTime - route.BusStops[i].ArrivalTime).TotalMinutes },
							Price = new Weight { Value = route.Price }
						});
					}
					// ребра пересадок
					foreach (var transitRoute in timetable.Routes.Except(new[] { route }))
					{
						foreach (var transitStop in transitRoute.BusStops.Where(x => x.StopId == route.BusStops[i].StopId && x.ArrivalTime >= route.BusStops[i].ArrivalTime))
						{
							moves.Add(new Move
							{
								From = route.BusStops[i],
								To = transitStop,
								Time = new Weight { Value = (int)(transitStop.ArrivalTime - route.BusStops[i].ArrivalTime).TotalMinutes },
								Price = new Weight { Value = 0 }
							});
						}
					}
				}
			}
			Moves = moves.ToArray();
		}

		public List<BusStop> Dijkstra(int startPoint, int endPoint, TimeOnly startTime, bool byPrice)
		{
			var actualBusStops = BusStops.Where(x => x.ArrivalTime >= startTime).ToArray();
			var actualMoves = Moves.Where(x => x.From.ArrivalTime >= startTime);
			var startBusStop = actualBusStops.FirstOrDefault(x => x.StopId == startPoint);
			if (startBusStop == null)
				return null;
			var pathValues = new Move[actualBusStops.Length];
			for (int i = 0; i < actualBusStops.Length; i++)
			{
				if (actualBusStops[i] == startBusStop)
				{
					pathValues[i] = new Move
					{
						From = startBusStop,
						To = startBusStop,
						Time = new Weight { Value = 0 },
						Price = new Weight { Value = 0 }
					};
				}
				else
				{
					var foundMove = actualMoves.FirstOrDefault(x => x.From == startBusStop && x.To == actualBusStops[i]);
					pathValues[i] = new Move()
					{
						From = startBusStop,
						To = actualBusStops[i],
						Price = foundMove?.Price,
						Time = foundMove?.Time
					};

				}
			}
			var visitedStops = new List<BusStop>() { startBusStop };
			while (visitedStops.Count() != actualBusStops.Count())
			{
				var pathValuesToSelect = pathValues.Where(x => !visitedStops.Contains(x.To));
				Move currentMove;
				if (byPrice)
					currentMove = pathValuesToSelect.MinBy(x => x.Price, _weightComparer);
				else
					currentMove = pathValuesToSelect.MinBy(x => x.Time, _weightComparer);
				var currentStop = currentMove.To;
				var possibleMoves = actualMoves.Where(x => x.From == currentStop);
				foreach (var possibleMove in possibleMoves)
				{
					var actualMove = pathValues.FirstOrDefault(x => x.To == possibleMove.To);
					if ((byPrice && actualMove.Price > (possibleMove.Price + currentMove.Price)) || (!byPrice && actualMove.Time > (possibleMove.Time + currentMove.Time)))
					{
						actualMove.From = possibleMove.From;
						actualMove.Time = possibleMove.Time + currentMove.Time;
						actualMove.Price = possibleMove.Price + currentMove.Price;
					}
				}
				visitedStops.Add(currentStop);
			}
			//restore path
			Move endMove;
			if (byPrice)
				endMove = pathValues.Where(x => x.To.StopId == endPoint).MinBy(x => x.Price, _weightComparer);
			else
				endMove = pathValues.Where(x => x.To.StopId == endPoint).MinBy(x=>x.Time,_weightComparer);
			var resPath = new List<BusStop>();
			if (endMove == null)
				return null;
			if (endMove.Price?.Value == null || endMove.Time?.Value == null)
				return null;
			do
			{
				resPath.Add(endMove.To);
				endMove = pathValues.FirstOrDefault(x => x.To == endMove.From);
			}
			while (endMove.From.StopId != startPoint);
			resPath.Add(endMove.From);
			resPath.Reverse();
			return resPath;

		}
	}
}
