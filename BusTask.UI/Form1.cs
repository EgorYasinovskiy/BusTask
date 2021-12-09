using BusTask.GraphLogic;
using System.Text;

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
				MessageBox.Show("���� �� ������");
			else
			{
				var answer = GeneratePathString(res);
				MessageBox.Show(answer);
			}
		}

		private string GeneratePathString(Edge[] edges)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var edge in edges)
			{
				if (edge.RouteFrom != edge.RouteTo)
				{
					builder.AppendLine($"��������� �� ��������� {edge.From} � �������� {edge.RouteFrom + 1} �� {edge.RouteTo + 1}\r\n");
				}
				else if (edge.From != edge.To)
				{
					builder.AppendLine($"�� {edge.From} �� {edge.To} �� �������� {edge.RouteTo + 1}\r\n");
				}
			}
			builder.AppendLine();
			builder.AppendLine($"����� � ����: {edges.Last().Time.Value} ���.");
			builder.AppendLine($"���� �������: {edges.Last().Price.Value} ���.");
			return builder.ToString();
		}
	}
}