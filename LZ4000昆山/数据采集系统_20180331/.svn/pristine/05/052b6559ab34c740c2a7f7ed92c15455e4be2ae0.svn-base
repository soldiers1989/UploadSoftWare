using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using WorkstationModel.Model;

namespace WorkstationUI.function
{
    public partial class ucHelp : UserControl
    {
        public ucHelp()
        {
            InitializeComponent();
            //string path = Environment.CurrentDirectory;
            //String extension = System.IO.Path.GetExtension(path + "\\report\\操作指导书.doc");
            //if (extension == ".rtf")
            //    richTextBox1.LoadFile(path + "\\report\\操作指导书.doc");
            //else if (extension == ".txt")
            //    richTextBox1.LoadFile(path + "\\report\\操作指导书.doc");
            //else
            //    richTextBox1.LoadFile("操作指导书.doc"); 
            
        }
        //private string filepath = Environment.CurrentDirectory + "\\report\\操作指导书.doc";
        private string filepath = Environment.CurrentDirectory + "\\report\\操作指导书.rtf";
        private clsdiary dy = new clsdiary();
        private void ucHelp_Load(object sender, EventArgs e)
        {
            //加载rtf文件
            //Path.GetExtension(filepath);
            richTextBox1.LoadFile(filepath, RichTextBoxStreamType.RichText);
            //Path.GetExtension(filepath );
            //richTextBox1.LoadFile(DocTOrtf(filepath), RichTextBoxStreamType.RichText);

            dy.savediary(DateTime.Now.ToString(), "进入帮助", "成功");
           
        }
        private string DocTOrtf(string doc)
        {
            //创建一个word的实例
            Word.Application newApp = new Word.Application();

            // 指定源文件和目标文件
            object Source = doc;

            object Target = Path.GetDirectoryName(doc) + "\\" + Path.GetFileNameWithoutExtension(doc) + ".rtf";
            File.Create(Target.ToString()).Dispose();
            object Unknown = Type.Missing;

            // 打开要转换的Word文件
            newApp.Documents.Open(ref Source, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown);

            // 指定文档的类型
            object format = Word.WdSaveFormat.wdFormatRTF;

            //改变文档类型
            newApp.ActiveDocument.SaveAs(ref Target, ref format,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown);

            //关闭word实例
            newApp.ActiveDocument.Close(ref Unknown, ref Unknown, ref Unknown);
            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);

            return Target.ToString();
        }
    }
}
