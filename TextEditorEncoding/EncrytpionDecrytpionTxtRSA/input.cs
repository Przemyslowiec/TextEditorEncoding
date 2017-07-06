using System;
using System.Windows.Forms;

namespace EncrytpionDecrytpionTxtRSA
{
    public partial class input : Form
    {
        public string input_text;
        public input()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (textBox1.Text.Length != 32)
            {
                if (textBox1.Text.Length > 32)
                {
                    input_text = textBox1.Text.Substring(0, 32);
                }
                else
                {
                    input_text = textBox1.Text.PadLeft((32 - textBox1.Text.Length+1), '/');
                }
            }
            
            this.Close();
        }
    }
}
