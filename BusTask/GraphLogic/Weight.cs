namespace BusTask.GraphLogic
{
	public class Weight
	{
		public int? Value { get; set; }
		public static bool operator >(Weight left, Weight right)
		{
			if (left?.Value == null && right?.Value != null)
				return true;
			else if (left?.Value == null && right?.Value == null)
				return false;
			else if (left?.Value != null && right?.Value == null)
				return false;
			else
				return left?.Value > right?.Value;
		}
		public static bool operator <(Weight left, Weight right)
		{
			if (left?.Value == null && right?.Value != null)
				return false;
			else if (left?.Value != null && right?.Value == null)
				return true;
			else if (left?.Value == null && right?.Value == null)
				return false;
			else
				return left?.Value < right?.Value;
		}
		public static bool operator ==(Weight left, Weight right)
		{
			return left?.Value == right?.Value;
		}
		public static bool operator !=(Weight left, Weight right)
		{
			return left?.Value != right?.Value;
		}
		public static bool operator <=(Weight left, Weight right)
		{
			return left < right || left == right;
		}
		public static bool operator >=(Weight left, Weight right)
		{
			return left > right || left == right;
		}
		public static Weight operator +(Weight left, Weight right)
		{
			if (left == null || right == null || left?.Value == null || right?.Value == null)
				return new Weight() { Value = null };
			else
				return new Weight() { Value = left?.Value + right?.Value };
		}
	}

}

