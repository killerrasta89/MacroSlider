using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace MacroSlider_Final
{
    public partial class Form1 : Form
    {
        string dataOUT;
        string dataIN;
        string MotorMicroStepText = "Full Step";
        string serialDataMotorMicroStep = "0";
        string OneStepSize = "10";
        decimal StackingDistance = 0;
        int IsStartPositionSet = 0;
        int IsTakeAShotSet = 0;
        int IsSetZeroSet = 0;
        int IsSetStartShot = 0;
        string MotorSpeed = "Fast";
        string serialDataMotorSpeed = "500";



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);
            serialPort1.DtrEnable = false;
            serialPort1.RtsEnable = false;
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            lblComPortStatus.Text = "OFF";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxComPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;

                serialPort1.Open();
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                lblComPortStatus.Text = "ON";
                lblComPortStatus.ForeColor = Color.Green;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                lblComPortStatus.Text = "OFF";
                lblComPortStatus.ForeColor = Color.Red;
            }
        }


        private void cBoxMotorMicroStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            MotorMicroStepText = cBoxMotorMicroStep.Text;

            if (MotorMicroStepText == "Full Step")
            {
                OneStepSize = "10";
                serialDataMotorMicroStep = "0";
            }
            else if (MotorMicroStepText == "1/2 Step")
            {
                OneStepSize = "5";
                serialDataMotorMicroStep = "1";
            }
            else if (MotorMicroStepText == "1/4 Step")
            {
                OneStepSize = "2,5";
                serialDataMotorMicroStep = "2";
            }
            else if (MotorMicroStepText == "1/8 Step")
            {
                OneStepSize = "1,25";
                serialDataMotorMicroStep = "3";
            }
            else if (MotorMicroStepText == "1/16 Step")
            {
                OneStepSize = "0,625";
                serialDataMotorMicroStep = "4";
            }
            else if (MotorMicroStepText == "1/32 Step")
            {
                OneStepSize = "0,3125";
                serialDataMotorMicroStep = "5";
            }

            StackingDistance = Convert.ToDecimal(OneStepSize) * nudNumberOfSnaps.Value;
            tBoxOneStepSize.Text = OneStepSize + " µm";
            tBoxStackingDistance.Text = Convert.ToString(StackingDistance) + " µm";
        }

        private void nudNumberOfSnaps_ValueChanged(object sender, EventArgs e)
        {
            StackingDistance = Convert.ToDecimal(OneStepSize) * nudNumberOfSnaps.Value;
            tBoxStackingDistance.Text = Convert.ToString(StackingDistance) + " µm";
        }

        private void btnStartShooting_Click(object sender, EventArgs e)
        {
            IsSetStartShot = 1;
            serialDataCreate();

        }

        private void nudStartPosition_ValueChanged(object sender, EventArgs e)
        {
            if (nudStartPosition.Value != 0)
            {
                IsStartPositionSet = 1;
            }
            else if (nudStartPosition.Value == 0)
            {
                IsStartPositionSet = 0;
            }
        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            MotorSpeed = cBoxMotorSpeed.Text;

            if (MotorSpeed == "Fast")
            {

                serialDataMotorSpeed = "500";
            }
            else if (MotorSpeed == "Slow")
            {

                serialDataMotorSpeed = "1000";
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataIN = serialPort1.ReadLine();
            this.Invoke(new EventHandler(ShowData)); //This method show the data Serial into TextBox|You can not show the Data to the TextBox without using this method

        }

        private void ShowData(object sender, EventArgs e)
        {

            lblCurrentPositionInStep.Text = dataIN;

            int i = Convert.ToInt32(lblCurrentPositionInStep.Text);
            int j = Convert.ToInt32(nudNumberOfSnaps.Text);
            pBarInProgress.Value = (100 / j) * i;
            if (i == j)
            {
                lblCurrentPositionInStep.Text = "0";
                pBarInProgress.Value = 0;
            }

        }


        private void btnTakeAShot_Click(object sender, EventArgs e)
        {
            //tBoxDataOut.Text = "";
            IsTakeAShotSet = 1;
            IsSetZeroSet = 0;
            IsStartPositionSet = 0;

            serialDataCreate();
            IsTakeAShotSet = 0;
            //tBoxDataOut.Text = "";
        }

        private void btnSetZero_Click(object sender, EventArgs e)
        {
            //tBoxDataOut.Text = "";
            IsSetZeroSet = 1;
            IsStartPositionSet = 0;
            IsTakeAShotSet = 0;

            serialDataCreate();
            IsSetZeroSet = 0;
            //tBoxDataOut.Text = "";
        }

        private void btnSetPosition_Click(object sender, EventArgs e)
        {
            //tBoxDataOut.Text = "";
            IsStartPositionSet = 1;
            IsSetZeroSet = 0;
            IsTakeAShotSet = 0;

            serialDataCreate();

            IsStartPositionSet = 0;
            //tBoxDataOut.Text = "";
        }

        public void serialDataCreate()
        {
            if (serialPort1.IsOpen)
            {
                   dataOUT = "X"+
                   IsTakeAShotSet + ";" +
                   IsStartPositionSet + ";" +
                   IsSetZeroSet + ";" +
                   nudNumberOfSnaps.Value + ";" +
                   nudSettleTime.Value + ";" +
                   nudHoldTime.Value + ";" +
                   nudStartPosition.Value + ";" +
                   serialDataMotorMicroStep + ";" +
                   serialDataMotorSpeed + ";" +
                   IsSetStartShot + ";";
                serialPort1.WriteLine(dataOUT);
                
            }
        }

    }
}
