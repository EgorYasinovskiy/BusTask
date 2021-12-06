namespace BusTask.GraphLogic
{
	public class Move
	{
		public BusStop From { get; set; }
		public BusStop To { get; set; }
		public Weight Time { get; set; }
		public Weight Price { get; set; }
		}
	}