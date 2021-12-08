namespace BusTask
{
	public class Route
	{
		public List<int> Stops { get; set; }
		public List<int> TimeSpans { get; set; }
		public TimeOnly StartTime { get; set; }
		public int Price { get; set; }
		public int Id { get; set; }
		public List<BusStop> BusStops { get; set; } = new List<BusStop>();

		public Route(IEnumerable<int> stops, IEnumerable<int> timespans, TimeOnly startTime, int price, int id)
		{
			Stops = stops.ToList();
			TimeSpans = timespans.ToList();
			StartTime = startTime;
			Price = price;
			Id = id;
			InitBusStops();
		}

		private void InitBusStops()
		{
			TimeOnly time = StartTime;
			var counter = 0;
			while(time >= StartTime)
			{
				BusStops.Add(new BusStop(Stops[counter],Id) { ArrivalTime = time });
				time = time.AddMinutes(TimeSpans[counter]);
				counter++;
				if (counter == Stops.Count)
					counter = 0;
			}
		}
	}
}