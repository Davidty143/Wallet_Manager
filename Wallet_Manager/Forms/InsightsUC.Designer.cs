﻿namespace Wallet_Manager.Forms
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.barChart1 = new Guna.Charts.WinForms.GunaChart();
            this.gunaBarDataset1 = new Guna.Charts.WinForms.GunaBarDataset();
            this.gunaBarDataset2 = new Guna.Charts.WinForms.GunaBarDataset();
            this.gunaBarDataset3 = new Guna.Charts.WinForms.GunaBarDataset();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2CustomGradientPanel2 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.timeFrame = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2CustomGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.guna2CustomGradientPanel2.SuspendLayout();
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
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chart1.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(82)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(91)))), ((int)(((byte)(206)))))};
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Income";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Expense";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Savings";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(752, 423);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // guna2CustomGradientPanel2
            // 
            this.guna2CustomGradientPanel2.Controls.Add(this.chart1);
            this.guna2CustomGradientPanel2.Location = new System.Drawing.Point(692, 97);
            this.guna2CustomGradientPanel2.Name = "guna2CustomGradientPanel2";
            this.guna2CustomGradientPanel2.Size = new System.Drawing.Size(752, 423);
            this.guna2CustomGradientPanel2.TabIndex = 3;
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
            this.timeFrame.Location = new System.Drawing.Point(251, 49);
            this.timeFrame.Name = "timeFrame";
            this.timeFrame.Size = new System.Drawing.Size(140, 36);
            this.timeFrame.TabIndex = 4;
            this.timeFrame.SelectedIndexChanged += new System.EventHandler(this.timeFrame_SelectedIndexChanged);
            // 
            // InsightsUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.timeFrame);
            this.Controls.Add(this.guna2CustomGradientPanel2);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Name = "InsightsUC";
            this.Size = new System.Drawing.Size(1509, 950);
            this.Load += new System.EventHandler(this.InsightsUC_Load);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.guna2CustomGradientPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.Charts.WinForms.GunaChart barChart1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.Charts.WinForms.GunaBarDataset gunaBarDataset1;
        private Guna.Charts.WinForms.GunaBarDataset gunaBarDataset2;
        private Guna.Charts.WinForms.GunaBarDataset gunaBarDataset3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel2;
        private Guna.UI2.WinForms.Guna2ComboBox timeFrame;
    }
}
