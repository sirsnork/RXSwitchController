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
        string eeprom_file = @"D:\eeprom_output.hex";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFlash_Click(object sender, EventArgs e)
        {
            int i = 1;
            string line;
            int address = 0;
            int[] channel = { this.Output1Selection.SelectedIndex + 1, this.Output2Selection.SelectedIndex + 1 };
            int[] mode = { Output1Mode.SelectedIndex == 1 ? 1 : 0, Output2Mode.SelectedIndex == 1 ? 1 : 0 };
            int[] threshold_lower = { Convert.ToInt32((Convert.ToInt32(this.Output1LowerThreshold.Value) * 13.64) + 342), Convert.ToInt32((Convert.ToInt32(this.Output2LowerThreshold.Value) * 13.64) + 342) };
            int[] threshold_upper = { Convert.ToInt32((Convert.ToInt32(this.Output1UpperThreshold.Value) * 13.64) + 342), Convert.ToInt32((Convert.ToInt32(this.Output2UpperThreshold.Value) * 13.64) + 342) };
            int[] pattern_lower = { Convert.ToInt32(this.patternLower1.Text, 2), Convert.ToInt32(this.patternLower2.Text, 2) };
            int[] pattern_middle = { Convert.ToInt32(this.patternMiddle1.Text, 2), Convert.ToInt32(this.patternMiddle2.Text, 2) };
            int[] pattern_upper = { Convert.ToInt32(this.patternUpper1.Text, 2), Convert.ToInt32(this.patternUpper2.Text, 2) };


            if (File.Exists(eeprom_file))
            {
                File.Delete(eeprom_file);
            }
            StringBuilder sb = new StringBuilder();

            for (i = 0; i < 2; i++)
            {
                line = @":20" + address.ToString("X4") + @"00" + channel[i].ToString("X2") + mode[i].ToString("X2") + InvertBytes(threshold_lower[i]) + InvertBytes(threshold_upper[i]) + InvertBytes(pattern_lower[i]) + InvertBytes(pattern_middle[i]) + InvertBytes(pattern_upper[i]) + @"FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
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

            Process avrdude = new Process();
            avrdude.StartInfo.UseShellExecute = true;
            avrdude.StartInfo.ErrorDialog = true;
            avrdude.StartInfo.FileName = @"C:\Windows\Notepad.exe";
            //avrdude.StartInfo.FileName = @".\avrdude\avrdude.exe -p t85 -c tgyusblinker -P " + SerialPortComboBox.SelectedItem.ToString().ToLower() + @" -U eeprom:w:" + eeprom_file;
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
        private string InvertBytes(int hex)
        {
            string output;
            output = hex.ToString("X4").Substring(2, 2) + hex.ToString("X4").Substring(0, 2);
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
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
                this.patternLower1.Text = "0000000000000000";
                this.patternLower1.Enabled = false;
                this.patternMiddle1.Text = "0101010101010101";
                this.patternMiddle1.Enabled = false;
                this.patternUpper1.Text = "1111111111111111";
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
                this.patternLower1.Text = "0000000000000000";
                this.patternLower1.Enabled = false;
                this.patternMiddle1.Text = "0000000000000000";
                this.patternMiddle1.Enabled = false;
                this.patternUpper1.Text = "0000111100000000";
                this.patternUpper1.Enabled = false;

                this.Output2Mode.SelectedIndex = this.Output1Mode.SelectedIndex;
                this.Output2Mode.Enabled = false;
                this.Output2Selection.SelectedIndex = this.Output1Selection.SelectedIndex;
                this.Output2Selection.Enabled = false;
                this.Output2LowerThreshold.Value = 40;
                this.Output2LowerThreshold.Enabled = false;
                this.Output2UpperThreshold.Value = 50;
                this.Output2UpperThreshold.Enabled = false;
                this.patternLower2.Text = "0000000000000000";
                this.patternLower2.Enabled = false;
                this.patternMiddle2.Text = "0000000000000000";
                this.patternMiddle2.Enabled = false;
                this.patternUpper2.Text = "0000000011110000";
                this.patternUpper2.Enabled = false;
            }
            else if (Output1Mode.SelectedIndex == 4) // Alternate flashing (Constant)
            {
                this.Output2Mode.Items.AddRange(new string[] { "Alternating Flashing (Wingtip Lights)", "Alternating Flashing (Constant)" });

                this.Output1LowerThreshold.Value = 40;
                this.Output1LowerThreshold.Enabled = false;
                this.Output1UpperThreshold.Value = 50;
                this.Output1UpperThreshold.Enabled = false;
                this.patternLower1.Text = "0000000000000000";
                this.patternLower1.Enabled = false;
                this.patternMiddle1.Text = "0000000000000000";
                this.patternMiddle1.Enabled = false;
                this.patternUpper1.Text = "1111111100000000";
                this.patternUpper1.Enabled = false;

                this.Output2Mode.SelectedIndex = this.Output1Mode.SelectedIndex;
                this.Output2Mode.Enabled = false;
                this.Output2Selection.SelectedIndex = this.Output1Selection.SelectedIndex;
                this.Output2Selection.Enabled = false;
                this.Output2LowerThreshold.Value = 40;
                this.Output2LowerThreshold.Enabled = false;
                this.Output2UpperThreshold.Value = 50;
                this.Output2UpperThreshold.Enabled = false;
                this.patternLower2.Text = "0000000000000000";
                this.patternLower2.Enabled = false;
                this.patternMiddle2.Text = "0000000000000000";
                this.patternMiddle2.Enabled = false;
                this.patternUpper2.Text = "0000000011111111";
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
    }
}
