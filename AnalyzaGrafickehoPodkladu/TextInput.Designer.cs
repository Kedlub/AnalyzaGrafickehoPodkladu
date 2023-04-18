namespace AnalyzaGrafickehoPodkladu
{
    partial class TextInput
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
            label1 = new Label();
            inputText = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(345, 23);
            label1.TabIndex = 0;
            label1.Text = "Něco se šeredně pokazilo";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // inputText
            // 
            inputText.Location = new Point(12, 35);
            inputText.Name = "inputText";
            inputText.Size = new Size(345, 23);
            inputText.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(145, 85);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Ok";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // TextInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(369, 120);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(inputText);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TextInput";
            ShowIcon = false;
            Text = "TextInput";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        public TextBox inputText;
    }
}