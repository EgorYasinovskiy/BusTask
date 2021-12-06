using BusTask.GraphLogic;

namespace BusTask.UI
{
	public partial class Form1 : Form
	{
		Timetable timetable;
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
					timetable = Timetable.Load(ofd.FileName);
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

			Graph graph = new Graph(timetable);
			var res = graph.Dijkstra(start,end,time, this.checkBox1.Checked);
			if (res == null)
				MessageBox.Show("ѕуть не найден");
			else
				MessageBox.Show(string.Join("\n", res.Select(x => $"{x.StopId},{x.ArrivalTime}").ToArray()));
		}
	}
}