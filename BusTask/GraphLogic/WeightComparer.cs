namespace BusTask.GraphLogic
{
	public class WeightComparer : IComparer<Weight>

	{
		public int Compare(Weight x, Weight y)
		{
			if (x > y)
				return 1;
			if (x < y)
				return -1;
			return 0;
		}
	}
}
