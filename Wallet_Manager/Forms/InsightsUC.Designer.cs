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
            Guna.Charts.WinForms.ChartFont chartFont49 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont50 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont51 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont52 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid19 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick19 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont53 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid20 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick20 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont54 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid21 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel7 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont55 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick21 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont56 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.LPoint lPoint1 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.ChartFont chartFont57 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont58 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont59 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont60 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid22 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick22 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont61 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid23 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick23 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont62 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid24 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel8 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont63 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick24 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont64 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.LPoint lPoint2 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint11 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint12 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint13 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint14 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint15 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint16 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint17 = new Guna.Charts.WinForms.LPoint();
            this.barChart1 = new Guna.Charts.WinForms.GunaChart();
            this.gunaBarDataset1 = new Guna.Charts.WinForms.GunaBarDataset();
            this.gunaBarDataset2 = new Guna.Charts.WinForms.GunaBarDataset();
            this.gunaBarDataset3 = new Guna.Charts.WinForms.GunaBarDataset();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2CustomGradientPanel2 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.pieChart1 = new Guna.Charts.WinForms.GunaChart();
            this.gunaPieDataset1 = new Guna.Charts.WinForms.GunaPieDataset();
            this.timeFrame = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2CustomGradientPanel3 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2CustomGradientPanel1.SuspendLayout();
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
            chartFont49.FontName = "Arial";
            this.barChart1.Legend.LabelFont = chartFont49;
            this.barChart1.Location = new System.Drawing.Point(0, 0);
            this.barChart1.Name = "barChart1";
            this.barChart1.Size = new System.Drawing.Size(692, 423);
            this.barChart1.TabIndex = 0;
            chartFont50.FontName = "Arial";
            chartFont50.Size = 12;
            chartFont50.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.barChart1.Title.Font = chartFont50;
            chartFont51.FontName = "Arial";
            this.barChart1.Tooltips.BodyFont = chartFont51;
            chartFont52.FontName = "Arial";
            chartFont52.Size = 9;
            chartFont52.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.barChart1.Tooltips.TitleFont = chartFont52;
            this.barChart1.XAxes.GridLines = grid19;
            chartFont53.FontName = "Arial";
            tick19.Font = chartFont53;
            this.barChart1.XAxes.Ticks = tick19;
            this.barChart1.YAxes.GridLines = grid20;
            chartFont54.FontName = "Arial";
            tick20.Font = chartFont54;
            this.barChart1.YAxes.Ticks = tick20;
            this.barChart1.ZAxes.GridLines = grid21;
            chartFont55.FontName = "Arial";
            pointLabel7.Font = chartFont55;
            this.barChart1.ZAxes.PointLabels = pointLabel7;
            chartFont56.FontName = "Arial";
            tick21.Font = chartFont56;
            this.barChart1.ZAxes.Ticks = tick21;
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
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(27, 56);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(692, 423);
            this.guna2CustomGradientPanel1.TabIndex = 1;
            // 
            // guna2CustomGradientPanel2
            // 
            this.guna2CustomGradientPanel2.Controls.Add(this.pieChart1);
            this.guna2CustomGradientPanel2.Location = new System.Drawing.Point(796, 56);
            this.guna2CustomGradientPanel2.Name = "guna2CustomGradientPanel2";
            this.guna2CustomGradientPanel2.Size = new System.Drawing.Size(655, 423);
            this.guna2CustomGradientPanel2.TabIndex = 3;
            // 
            // pieChart1
            // 
            this.pieChart1.Datasets.AddRange(new Guna.Charts.Interfaces.IGunaDataset[] {
            this.gunaPieDataset1});
            this.pieChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            chartFont57.FontName = "Arial";
            this.pieChart1.Legend.LabelFont = chartFont57;
            this.pieChart1.Location = new System.Drawing.Point(0, 0);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.PaletteCustomColors.FillColors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(67)))), ((int)(((byte)(159))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(76)))), ((int)(((byte)(204))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(91)))), ((int)(((byte)(207))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(123)))), ((int)(((byte)(189))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(154)))), ((int)(((byte)(209))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(127)))), ((int)(((byte)(205))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(68)))), ((int)(((byte)(162))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))))});
            this.pieChart1.PaletteCustomColors.PointBorderColors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(67)))), ((int)(((byte)(159))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(76)))), ((int)(((byte)(204))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(91)))), ((int)(((byte)(207))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(123)))), ((int)(((byte)(189))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(154)))), ((int)(((byte)(209))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(127)))), ((int)(((byte)(205))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(68)))), ((int)(((byte)(162))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))))});
            this.pieChart1.PaletteCustomColors.PointFillColors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(67)))), ((int)(((byte)(159))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(76)))), ((int)(((byte)(204))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(91)))), ((int)(((byte)(207))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(123)))), ((int)(((byte)(189))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(154)))), ((int)(((byte)(209))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(127)))), ((int)(((byte)(205))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(68)))), ((int)(((byte)(162))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(118)))), ((int)(((byte)(255)))))});
            this.pieChart1.Size = new System.Drawing.Size(655, 423);
            this.pieChart1.TabIndex = 0;
            chartFont58.FontName = "Arial";
            chartFont58.Size = 12;
            chartFont58.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.pieChart1.Title.Font = chartFont58;
            chartFont59.FontName = "Arial";
            this.pieChart1.Tooltips.BodyFont = chartFont59;
            chartFont60.FontName = "Arial";
            chartFont60.Size = 9;
            chartFont60.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.pieChart1.Tooltips.TitleFont = chartFont60;
            this.pieChart1.XAxes.Display = false;
            this.pieChart1.XAxes.GridLines = grid22;
            chartFont61.FontName = "Arial";
            tick22.Font = chartFont61;
            this.pieChart1.XAxes.Ticks = tick22;
            this.pieChart1.YAxes.Display = false;
            this.pieChart1.YAxes.GridLines = grid23;
            chartFont62.FontName = "Arial";
            tick23.Font = chartFont62;
            this.pieChart1.YAxes.Ticks = tick23;
            this.pieChart1.ZAxes.GridLines = grid24;
            chartFont63.FontName = "Arial";
            pointLabel8.Font = chartFont63;
            this.pieChart1.ZAxes.PointLabels = pointLabel8;
            chartFont64.FontName = "Arial";
            tick24.Font = chartFont64;
            this.pieChart1.ZAxes.Ticks = tick24;
            this.pieChart1.Load += new System.EventHandler(this.pieChart1_Load);
            // 
            // gunaPieDataset1
            // 
            lPoint2.Y = 0D;
            lPoint11.Y = 0D;
            lPoint12.Y = 0D;
            lPoint13.Y = 0D;
            lPoint14.Y = 0D;
            lPoint15.Y = 0D;
            lPoint16.Y = 0D;
            lPoint17.Y = 0D;
            this.gunaPieDataset1.DataPoints.AddRange(new Guna.Charts.WinForms.LPoint[] {
            lPoint2,
            lPoint11,
            lPoint12,
            lPoint13,
            lPoint14,
            lPoint15,
            lPoint16,
            lPoint17});
            this.gunaPieDataset1.FillColors.AddRange(new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(201)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(255)))), ((int)(((byte)(214))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(118)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))),
            System.Drawing.Color.MediumAquamarine,
            System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(154)))), ((int)(((byte)(209)))))});
            this.gunaPieDataset1.Label = "Food";
            this.gunaPieDataset1.LegendBoxFillColor = System.Drawing.Color.White;
            this.gunaPieDataset1.TargetChart = this.pieChart1;
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
            this.timeFrame.Location = new System.Drawing.Point(579, 14);
            this.timeFrame.Name = "timeFrame";
            this.timeFrame.Size = new System.Drawing.Size(140, 36);
            this.timeFrame.TabIndex = 4;
            this.timeFrame.SelectedIndexChanged += new System.EventHandler(this.timeFrame_SelectedIndexChanged);
            // 
            // guna2CustomGradientPanel3
            // 
            this.guna2CustomGradientPanel3.Location = new System.Drawing.Point(27, 497);
            this.guna2CustomGradientPanel3.Name = "guna2CustomGradientPanel3";
            this.guna2CustomGradientPanel3.Size = new System.Drawing.Size(692, 379);
            this.guna2CustomGradientPanel3.TabIndex = 5;
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
    }
}
