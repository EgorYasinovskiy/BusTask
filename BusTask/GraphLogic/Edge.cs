namespace BusTask.GraphLogic
{
	public class Edge
	{
		public int From { get; set; }
		public int RouteFrom { get; set; }
		public int To { get; set; }
		public int RouteTo { get; set; }
		public Weight Time { get; set; }
		public Weight Price { get; set; }
	}
}
