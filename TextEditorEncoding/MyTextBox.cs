using System;

public class MyTextBox : TextBox
{
    public MyTextBox()
    {
        AllowDrop = true;
        Multiline = true;
        DragDrop += new DragEventHandler(MyTextBox_DragDrop);
        DragEnter += new DragEventHandler(MyTextBox_DragEnter);
    }

    void MyTextBox_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Copy;
    }

    void MyTextBox_DragDrop(object sender, DragEventArgs e)
    {
        DataObject data = (DataObject)e.Data;
        if (data.ContainsFileDropList())
        {
            string[] rawFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (rawFiles != null)
            {
                List<string> lines = new List<string>();
                foreach (string path in rawFiles)
                {
                    lines.AddRange(File.ReadAllLines(path));
                }
                Lines = lines.ToArray();
            }
        }
    }
}