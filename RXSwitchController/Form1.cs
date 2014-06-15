using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace _FirstWindowsFormsApplication
{
    public partial class Form1 : Form
    {
        string eeprom_file = @"C:\temp\eeprom_output.hex";
        public Form1()
        {
            InitializeComponent();

            Output1Selection.SelectedIndex = 0;
            Output2Selection.SelectedIndex = 0;
            Output1Mode.SelectedIndex = 0;
            Output2Mode.SelectedIndex = 0;
            flashTool.SelectedIndex = 0;

            btnFlash.Enabled = false;

            patternLower1.BackColor = Color.Red;
            patternLower2.BackColor = Color.Red;
            patternMiddle1.BackColor = Color.Red;
            patternMiddle2.BackColor = Color.Red;
            patternUpper1.BackColor = Color.Red;
            patternUpper2.BackColor = Color.Red;

            string[] ports = SerialPort.GetPortNames();
            SerialPortComboBox.Items.Add("Select COM port...");
            SerialPortComboBox.Items.AddRange(ports);
            SerialPortComboBox.SelectedIndex = 0;

            this.patternLower1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.patternLower2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.patternMiddle1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.patternMiddle2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.patternUpper1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.patternUpper2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
        }

        private void btnFlash_Click(object sender, EventArgs e)
        {
            int i = 1;
            string line;
            int address = 0;
            int[] channel = { this.Output1Selection.SelectedIndex + 1, this.Output2Selection.SelectedIndex + 1 };
            int[] mode = { Output1Mode.SelectedIndex == 1 ? 1 : 0, Output2Mode.SelectedIndex == 1 ? 1 : 0 };
            UInt32[] threshold_lower = { Convert.ToUInt32((Convert.ToInt32(this.Output1LowerThreshold.Value) * 13.64) + 342), Convert.ToUInt32((Convert.ToInt32(this.Output2LowerThreshold.Value) * 13.64) + 342) };
            UInt32[] threshold_upper = { Convert.ToUInt32((Convert.ToInt32(this.Output1UpperThreshold.Value) * 13.64) + 342), Convert.ToUInt32((Convert.ToInt32(this.Output2UpperThreshold.Value) * 13.64) + 342) };
            UInt32[] pattern_lower = { Convert.ToUInt32(this.patternLower1.Text, 2), Convert.ToUInt32(this.patternLower2.Text, 2) };
            UInt32[] pattern_middle = { Convert.ToUInt32(this.patternMiddle1.Text, 2), Convert.ToUInt32(this.patternMiddle2.Text, 2) };
            UInt32[] pattern_upper = { Convert.ToUInt32(this.patternUpper1.Text, 2), Convert.ToUInt32(this.patternUpper2.Text, 2) };
            string flashtool = "";


            if (File.Exists(eeprom_file))
            {
                File.Delete(eeprom_file);
            }
            StringBuilder sb = new StringBuilder();

            for (i = 0; i < 2; i++)
            {
                line = @":20" + address.ToString("X4") + @"00" + channel[i].ToString("X2") + mode[i].ToString("X2") + InvertBytes(threshold_lower[i], 4) + InvertBytes(threshold_upper[i], 4) + InvertBytes(pattern_lower[i], 8) + InvertBytes(pattern_middle[i], 8) + InvertBytes(pattern_upper[i], 8) + @"FFFFFFFFFFFFFFFFFFFFFFFFFFFF";
                line = add_checksum(line);
                address += 0x80;
                sb.AppendLine(line);
            }
            sb.AppendLine(@":00000001FF");

            using (StreamWriter outfile = new StreamWriter(eeprom_file, true))
            {
                outfile.Write(sb.ToString());
            }
            sb.Clear();

            if (flashTool.SelectedIndex == 0 )
            {
                flashtool = "tgyusblinker";
            }
            else if (flashTool.SelectedIndex == 1)
            {
                flashtool = "arduinousblinker";
            }

            Process avrdude = new Process();
            avrdude.StartInfo.UseShellExecute = true;
            avrdude.StartInfo.ErrorDialog = true;
            avrdude.StartInfo.Arguments = @"-p t85 -c " + flashtool + @" -P " + SerialPortComboBox.SelectedItem.ToString().ToLower() + @" -U eeprom:w:" + eeprom_file + @":i";
            avrdude.StartInfo.FileName = @"c:\avrdude\avrdude.exe";
            avrdude.Start();

            while (!avrdude.HasExited)
                System.Threading.Thread.Sleep(100);
            if (avrdude.ExitCode != 0)
            {
                MessageBox.Show("Updating EEPROM Failed");
                SerialPortComboBox.Focus();
            }
            else 
            {
                MessageBox.Show("Success Updating EEPROM");
                SerialPortComboBox.Focus();
            }
            avrdude.Dispose();
            //File.Delete(eeprom_file);
        }

        // AVR needs EEPROM bytes inverted
        private string InvertBytes(UInt32 hex, int len)
        {
            string output = "";
            int i;
            for (i = len; i > 0; i = i - 2)
            {
                output = output + hex.ToString("X" + len).Substring(i - 2, 2);
            }
            return output;
        }

        // Do calculation to add a checksum byte to the end of the variable passed in, remove first byte before calculating checksum
        private string add_checksum(string line)
        {
            int j = 0;
            byte[] lineb = { 0x0 };
            int checksum = 0x0;
            lineb = StringToByteArray(line.Substring(1, line.Length - 1)); // Strip ":" at start of line before calculating checksum
            for (j = 0; j < lineb.Length; j++)
            {
                checksum += lineb[j];
            }
            checksum = 0x100 - (checksum & 0x00FF);
            return line + checksum.ToString("X2");
        }
        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private void textBoxBinary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow backspace, 0 and 1
            e.Handled = !("01".Contains(e.KeyChar) || e.KeyChar == 8);
        }

        private void Output1Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Output1Mode.SelectedIndex == 0) // Custom
            {
                resetOutput2();
                this.patternLower1.Enabled = true;
                this.patternMiddle1.Enabled = true;
                this.patternUpper1.Enabled = true;
                this.Output1LowerThreshold.Enabled = true;
                this.Output1UpperThreshold.Enabled = true;
            }
            else if (Output1Mode.SelectedIndex == 1) // Off/Flashing/On
            {
                resetOutput2();
                this.Output1LowerThreshold.Value = 40;
                this.Output1LowerThreshold.Enabled = false;
                this.Output1UpperThreshold.Value = 60;
                this.Output1UpperThreshold.Enabled = false;
                this.patternLower1.Text = "00000000000000000000000000000000";
                this.patternLower1.Enabled = false;
                this.patternMiddle1.Text = "01010101010101010101010101010101";
                this.patternMiddle1.Enabled = false;
                this.patternUpper1.Text = "11111111111111111111111111111111";
                this.patternUpper1.Enabled = false;
            }
            else if (Output1Mode.SelectedIndex == 2) // Brightness
            {
                resetOutput2();
                this.patternLower1.Enabled = false;
                this.patternMiddle1.Enabled = false;
                this.patternUpper1.Enabled = false;
            }
            else if (Output1Mode.SelectedIndex == 3) // Alternate flashing (Wingtip)
            {
                this.Output2Mode.Items.AddRange(new string[] {"Alternating Flashing (Wingtip Lights)", "Alternating Flashing (Constant)"});

                this.Output1LowerThreshold.Value = 40;
                this.Output1LowerThreshold.Enabled = false;
                this.Output1UpperThreshold.Value = 50;
                this.Output1UpperThreshold.Enabled = false;
                this.patternLower1.Text = "00000000000000000000000000000000";
                this.patternLower1.Enabled = false;
                this.patternMiddle1.Text = "00000000000000000000000000000000";
                this.patternMiddle1.Enabled = false;
                this.patternUpper1.Text = "10000000000000000000000000000000";
                this.patternUpper1.Enabled = false;

                this.Output2Mode.SelectedIndex = this.Output1Mode.SelectedIndex;
                this.Output2Mode.Enabled = false;
                this.Output2Selection.SelectedIndex = this.Output1Selection.SelectedIndex;
                this.Output2Selection.Enabled = false;
                this.Output2LowerThreshold.Value = 40;
                this.Output2LowerThreshold.Enabled = false;
                this.Output2UpperThreshold.Value = 50;
                this.Output2UpperThreshold.Enabled = false;
                this.patternLower2.Text = "00000000000000000000000000000000";
                this.patternLower2.Enabled = false;
                this.patternMiddle2.Text = "00000000000000000000000000000000";
                this.patternMiddle2.Enabled = false;
                this.patternUpper2.Text = "00100000000000000000000000000000";
                this.patternUpper2.Enabled = false;
            }
            else if (Output1Mode.SelectedIndex == 4) // Alternate flashing (Constant)
            {
                this.Output2Mode.Items.AddRange(new string[] { "Alternating Flashing (Wingtip Lights)", "Alternating Flashing (Constant)" });

                this.Output1LowerThreshold.Value = 40;
                this.Output1LowerThreshold.Enabled = false;
                this.Output1UpperThreshold.Value = 50;
                this.Output1UpperThreshold.Enabled = false;
                this.patternLower1.Text = "00000000000000000000000000000000";
                this.patternLower1.Enabled = false;
                this.patternMiddle1.Text = "00000000000000000000000000000000";
                this.patternMiddle1.Enabled = false;
                this.patternUpper1.Text = "11111111111111110000000000000000";
                this.patternUpper1.Enabled = false;

                this.Output2Mode.SelectedIndex = this.Output1Mode.SelectedIndex;
                this.Output2Mode.Enabled = false;
                this.Output2Selection.SelectedIndex = this.Output1Selection.SelectedIndex;
                this.Output2Selection.Enabled = false;
                this.Output2LowerThreshold.Value = 40;
                this.Output2LowerThreshold.Enabled = false;
                this.Output2UpperThreshold.Value = 50;
                this.Output2UpperThreshold.Enabled = false;
                this.patternLower2.Text = "00000000000000000000000000000000";
                this.patternLower2.Enabled = false;
                this.patternMiddle2.Text = "00000000000000000000000000000000";
                this.patternMiddle2.Enabled = false;
                this.patternUpper2.Text = "00000000000000001111111111111111";
                this.patternUpper2.Enabled = false;
            }
        }
        private void Output2Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Output2Mode.SelectedIndex == 0) // Custom
            {
                this.patternLower2.Enabled = true;
                this.patternMiddle2.Enabled = true;
                this.patternUpper2.Enabled = true;
                this.Output2LowerThreshold.Enabled = true;
                this.Output2UpperThreshold.Enabled = true;
            }
            else if (Output2Mode.SelectedIndex == 1) // Off/Flashing/On
            {
                this.Output2LowerThreshold.Value = 40;
                this.Output2LowerThreshold.Enabled = false;
                this.Output2UpperThreshold.Value = 60;
                this.Output2UpperThreshold.Enabled = false;
                this.patternLower2.Text = "00000000000000000000000000000000";
                this.patternLower2.Enabled = false;
                this.patternMiddle2.Text = "01010101010101010101010101010101";
                this.patternMiddle2.Enabled = false;
                this.patternUpper2.Text = "11111111111111111111111111111111";
                this.patternUpper2.Enabled = false;
            }
            else if (Output2Mode.SelectedIndex == 2) // Brightness
            {
                this.patternLower2.Enabled = false;
                this.patternMiddle2.Enabled = false;
                this.patternUpper2.Enabled = false;
            }
        }

        private void resetOutput2()
        {
            Output2Mode.Enabled = true;
            Output2Mode.SelectedIndex = 0;
            Output2Selection.Enabled = true;
            Output2Selection.SelectedIndex = 0;
            Output2LowerThreshold.Enabled = true;
            Output2UpperThreshold.Enabled = true;
            patternLower2.Enabled = true;
            patternMiddle2.Enabled = true;
            patternUpper2.Enabled = true;
            Output2Mode.Items.Clear();
            Output2Mode.Items.AddRange(new object[] {
            "Custom...",
            "On/Flashing/Off",
            "Brightness/PWM"});
            Output2Mode.SelectedIndex = 0;
        }

        private void Output1LowerThreshold_ValueChanged(object sender, EventArgs e)
        {
            Output1UpperThreshold.Minimum = Output1LowerThreshold.Value;
        }

        private void Output2LowerThreshold_ValueChanged(object sender, EventArgs e)
        {
            Output2UpperThreshold.Minimum = Output2LowerThreshold.Value;
        }

        private void SerialPortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SerialPortComboBox.SelectedIndex > 0)
            {
                btnFlash.Enabled = true;
            }
            else
            {
                btnFlash.Enabled = false;
            }
        }

        // Changes the colour of the pattern boxes to highlight incorrect number of bits entered
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (this.patternLower1.Text.Length == 32)
            {
                this.patternLower1.BackColor = Color.LightGreen;
            }
            else
            {
                this.patternLower1.BackColor = Color.Red;
            }
            if (this.patternLower2.Text.Length == 32)
            {
                this.patternLower2.BackColor = Color.LightGreen;
            }
            else
            {
                this.patternLower2.BackColor = Color.Red;
            }
            if (this.patternMiddle1.Text.Length == 32)
            {
                this.patternMiddle1.BackColor = Color.LightGreen;
            }
            else
            {
                this.patternMiddle1.BackColor = Color.Red;
            }
            if (this.patternMiddle2.Text.Length == 32)
            {
                this.patternMiddle2.BackColor = Color.LightGreen;
            }
            else
            {
                this.patternMiddle2.BackColor = Color.Red;
            }
            if (this.patternUpper1.Text.Length == 32)
            {
                this.patternUpper1.BackColor = Color.LightGreen;
            }
            else
            {
                this.patternUpper1.BackColor = Color.Red;
            }
            if (this.patternUpper2.Text.Length == 32)
            {
                this.patternUpper2.BackColor = Color.LightGreen;
            }
            else
            {
                this.patternUpper2.BackColor = Color.Red;
            }
        }
    }
}
