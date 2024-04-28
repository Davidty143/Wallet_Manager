namespace Wallet_Manager.Forms
{
    partial class InsightsUC
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.Charts.WinForms.ChartFont chartFont1 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont2 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont3 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont4 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid1 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick1 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont5 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid2 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick2 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont6 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid3 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel1 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont7 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick3 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont8 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.LPoint lPoint1 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.ChartFont chartFont9 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont10 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont11 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont12 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid4 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick4 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont13 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid5 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick5 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont14 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid6 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel2 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont15 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick6 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont16 = new Guna.Charts.WinForms.ChartFont();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.barChart1 = new Guna.Charts.WinForms.GunaChart();
            this.gunaBarDataset1 = new Guna.Charts.WinForms.GunaBarDataset();
            this.gunaBarDataset2 = new Guna.Charts.WinForms.GunaBarDataset();
            this.gunaBarDataset3 = new Guna.Charts.WinForms.GunaBarDataset();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2CustomGradientPanel2 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.pieChart1 = new Guna.Charts.WinForms.GunaChart();
            this.timeFrame = new Guna.UI2.WinForms.Guna2ComboBox();
            this.gunaPieDataset1 = new Guna.Charts.WinForms.GunaPieDataset();
            this.guna2CustomGradientPanel3 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.pieChart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.guna2CustomGradientPanel2.SuspendLayout();
            this.guna2CustomGradientPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart2)).BeginInit();
            this.SuspendLayout();
            // 
            // barChart1
            // 
            this.barChart1.Animation.Easing = Guna.Charts.WinForms.Easing.EaseOutBack;
            this.barChart1.Datasets.AddRange(new Guna.Charts.Interfaces.IGunaDataset[] {
            this.gunaBarDataset1,
            this.gunaBarDataset2,
            this.gunaBarDataset3});
            this.barChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            chartFont1.FontName = "Arial";
            this.barChart1.Legend.LabelFont = chartFont1;
            this.barChart1.Location = new System.Drawing.Point(0, 0);
            this.barChart1.Name = "barChart1";
            this.barChart1.Size = new System.Drawing.Size(654, 423);
            this.barChart1.TabIndex = 0;
            chartFont2.FontName = "Arial";
            chartFont2.Size = 12;
            chartFont2.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.barChart1.Title.Font = chartFont2;
            chartFont3.FontName = "Arial";
            this.barChart1.Tooltips.BodyFont = chartFont3;
            chartFont4.FontName = "Arial";
            chartFont4.Size = 9;
            chartFont4.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.barChart1.Tooltips.TitleFont = chartFont4;
            this.barChart1.XAxes.GridLines = grid1;
            chartFont5.FontName = "Arial";
            tick1.Font = chartFont5;
            this.barChart1.XAxes.Ticks = tick1;
            this.barChart1.YAxes.GridLines = grid2;
            chartFont6.FontName = "Arial";
            tick2.Font = chartFont6;
            this.barChart1.YAxes.Ticks = tick2;
            this.barChart1.ZAxes.GridLines = grid3;
            chartFont7.FontName = "Arial";
            pointLabel1.Font = chartFont7;
            this.barChart1.ZAxes.PointLabels = pointLabel1;
            chartFont8.FontName = "Arial";
            tick3.Font = chartFont8;
            this.barChart1.ZAxes.Ticks = tick3;
            this.barChart1.Load += new System.EventHandler(this.barChart1_Load_1);
            // 
            // gunaBarDataset1
            // 
            lPoint1.Y = 0D;
            this.gunaBarDataset1.DataPoints.AddRange(new Guna.Charts.WinForms.LPoint[] {
            lPoint1});
            this.gunaBarDataset1.FillColors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.MediumOrchid});
            this.gunaBarDataset1.Label = "Income";
            this.gunaBarDataset1.TargetChart = this.barChart1;
            // 
            // gunaBarDataset2
            // 
            this.gunaBarDataset2.FillColors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206)))))});
            this.gunaBarDataset2.Label = "Expense";
            this.gunaBarDataset2.TargetChart = this.barChart1;
            // 
            // gunaBarDataset3
            // 
            this.gunaBarDataset3.FillColors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))))});
            this.gunaBarDataset3.Label = "Savings";
            this.gunaBarDataset3.TargetChart = this.barChart1;
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.BorderRadius = 5;
            this.guna2CustomGradientPanel1.Controls.Add(this.barChart1);
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(20, 97);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(654, 423);
            this.guna2CustomGradientPanel1.TabIndex = 1;
            // 
            // guna2CustomGradientPanel2
            // 
            this.guna2CustomGradientPanel2.Controls.Add(this.pieChart1);
            this.guna2CustomGradientPanel2.Location = new System.Drawing.Point(692, 97);
            this.guna2CustomGradientPanel2.Name = "guna2CustomGradientPanel2";
            this.guna2CustomGradientPanel2.Size = new System.Drawing.Size(752, 423);
            this.guna2CustomGradientPanel2.TabIndex = 3;
            // 
            // pieChart1
            // 
            this.pieChart1.Datasets.AddRange(new Guna.Charts.Interfaces.IGunaDataset[] {
            this.gunaPieDataset1});
            this.pieChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            chartFont9.FontName = "Arial";
            this.pieChart1.Legend.LabelFont = chartFont9;
            this.pieChart1.Location = new System.Drawing.Point(0, 0);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(752, 423);
            this.pieChart1.TabIndex = 0;
            chartFont10.FontName = "Arial";
            chartFont10.Size = 12;
            chartFont10.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.pieChart1.Title.Font = chartFont10;
            chartFont11.FontName = "Arial";
            this.pieChart1.Tooltips.BodyFont = chartFont11;
            chartFont12.FontName = "Arial";
            chartFont12.Size = 9;
            chartFont12.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.pieChart1.Tooltips.TitleFont = chartFont12;
            this.pieChart1.XAxes.GridLines = grid4;
            chartFont13.FontName = "Arial";
            tick4.Font = chartFont13;
            this.pieChart1.XAxes.Ticks = tick4;
            this.pieChart1.YAxes.Display = false;
            this.pieChart1.YAxes.GridLines = grid5;
            chartFont14.FontName = "Arial";
            tick5.Font = chartFont14;
            this.pieChart1.YAxes.Ticks = tick5;
            this.pieChart1.ZAxes.GridLines = grid6;
            chartFont15.FontName = "Arial";
            pointLabel2.Font = chartFont15;
            this.pieChart1.ZAxes.PointLabels = pointLabel2;
            chartFont16.FontName = "Arial";
            tick6.Font = chartFont16;
            this.pieChart1.ZAxes.Ticks = tick6;
            // 
            // timeFrame
            // 
            this.timeFrame.BackColor = System.Drawing.Color.Transparent;
            this.timeFrame.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.timeFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeFrame.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.timeFrame.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.timeFrame.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.timeFrame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.timeFrame.ItemHeight = 30;
            this.timeFrame.Location = new System.Drawing.Point(534, 31);
            this.timeFrame.Name = "timeFrame";
            this.timeFrame.Size = new System.Drawing.Size(140, 36);
            this.timeFrame.TabIndex = 4;
            this.timeFrame.SelectedIndexChanged += new System.EventHandler(this.timeFrame_SelectedIndexChanged);
            // 
            // gunaPieDataset1
            // 
            this.gunaPieDataset1.Label = "Pie1";
            this.gunaPieDataset1.TargetChart = this.pieChart1;
            // 
            // guna2CustomGradientPanel3
            // 
            this.guna2CustomGradientPanel3.Controls.Add(this.pieChart2);
            this.guna2CustomGradientPanel3.Location = new System.Drawing.Point(20, 553);
            this.guna2CustomGradientPanel3.Name = "guna2CustomGradientPanel3";
            this.guna2CustomGradientPanel3.Size = new System.Drawing.Size(654, 356);
            this.guna2CustomGradientPanel3.TabIndex = 5;
            // 
            // pieChart2
            // 
            chartArea1.Name = "ChartArea1";
            this.pieChart2.ChartAreas.Add(chartArea1);
            this.pieChart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.pieChart2.Legends.Add(legend1);
            this.pieChart2.Location = new System.Drawing.Point(0, 0);
            this.pieChart2.Name = "pieChart2";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.pieChart2.Series.Add(series1);
            this.pieChart2.Size = new System.Drawing.Size(654, 356);
            this.pieChart2.TabIndex = 0;
            this.pieChart2.Text = "chart1";
            // 
            // InsightsUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2CustomGradientPanel3);
            this.Controls.Add(this.timeFrame);
            this.Controls.Add(this.guna2CustomGradientPanel2);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Name = "InsightsUC";
            this.Size = new System.Drawing.Size(1509, 950);
            this.Load += new System.EventHandler(this.InsightsUC_Load);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel2.ResumeLayout(false);
            this.guna2CustomGradientPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pieChart2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.Charts.WinForms.GunaChart barChart1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.Charts.WinForms.GunaBarDataset gunaBarDataset1;
        private Guna.Charts.WinForms.GunaBarDataset gunaBarDataset2;
        private Guna.Charts.WinForms.GunaBarDataset gunaBarDataset3;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel2;
        private Guna.UI2.WinForms.Guna2ComboBox timeFrame;
        private Guna.Charts.WinForms.GunaChart pieChart1;
        private Guna.Charts.WinForms.GunaPieDataset gunaPieDataset1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel3;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart2;
    }
}
