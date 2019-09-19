using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Test.model;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ServiceReference1.TraceSiteServiceSoapClient rt = new ServiceReference1.TraceSiteServiceSoapClient();
            //rt.GetTicket("","");
            //ServiceReference1.TongtaiSoapHeader soapheader = new ServiceReference1.TongtaiSoapHeader();
            //soapheader.ticket = "";
   
            //rt.GetProduceLots(soapheader);
            //ServiceReference2.wireDataServiceSoapClient sop = new ServiceReference2.wireDataServiceSoapClient();
            //sop.GetTicket("", "");
            //sop.Upload("", "");
            //cn.org.xancpzlzs.www.TraceSiteService ts = new cn.org.xancpzlzs.www.TraceSiteService();
            //ts.GetTicket("","");
            //com.tainot.enshi.wireDataService wire = new com.tainot.enshi.wireDataService();
            //wire.GetTicket("","");
            //wire.Upload("", "");
            
           

            //using (StringWriter sw = new StringWriter())
            //{
            //    XmlTextWriter xtw = new XmlTextWriter(sw);
            //    XmlDocument xmlDoc = new XmlDocument();
            //    //创建类型声明节点  
            //    XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            //    xmlDoc.AppendChild(node);

            //    xtw.WriteStartDocument();

            //    xtw.WriteStartElement("root");

            //    //test
            //    xtw.WriteStartElement("test");
            //    xtw.WriteString("test content");
            //    xtw.WriteEndElement();

            //    xtw.WriteEndElement();//root
            //    xtw.WriteEndDocument();

            //    string result = sw.ToString();
            //}

            var model1 = new createInfoXml
            {
                Serviceid = "11",
                Projectname = "测试",
                Applyname = "应用",
                Mobile = "152",
                Phone = "345",
                Address = "广州",
                Postcode = "1001",
                Email = "111345@qq.com",
                Contactman = "达元",
                Legalman = "达元",
                Idcard = "IC卡",
                Create_time = "20180927",
                Receive_time = "20180927",
            };
            var dataxml = XmlUtils.Serialize(model1, true);
        }
        /// <summary>  
        /// 创建上传数据的XML,返回XML字符串  
        /// </summary>  
        public string CreateBookXML()
        {
            StringBuilder xmlResult = new StringBuilder();
            //<request><head><data_count>1</data_count></head><dataset><data no="1"><ID>15C8B822-CF10-4080-B874-8EABB1D4D9E8</ID><SAMPLE>白菜</SAMPLE><CHECKITEM>农药残留</CHECKITEM><CHECKRESULT>1</CHECKRESULT><CHECKTIME>2018-09-29 00:00:00</CHECKTIME><SENDCOMPANY>湖北泰诺通科技有限公司</SENDCOMPANY><CHECKVALUE>20</CHECKVALUE><UNIT>%</UNIT><CITY>武汉</CITY><SUPPLIER>泰诺通农业科技</SUPPLIER><SAMPLENO>123456789</SAMPLENO><CHANNEL></CHANNEL><REFINFO></REFINFO><REFVALUE></REFVALUE><GREATKIND>蔬菜类</GREATKIND><LITTLEKIND>叶菜类</LITTLEKIND><A1>0</A1><A2>0</A2><CHECKER>管理员</CHECKER><DEVICE>农药残留检测仪</DEVICE><LONGITUDE>0</LONGITUDE><LATITUDE>0</LATITUDE><FEE>0</FEE><LINKMAN>王永波</LINKMAN><TEL>027-87777777</TEL><DESCRIPT></DESCRIPT><DATATYPE>1</DATATYPE></data></dataset></request>

            xmlResult.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xmlResult.Append("<request>");
            xmlResult.Append("<head>");
            xmlResult.AppendFormat("<data_count>{0}</data_count>", 1);
            xmlResult.Append("</head>");
            xmlResult.Append("<dataset>");
            xmlResult.Append("<data no=\"1\">");
            xmlResult.Append("<ID>20180930</ID>");
            xmlResult.Append("<SAMPLE>玉米</SAMPLE>");
            xmlResult.Append("<CHECKITEM>农药残留</CHECKITEM>");
            xmlResult.Append("<CHECKRESULT>1</CHECKRESULT>");
            xmlResult.Append("<CHECKTIME>2018-09-30 09:21:23</CHECKTIME>");
            xmlResult.Append("<SENDCOMPANY>广东达元</SENDCOMPANY>");
            xmlResult.Append("<CHECKVALUE>3.5</CHECKVALUE>");
            xmlResult.Append("<UNIT>%</UNIT>");
            xmlResult.Append("<CITY>广东广州</CITY>");
            xmlResult.Append("<SUPPLIER>欣泰扬科技</SUPPLIER>");
            xmlResult.Append("<SAMPLENO>20180927</SAMPLENO>");
            xmlResult.Append("<CHANNEL>1</CHANNEL>");
            xmlResult.Append("<REFINFO>GB2018</REFINFO>");
            xmlResult.Append("<REFVALUE>50</REFVALUE>");
            xmlResult.Append("<GREATKIND>食用农产品</GREATKIND>");
            xmlResult.Append("<LITTLEKIND>蔬菜</LITTLEKIND>");
            xmlResult.Append("<A1>0</A1>");
            xmlResult.Append("<A2>0</A2>");
            xmlResult.Append("<CHECKER>达元</CHECKER>");

            xmlResult.Append("<DEVICE>达元</DEVICE>");//少
            //xmlResult.Append("<REFDEVICEINFO>LZ4000</REFDEVICEINFO>"); //无
            xmlResult.Append("<LONGITUDE>12</LONGITUDE>");
            xmlResult.Append("<LATITUDE>12</LATITUDE>");
            //xmlResult.Append("<FEE>12</FEE>");//少
            //xmlResult.Append("<LINKMAN>王永波</LINKMAN>");//少
            //xmlResult.Append("<TEL>027-87777777</TEL>");//少
            //xmlResult.Append("<DESCRIPT>027-87777777</DESCRIPT>");//少
            //xmlResult.Append("<DATATYPE>1</DATATYPE>");//少
            //xmlResult.Append("<BARCODE>1234567</BARCODE>");//无
            xmlResult.Append("</data>");

            //xmlResult.Append("<data no=\"2\">");
            //xmlResult.Append("<ID>20180930</ID>");
            //xmlResult.Append("<SAMPLE>菜花</SAMPLE>");
            //xmlResult.Append("<CHECKITEM>农药残留</CHECKITEM>");
            //xmlResult.Append("<CHECKRESULT>1</CHECKRESULT>");
            //xmlResult.Append("<CHECKTIME>2018-09-30 09:21:23</CHECKTIME>");
            //xmlResult.Append("<SENDCOMPANY>广东达元</SENDCOMPANY>");
            //xmlResult.Append("<CHECKVALUE>2.5</CHECKVALUE>");
            //xmlResult.Append("<UNIT>%</UNIT>");
            //xmlResult.Append("<CITY>广东广州</CITY>");
            //xmlResult.Append("<SUPPLIER>欣泰扬科技</SUPPLIER>");
            //xmlResult.Append("<SAMPLENO>20180927</SAMPLENO>");
            //xmlResult.Append("<CHANNEL>1</CHANNEL>");
            //xmlResult.Append("<REFINFO>GB2018</REFINFO>");
            //xmlResult.Append("<REFVALUE>50</REFVALUE>");
            //xmlResult.Append("<GREATKIND>食用农产品</GREATKIND>");
            //xmlResult.Append("<LITTLEKIND>蔬菜</LITTLEKIND>");
            //xmlResult.Append("<A1>0</A1>");
            //xmlResult.Append("<A2>0</A2>");
            //xmlResult.Append("<CHECKER>达元</CHECKER>");

            //xmlResult.Append("<DEVICE>达元</DEVICE>");//少
            ////xmlResult.Append("<REFDEVICEINFO>LZ4000</REFDEVICEINFO>"); //无
            //xmlResult.Append("<LONGITUDE>12</LONGITUDE>");
            //xmlResult.Append("<LATITUDE>12</LATITUDE>");
            //xmlResult.Append("<FEE>12</FEE>");//少
            //xmlResult.Append("<LINKMAN>王永波</LINKMAN>");//少
            //xmlResult.Append("<TEL>027-87777777</TEL>");//少
            //xmlResult.Append("<DESCRIPT>027-87777777</DESCRIPT>");//少
            //xmlResult.Append("<DATATYPE>1</DATATYPE>");//少
            ////xmlResult.Append("<BARCODE>1234567</BARCODE>");//无
            //xmlResult.Append("</data>");


            xmlResult.Append("</dataset>");
            xmlResult.Append("</request>");



            //List<BookInfo> bookList = GetBookList();    //获取图书列表  
            //if (bookList != null && bookList.Count > 0)
            //{
            //    xmlResult.Append("<bookstore>");
            //    foreach (BookInfo book in bookList)
            //    {
            //        xmlResult.AppendFormat("<book id=\"{0}\" category=\"{1}\">", book.BookId, book.Category);
            //        xmlResult.AppendFormat("<title>{0}</title>", book.Title);
            //        xmlResult.AppendFormat("<author>{0}</author>", book.Author);
            //        xmlResult.AppendFormat("<publishDate>{0}</publishDate>", book.PublishDate.ToString("yyyy-MM-dd"));
            //        xmlResult.AppendFormat("<price>{0}</price>", book.Price);
            //        xmlResult.Append("</book>");
            //    }
            //    xmlResult.Append("</bookstore>");
            //}

            return xmlResult.ToString();

        } 
         /// <summary>  
        /// 获取图书列表,添加模拟数据  
        /// </summary>  
        /// <returns></returns>  
        public List<BookInfo> GetBookList()
        {
            List<BookInfo> bookList = new List<BookInfo>();
            BookInfo book1 = new BookInfo()
            {
                BookId = 1,
                Category = "CHILDREN",
                Title = "H哈利波特",
                Author = "J K.罗琳",
                PublishDate = new DateTime(2005, 08, 15),
                Price = 29.99
            };
            bookList.Add(book1);
            BookInfo book2 = new BookInfo()
            {
                BookId = 2,
                Category = "WEB",
                Title = "学习XML",
                Author = "艾瑞克.雷",
                PublishDate = new DateTime(2003, 10, 18),
                Price = 39.95
            };
            bookList.Add(book2);
            return bookList;
        }  


        public class BookInfo
        {
            public int BookId { set; get; }             //图书ID  
            public string Title { set; get; }           //图书名称  
            public string Category { set; get; }        //图书分类  
            public string Author { set; get; }          //图书作者  
            public DateTime PublishDate { set; get; }   //出版时间  
            public Double Price { set; get; }           //销售价格  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                com.tainot.foodsafe.wireDataService ds = new com.tainot.foodsafe.wireDataService();
                string tick = ds.GetTicket("hf_test001", "hf_test0012");
                string data = CreateBookXML();

                //string data = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response><head><success_error>0</success_error><success_count>2</success_count><error_count>1</error_count></head><success_dataset><data id=\"1\" /><data id=\"3\" /></success_dataset><error_dataset><data id=\"2\" error_code=\"0001\" /></error_dataset></response>";

                //string data =  "<request><head><data_count>1</data_count></head><dataset><data no=\"1\"><ID>15C8B822-CF10-4080-B874-8EABB1D4D9E8</ID><SAMPLE>白菜</SAMPLE><CHECKITEM>农药残留</CHECKITEM><CHECKRESULT>1</CHECKRESULT><CHECKTIME>2018-09-29 00:00:00</CHECKTIME><SENDCOMPANY>湖北泰诺通科技有限公司</SENDCOMPANY><CHECKVALUE>20</CHECKVALUE><UNIT>%</UNIT><CITY>武汉</CITY><SUPPLIER>泰诺通农业科技</SUPPLIER><SAMPLENO>123456789</SAMPLENO><CHANNEL></CHANNEL><REFINFO></REFINFO><REFVALUE></REFVALUE><GREATKIND>蔬菜类</GREATKIND><LITTLEKIND>叶菜类</LITTLEKIND><A1>0</A1><A2>0</A2><CHECKER>管理员</CHECKER><DEVICE>农药残留检测仪</DEVICE><LONGITUDE>0</LONGITUDE><LATITUDE>0</LATITUDE><FEE>0</FEE><LINKMAN>王永波</LINKMAN><TEL>027-87777777</TEL><DESCRIPT></DESCRIPT><DATATYPE>1</DATATYPE></data></dataset></request>";

                string rt = ds.Upload(tick, data);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(data);//加载XML字符串为XML文档

                //查找<users>    
                XmlNode root = xmlDoc.SelectSingleNode("response");   
                //获取到所有<users>的子节点    
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("response").ChildNodes;
               
                //XmlNodeList nodeCount = nodeList.Item(0).SelectNodes("success_dataset");

                string success = "";
                string errMsg = "";

                //遍历所有子节点   
                foreach (XmlNode xn in nodeList)    
                {
                    string name = xn.Name;
                    Console.WriteLine(name);

                    XmlElement xe = (XmlElement)xn;
                    XmlNodeList subList = xe.ChildNodes;    

                    if (xn.Name == "head")
                    {
                        foreach (XmlNode xmlNode in subList)
                        {
                            if ("success_error".Equals(xmlNode.Name))
                            {
                                success = xmlNode.InnerText;
                                Console.WriteLine("成功：" + xmlNode.InnerText);
                            }
                            else if ("success_count".Equals(xmlNode.Name))
                            {
                                Console.WriteLine("成功：" + xmlNode.InnerText);
                            }
                            else if ("error_count".Equals(xmlNode.Name))
                            {
                                Console.WriteLine("成功：" + xmlNode.InnerText);
                            }
                        }
                    }
                    else if (xn.Name == "success_dataset")
                    {
                        foreach (XmlNode xmlNode in subList)
                        {
                             if ("data".Equals(xmlNode.Name))
                             {
                                 string id = xmlNode.Attributes["id"].Value;//通过Attributes获得属性名为id的属性
                             }
                        }
 
                    }
                    else if (xn.Name == "error_dataset")
                    {
                        foreach (XmlNode xmlNode in subList)
                        {
                            if ("data".Equals(xmlNode.Name))
                            {
                                string id = xmlNode.Attributes["id"].Value;
                                string err = xmlNode.Attributes["error_code"].Value;//通过Attributes获得属性名为id的属性
                                errMsg = err;
                            }
                        }
                    }
                    if(success!="1")
                    {
                        MessageBox.Show("上传失败" );
                        string mesg = "";
                        if (errMsg == "0001")
                        {
                            mesg = "字符串长度过长";
                        }
                        else if(errMsg == "0002")
                        {
                            mesg = "时间格式不正确";
                        }
                        else if (errMsg == "0003")
                        {
                            mesg = "数值格式不正确";
                        }
                        else if (errMsg == "0011")
                        {
                            mesg = "检测设备号不匹配";
                        }
                        else if (errMsg == "0012")
                        {
                            mesg = "检测样品号不匹配";
                        }
                        else if (errMsg == "0013")
                        {
                            mesg = "检测项目号不匹配";
                        }
                        else if(errMsg == "0014")
                        {
                            mesg = "产地号不匹配";
                        }
                        else if (errMsg == "0015")
                        {
                            mesg = "检测单位号不匹配";
                        }
                        else if (errMsg == "0016")
                        {
                            mesg = "被检单位号不匹配";
                        }
                        else if (errMsg == "0091")
                        {
                            mesg = "为知错误";
                        }
                    }
                    else if(success=="1")
                    {
                        MessageBox.Show("上传成功");
                    }
                    //XmlElement xe = (XmlElement)xn;
                    //string id = xn.FirstChild.InnerXml; //结果为1，2，3
                    //Console.WriteLine("节点的ID为： " + xe.GetAttribute("success_error"));   
                    //XmlNodeList subList = xe.ChildNodes;     
                    //foreach (XmlNode xmlNode in subList)   
                    //{
                       
                    //    if ("success_error>".Equals(xmlNode.Name))        
                    //    {     
                    //        Console.WriteLine("头：" + xmlNode.InnerText);     
                    //    }
                    //    else if ("success_count".Equals(xmlNode.Name))    
                    //    {        
                    //        Console.WriteLine("内容：" + xmlNode.InnerText);     
                    //    }
                    //    else if ("error_count".Equals(xmlNode.Name))
                    //    {
                    //        Console.WriteLine("说明：" + xmlNode.InnerText);
                    //    }
                    //    else if ("error_count".Equals(xmlNode.Name))
                    //    {
                    //        Console.WriteLine("说明：" + xmlNode.InnerText);
                    //    }
                    //    else if ("error_count".Equals(xmlNode.Name))
                    //    {
                    //        Console.WriteLine("说明：" + xmlNode.InnerText);
                    //    }
                    //    else if ("error_count".Equals(xmlNode.Name))
                    //    {
                    //        Console.WriteLine("说明：" + xmlNode.InnerText);
                    //    }
                    //    else if ("error_count".Equals(xmlNode.Name))
                    //    {
                    //        Console.WriteLine("说明：" + xmlNode.InnerText);
                    //    }
                    //    else if ("data".Equals(xmlNode.Name))
                    //    {
                    //        string id = xmlNode.Attributes["id"].Value;//通过Attributes获得属性名为id的属性
                    //        Console.WriteLine("说明：" + xmlNode.InnerText);
                    //    }

                    //    Console.WriteLine(xmlNode.Name);     
                    //}   
                }



                //doc.Load(data);//加载本地文件

                //MessageBox.Show(rt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //创建XmlDocument对象
            XmlDocument xmlDoc = new XmlDocument();
            //XML的声明<?xml version="1.0" encoding="gb2312"?> 
            XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            //追加xmldecl位置
            xmlDoc.AppendChild(xmlSM);
            //添加一个名为Gen的根节点
            XmlElement xml = xmlDoc.CreateElement("", "Gen", "");
            //追加Gen的根节点位置
            xmlDoc.AppendChild(xml);
            //添加另一个节点,与Gen所匹配，查找<Gen>
            XmlNode gen = xmlDoc.SelectSingleNode("Gen");
            //添加一个名为<Zi>的节点   
            XmlElement zi = xmlDoc.CreateElement("Zi");
            //为<Zi>节点的属性
            zi.SetAttribute("name", "博客园");
            zi.SetAttribute("age", "26");
            XmlElement x1 = xmlDoc.CreateElement("title");
            //InnerText:获取或设置节点及其所有子节点的串连值
            x1.InnerText = "C#从入门到放弃";
            zi.AppendChild(x1);//添加到<Zi>节点中
            XmlElement x2 = xmlDoc.CreateElement("unit");
            x2.InnerText = "第一讲，如何放弃";
            zi.AppendChild(x2);
            XmlElement x3 = xmlDoc.CreateElement("fm");
            x3.InnerText = "123.06兆赫";
            zi.AppendChild(x3);
            gen.AppendChild(zi);//添加到<Gen>节点中   
            //保存好创建的XML文档
            string xmll=  xmlDoc.InnerXml;
            xmlDoc.ToString();
            xmlDoc.Save("D:/data.xml");    
        }  


        
    }
}
