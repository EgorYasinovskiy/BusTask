using BusTask.GraphLogic;

namespace BusTask.UI
{
	public partial class Form1 : Form
	{
		Timetable _timetable;
		Graph _graph;
		public Form1()
		{
			InitializeComponent();
			TurnControls(false);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					_timetable = Timetable.Load(ofd.FileName);
					_graph = new Graph(_timetable);
					TurnControls(true);
				}
			}
		}
		private void TurnControls(bool enabled)
		{
			tbStart.Enabled = enabled;
			tbEnd.Enabled = enabled;
			tbTime.Enabled = enabled;
			button2.Enabled = enabled;
			checkBox1.Enabled = enabled;
		}
		private void button2_Click(object sender, EventArgs e)
		{
			int start = int.Parse(tbStart.Text);
			int end = int.Parse(tbEnd.Text);
			TimeOnly time = TimeOnly.Parse(tbTime.Text);
			var res = _graph.FindRoute(start, end, time, checkBox1.Checked);
			if (res == null)
				MessageBox.Show("Путь не найден");
			else
			{
				var answer = string.Join('\n', res.Select(x => $"От остановки {x.From}(Маршрут {x.RouteFrom + 1}) до остановки{x.To}(Маршрут {x.RouteTo + 1})").ToArray());
				MessageBox.Show(answer);
			}
		}
	}
}