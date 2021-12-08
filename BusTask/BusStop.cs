namespace BusTask
{
	public class BusStop
	{
		public int StopId { get; set; }
		public int Route { get; set;}
		public TimeOnly ArrivalTime { get; set; }

		public BusStop(int stopId, int route)
		{
			StopId = stopId;
			Route = route;
		}

	}
}