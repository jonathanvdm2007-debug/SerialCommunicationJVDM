using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SerialCommunication
{
    public partial class titel1 : Form
    {
        public titel1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();
                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;

                comboBoxBaudrate.SelectedIndex = comboBoxBaudrate.Items.IndexOf("115200");
            }
            catch (Exception)
            { }
        }

        private void cboPoort_DropDown(object sender, EventArgs e)
        {
            try
            {
                string selected = (string)comboBoxPoort.SelectedItem;
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();

                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);

                comboBoxPoort.SelectedIndex = comboBoxPoort.Items.IndexOf(selected);
            }
            catch (Exception)
            {
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            {
                // abc def ghi jkl
                try
                {
                    if (serialPortArduino.IsOpen)
                    {
                        // ik heb een verbinding -> de gebruiker wil deze verbreken
                        serialPortArduino.Close();
                        radioButtonVerbonden.Checked = false;
                        buttonConnect.Text = "Connect";
                        labelStatus.Text = "Status: Disconnected";
                    }
                    else
                    {
                        // ik heb geen verbinding -> de gebruiker wil een verbinding maken
                        serialPortArduino.PortName = (string)comboBoxPoort.SelectedItem;
                        serialPortArduino.BaudRate = Int32.Parse((string)comboBoxBaudrate.SelectedItem);
                        serialPortArduino.DataBits = (int)numericUpDownDatabits.Value;

                        if (radioButtonParityEven.Checked) serialPortArduino.Parity = Parity.Even;
                        else if (radioButtonParityOdd.Checked) serialPortArduino.Parity = Parity.Odd;
                        else if (radioButtonParityNone.Checked) serialPortArduino.Parity = Parity.None;
                        else if (radioButtonParityMark.Checked) serialPortArduino.Parity = Parity.Mark;
                        else if (radioButtonParitySpace.Checked) serialPortArduino.Parity = Parity.Space;

                        if (radioButtonStopbitsNone.Checked) serialPortArduino.StopBits = StopBits.None;
                        else if (radioButtonStopbitsOne.Checked) serialPortArduino.StopBits = StopBits.One;
                        else if (radioButtonStopbitsOnePointFive.Checked) serialPortArduino.StopBits = StopBits.OnePointFive;
                        else if (radioButtonStopbitsTwo.Checked) serialPortArduino.StopBits = StopBits.Two;

                        if (radioButtonHandshakeNone.Checked) serialPortArduino.Handshake = Handshake.None;
                        else if (radioButtonHandshakeRTS.Checked) serialPortArduino.Handshake = Handshake.RequestToSend;
                        else if (radioButtonHandshakeRTSXonXoff.Checked) serialPortArduino.Handshake = Handshake.RequestToSendXOnXOff;
                        else if (radioButtonHandshakeXonXoff.Checked) serialPortArduino.Handshake = Handshake.XOnXOff;
                        serialPortArduino.RtsEnable = checkBoxRtsEnable.Checked;
                        serialPortArduino.DtrEnable = checkBoxDtrEnable.Checked;
                        serialPortArduino.Open();
                        string commando = "ping";
                        serialPortArduino.WriteLine(commando);
                        string antwoord = serialPortArduino.ReadLine();
                        antwoord = antwoord.TrimEnd();
                        if (antwoord == "pong")
                        {
                            radioButtonVerbonden.Checked = true;
                            buttonConnect.Text = "Disconnect";
                            labelStatus.Text = "Status: Connected";
                        }
                        else
                        {
                            serialPortArduino.Close();
                            labelStatus.Text = "Error: verkeerd antwoord";
                        }
                    }
                }
                catch (Exception exception)
                {
                    labelStatus.Text = "Error: " + exception.Message;
                    serialPortArduino.Close();
                    radioButtonVerbonden.Checked = false;
                    buttonConnect.Text = "Connect";
                }

            }

        }

        private void checkBoxDigital2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string comando;
                if (checkBoxDigital2.Checked) comando = "set d2 high";
                else comando = "set d2 low";
                serialPortArduino.WriteLine(comando);
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void checkBoxDigital3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string comando;
                if (checkBoxDigital3.Checked) comando = "set d3 high";
                else comando = "set d3 low";
                serialPortArduino.WriteLine(comando);
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void checkBoxDigital4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string comando;
                if (checkBoxDigital4.Checked) comando = "set d4 high";
                else comando = "set d4 low";
                serialPortArduino.WriteLine(comando);
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void trackBarPWM9_Scroll(object sender, EventArgs e)
        {
            try
            {
                string comando;
                if (trackBarPWM9.Value == 0) comando = "set pwm9 0";
                else comando = "set pwm9 " + trackBarPWM9.Value;
                serialPortArduino.WriteLine(comando);
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void trackBarPWM10_Scroll(object sender, EventArgs e)
        {
            try
            {
                string comando;
                if (trackBarPWM10.Value == 0) comando = "set pwm10 0";
                else comando = "set pwm10 " + trackBarPWM10.Value;
                serialPortArduino.WriteLine(comando);
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void trackBarPWM11_Scroll(object sender, EventArgs e)
        {
            try
            {
                string comando;
                if (trackBarPWM11.Value == 0) comando = "set pwm11 0";
                else comando = "set pwm11 " + trackBarPWM11.Value;
                serialPortArduino.WriteLine(comando);
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerOefening3.Enabled = tabControl.SelectedIndex == 3;
            timerOefening5.Enabled = tabControl.SelectedIndex == 5;
        }

        private void timerOefening3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!serialPortArduino.IsOpen) ;
                {
                    serialPortArduino.ReadExisting();
                    string comando = "get d5";
                    serialPortArduino.WriteLine(comando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital5.Checked = (antwoord == "1");
                }
                if (!serialPortArduino.IsOpen) ;
                {
                    serialPortArduino.ReadExisting();
                    string comando = "get d6";
                    serialPortArduino.WriteLine(comando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital6.Checked = (antwoord == "1");
                }
                if (!serialPortArduino.IsOpen) ;
                {
                    serialPortArduino.ReadExisting();
                    string comando = "get d7";
                    serialPortArduino.WriteLine(comando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital7.Checked = (antwoord == "1");
                }

            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

            private void timerOefening5_Tick(object sender, EventArgs e)
        {
            if (!serialPortArduino.IsOpen)
                return;

            try
            {
                
                serialPortArduino.ReadExisting();
                serialPortArduino.WriteLine("get a0");
                string antwoordA0 = serialPortArduino.ReadLine();
                antwoordA0 = antwoordA0.TrimEnd();
                antwoordA0 = antwoordA0.Substring(4); 
                double rawA0 = double.Parse(antwoordA0, System.Globalization.CultureInfo.InvariantCulture);

                double rcGewenst = (45.0 - 5.0) / (1023.0 - 0.0);
                double gewensteTemp = rcGewenst * rawA0 + 5.0;

                labelGewensteTemp.Text = gewensteTemp.ToString("F1") + " °C";

                
                serialPortArduino.ReadExisting();
                serialPortArduino.WriteLine("get a1");
                string antwoordA1 = serialPortArduino.ReadLine();
                antwoordA1 = antwoordA1.TrimEnd();
                antwoordA1 = antwoordA1.Substring(4); 
                double rawA1 = double.Parse(antwoordA1, System.Globalization.CultureInfo.InvariantCulture);

                double rcHuidig = (500.0 - 0.0) / (1023.0 - 0.0);
                double huidigeTemp = rcHuidig * rawA1;

                labelHuidigeTemp.Text = huidigeTemp.ToString("F1") + " °C";

                
                if (huidigeTemp < gewensteTemp)
                    serialPortArduino.WriteLine("set d2 high");
                else
                    serialPortArduino.WriteLine("set d2 low");
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }
    }           
}
