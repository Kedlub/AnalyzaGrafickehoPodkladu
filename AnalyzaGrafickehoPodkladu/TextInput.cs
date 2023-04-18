using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzaGrafickehoPodkladu
{
    public partial class TextInput : Form
    {
        public TextInput()
        {
            InitializeComponent();
        }

        public TextInput(string title) : this()
        {
            label1.Text = title;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
