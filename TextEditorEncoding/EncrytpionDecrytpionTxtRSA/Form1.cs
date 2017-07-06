using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EncrytpionDecrytpionTxtRSA
{
    public partial class Form1 : Form
    {
        private String EncryptIt(String to_encrypt, byte[] key, byte[] IV)
        {
            String result;
            RijndaelManaged Rijndael = new RijndaelManaged();
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (ICryptoTransform encryptor = Rijndael.CreateEncryptor(key, IV))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(to_encrypt);
                        }
                    }
                }
                result = System.Convert.ToBase64String(msEncrypt.ToArray());
            }
            Rijndael.Clear();
            return result;
        }

        private String DecryptIt(String to_encrypt, byte[] key, byte[] IV)
        {
            String result;
            RijndaelManaged Rijndael = new RijndaelManaged();
            using (MemoryStream msDecrypt = new MemoryStream(System.Convert.FromBase64String(to_encrypt)))
            {
                using (ICryptoTransform decryptor = Rijndael.CreateDecryptor(key, IV))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader swDecrypt = new StreamReader(csDecrypt))
                        {
                            result = swDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            Rijndael.Clear();
            return result;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Text Documents(*.txt)|*.txt|All Files(*.*)|*.*";
            if (sv.ShowDialog() == DialogResult.OK)
               myTextBox1.SaveFile(sv.FileName, RichTextBoxStreamType.PlainText);
            this.Text = sv.FileName;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (op.ShowDialog() == DialogResult.OK)
                    myTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = op.FileName;
            }catch(Exception ex)
            {
                MessageBox.Show("Error, somthing gone wrong.\n Orginal message:"+ex.Message);
            }
        }
        private void encodingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string haslo;
            using (input input = new input())
            {
                if (input.ShowDialog() == DialogResult.OK)
                {
                    haslo = input.input_text;
                    byte[] rijnKey = Encoding.ASCII.GetBytes(haslo);
                    byte[] rijnIV = Encoding.ASCII.GetBytes(haslo.Substring(0, 16));
                    myTextBox1.Text = EncryptIt(myTextBox1.Text, rijnKey, rijnIV);
                }
            }
        }

        private void decodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string haslo;
            using (input input = new input())
            {
                if (input.ShowDialog() == DialogResult.OK)
                {
                    haslo = input.input_text;
                    byte[] rijnKey = Encoding.ASCII.GetBytes(haslo);
                    byte[] rijnIV = Encoding.ASCII.GetBytes(haslo.Substring(0, 16));
                    myTextBox1.Text = DecryptIt(myTextBox1.Text, rijnKey, rijnIV);
                }
            }
        }
    }
}
