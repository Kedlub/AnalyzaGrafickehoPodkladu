namespace AnalyzaGrafickehoPodkladu
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            loadImageButton = new Button();
            createMeritko = new Button();
            newSelectionButton = new Button();
            polygonSizeLabel = new Label();
            groupBox2 = new GroupBox();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(flowLayoutPanel1);
            groupBox1.Location = new Point(637, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(151, 426);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ovládání";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(loadImageButton);
            flowLayoutPanel1.Controls.Add(createMeritko);
            flowLayoutPanel1.Controls.Add(newSelectionButton);
            flowLayoutPanel1.Controls.Add(polygonSizeLabel);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 19);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(145, 404);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // loadImageButton
            // 
            loadImageButton.Location = new Point(3, 3);
            loadImageButton.Name = "loadImageButton";
            loadImageButton.Size = new Size(139, 23);
            loadImageButton.TabIndex = 1;
            loadImageButton.Text = "Načíst obrázek";
            loadImageButton.UseVisualStyleBackColor = true;
            loadImageButton.Click += loadImageButton_Click;
            // 
            // createMeritko
            // 
            createMeritko.Location = new Point(3, 32);
            createMeritko.Name = "createMeritko";
            createMeritko.Size = new Size(139, 23);
            createMeritko.TabIndex = 0;
            createMeritko.Text = "Vytvořit měřítko";
            createMeritko.UseVisualStyleBackColor = true;
            createMeritko.Click += createMeritko_Click;
            // 
            // newSelectionButton
            // 
            newSelectionButton.Location = new Point(3, 61);
            newSelectionButton.Name = "newSelectionButton";
            newSelectionButton.Size = new Size(139, 23);
            newSelectionButton.TabIndex = 2;
            newSelectionButton.Text = "Nový výběr";
            newSelectionButton.UseVisualStyleBackColor = true;
            newSelectionButton.Click += newSelectionButton_Click;
            // 
            // polygonSizeLabel
            // 
            polygonSizeLabel.Location = new Point(3, 87);
            polygonSizeLabel.Name = "polygonSizeLabel";
            polygonSizeLabel.Size = new Size(139, 24);
            polygonSizeLabel.TabIndex = 3;
            polygonSizeLabel.Text = "Velikost polygonu: 0";
            polygonSizeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(pictureBox1);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(619, 426);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Mapa";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(6, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(607, 398);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Analýza";
            WindowState = FormWindowState.Maximized;
            groupBox1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private PictureBox pictureBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button createMeritko;
        private Button loadImageButton;
        private Button newSelectionButton;
        private Label polygonSizeLabel;
    }
}