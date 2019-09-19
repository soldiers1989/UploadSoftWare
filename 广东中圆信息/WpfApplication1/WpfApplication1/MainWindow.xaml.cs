using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();

            try
            {
                //先打开两个类库文件
                // con.ConnectionString = "server=505-03;database=ttt;user=sa;pwd=123";
                con.ConnectionString = "server=gettersoft.sqlserver.rds.aliyuncs.com;database=gtdbha01;uid=hengan01;pwd=HengAn01";
                con.Open();
                if(con.State ==ConnectionState.Open )
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO t_foodcheck01 (checkno,checkmachine,checktime,checkmeth,itemtype,itemname,samplecode,samplename,checkhole,checkvalue,standvalue,result,makefactory,checkuser)VALUES(");
                    sb.AppendFormat("'{0}',", "201904231323327000001");
                    sb.AppendFormat("'{0}',", "DY3500(I)食品综合分析仪");
                    sb.AppendFormat("'{0}',", "2019/4/23 13:50:50");
                    sb.AppendFormat("'{0}',", "标准曲线法");
                    sb.AppendFormat("'{0}',", "胶体金");
                    sb.AppendFormat("'{0}',", "黄曲霉素B1");
                    sb.AppendFormat("'{0}',", "0021");
                    sb.AppendFormat("'{0}',", "菜油");
                    sb.AppendFormat("'{0}',", "02");
                    sb.AppendFormat("'{0}',", "阴性");
                    sb.AppendFormat("'{0}',", "50");
                    sb.AppendFormat("'{0}',", "合格");
                    sb.AppendFormat("'{0}',", "中山市桂乡油花生油厂");
                    sb.AppendFormat("'{0}')", "达元");

                    string IntData = sb.ToString ();
                    //插入数据
                    SqlCommand cmd = new SqlCommand(IntData, con);
                    int a = cmd.ExecuteNonQuery();


                    string sql = "select * from t_foodcheck01";
                    SqlDataAdapter adp = new SqlDataAdapter(sql, con);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if(dt!=null && dt.Rows.Count >0)
                    {

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();    //关闭数据库  
                }
            }
        }
        public string connString = "Data Source=xp;Initial Catalog=ExpressManager;Integrated Security=TRUE";
        //创建连接对象的变量  
        public SqlConnection conn;
        // 执行对数据表中数据的增加、删除、修改操作  
        public int NonQuery(string sql)
        {
            conn = new SqlConnection(connString);
            int a = -1;
            try
            {
                conn.Open();  //打开数据库  
                SqlCommand cmd = new SqlCommand(sql, conn);
                a = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();    //关闭数据库  
                }
            }
            return a;

        }
        // 执行对数据表中数据的查询操作  
        public DataSet Query(string sql)
        {
            conn = new SqlConnection(connString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();      //打开数据库  
                SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
                adp.Fill(ds);
            }
            catch
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();      //关闭数据库  
            }
            return null;
        }
    }
}
