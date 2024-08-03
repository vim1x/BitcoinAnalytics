namespace BitcoinAnalytics
{
    partial class BitcoinChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            SuspendLayout();
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart.Legends.Add(legend1);
            chart.Location = new Point(11, 33);
            chart.Name = "chart";
            series1.BorderWidth = 5;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 4;
            chart.Series.Add(series1);
            chart.Size = new Size(1131, 637);
            chart.TabIndex = 0;
            chart.Text = " ngf";
            // 
            // button1
            // 
            button1.Location = new Point(1150, 33);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "6 Годин";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1150, 68);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 2;
            button2.Text = "День";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1150, 103);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 3;
            button3.Text = "Тиждень";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1150, 139);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 4;
            button4.Text = "Місяць";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(14, 673);
            label1.Name = "label1";
            label1.Size = new Size(107, 32);
            label1.TabIndex = 5;
            label1.Text = "Прогноз";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 707);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 6;
            label2.Text = "SMA(10)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(80, 707);
            label3.Name = "label3";
            label3.Size = new Size(94, 20);
            label3.TabIndex = 7;
            label3.Text = "Нейтрально";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 727);
            label4.Name = "label4";
            label4.Size = new Size(66, 20);
            label4.TabIndex = 8;
            label4.Text = "SMA(20)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(80, 727);
            label5.Name = "label5";
            label5.Size = new Size(94, 20);
            label5.TabIndex = 9;
            label5.Text = "Нейтрально";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 747);
            label6.Name = "label6";
            label6.Size = new Size(66, 20);
            label6.TabIndex = 10;
            label6.Text = "SMA(50)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(80, 747);
            label7.Name = "label7";
            label7.Size = new Size(94, 20);
            label7.TabIndex = 11;
            label7.Text = "Нейтрально";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(14, 767);
            label8.Name = "label8";
            label8.Size = new Size(141, 20);
            label8.TabIndex = 12;
            label8.Text = "Awesome Oscillator";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(190, 767);
            label9.Name = "label9";
            label9.Size = new Size(94, 20);
            label9.TabIndex = 13;
            label9.Text = "Нейтрально";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(14, 787);
            label10.Name = "label10";
            label10.Size = new Size(56, 20);
            label10.TabIndex = 14;
            label10.Text = "RSI(14)";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(115, 787);
            label11.Name = "label11";
            label11.Size = new Size(94, 20);
            label11.TabIndex = 15;
            label11.Text = "Нейтрально";
            // 
            // BitcoinChart
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1313, 1055);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(chart);
            Name = "BitcoinChart";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
    }
}