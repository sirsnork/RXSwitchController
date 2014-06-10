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
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFD.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsFD.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label5.Show();
                comboBox3.Show();
                label7.Show();
                comboBox5.Show();

                comboBox4.Items.Remove("Test");
            }
            else
            {
                label5.Hide();
                comboBox3.Hide();
                label7.Hide();
                comboBox5.Hide();

                comboBox4.Items.Add("Test");
            }
        }
        private void btnFlash_Click(object sender, EventArgs e)
        {
            int i = 1;
            string line;
            int address = 0;
            int[] channel = { 1, 8 };
            int[] mode = { 0, 0 };
            int[] threshold_lower = { 0xCC02, 0xCC02 };
            int[] threshold_upper = { 0x3205, 0x3205 };
            int[] pattern_lower = { 0x0, 0x0 };
            int[] pattern_middle = { 0x5555, 0x5555 };
            int[] pattern_upper = { 0xffff, 0xffff };

            if (File.Exists(@"D:\output.hex"))
            {
                File.Delete(@"D:\output.hex");
            }
            // Create a stringbuilder and write the new user input to it.
            StringBuilder sb = new StringBuilder();

            for (i = 0; i < 2; i++)
            {
                line = @":20" + address.ToString("X4") + @"00" + channel[i].ToString("X2") + mode[i].ToString("X2") + threshold_lower[i].ToString("X4") + threshold_upper[i].ToString("X4") + pattern_lower[i].ToString("X4") + pattern_middle[i].ToString("X4") + pattern_upper[i].ToString("X4") + @"FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
                line = add_checksum(line);
                address += 0x80;
                sb.AppendLine(line);
            }
            sb.AppendLine(@":00000001FF");

            // Open a streamwriter to a new text file named "UserInputFile.txt"and write the contents of 
            // the stringbuilder to it. 
            using (StreamWriter outfile = new StreamWriter(@"D:\output.hex", true))
            {
                outfile.Write(sb.ToString());
            }
            sb.Clear();

            // avrdude -p t85 -c tgyusblinker -P com7 -U flash:w:application.hex
            Process avrdude = new Process();
            avrdude.StartInfo.UseShellExecute = true;
            avrdude.StartInfo.FileName = @"C:\Windows\Notepad.exe";
            //avrdude.StartInfo.FileName = @"avrdude.exe -p t85 -c tgyusblinker -P " + SerialPortComboBox.SelectedItem.ToString().ToLower() + @" -U eeprom:w:D:\output.hex"
            avrdude.StartInfo.CreateNoWindow = true;
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
            //File.Delete(@"D:\output.hex");
        }
        // Do calculation to add a checksum byte to the end of the variable passed in, remove first byte before calculating checksum
        private string add_checksum(string line)
        {
            int j = 0;
            byte[] lineb = { 0x0 };
            int checksum = 0x0;
            lineb = StringToByteArray(line.Substring(1, line.Length - 1));
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
    }
}
