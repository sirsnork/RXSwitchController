using System;
using System.IO.Ports;

namespace _FirstWindowsFormsApplication
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
            this.Output1Selection = new System.Windows.Forms.ComboBox();
            this.Output2Selection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Output1Mode = new System.Windows.Forms.ComboBox();
            this.Output2Mode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SerialPortComboBox = new System.Windows.Forms.ComboBox();
            this.btnFlash = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Output1LowerThreshold = new System.Windows.Forms.NumericUpDown();
            this.Output1UpperThreshold = new System.Windows.Forms.NumericUpDown();
            this.patternLower1 = new System.Windows.Forms.TextBox();
            this.patternMiddle1 = new System.Windows.Forms.TextBox();
            this.patternUpper1 = new System.Windows.Forms.TextBox();
            this.patternLower1_label = new System.Windows.Forms.Label();
            this.patternMiddle_label = new System.Windows.Forms.Label();
            this.patternUpper_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.patternUpper2 = new System.Windows.Forms.TextBox();
            this.patternMiddle2 = new System.Windows.Forms.TextBox();
            this.patternLower2 = new System.Windows.Forms.TextBox();
            this.Output2UpperThreshold = new System.Windows.Forms.NumericUpDown();
            this.Output2LowerThreshold = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Output1LowerThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Output1UpperThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Output2UpperThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Output2LowerThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // Output1Selection
            // 
            this.Output1Selection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Output1Selection.FormattingEnabled = true;
            this.Output1Selection.Items.AddRange(new object[] {
            "Channel 1",
            "Channel 2",
            "Channel 3",
            "Channel 4",
            "Channel 5",
            "Channel 6",
            "Channel 7",
            "Channel 8",
            "Channel 9",
            "Channel 10",
            "Channel 11",
            "Channel 12",
            "Channel 13",
            "Channel 14",
            "Channel 15",
            "Channel 16"});
            this.Output1Selection.Location = new System.Drawing.Point(102, 45);
            this.Output1Selection.Name = "Output1Selection";
            this.Output1Selection.Size = new System.Drawing.Size(121, 21);
            this.Output1Selection.TabIndex = 7;
            // 
            // Output2Selection
            // 
            this.Output2Selection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Output2Selection.FormattingEnabled = true;
            this.Output2Selection.Items.AddRange(new object[] {
            "Channel 1",
            "Channel 2",
            "Channel 3",
            "Channel 4",
            "Channel 5",
            "Channel 6",
            "Channel 7",
            "Channel 8",
            "Channel 9",
            "Channel 10",
            "Channel 11",
            "Channel 12",
            "Channel 13",
            "Channel 14",
            "Channel 15",
            "Channel 16"});
            this.Output2Selection.Location = new System.Drawing.Point(102, 219);
            this.Output2Selection.Name = "Output2Selection";
            this.Output2Selection.Size = new System.Drawing.Size(121, 21);
            this.Output2Selection.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Output 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Output 2";
            // 
            // Output1Mode
            // 
            this.Output1Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Output1Mode.FormattingEnabled = true;
            this.Output1Mode.Items.AddRange(new object[] {
            "Custom...",
            "On/Flashing/Off",
            "Brightness/PWM",
            "Alternating Flashing (Wingtip Lights)",
            "Alternating Flashing (Constant)"});
            this.Output1Mode.Location = new System.Drawing.Point(302, 45);
            this.Output1Mode.Name = "Output1Mode";
            this.Output1Mode.Size = new System.Drawing.Size(200, 21);
            this.Output1Mode.TabIndex = 11;
            this.Output1Mode.SelectedIndexChanged += new System.EventHandler(this.Output1Mode_SelectedIndexChanged);
            // 
            // Output2Mode
            // 
            this.Output2Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Output2Mode.FormattingEnabled = true;
            this.Output2Mode.Items.AddRange(new object[] {
            "Custom...",
            "On/Flashing/Off",
            "Brightness/PWM"});
            this.Output2Mode.Location = new System.Drawing.Point(302, 219);
            this.Output2Mode.Name = "Output2Mode";
            this.Output2Mode.Size = new System.Drawing.Size(200, 21);
            this.Output2Mode.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Mode";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Mode";
            // 
            // SerialPortComboBox
            // 
            this.SerialPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialPortComboBox.FormattingEnabled = true;
            this.SerialPortComboBox.Location = new System.Drawing.Point(297, 407);
            this.SerialPortComboBox.Name = "SerialPortComboBox";
            this.SerialPortComboBox.Size = new System.Drawing.Size(131, 21);
            this.SerialPortComboBox.TabIndex = 25;
            this.SerialPortComboBox.SelectedIndexChanged += new System.EventHandler(this.SerialPortComboBox_SelectedIndexChanged);
            // 
            // btnFlash
            // 
            this.btnFlash.Enabled = false;
            this.btnFlash.Location = new System.Drawing.Point(216, 407);
            this.btnFlash.Name = "btnFlash";
            this.btnFlash.Size = new System.Drawing.Size(75, 21);
            this.btnFlash.TabIndex = 26;
            this.btnFlash.Text = "Flash";
            this.btnFlash.UseVisualStyleBackColor = true;
            this.btnFlash.Click += new System.EventHandler(this.btnFlash_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Lower Threshold (%)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(308, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Upper Threshold (%)";
            // 
            // Output1LowerThreshold
            // 
            this.Output1LowerThreshold.Location = new System.Drawing.Point(165, 85);
            this.Output1LowerThreshold.Name = "Output1LowerThreshold";
            this.Output1LowerThreshold.Size = new System.Drawing.Size(47, 20);
            this.Output1LowerThreshold.TabIndex = 35;
            this.Output1LowerThreshold.ValueChanged += new System.EventHandler(this.Output1LowerThreshold_ValueChanged);
            // 
            // Output1UpperThreshold
            // 
            this.Output1UpperThreshold.Location = new System.Drawing.Point(417, 85);
            this.Output1UpperThreshold.Name = "Output1UpperThreshold";
            this.Output1UpperThreshold.Size = new System.Drawing.Size(47, 20);
            this.Output1UpperThreshold.TabIndex = 36;
            // 
            // patternLower1
            // 
            this.patternLower1.Location = new System.Drawing.Point(81, 150);
            this.patternLower1.MaxLength = 32;
            this.patternLower1.Name = "patternLower1";
            this.patternLower1.Size = new System.Drawing.Size(100, 20);
            this.patternLower1.TabIndex = 38;
            this.patternLower1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBinary_KeyPress);
            // 
            // patternMiddle1
            // 
            this.patternMiddle1.Location = new System.Drawing.Point(210, 150);
            this.patternMiddle1.MaxLength = 32;
            this.patternMiddle1.Name = "patternMiddle1";
            this.patternMiddle1.Size = new System.Drawing.Size(100, 20);
            this.patternMiddle1.TabIndex = 39;
            this.patternMiddle1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBinary_KeyPress);
            // 
            // patternUpper1
            // 
            this.patternUpper1.Location = new System.Drawing.Point(343, 150);
            this.patternUpper1.MaxLength = 32;
            this.patternUpper1.Name = "patternUpper1";
            this.patternUpper1.Size = new System.Drawing.Size(100, 20);
            this.patternUpper1.TabIndex = 40;
            this.patternUpper1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBinary_KeyPress);
            // 
            // patternLower1_label
            // 
            this.patternLower1_label.AutoSize = true;
            this.patternLower1_label.Location = new System.Drawing.Point(93, 134);
            this.patternLower1_label.Name = "patternLower1_label";
            this.patternLower1_label.Size = new System.Drawing.Size(73, 13);
            this.patternLower1_label.TabIndex = 41;
            this.patternLower1_label.Text = "Pattern Lower";
            // 
            // patternMiddle_label
            // 
            this.patternMiddle_label.AutoSize = true;
            this.patternMiddle_label.Location = new System.Drawing.Point(221, 134);
            this.patternMiddle_label.Name = "patternMiddle_label";
            this.patternMiddle_label.Size = new System.Drawing.Size(75, 13);
            this.patternMiddle_label.TabIndex = 42;
            this.patternMiddle_label.Text = "Pattern Middle";
            // 
            // patternUpper_label
            // 
            this.patternUpper_label.AutoSize = true;
            this.patternUpper_label.Location = new System.Drawing.Point(355, 134);
            this.patternUpper_label.Name = "patternUpper_label";
            this.patternUpper_label.Size = new System.Drawing.Size(73, 13);
            this.patternUpper_label.TabIndex = 43;
            this.patternUpper_label.Text = "Upper Pattern";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Upper Pattern";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(221, 312);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 52;
            this.label9.Text = "Pattern Middle";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(93, 312);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 51;
            this.label10.Text = "Pattern Lower";
            // 
            // patternUpper2
            // 
            this.patternUpper2.Location = new System.Drawing.Point(343, 328);
            this.patternUpper2.MaxLength = 32;
            this.patternUpper2.Name = "patternUpper2";
            this.patternUpper2.Size = new System.Drawing.Size(100, 20);
            this.patternUpper2.TabIndex = 50;
            this.patternUpper2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBinary_KeyPress);
            // 
            // patternMiddle2
            // 
            this.patternMiddle2.Location = new System.Drawing.Point(210, 328);
            this.patternMiddle2.MaxLength = 32;
            this.patternMiddle2.Name = "patternMiddle2";
            this.patternMiddle2.Size = new System.Drawing.Size(100, 20);
            this.patternMiddle2.TabIndex = 49;
            this.patternMiddle2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBinary_KeyPress);
            // 
            // patternLower2
            // 
            this.patternLower2.Location = new System.Drawing.Point(81, 328);
            this.patternLower2.MaxLength = 32;
            this.patternLower2.Name = "patternLower2";
            this.patternLower2.Size = new System.Drawing.Size(100, 20);
            this.patternLower2.TabIndex = 48;
            this.patternLower2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBinary_KeyPress);
            // 
            // Output2UpperThreshold
            // 
            this.Output2UpperThreshold.Location = new System.Drawing.Point(417, 263);
            this.Output2UpperThreshold.Name = "Output2UpperThreshold";
            this.Output2UpperThreshold.Size = new System.Drawing.Size(47, 20);
            this.Output2UpperThreshold.TabIndex = 47;
            // 
            // Output2LowerThreshold
            // 
            this.Output2LowerThreshold.Location = new System.Drawing.Point(165, 263);
            this.Output2LowerThreshold.Name = "Output2LowerThreshold";
            this.Output2LowerThreshold.Size = new System.Drawing.Size(47, 20);
            this.Output2LowerThreshold.TabIndex = 46;
            this.Output2LowerThreshold.ValueChanged += new System.EventHandler(this.Output2LowerThreshold_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(308, 265);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Upper Threshold (%)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 265);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 44;
            this.label12.Text = "Lower Threshold (%)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 476);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.patternUpper2);
            this.Controls.Add(this.patternMiddle2);
            this.Controls.Add(this.patternLower2);
            this.Controls.Add(this.Output2UpperThreshold);
            this.Controls.Add(this.Output2LowerThreshold);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.patternUpper_label);
            this.Controls.Add(this.patternMiddle_label);
            this.Controls.Add(this.patternLower1_label);
            this.Controls.Add(this.patternUpper1);
            this.Controls.Add(this.patternMiddle1);
            this.Controls.Add(this.patternLower1);
            this.Controls.Add(this.Output1UpperThreshold);
            this.Controls.Add(this.Output1LowerThreshold);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFlash);
            this.Controls.Add(this.SerialPortComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Output2Mode);
            this.Controls.Add(this.Output1Mode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Output2Selection);
            this.Controls.Add(this.Output1Selection);
            this.Name = "Form1";
            this.Text = "Lighting Controller Programmer";
            ((System.ComponentModel.ISupportInitialize)(this.Output1LowerThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Output1UpperThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Output2UpperThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Output2LowerThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.ComboBox Output1Selection;
        private System.Windows.Forms.ComboBox Output2Selection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Output1Mode;
        private System.Windows.Forms.ComboBox Output2Mode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox SerialPortComboBox;
        private System.Windows.Forms.Button btnFlash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown Output1LowerThreshold;
        private System.Windows.Forms.NumericUpDown Output1UpperThreshold;
        private System.Windows.Forms.TextBox patternLower1;
        private System.Windows.Forms.TextBox patternMiddle1;
        private System.Windows.Forms.TextBox patternUpper1;
        private System.Windows.Forms.Label patternLower1_label;
        private System.Windows.Forms.Label patternMiddle_label;
        private System.Windows.Forms.Label patternUpper_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox patternUpper2;
        private System.Windows.Forms.TextBox patternMiddle2;
        private System.Windows.Forms.TextBox patternLower2;
        private System.Windows.Forms.NumericUpDown Output2UpperThreshold;
        private System.Windows.Forms.NumericUpDown Output2LowerThreshold;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}

