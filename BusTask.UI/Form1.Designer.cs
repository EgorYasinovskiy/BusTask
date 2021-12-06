namespace BusTask.UI
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.tbStart = new System.Windows.Forms.TextBox();
			this.tbEnd = new System.Windows.Forms.TextBox();
			this.tbTime = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(237, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Выбрать файл";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(12, 128);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(104, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "Найти путь";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(122, 132);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(71, 19);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "По цене";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// tbStart
			// 
			this.tbStart.Location = new System.Drawing.Point(137, 45);
			this.tbStart.Name = "tbStart";
			this.tbStart.Size = new System.Drawing.Size(112, 23);
			this.tbStart.TabIndex = 2;
			// 
			// tbEnd
			// 
			this.tbEnd.Location = new System.Drawing.Point(137, 74);
			this.tbEnd.Name = "tbEnd";
			this.tbEnd.Size = new System.Drawing.Size(112, 23);
			this.tbEnd.TabIndex = 2;
			// 
			// tbTime
			// 
			this.tbTime.Location = new System.Drawing.Point(137, 103);
			this.tbTime.Name = "tbTime";
			this.tbTime.Size = new System.Drawing.Size(112, 23);
			this.tbTime.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Начальная точка";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Конечная  точка";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 15);
			this.label3.TabIndex = 3;
			this.label3.Text = "Время отправления";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.tbEnd);
			this.Controls.Add(this.tbStart);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button button1;
		private Button button2;
		private CheckBox checkBox1;
		private TextBox tbStart;
		private TextBox tbEnd;
		private TextBox tbTime;
		private Label label1;
		private Label label2;
		private Label label3;
	}
}