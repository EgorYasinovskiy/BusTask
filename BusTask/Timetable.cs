namespace BusTask
{
	public class Timetable
	{
		public List<Route> Routes { get; set; }

		private Timetable() { }

		public static Timetable Load(string filePath)
		{
			var fileData = File.ReadAllLines(filePath);
			var startTimes = fileData[2].Split(" ").Select(x => TimeOnly.Parse(x)).ToArray();
			var prices = fileData[3].Split(" ").Select(x => int.Parse(x)).ToArray();
			var routes = new List<Route>();
			for(int i = 0; i < fileData.Length - 4; i++)
			{		
				var routeData = fileData[i + 4].Split(" ").Skip(1);
				var stops = routeData.Take(routeData.Count() / 2).Select(x => int.Parse(x));
				var times = routeData.Skip(routeData.Count() / 2).Select(x => int.Parse(x));
				routes.Add(new Route(stops, times, startTimes[i], prices[i],i));
			}
			
			var timeTable = new Timetable();
			timeTable.Routes = routes;
			return timeTable;
		}
	}
}