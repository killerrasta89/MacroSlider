namespace MacroSlider_Final
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxComPort = new System.Windows.Forms.ComboBox();
            this.cBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblComPortStatus = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nudNumberOfSnaps = new System.Windows.Forms.NumericUpDown();
            this.btnStartShooting = new System.Windows.Forms.Button();
            this.nudHoldTime = new System.Windows.Forms.NumericUpDown();
            this.nudStartPosition = new System.Windows.Forms.NumericUpDown();
            this.btnSetZero = new System.Windows.Forms.Button();
            this.btnTakeAShot = new System.Windows.Forms.Button();
            this.nudSettleTime = new System.Windows.Forms.NumericUpDown();
            this.tBoxStackingDistance = new System.Windows.Forms.TextBox();
            this.btnSetPosition = new System.Windows.Forms.Button();
            this.tBoxOneStepSize = new System.Windows.Forms.TextBox();
            this.cBoxMotorSpeed = new System.Windows.Forms.ComboBox();
            this.cBoxMotorMicroStep = new System.Windows.Forms.ComboBox();
            this.cBoxRailType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.pBarInProgress = new System.Windows.Forms.ProgressBar();
            this.lblCurrentPositionInStep = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfSnaps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoldTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettleTime)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM PORT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "BAUD RATE";
            // 
            // cBoxComPort
            // 
            this.cBoxComPort.FormattingEnabled = true;
            this.cBoxComPort.Location = new System.Drawing.Point(90, 37);
            this.cBoxComPort.Name = "cBoxComPort";
            this.cBoxComPort.Size = new System.Drawing.Size(121, 21);
            this.cBoxComPort.TabIndex = 2;
            // 
            // cBoxBaudRate
            // 
            this.cBoxBaudRate.FormattingEnabled = true;
            this.cBoxBaudRate.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "74880",
            "115200",
            "230400",
            "250000",
            "500000",
            "1000000",
            "2000000"});
            this.cBoxBaudRate.Location = new System.Drawing.Point(90, 64);
            this.cBoxBaudRate.Name = "cBoxBaudRate";
            this.cBoxBaudRate.Size = new System.Drawing.Size(121, 21);
            this.cBoxBaudRate.TabIndex = 6;
            this.cBoxBaudRate.Text = "115200";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(9, 16);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 23);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(9, 44);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 81);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COM Port Control";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Location = new System.Drawing.Point(12, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 74);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblComPortStatus);
            this.groupBox3.Location = new System.Drawing.Point(107, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(112, 57);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "COM Port Status";
            // 
            // lblComPortStatus
            // 
            this.lblComPortStatus.AutoSize = true;
            this.lblComPortStatus.Font = new System.Drawing.Font("Arial Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblComPortStatus.Location = new System.Drawing.Point(27, 16);
            this.lblComPortStatus.Name = "lblComPortStatus";
            this.lblComPortStatus.Size = new System.Drawing.Size(62, 31);
            this.lblComPortStatus.TabIndex = 0;
            this.lblComPortStatus.Text = "OFF";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nudNumberOfSnaps);
            this.groupBox4.Controls.Add(this.btnStartShooting);
            this.groupBox4.Controls.Add(this.nudHoldTime);
            this.groupBox4.Controls.Add(this.nudStartPosition);
            this.groupBox4.Controls.Add(this.btnSetZero);
            this.groupBox4.Controls.Add(this.btnTakeAShot);
            this.groupBox4.Controls.Add(this.nudSettleTime);
            this.groupBox4.Controls.Add(this.tBoxStackingDistance);
            this.groupBox4.Controls.Add(this.btnSetPosition);
            this.groupBox4.Controls.Add(this.tBoxOneStepSize);
            this.groupBox4.Controls.Add(this.cBoxMotorSpeed);
            this.groupBox4.Controls.Add(this.cBoxMotorMicroStep);
            this.groupBox4.Controls.Add(this.cBoxRailType);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(12, 170);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(231, 344);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Macro Slider Settings";
            // 
            // nudNumberOfSnaps
            // 
            this.nudNumberOfSnaps.Location = new System.Drawing.Point(104, 50);
            this.nudNumberOfSnaps.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudNumberOfSnaps.Name = "nudNumberOfSnaps";
            this.nudNumberOfSnaps.Size = new System.Drawing.Size(121, 20);
            this.nudNumberOfSnaps.TabIndex = 29;
            this.nudNumberOfSnaps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNumberOfSnaps.ValueChanged += new System.EventHandler(this.nudNumberOfSnaps_ValueChanged);
            // 
            // btnStartShooting
            // 
            this.btnStartShooting.Location = new System.Drawing.Point(43, 239);
            this.btnStartShooting.Name = "btnStartShooting";
            this.btnStartShooting.Size = new System.Drawing.Size(145, 23);
            this.btnStartShooting.TabIndex = 28;
            this.btnStartShooting.Text = "Start Shooting";
            this.btnStartShooting.UseVisualStyleBackColor = true;
            this.btnStartShooting.Click += new System.EventHandler(this.btnStartShooting_Click);
            // 
            // nudHoldTime
            // 
            this.nudHoldTime.Location = new System.Drawing.Point(104, 102);
            this.nudHoldTime.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudHoldTime.Name = "nudHoldTime";
            this.nudHoldTime.Size = new System.Drawing.Size(121, 20);
            this.nudHoldTime.TabIndex = 25;
            this.nudHoldTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudHoldTime.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nudStartPosition
            // 
            this.nudStartPosition.Location = new System.Drawing.Point(80, 278);
            this.nudStartPosition.Maximum = new decimal(new int[] {
            28000,
            0,
            0,
            0});
            this.nudStartPosition.Name = "nudStartPosition";
            this.nudStartPosition.Size = new System.Drawing.Size(121, 20);
            this.nudStartPosition.TabIndex = 26;
            this.nudStartPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudStartPosition.ValueChanged += new System.EventHandler(this.nudStartPosition_ValueChanged);
            // 
            // btnSetZero
            // 
            this.btnSetZero.Location = new System.Drawing.Point(78, 313);
            this.btnSetZero.Name = "btnSetZero";
            this.btnSetZero.Size = new System.Drawing.Size(75, 23);
            this.btnSetZero.TabIndex = 18;
            this.btnSetZero.Text = "Set Zero";
            this.btnSetZero.UseVisualStyleBackColor = true;
            this.btnSetZero.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // btnTakeAShot
            // 
            this.btnTakeAShot.Location = new System.Drawing.Point(3, 313);
            this.btnTakeAShot.Name = "btnTakeAShot";
            this.btnTakeAShot.Size = new System.Drawing.Size(75, 23);
            this.btnTakeAShot.TabIndex = 19;
            this.btnTakeAShot.Text = "Take a Shot";
            this.btnTakeAShot.UseVisualStyleBackColor = true;
            this.btnTakeAShot.Click += new System.EventHandler(this.btnTakeAShot_Click);
            // 
            // nudSettleTime
            // 
            this.nudSettleTime.Location = new System.Drawing.Point(104, 76);
            this.nudSettleTime.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudSettleTime.Name = "nudSettleTime";
            this.nudSettleTime.Size = new System.Drawing.Size(121, 20);
            this.nudSettleTime.TabIndex = 24;
            this.nudSettleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSettleTime.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // tBoxStackingDistance
            // 
            this.tBoxStackingDistance.Enabled = false;
            this.tBoxStackingDistance.Location = new System.Drawing.Point(104, 208);
            this.tBoxStackingDistance.Name = "tBoxStackingDistance";
            this.tBoxStackingDistance.Size = new System.Drawing.Size(121, 20);
            this.tBoxStackingDistance.TabIndex = 23;
            this.tBoxStackingDistance.Text = "0 µm";
            this.tBoxStackingDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSetPosition
            // 
            this.btnSetPosition.Location = new System.Drawing.Point(153, 313);
            this.btnSetPosition.Name = "btnSetPosition";
            this.btnSetPosition.Size = new System.Drawing.Size(75, 23);
            this.btnSetPosition.TabIndex = 17;
            this.btnSetPosition.Text = "Set Position";
            this.btnSetPosition.UseVisualStyleBackColor = true;
            this.btnSetPosition.Click += new System.EventHandler(this.btnSetPosition_Click);
            // 
            // tBoxOneStepSize
            // 
            this.tBoxOneStepSize.Enabled = false;
            this.tBoxOneStepSize.Location = new System.Drawing.Point(104, 182);
            this.tBoxOneStepSize.Name = "tBoxOneStepSize";
            this.tBoxOneStepSize.Size = new System.Drawing.Size(121, 20);
            this.tBoxOneStepSize.TabIndex = 22;
            this.tBoxOneStepSize.Text = "10 µm";
            this.tBoxOneStepSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cBoxMotorSpeed
            // 
            this.cBoxMotorSpeed.FormattingEnabled = true;
            this.cBoxMotorSpeed.Items.AddRange(new object[] {
            "Fast",
            "Slow"});
            this.cBoxMotorSpeed.Location = new System.Drawing.Point(104, 155);
            this.cBoxMotorSpeed.Name = "cBoxMotorSpeed";
            this.cBoxMotorSpeed.Size = new System.Drawing.Size(121, 21);
            this.cBoxMotorSpeed.TabIndex = 21;
            this.cBoxMotorSpeed.Text = "Fast";
            this.cBoxMotorSpeed.SelectedIndexChanged += new System.EventHandler(this.comboBox16_SelectedIndexChanged);
            // 
            // cBoxMotorMicroStep
            // 
            this.cBoxMotorMicroStep.FormattingEnabled = true;
            this.cBoxMotorMicroStep.Items.AddRange(new object[] {
            "Full Step",
            "1/2 Step",
            "1/4 Step",
            "1/8 Step",
            "1/16 Step",
            "1/32 Step"});
            this.cBoxMotorMicroStep.Location = new System.Drawing.Point(104, 128);
            this.cBoxMotorMicroStep.Name = "cBoxMotorMicroStep";
            this.cBoxMotorMicroStep.Size = new System.Drawing.Size(121, 21);
            this.cBoxMotorMicroStep.TabIndex = 20;
            this.cBoxMotorMicroStep.Text = "Full Step";
            this.cBoxMotorMicroStep.SelectedIndexChanged += new System.EventHandler(this.cBoxMotorMicroStep_SelectedIndexChanged);
            // 
            // cBoxRailType
            // 
            this.cBoxRailType.AutoCompleteCustomSource.AddRange(new string[] {
            "40 mm"});
            this.cBoxRailType.Enabled = false;
            this.cBoxRailType.FormattingEnabled = true;
            this.cBoxRailType.Location = new System.Drawing.Point(104, 23);
            this.cBoxRailType.Name = "cBoxRailType";
            this.cBoxRailType.Size = new System.Drawing.Size(121, 21);
            this.cBoxRailType.TabIndex = 11;
            this.cBoxRailType.Text = "40 mm";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 159);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 13);
            this.label17.TabIndex = 10;
            this.label17.Text = "Motor Speed";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 132);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Motor Micro Step";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 282);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Start Position";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 186);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "One Step Size";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 212);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "Stacking Distance";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Hold Time";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Settle Time";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Number Of Snaps";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Rail type";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.pBarInProgress);
            this.groupBox6.Controls.Add(this.lblCurrentPositionInStep);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Location = new System.Drawing.Point(12, 514);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(231, 81);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "In Progress Data";
            // 
            // pBarInProgress
            // 
            this.pBarInProgress.Location = new System.Drawing.Point(6, 51);
            this.pBarInProgress.Name = "pBarInProgress";
            this.pBarInProgress.Size = new System.Drawing.Size(219, 23);
            this.pBarInProgress.TabIndex = 14;
            // 
            // lblCurrentPositionInStep
            // 
            this.lblCurrentPositionInStep.AutoSize = true;
            this.lblCurrentPositionInStep.Location = new System.Drawing.Point(142, 18);
            this.lblCurrentPositionInStep.Name = "lblCurrentPositionInStep";
            this.lblCurrentPositionInStep.Size = new System.Drawing.Size(13, 13);
            this.lblCurrentPositionInStep.TabIndex = 24;
            this.lblCurrentPositionInStep.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Shoot in Progress";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Current Position";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(254, 604);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.cBoxBaudRate);
            this.Controls.Add(this.cBoxComPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "Form1";
            this.Text = "Macro Slider Communicator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfSnaps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoldTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSettleTime)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxComPort;
        private System.Windows.Forms.ComboBox cBoxBaudRate;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblComPortStatus;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cBoxMotorSpeed;
        private System.Windows.Forms.ComboBox cBoxMotorMicroStep;
        private System.Windows.Forms.ComboBox cBoxRailType;
        private System.Windows.Forms.Button btnSetPosition;
        private System.Windows.Forms.Button btnSetZero;
        private System.Windows.Forms.Button btnTakeAShot;
        private System.Windows.Forms.TextBox tBoxStackingDistance;
        private System.Windows.Forms.TextBox tBoxOneStepSize;
        private System.Windows.Forms.NumericUpDown nudStartPosition;
        private System.Windows.Forms.NumericUpDown nudHoldTime;
        private System.Windows.Forms.NumericUpDown nudSettleTime;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.NumericUpDown nudNumberOfSnaps;
        private System.Windows.Forms.Button btnStartShooting;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ProgressBar pBarInProgress;
        private System.Windows.Forms.Label lblCurrentPositionInStep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
