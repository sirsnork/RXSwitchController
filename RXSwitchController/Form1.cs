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
        private async void btnFlash_Click(object sender, EventArgs e)
        {

            // Create a stringbuilder and write the new user input to it.
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("New User Input");
            sb.AppendLine("= = = = = =");
            sb.AppendLine();
            sb.AppendLine();

            // Open a streamwriter to a new text file named "UserInputFile.txt"and write the contents of 
            // the stringbuilder to it. 
            //using (StreamWriter outfile = new StreamWriter(@"%temp%\UserInputFile.txt", true))
            //{
            //    await outfile.WriteAsync(sb.ToString());
            //}


            // avrdude -p t85 -c tgyusblinker -P com7 -U flash:w:application.hex
            Process avrdude = new Process();
            avrdude.StartInfo.UseShellExecute = true;
            avrdude.StartInfo.FileName = @"C:\Windows\Notepad.exe";
            //avrdude.StartInfo.FileName = @"avrdude -p t85 -c tgyusblinker -P " + SerialPortComboBox.SelectedItem.ToString().ToLower() + @" -U flash:w:%temp%\output.hex"
            //avrdude.StartInfo.CreateNoWindow = true;
            avrdude.StartInfo.CreateNoWindow = false;
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
        }
    }
}
