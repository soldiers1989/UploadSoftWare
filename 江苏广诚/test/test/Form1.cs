using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using test.model;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            //通信测试
            test.WebReference.DatRockService webseb = new WebReference.DatRockService();
            string rtn = webseb.getUser("admin", "a123456");
            Console.WriteLine(rtn);
            Resul resultlist = Json.JsonToEntity<Resul>(rtn);
            //项目部获取
            string xiangmubu = webseb.getDepartment(resultlist.token);
            //被检单位
            string company = webseb.getCompany("15", resultlist.token);
            //摊位号
            string stall = webseb.getBooth("83", resultlist.token);//1840
            //数据上传
            StringBuilder sb = new StringBuilder();
            sb.Append("[{");
            sb.Append("\"CheckID\":\"20190318082722100110\",");
            sb.Append("\"department\":\"15\",");
            sb.Append("\"detComp\":\"90\",");
            sb.Append("\"boothNumber\":\"1840\",");
            sb.Append("\"sampNumber\":\"100021305\",");

             sb.Append("\"category\":\"果菜类\",");
             sb.Append("\"sampleName\":\"青瓜\",");
             sb.Append("\"testItem\":\"农药残留\",");
             sb.Append("\"standardVal\":\"50\",");

            sb.Append("\"detection\":\"50.55\",");
            sb.Append("\"detectionCom\":\"%\",");
            sb.Append("\"detResult\":\"不合格\",");
            sb.Append("\"detGist\":\"GB2013-009\",");
            sb.Append("\"detDate\":\"2019-03-18 08:26:10\",");
            sb.Append("\"jcshi\":\"达元检测室\",");
            sb.Append("\"inspector\":\"食安小卫士\"");
            sb.Append("}]");

            string upload = webseb.uploadDataDock(sb.ToString(), resultlist.token);
            Console.WriteLine(upload);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<MessageBullite> mm = new List<MessageBullite>();
            MessageBullite mb = new MessageBullite();
            mb.from_user_id = "00";
            mb.from_user_name = "达元";
            mb.id = "id";
            mm.Add(mb);

            mm.Add(mb);
            //string json = Json.JsonArraw(mm);

            string ar = JsonConvert.SerializeObject(mm);
        }
    }
}
