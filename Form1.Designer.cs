
namespace CoCSaver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ImportImageButton = new System.Windows.Forms.Button();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ParsedTextTextBox = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.RootDirLabel = new System.Windows.Forms.Label();
            this.SetRootDirButton = new System.Windows.Forms.Button();
            this.ParseTextButton = new System.Windows.Forms.Button();
            this.SaveImageButton = new System.Windows.Forms.Button();
            this.ImagePanel = new System.Windows.Forms.Panel();
            this.SaveFormatComboBox = new System.Windows.Forms.ComboBox();
            this.CroppedImagePictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ppBrightnessThresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RotateImageButton = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CroppedImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppBrightnessThresholdTrackBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImportImageButton
            // 
            this.ImportImageButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ImportImageButton.Location = new System.Drawing.Point(559, 726);
            this.ImportImageButton.Name = "ImportImageButton";
            this.ImportImageButton.Size = new System.Drawing.Size(207, 23);
            this.ImportImageButton.TabIndex = 1;
            this.ImportImageButton.Text = "Import Image";
            this.ImportImageButton.UseVisualStyleBackColor = false;
            this.ImportImageButton.Click += new System.EventHandler(this.ImportImageButton_Click);
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LoadImageButton.Location = new System.Drawing.Point(560, 697);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(206, 23);
            this.LoadImageButton.TabIndex = 2;
            this.LoadImageButton.Text = "Load Image";
            this.LoadImageButton.UseVisualStyleBackColor = false;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parsed Text:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.ParsedTextTextBox);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(556, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(207, 43);
            this.panel4.TabIndex = 9;
            // 
            // ParsedTextTextBox
            // 
            this.ParsedTextTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ParsedTextTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ParsedTextTextBox.Location = new System.Drawing.Point(3, 18);
            this.ParsedTextTextBox.Name = "ParsedTextTextBox";
            this.ParsedTextTextBox.Size = new System.Drawing.Size(199, 20);
            this.ParsedTextTextBox.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(556, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(207, 43);
            this.panel5.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Save Location:";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.RootDirLabel);
            this.panel6.Location = new System.Drawing.Point(3, 18);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 22);
            this.panel6.TabIndex = 7;
            // 
            // RootDirLabel
            // 
            this.RootDirLabel.AutoSize = true;
            this.RootDirLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RootDirLabel.Location = new System.Drawing.Point(-1, 1);
            this.RootDirLabel.Name = "RootDirLabel";
            this.RootDirLabel.Size = new System.Drawing.Size(0, 16);
            this.RootDirLabel.TabIndex = 5;
            // 
            // SetRootDirButton
            // 
            this.SetRootDirButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SetRootDirButton.Location = new System.Drawing.Point(560, 668);
            this.SetRootDirButton.Name = "SetRootDirButton";
            this.SetRootDirButton.Size = new System.Drawing.Size(206, 23);
            this.SetRootDirButton.TabIndex = 11;
            this.SetRootDirButton.Text = "Set Save Location";
            this.SetRootDirButton.UseVisualStyleBackColor = false;
            this.SetRootDirButton.Click += new System.EventHandler(this.SetRootDirButton_Click);
            // 
            // ParseTextButton
            // 
            this.ParseTextButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ParseTextButton.Location = new System.Drawing.Point(560, 639);
            this.ParseTextButton.Name = "ParseTextButton";
            this.ParseTextButton.Size = new System.Drawing.Size(203, 23);
            this.ParseTextButton.TabIndex = 12;
            this.ParseTextButton.Text = "Parse Text";
            this.ParseTextButton.UseVisualStyleBackColor = false;
            this.ParseTextButton.Click += new System.EventHandler(this.ParseTextButton_Click);
            // 
            // SaveImageButton
            // 
            this.SaveImageButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SaveImageButton.Location = new System.Drawing.Point(560, 610);
            this.SaveImageButton.Name = "SaveImageButton";
            this.SaveImageButton.Size = new System.Drawing.Size(203, 23);
            this.SaveImageButton.TabIndex = 13;
            this.SaveImageButton.Text = "Save Image";
            this.SaveImageButton.UseVisualStyleBackColor = false;
            this.SaveImageButton.Click += new System.EventHandler(this.SaveImageButton_Click);
            // 
            // ImagePanel
            // 
            this.ImagePanel.BackColor = System.Drawing.SystemColors.Control;
            this.ImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImagePanel.Location = new System.Drawing.Point(3, 3);
            this.ImagePanel.Name = "ImagePanel";
            this.ImagePanel.Size = new System.Drawing.Size(547, 746);
            this.ImagePanel.TabIndex = 14;
            // 
            // SaveFormatComboBox
            // 
            this.SaveFormatComboBox.FormattingEnabled = true;
            this.SaveFormatComboBox.Location = new System.Drawing.Point(638, 583);
            this.SaveFormatComboBox.Name = "SaveFormatComboBox";
            this.SaveFormatComboBox.Size = new System.Drawing.Size(121, 21);
            this.SaveFormatComboBox.TabIndex = 15;
            // 
            // CroppedImagePictureBox
            // 
            this.CroppedImagePictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.CroppedImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CroppedImagePictureBox.Location = new System.Drawing.Point(3, 24);
            this.CroppedImagePictureBox.Name = "CroppedImagePictureBox";
            this.CroppedImagePictureBox.Size = new System.Drawing.Size(201, 47);
            this.CroppedImagePictureBox.TabIndex = 16;
            this.CroppedImagePictureBox.TabStop = false;
            this.CroppedImagePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.CroppedImagePictureBox_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(565, 586);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Save Format:";
            // 
            // ppBrightnessThresholdTrackBar
            // 
            this.ppBrightnessThresholdTrackBar.Location = new System.Drawing.Point(1, 96);
            this.ppBrightnessThresholdTrackBar.Maximum = 40;
            this.ppBrightnessThresholdTrackBar.Name = "ppBrightnessThresholdTrackBar";
            this.ppBrightnessThresholdTrackBar.Size = new System.Drawing.Size(203, 45);
            this.ppBrightnessThresholdTrackBar.TabIndex = 18;
            this.ppBrightnessThresholdTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.ppBrightnessThresholdTrackBar.Value = 20;
            this.ppBrightnessThresholdTrackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.CroppedImagePictureBox);
            this.panel1.Controls.Add(this.ppBrightnessThresholdTrackBar);
            this.panel1.Location = new System.Drawing.Point(556, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 142);
            this.panel1.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Brightness Threshold:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Cropped Preview:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.RotateImageButton);
            this.panel2.Controls.Add(this.ImagePanel);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.ImportImageButton);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.LoadImageButton);
            this.panel2.Controls.Add(this.SaveFormatComboBox);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.SaveImageButton);
            this.panel2.Controls.Add(this.SetRootDirButton);
            this.panel2.Controls.Add(this.ParseTextButton);
            this.panel2.Location = new System.Drawing.Point(-1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(770, 753);
            this.panel2.TabIndex = 20;
            // 
            // RotateImageButton
            // 
            this.RotateImageButton.Image = ((System.Drawing.Image)(resources.GetObject("RotateImageButton.Image")));
            this.RotateImageButton.Location = new System.Drawing.Point(557, 246);
            this.RotateImageButton.Name = "RotateImageButton";
            this.RotateImageButton.Size = new System.Drawing.Size(40, 42);
            this.RotateImageButton.TabIndex = 20;
            this.RotateImageButton.UseVisualStyleBackColor = true;
            this.RotateImageButton.Click += new System.EventHandler(this.RotateImageButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 755);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CoC Saver";
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CroppedImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppBrightnessThresholdTrackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ImportImageButton;
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label RootDirLabel;
        private System.Windows.Forms.Button SetRootDirButton;
        private System.Windows.Forms.Button ParseTextButton;
        private System.Windows.Forms.Button SaveImageButton;
        private System.Windows.Forms.TextBox ParsedTextTextBox;
        private System.Windows.Forms.Panel ImagePanel;
        private System.Windows.Forms.ComboBox SaveFormatComboBox;
        private System.Windows.Forms.PictureBox CroppedImagePictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar ppBrightnessThresholdTrackBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RotateImageButton;
    }
}

