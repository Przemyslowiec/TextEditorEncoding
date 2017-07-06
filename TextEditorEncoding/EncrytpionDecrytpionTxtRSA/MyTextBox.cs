using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace EncrytpionDecrytpionTxtRSA {
    class MyTextBox : RichTextBox
    {
        public MyTextBox()
        {
            
            this.AllowDrop = true;
            this.DragDrop += new DragEventHandler(MyTextBox_DragDrop);
        }

        private void MyTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (fileNames != null)
            {
                foreach(string name in fileNames)
                {
                    try
                    {
                          this.AppendText(File.ReadAllText(name, Encoding.Default));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
