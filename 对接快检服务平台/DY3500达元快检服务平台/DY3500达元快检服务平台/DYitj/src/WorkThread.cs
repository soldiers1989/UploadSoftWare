using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Windows;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataSentence.Kjc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DYSeriesDataSet.DataModel;
using System.Threading;
using System.Collections;

namespace AIO
{
    public class WorkThread : ChildThread, IDisposable
    { 
        private static tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        bool _disposed = false;
        private DataTable dt = null;
        private ComPort _port = new ComPort();
        private string lasttime = "";
        /// <summary>
        /// 仪器检测项目
        /// </summary>
        private string _CheckItemSecondDisplay;
        /// <summary>
        /// 检测项目
        /// </summary>
        private string _CheckItems;
        /// <summary>
        /// 检测标准
        /// </summary>
        private string _Standard;
        private string _SampleStandardAgainDisplay;
        /// <summary>
        /// 检测任务
        /// </summary>
        private string _CheckItemStandard;
        /// <summary>
        /// 被检单位
        /// </summary>
        private string _DownCompany;
        private clsCompanyOpr _clsCompanyOpr = new clsCompanyOpr();
        private clsttStandardDecideOpr _clsttStandardDecideOpr = new clsttStandardDecideOpr();

        protected override void HandleMessage(Message msg)
        {
            StringBuilder sb = new StringBuilder();
            base.HandleMessage(msg);
            switch (msg.what)
            {
                case MsgCode.MSG_COMM_TEST:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (Test(_port, 3))
                        {
                            msg.result = true;
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_AD:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (Test(_port, 3))
                        {
                            byte[] data = ReadADValue(_port);
                            if (null != data)
                            {
                                msg.result = true;
                                msg.data = data;
                            }
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_AD_CYCLE:
                    if (_port.Open(msg.str1))
                    {
                        if (Test(_port, 3))
                        {
                            while (bWhileReadADCycle)
                            {
                                byte[] data = ReadADValue(_port);
                                if (null != data)
                                {
                                    msg.result = true;
                                    msg.data = data;
                                    if (null != this.target)
                                        target.SendMessage(msg, null);
                                }
                                DateUtils.WaitMs(20);
                            }
                        }
                        _port.Close();
                    }
                    else
                    {
                        msg.result = false;
                        msg.data = null;
                        if (null != this.target)
                            target.SendMessage(msg, null);
                    }
                    break;
                // 设置TC线没有返回，这个最好是放在读取摄像头之前发送。
                case MsgCode.MSG_SET_TCLINE:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        string val = string.Empty;
                        for (int i = 0; i < msg.data.Length; i++)
                        {
                            val += msg.data[i] + ",";
                        }
                        System.Console.WriteLine(val);
                        WriteData(_port, msg.data, 0, msg.data.Length);
                        msg.result = true;
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_CAM:
                    msg.result = false;
                    if (Global.JtjVersion == 3)//新的胶体金模块3.0
                    {
                        //新模块
                        ComPort.isver = true;
                        List<ComPort> ports = null;
                        List<byte[]> datas = null;
                        msg.result = true;
                        if (msg.obj1 != null)
                        {
                            ports = msg.obj1 as List<ComPort>;
                            datas = new List<byte[]>();
                            //其他操作指令
                            //for (int i = 0; i < ports.Count; i++)
                            //{
                            //    if (ports[i] == null || msg.obj3.ToString().Length == 0) continue;
                            //    byte cmd = 0x00;
                            //    switch (msg.obj3.ToString())
                            //    {
                            //        case "in"://进卡
                            //            cmd = 0x01;
                            //            break;
                            //        case "out"://出卡
                            //            cmd = 0x02;
                            //            break;
                            //        case "qr"://二维码位置
                            //            cmd = 0x04;
                            //            break;
                            //        case "test"://检测时，判定是否有卡
                            //            cmd = 0x13;
                            //            break;

                            //        default:
                            //            break;
                            //    }
                            //}
                            //DY-3500一直读取数据
                            //采集数据
                            for (int j = 0; j < ports.Count; j++)
                            {
                                datas.Add(null);
                                if (ports[j] == null) continue;

                                byte[] data = ReadImg(ports[j]);
                                if (data != null)
                                {
                                    datas[j] = data;
                                }
                            }
                            msg.obj2 = datas;
                        }
                        ComPort.isver = false;
                    }
                    else
                    {
                        if (_port.Open(msg.str1))
                        {
                            byte[] data = ReadCam(_port, msg.arg1);
                            if (null != data)
                            {
                                msg.result = true;
                                msg.data = data;
                            }
                            else
                            {
                                DateUtils.WaitMs(3000);
                                FileUtils.ErrorLog("SXT ERROR", msg.str1, "NULL");
                            }
                            _port.Close();
                        }
                    }

                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_GRAYVALUES:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = ReadGrayValue(_port, msg.arg1, msg.arg2);
                        if (null != data)
                        {
                            msg.result = true;
                            msg.data = data;
                        }
                        //byte[] value = ReadCTGrayValue(_port, msg.arg1, msg.arg2);
                        //if (value != null)
                        //{
                        //    msg.cValue = value[0] | (value[1] << 8) | (value[2] << 16) | (value[3] << 24);
                        //    msg.tValue = value[4] | (value[5] << 8) | (value[6] << 16) | (value[7] << 24); ;
                        //}
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_READ_RGBVALUE:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        byte[] data = ReadRGBValue(_port, msg.arg1);
                        if (null != data)
                        {
                            msg.result = true;
                            msg.data = data;
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_PRINT:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (Test(_port, 3))
                        {
                            _port.Write(msg.data, msg.arg1, msg.arg2);
                            msg.result = true;
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;
             
                case MsgCode.MSG_CHECK_CONNECTION://通信测试
                    msg.result = false;
                    msg.errMsg = "";
                    try
                    {
                        string rtn = InterfaceHelper.QuickTestServerLogin(msg.str1 ,msg.str2 ,msg.str3,"",1);
                        FileUtils.KLog(rtn, "接收", 1);

                        if (rtn != "" && rtn.Contains("success") || rtn != "" && rtn.Contains("msg"))
                        {
                            ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(rtn);

                            if (Jresult.msg == "操作成功" && Jresult.success == true)
                            {
                                objdata obj = JsonHelper.JsonToEntity<objdata>(Jresult.obj.ToString());
                                sysRights(obj.rights);
                                userdata ud = JsonHelper.JsonToEntity<userdata>(obj.user.ToString());
                                Global.d_depart_name = ud.d_depart_name;
                                Global.depart_id = ud.depart_id;
                                Global.p_point_name = ud.p_point_name;
                                Global.point_id = ud.point_id;
                                Global.user_name = ud.user_name;
                                Global.id = ud.id;
                                Global.realname = ud.realname;
                                Global.pointName = ud.p_point_name;
                                Global.orgName = ud.d_depart_name;
                                Global.Token = obj.token;
                                msg.responseInfo = Global.Token;
                                msg.result = true;
                            }
                            else
                            {
                                msg.errMsg = Jresult.msg;
                            }
                        }
                        else
                        {
                            msg.errMsg = rtn;
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.errMsg = ex.Message;
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_UPLOAD://数据上传
                    msg.result = false;
                    string err = "";
                    
                    try
                    {
                        CheckPointInfo cpi = (CheckPointInfo)msg.obj1;
                        Global.UploadSCount = Global.UploadFCount = 0;

                        string urlUp = InterfaceHelper.GetServiceURL(cpi.url, 6);
                        string pwd = Global.MD5(Global.MD5(cpi.pwd));
                        for (int i = 0; i < msg.selectedRecords.Count; i++)
                        {
                            if (msg.selectedRecords[i].FoodName == "对照样")
                            {
                                continue;
                            }
                            if (msg.selectedRecords[i].Result == "合格" || msg.selectedRecords[i].Result == "不合格" || msg.selectedRecords[i].Result == "阴性" || msg.selectedRecords[i].Result == "阳性")
                            {

                                if (msg.selectedRecords[i].shoudong == "是")//手动测试
                                {
                                    MutestUpload(msg.selectedRecords[i], urlUp, cpi.user, out err);
                                }
                                else if (Global.IsSweepCode)//扫码检测上传
                                {
                                    urlUp = InterfaceHelper.GetServiceURL(cpi.url, 18);
                                    QuickSweenUpLoad(msg.selectedRecords[i], urlUp, cpi.user, pwd);
                                }
                                else//自动测试
                                {
                                    QuickUpLoad(msg.selectedRecords[i], urlUp, cpi.user, pwd);
                                }
                            }
                        }
                        msg.result = true;
                    }
                    catch (Exception ex)
                    {
                        FileUtils.ErrorLog("上传数据时出现异常", "上传失败", ex.Message);
                    }

                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_CHECK_SYNC:
                    msg.result = false;
                    if (Global.InterfaceType.Equals("KJC"))
                    {
                        msg.DownLoadCompany = InterfaceHelper.DownloadBasicData(msg.str1, msg.str2, msg.str3, "company");
                        msg.result = true;
                    }
                    else if (Global.InterfaceType.Equals("DY"))
                    {
                        if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "all")))
                            msg.result = true;
                        if (null != this.target)
                        {
                            msg.SampleStandardName = _SampleStandardAgainDisplay;
                            msg.CheckItemsTempList = _CheckItemSecondDisplay;
                            msg.CheckItems = _CheckItems;
                            msg.Standard = _Standard;
                            msg.CheckItemsAdapter = _CheckItemStandard;
                            msg.DownLoadCompany = _DownCompany;
                        }
                    }
                    target.SendMessage(msg, null);
                    break;

                case MsgCode.MSG_DownCompany:
                    msg.result = false;
                    if ("true".Equals(DownLoadAllData(msg.str1, msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.args.Dequeue(), msg.str2, msg.str3, "Company")))
                        msg.result = true;
                    if (null != this.target)
                    {
                        msg.DownLoadCompany = _DownCompany;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_DownCheckItems://检测项目下载
                    msg.result = true ;
                    lasttime = "";
                    msg.responseInfo = "";
                    dt = _bll.GetRequestTime("RequestName='CheckItem'", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lasttime = dt.Rows[0]["UpdateTime"].ToString();
                        _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='CheckItem'", "", 1, out err);
                    }
                    else
                    {
                        _bll.InsertResquestTime("'CheckItem','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                    }

                    string url2 = msg.str1;
                   
                    string ItemAddr= InterfaceHelper.GetServiceURL(url2, 5);//地址
                    sb.Length = 0;
                    sb.Append(ItemAddr);
                    sb.AppendFormat("?userToken={0}", Global.Token);
                    sb.AppendFormat("&type={0}", "item");
                    sb.AppendFormat("&serialName={0}", "");
                    sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                    FileUtils.KLog(sb.ToString(), "发送", 7);
                    string itemlist= InterfaceHelper.HttpsPost(sb.ToString());
                    FileUtils.KLog(itemlist, "接收", 7);
                    Global.Gitem = 0;
                    if (itemlist.Length > 0)
                    {
                        ResultData Jitem = JsonHelper.JsonToEntity<ResultData>(itemlist);
                        if (Jitem != null && Jitem.success)
                        {
                            msg.responseInfo = Jitem.obj.ToString();
                        }

                        //detectitem obj = JsonHelper.JsonToEntity<detectitem>(Jitem.obj.ToString());
                        //if (obj.detectItem.Count > 0)
                        //{
                        //    CheckItem CI = new CheckItem();
                        //    for (int i = 0; i < obj.detectItem.Count; i++)
                        //    {
                        //        int rt = 0;
                        //        sb.Length =0;
                        //        sb.AppendFormat("cid='{0}' ", obj.detectItem[i].id);
                               
                        //        dt= _bll.GetDetectItem(sb.ToString(),"",out err);

                        //        CI = obj.detectItem[i];
                        //        if (dt != null && dt.Rows.Count > 0)
                        //        {
                        //            rt = _bll.UpdateDetectItem(CI);
                        //            if (rt == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            rt = _bll.InsertDetectItem(CI);
                        //            if (rt == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //    }    
                        //}
                    }
                   
                    if (null != this.target)
                    {
                        msg.result = true;
                       
                        target.SendMessage(msg, null);
                    }
                    break;
                //下载仪器检测项目
                case MsgCode.MSG_DownMachineItem:
                    msg.result = true ;
                    msg.responseInfo = "";
                    string url3 = msg.str1;
                    lasttime = "";
                    string CItemAddr= InterfaceHelper.GetServiceURL(url3, 5);//地址
                    dt = _bll.GetRequestTime("RequestName='MachineItem'", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lasttime = dt.Rows[0]["UpdateTime"].ToString();
                        _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='MachineItem'", "", 1, out err);
                    }
                    else 
                    {
                        _bll.InsertResquestTime("'MachineItem','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                    }
                    sb.Length = 0;
                    Global.Gitem = 0;
                    sb.Append(CItemAddr);
                    sb.AppendFormat("?userToken={0}", Global.Token);
                    sb.AppendFormat("&type={0}", "deviceItem");
                    sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                    sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);//lastDateTime 全部数据 lastUpdateTime
                    FileUtils.KLog(sb.ToString(), "发送", 10);
                    string Citemlist= InterfaceHelper.HttpsPost(sb.ToString());
                    FileUtils.KLog(Citemlist, "接收", 10);
                    if (Citemlist.Length > 0)
                    {
                        ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(Citemlist);
                        if(Jresult!=null && Jresult.success )
                        {
                            msg.responseInfo = Jresult.obj.ToString();
                        }
                        //DeviceCheckItem obj = JsonHelper.JsonToEntity<DeviceCheckItem>(Jresult.obj.ToString());
                        //if (obj.deviceItem.Count > 0)
                        //{
                        //    deviceItem DI = new deviceItem();
                        //    for(int i=0;i<obj.deviceItem.Count;i++)
                        //    {
                        //        int rt = 0;
                        //        DataTable mdt = _bll.GetMachineSave("mid='" + obj.deviceItem[i].id+"'", "", out err);
                        //        if (mdt != null && mdt.Rows.Count > 0)//记录存在就更新
                        //        {
                        //            DI = obj.deviceItem[i];

                        //            rt = _bll.UpdateMachineItem(DI,out err);
                        //            if (rt == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            DI = obj.deviceItem[i];

                        //            rt = _bll.IsertMachineItem(DI);
                        //            if (rt == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                  
                    if (null != this.target)
                    {
                        msg.result = true;
                        target.SendMessage(msg, null);
                    }
                    break;
                case MsgCode.MSG_SAMPLETYPE://食品种类下载
                    msg.result = true ;
                    msg.responseInfo = "";
                    string saddr= InterfaceHelper.GetServiceURL(msg.str1,8);//地址
                    lasttime = "";
                    dt = _bll.GetRequestTime("RequestName='foodTypes'", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lasttime = dt.Rows[0]["UpdateTime"].ToString();
                        _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='foodTypes'", "", 1, out err);
                    }
                    else
                    {
                        _bll.InsertResquestTime("'foodTypes','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                    }
                    sb.Length = 0;
                    sb.Append(saddr);
                    sb.AppendFormat("?userToken={0}", Global.Token);
                    sb.AppendFormat("&type={0}", "food");
                    sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                    sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                    FileUtils.KLog(sb.ToString(), "发送", 8);
                    string stype= InterfaceHelper.HttpsPost(sb.ToString());
                    FileUtils.KLog(stype, "接收", 8);
                    if (stype.Length > 0)
                    {
                        ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stype);
                        if(Jresult !=null && Jresult.success )
                        {
                            msg.responseInfo = Jresult.obj.ToString();
                        }
                        //sampletype obj = JsonHelper.JsonToEntity<sampletype>(Jresult.obj.ToString());
                        //if (obj.food.Count > 0)
                        //{
                        //    foodtype ft = new foodtype();
                        //    for (int i = 0; i < obj.food.Count;i++ )
                        //    {
                        //        int rt = 0;
                        //        sb.Length = 0;
                        //        sb.AppendFormat("fid='{0}' ", obj.food[i].id);
                        //        //sb.AppendFormat("food_name='{0}' and ", obj.food[i].food_name);
                        //        //sb.AppendFormat("parent_id='{0}' and ", obj.food[i].parent_id);
                        //        //sb.AppendFormat("sorting='{0}' and ", obj.food[i].sorting);
                        //        //sb.AppendFormat("update_date='{0}'", obj.food[i].update_date);
                        //        dt= _bll.Getfoodtype(sb.ToString(),"",out err);
                        //        ft = obj.food[i];
                        //        if (dt != null && dt.Rows.Count > 0)
                        //        {
                        //            rt = _bll.Updatefoodtype(ft);
                        //            if (rt == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            rt = _bll.Insertfoodtype(ft);
                        //            if (rt == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //    }   
                        //}
                    }

                    if (null != this.target)
                    {
                        msg.result  = true;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_STANDARD://国家标准下载
                    msg.result = true ;
                    msg.responseInfo = "";
                    lasttime = "";
                    dt = _bll.GetRequestTime("RequestName='Standard'", "", out err);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lasttime = dt.Rows[0]["UpdateTime"].ToString();
                        _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='Standard'", "", 1, out err);
                    }
                    else
                    {
                        _bll.InsertResquestTime("'Standard','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                    }
                    string stdaddr= InterfaceHelper.GetServiceURL(msg.str1,8);//地址
                    sb.Length = 0;
                    sb.Append(stdaddr);
                    sb.AppendFormat("?userToken={0}", Global.Token);
                    sb.AppendFormat("&type={0}", "standard");
                    sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                    sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                    FileUtils.KLog(sb.ToString(), "发送", 6);
                    string stdtype= InterfaceHelper.HttpsPost(sb.ToString());
                    FileUtils.KLog(stdtype, "接收", 6);
                    if (stdtype.Length > 0)
                    {
                        ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stdtype);
                        if (Jresult != null && Jresult.success)
                        {
                            msg.responseInfo = Jresult.obj.ToString();
                        }
                        //standardlist obj = JsonHelper.JsonToEntity<standardlist>(Jresult.obj.ToString());
                        //if (obj.standard.Count > 0)
                        //{
                        //    standard sd = new standard();
                        //    for (int i = 0; i < obj.standard.Count; i++)
                        //    {
                        //        int rt = 0;
                        //        sb.Length = 0;
                        //        sb.AppendFormat("sid='{0}'", obj.standard[i].id);
                            
                        //        dt= _bll.GetStandard(sb.ToString(),"",out err);
                        //        sd = obj.standard[i];

                        //        if (dt != null && dt.Rows.Count > 0)
                        //        {
                        //            rt = _bll.UpdateStandard(sd);
                        //            if (rt == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            rt = _bll.InsertStandard(sd);
                        //            if (rt == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                  
                    if (null != this.target)
                    {
                        msg.result = true;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_DownTask:  //仪器检测任务下载
                    try
                    {
                        msg.result = false;
                        msg.responseInfo = "";
                        int f = 0;

                        string Dpassword = Global.MD5(Global.MD5(msg.str3));
                        string tasktime = string.Empty;
                        DataTable dtt = _bll.GetRequestTime("RequestName='MachineTask'", "", out err);// _Tskbll.getlastoneItem("", "");
                        if (dtt != null && dtt.Rows.Count > 0)
                        {
                            tasktime = dtt.Rows[0]["UpdateTime"].ToString();//获取上一次获取任务的时间
                            //数据库存在就更新请求时间
                            _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='MachineTask'", "", 1, out err);
                            CFGUtils.SaveConfig("tasknumtime", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            //没有数据就重新在数据库插入请求时间
                            _bll.InsertResquestTime("'MachineTask','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                            CFGUtils.SaveConfig("tasknumtime", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        int index = msg.str1.LastIndexOf('/');
                        msg.str1 = (index == msg.str1.Length - 1) ? msg.str1 : msg.str1 + "/";

                        string YQtask = msg.str1 + "iSampling/downloadSampling.do";
                        sb.Length = 0;
                        sb.Append(YQtask);
                        sb.AppendFormat("?userToken={0}", Global.Token);
                        sb.AppendFormat("&type={0}", "recevieSampling");
                        sb.AppendFormat("&serialNumber={0}", Global.MachineNum);//DY-3500_20171028101105
                        sb.AppendFormat("&lastUpdateTime={0}", tasktime == "" ? "2000-01-01 00:00:01" : tasktime);//System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        sb.AppendFormat("&param1={0}", "");
                        sb.AppendFormat("&param2={0}", "");
                        sb.AppendFormat("&param3={0}", "");
                        FileUtils.KLog(sb.ToString(), "发送", 4);
                        string tasklist = InterfaceHelper.HttpsPost(sb.ToString());
                        FileUtils.KLog(tasklist, "接收", 4);
                        ResultData Mresult = JsonHelper.JsonToEntity<ResultData>(tasklist);

                        Mtask mt = JsonHelper.JsonToEntity<Mtask>(Mresult.obj.ToString());
                        MMtask mk = new MMtask();

                        for (int i = 0; i < mt.result.Count; i++)
                        {
                            sb.Length = 0;
                            StringBuilder sbt = new StringBuilder();
                            sbt.AppendFormat("tid='{0}'", mt.result[i].id);
                            sbt.AppendFormat(" and s_sampling_date='{0}'", mt.result[i].s_sampling_date);

                            DataTable Istaksave = _Tskbll.GetQtask(sbt.ToString(), "", 1);

                            if (Istaksave != null && Istaksave.Rows.Count > 0)
                            {
                                mk = mt.result[i];
                                DataTable mdt = _bll.GetModel(mt.result[i].item_name, "", out err);
                                if (mdt != null && mdt.Rows.Count > 0)
                                {
                                    mk.mokuai = mdt.Rows[0]["project_type"].ToString();
                                }
                                //string tmodel = searchmodel(mt.result[i].item_name);
                               
                                //mk.mokuai = tmodel;
                                mk.username = msg.str2;
                                _bll.UpdteKTask(mk);
                                f = f + 1;
                            }
                            else
                            {
                                mk = mt.result[i];
                                //查询检测项目属于哪个模块
                                DataTable mdt = _bll.GetModel(mt.result[i].item_name, "", out err);
                                if (mdt != null && mdt.Rows.Count > 0)
                                {
                                    mk.mokuai = mdt.Rows[0]["project_type"].ToString();
                                }
                                //string tmodel = searchmodel(mt.result[i].item_name);
                              
                                //mk.mokuai = tmodel;
                                mk.username = msg.str2;
                                _bll.InsertKTask(mk);
                                f = f + 1;
                            }
                            //更新任务状态   边保存边更新  接收
                            string RcAddr = msg.str1 + "iSampling/updateStatus.do";
                            sb.Length = 0;
                            sb.Append(RcAddr);
                            sb.AppendFormat("?userToken={0}", Global.Token);
                            sb.AppendFormat("&sdId={0}", mt.result[i].id);
                            sb.AppendFormat("&recevieSerialNumber={0}", Global.MachineNum);
                            sb.AppendFormat("&recevieStatus={0}", "1");
                            sb.AppendFormat("&param1={0}", "");
                            sb.AppendFormat("&param2={0}", "");
                            sb.AppendFormat("&param3={0}", "");
                            FileUtils.KLog(sb.ToString(), "发送", 16);
                            string upd = InterfaceHelper.HttpsPost(sb.ToString());
                            FileUtils.KLog(sb.ToString(), "接收", 16);
                            ResultData ut = JsonHelper.JsonToEntity<ResultData>(upd);
                        }
                        msg.responseInfo = f.ToString();
                        msg.result = true;

                        if (null != this.target)
                        {
                            msg.DownLoadTask = _CheckItemStandard;
                            target.SendMessage(msg, null);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"任务下载");
                    }

                    break;

                case MsgCode.MSG_COMM_TEST_HM:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        if (TestHM(_port))
                            msg.result = true;
                        _port.Close();
                    }
                    if (null != this.target)
                        target.SendMessage(msg, null);
                    break;
                //仪器注册
                case MsgCode.MSG_REGISTE_MACHINE:
                    msg.result = false;
                    try
                    {
                        string Rpassword = Global.MD5(Global.MD5(msg.str3));
                        string RAddr = InterfaceHelper.GetServiceURL(msg.str1, 7);//地址
                        sb.Length = 0;
                        sb.Append(RAddr);
                        sb.AppendFormat("?userToken={0}", Global.Token);
                        sb.AppendFormat("&series={0}", Global.MachineModel);//DY-3500(I)
                        sb.AppendFormat("&mac={0}", Global.GetMACComputer());
                        sb.AppendFormat("&param1={0}", "");
                        sb.AppendFormat("&param2={0}", "");
                        sb.AppendFormat("&param3={0}", "");

                        string Rlist = InterfaceHelper.HttpsPost(sb.ToString());

                        ResultData Zresult = JsonHelper.JsonToEntity<ResultData>(Rlist);
                        if (Zresult.success==true )
                        {
                            zhuce zdata = JsonHelper.JsonToEntity<zhuce>(Zresult.obj.ToString());
                            msg.responseInfo = "注册成功";
                            Global.MachineNum = zdata.serial_number;
                            msg.result = true;
                        }
                        else
                        {
                            msg.responseInfo = Zresult.msg;
                            msg.result = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.responseInfo = ex.Message;
                        msg.result = true;
                    }
                    if (null != this.target)
                    {
                        msg.result = true;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_RECEIVETASK://任务接受
                    msg.result = false;
                    msg.responseInfo = "";
                    try
                    {
                         //string Rcpassword = Global.MD5(Global.MD5(msg.str3));
                        string[] tt = Global.samplingnumRecive.Split(',');
                        if (tt != null && tt.GetLength(0) > 0)
                        {
                            for (int j = 0; j < tt.GetLength(0); j++)
                            {
                                int ind = msg.str1.LastIndexOf('/');
                                msg.str1 = (ind == msg.str1.Length - 1) ? msg.str1 : msg.str1 + "/";
                                string RcTAddr = msg.str1 + "iSampling/updateStatus.do";
                                sb.Length = 0;
                                sb.Append(RcTAddr);
                                sb.AppendFormat("?userToken={0}", Global.Token);
                                sb.AppendFormat("&sdId={0}", tt[j]);
                                sb.AppendFormat("&recevieSerialNumber={0}", Global.MachineNum);
                                sb.AppendFormat("&recevieStatus={0}", "1");
                                sb.AppendFormat("&param1={0}", "");
                                sb.AppendFormat("&param2={0}", "");
                                sb.AppendFormat("&param3={0}", "");
                                FileUtils.KLog(sb.ToString(), "发送", 16);
                                string Rclist = InterfaceHelper.HttpsPost(sb.ToString());

                                ResultData Rrclist = JsonHelper.JsonToEntity<ResultData>(Rclist);
                                FileUtils.KLog(sb.ToString(), "接收", 16);
                                if (Rrclist.msg == "操作成功")
                                {
                                    _bll.ReceiveTaskTest(tt[j], 1, out err);
                                    msg.result = true;
                                }
                                else
                                {
                                    msg.responseInfo = Rrclist.msg;
                                }
                            }
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        msg.responseInfo = ex.Message;
                    }

                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }
                    
                    break;
                case MsgCode.MSG_OBJECTASK ://任务拒收
                    msg.result = false;
                    msg.responseInfo = "";
                    int dex = msg.str1 .LastIndexOf('/');
                    msg.str1 = (dex == msg.str1.Length - 1) ? msg.str1 : msg.str1 + "/";
                    string js = msg.str1 + "iSampling/updateStatus.do";
                    if (Global.ObjectSampling.Length > 0)
                    {
                        string[] rr = Global.ObjectSampling.Split(',');
                        if (rr != null && rr.GetLength(0) > 0)
                        {
                            for (int j = 0; j < rr.GetLength(0); j++)
                            {
                                sb.Length = 0;
                                sb.Append(js);
                                sb.AppendFormat("?userToken={0}", Global.Token);
                                sb.AppendFormat("&sdId={0}", rr[j]);
                                sb.AppendFormat("&recevieSerialNumber={0}", Global.MachineNum);
                                sb.AppendFormat("&recevieStatus={0}", "2");
                                sb.AppendFormat("&param1={0}", "");
                                sb.AppendFormat("&param2={0}", "");
                                sb.AppendFormat("&param3={0}", "");
                                FileUtils.KLog(sb.ToString(), "发送", 15);

                                string Roclist = InterfaceHelper.HttpsPost(sb.ToString());
                                ResultData Rroclist = JsonHelper.JsonToEntity<ResultData>(Roclist);
                                FileUtils.KLog(Roclist, "接收", 15);

                                if (Rroclist.msg == "操作成功")
                                {
                                    _bll.ReceiveTaskTest(rr[j], 2, out err);
                                    msg.result = true;
                                }
                                else
                                {
                                    msg.responseInfo = msg.responseInfo + Rroclist.msg;
                                }
                            }
                        }
                    }
                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }

                    break;
                case MsgCode.MSG_DownLaw://法律法规下载
                    msg.result = true ;
                    msg.responseInfo = "";
                    string lw =InterfaceHelper.GetServiceURL(msg.str1,8);//地址 msg.str1 + "iSampling/updateStatus.do";
                    Global.Gitem = 0;

                    dt = _bll.GetRequestTime("RequestName='laws'", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lasttime = dt.Rows[0]["UpdateTime"].ToString();//获取上一次获取时间
                        _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='laws'", "", 2, out err);
                    }
                    else
                    {
                        _bll.InsertResquestTime("'laws','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                    }

                    sb.Length = 0;
                    sb.Append(lw);
                    sb.AppendFormat("?userToken={0}", Global.Token);
                    sb.AppendFormat("&type={0}", "laws");
                    sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                    sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                    sb.AppendFormat("&pageNumber={0}", "");
                    FileUtils.KLog(sb.ToString(),"发送",5);
                    string law = InterfaceHelper.HttpsPost(sb.ToString());
                    FileUtils.KLog(law, "接收", 5);
                    ResultData lawlist = JsonHelper.JsonToEntity<ResultData>(law);

                    if (lawlist!=null  &&　lawlist.success==true)
                    {
                        msg.responseInfo = lawlist.obj.ToString();
                        //Lawlist zdata = JsonHelper.JsonToEntity<Lawlist>(lawlist.obj.ToString());
                        //if (zdata.laws.Count > 0)
                        //{
                        //    law lws = new law();
                        //    for (int i = 0; i < zdata.laws.Count; i++)
                        //    {
                        //        int w = 0;
                        //        sb.Length = 0;
                        //        sb.AppendFormat("wid='{0}' ", zdata.laws[i].id);
                           
                        //        dt = _bll.GetLaws(sb.ToString(), "", out err);

                        //        lws = zdata.laws[i];

                        //        if (dt != null && dt.Rows.Count > 0)
                        //        {
                        //            w = _bll.UpdateLaws(lws);
                        //            if (w == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            w = _bll.InsertLaws(lws);
                        //            if (w == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    
                    if (null != this.target)
                    {
                        msg.result = true;
                        target.SendMessage(msg, null);
                    }
                    break;

                case MsgCode.MSG_SampleStand://样品/检测项目/标准关联foodItem
                    msg.result = false;
                    msg.responseInfo = "";
                    string SD =InterfaceHelper.GetServiceURL(msg.str1,8);//地址 msg.str1 + "iSampling/updateStatus.do";
                    Global.Gitem = 0;

                    dt = _bll.GetRequestTime("RequestName='SampleItemstd'", "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lasttime = dt.Rows[0]["UpdateTime"].ToString();//获取上一次获取时间
                        _bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='SampleItemstd'", "", 3, out err);
                    }
                    else
                    {
                        _bll.InsertResquestTime("'SampleItemstd','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 3, out err);
                    }
                    sb.Length = 0;
                    sb.Append(SD);
                    sb.AppendFormat("?userToken={0}", Global.Token);
                    sb.AppendFormat("&type={0}", "foodItem");
                    sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                    sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                    sb.AppendFormat("&pageNumber={0}", "");

                    FileUtils.KLog(sb.ToString(), "发送", 9);
                    string std = InterfaceHelper.HttpsPost(sb.ToString());
                    FileUtils.KLog(std, "接收", 9);

                    ResultData sslist = JsonHelper.JsonToEntity<ResultData>(std);
                    if (sslist!=null  && sslist.success == true)
                    {
                        msg.responseInfo = sslist.obj.ToString();

                        //fooditemlist flist = JsonHelper.JsonToEntity<fooditemlist>(sslist.obj.ToString());
                        //if (flist.foodItem.Count > 0)
                        //{
                        //    fooditem fi = new fooditem();
                        //    for (int i = 0; i < flist.foodItem.Count; i++)
                        //    {
                        //        int w = 0;
                        //        sb.Length = 0;
                        //        sb.AppendFormat("sid='{0}' ", flist.foodItem[i].id);
                        //        //sb.AppendFormat("food_id='{0}' and ", flist.foodItem[i].food_id);
                        //        //sb.AppendFormat("item_id='{0}' and ", flist.foodItem[i].item_id);
                        //        //sb.AppendFormat("detect_value='{0}' and ", flist.foodItem[i].detect_value);
                        //        //sb.AppendFormat("update_date='{0}'", flist.foodItem[i].update_date);
                        //        dt= _bll.GetSampleStand(sb.ToString(),"",out err);
                        //        fi = flist.foodItem[i];
                        //        if (dt != null && dt.Rows.Count > 0)
                        //        {
                        //            w = _bll.UpdateSampleStandard(fi);
                        //            if (w == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            w = _bll.InsertSampleStandard(fi);
                        //            if (w == 1)
                        //            {
                        //                Global.Gitem = Global.Gitem + 1;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                   
                    if (null != this.target)
                    {
                        msg.result = true;
                        target.SendMessage(msg, null);
                    }
                    break;

                // 重金属检测
                case MsgCode.MSG_DETECTE_START_HEAVYMETAL:
                    msg.result = false;
                    if (_port.Open(msg.str1))
                    {
                        HMProto hmProto = new HMProto(_port);
                        byte[] hand = ASCIIEncoding.ASCII.GetBytes("DY6300-2--201503");
                        if (hmProto.ProtocolPackage(0x1483, 0x010008, hand, 4000))
                        {
                            // 停止测量
                            if (hmProto.ProtocolPackage(0x0683, 0x01f801, new byte[] { 0x00, 0x0f }, 4000))
                            {
                                // 启动测量
                                if (hmProto.ProtocolPackage(msg.arg1, msg.arg2, msg.data, 4000))
                                {
                                    byte[] buffer = new byte[22];
                                    while (hmProto.ReceveProtocolPackage(15000)) // 10s没有数据认为已经结束测量了。
                                    {
                                        Array.Copy(hmProto.buffer, 0, buffer, 0, 22);
                                        Message msg2 = new Message();
                                        msg2.what = MsgCode.MSG_READ_HEAVYMETAL;
                                        //New Add byte
                                        msg2.data = new byte[22];
                                        buffer.CopyTo(msg2.data, 0);
                                        msg2.result = true;
                                        if (null != this.target)
                                        {
                                            target.SendMessage(msg, null);
                                        }
                                    }

                                    msg.result = true;
                                }
                            }
                        }
                        _port.Close();
                    }
                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }
                    break;

                #region 获取版本号
                case MsgCode.MSG_GET_VERSION:
                    string[] com = { Global.strSXT1PORT, Global.strSXT2PORT, Global.strSXT3PORT, Global.strSXT4PORT };
                    for (int i = 0; i < com.Length;i++ )
                    {
                        if (_port.Open(com[i]))
                        {
                            JtjGetVersion(_port, "通道"+(i+1));
                            _port.Close();
                        }
                    }

                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }
       
                    break;
                #endregion
                #region 系统软件版本更新
                case MsgCode.MSG_SoftwareVersion:
                    msg.result = false;
                    if (msg.str1 != "" && msg.str1.Length > 0)
                    {
                        string localXmlFile = msg.str1;
                        string serverXmlFile = string.Empty;
                        //从本地读取更新配置文件信息
                        XmlFiles updaterXmlFiles = new XmlFiles(localXmlFile);
                        //获取服务器地址
                        string updateUrl = updaterXmlFiles.GetNodeValue("//Url");
                        AppUpdater appUpdater = new AppUpdater();
                        appUpdater.UpdaterUrl = updateUrl + "/UpdateList.xml";
                        string tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
                        if (appUpdater.DownAutoUpdateFile(tempUpdatePath))
                        {
                            //获取更新文件列表
                            Hashtable htUpdateFile = new Hashtable();
                            serverXmlFile = tempUpdatePath + "\\UpdateList.xml";
                            if (!File.Exists(serverXmlFile))
                            {
                                msg.result = false;
                            }
                            int availableUpdate = appUpdater.CheckForUpdate(serverXmlFile, localXmlFile, out htUpdateFile);
                            if (availableUpdate > 0)
                            {
                                msg.result = true;
                            }
                            else
                            {
                                msg.result = false;
                            }
                        }
                    }
                    msg.str1 = "";
                    if (null != this.target)
                    {
                        target.SendMessage(msg, null);
                    }
                    break;
                #endregion
                default:
                    msg.result = false;
                    break;
            }
        }
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private void JtjGetVersion(ComPort comPort, string holenum)
        {
            if (Global.JtjVersionInfo == null || Global.JtjVerTwo == null || Global.JtjVerThree == null || Global.JtjVerFour == null)
            {
                try
                {
                    //ComPort.isver = true;
                    int _timeout = 500;
                    int  _length = 9;
                    byte[] receiveData = new byte[_length];
                    byte[] sendBuffer = null;
                    //获取硬件版本信息协议使用旧模块的和校验，新模块不做校验
                    //_agreement.cmd = 0x01;
                    //_agreement.len[0] = 0x00;
                    //sendBuffer = _agreement.getAgreement();
                    sendBuffer = CMD.GetBuffer(0x01);//胶体金3.0版本协议获取
                    comPort.Clear();
                    comPort.Write(sendBuffer, 0, sendBuffer.Length);

                    //采用摄像头数组复制读取对1.0和2.0有影响，有些没改过来

                    comPort.Read(receiveData, 0, _length, _timeout);
                    //头和命令编号相符时开始记录crc
                    if (receiveData[0] == 0x7e && receiveData[1] == 0x02 && receiveData[8] == 0x7e)
                    {
                        Console.WriteLine("获取胶体金版本成功！");
                        //兼容旧模块/*receiveData[7] == (receiveData[1] + receiveData[2] + receiveData[3] + receiveData[4] + receiveData[5] + receiveData[6]) ||
                        if (CMD.Validation(receiveData))//新摄像头校验
                        {
                            if (holenum == "通道1")
                            {
                                Global.JtjVersionInfo = new byte[3];
                                Global.JtjVersionInfo[0] = receiveData[4];
                                Global.JtjVersionInfo[1] = receiveData[5];
                                Global.JtjVersionInfo[2] = receiveData[6];
                                Global.JtjVersion = receiveData[5] == 0x30 ? 3 : (receiveData[5] == 0x20 ? 2 : 1);
                                CFGUtils.SaveConfig("chanelOne", Global.JtjVersionInfo[1].ToString("X") + "/" + Global.JtjVersionInfo[2].ToString("X"));
                            }
                            else if (holenum == "通道2")
                            {
                                Global.JtjVerTwo = new byte[3];
                                Global.JtjVerTwo[0] = receiveData[4];
                                Global.JtjVerTwo[1] = receiveData[5];
                                Global.JtjVerTwo[2] = receiveData[6];
                                Global.JtjVersion = Global.JtjVersionTwo = receiveData[5] == 0x30 ? 3 : (receiveData[5] == 0x20 ? 2 : 1);
                                CFGUtils.SaveConfig("chanelTwo", Global.JtjVerTwo[1].ToString("X") + "/" + Global.JtjVerTwo[2].ToString("X"));
                            }
                            else if (holenum == "通道3")
                            {
                                Global.JtjVerThree = new byte[3];
                                Global.JtjVerThree[0] = receiveData[4];
                                Global.JtjVerThree[1] = receiveData[5];
                                Global.JtjVerThree[2] = receiveData[6];
                                Global.JtjVersion = Global.JtjVersionThree = receiveData[5] == 0x30 ? 3 : (receiveData[5] == 0x20 ? 2 : 1);
                                CFGUtils.SaveConfig("chanelThree", Global.JtjVerThree[1].ToString("X") + "/" + Global.JtjVerThree[2].ToString("X"));
                            }
                            else if (holenum == "通道4")
                            {
                                Global.JtjVerFour = new byte[3];
                                Global.JtjVerFour[0] = receiveData[4];
                                Global.JtjVerFour[1] = receiveData[5];
                                Global.JtjVerFour[2] = receiveData[6];
                                Global.JtjVersion = Global.JtjVersionFour = receiveData[5] == 0x30 ? 3 : (receiveData[5] == 0x20 ? 2 : 1);
                                CFGUtils.SaveConfig("chanelfour", Global.JtjVerFour[1].ToString("X") + "/" + Global.JtjVerFour[2].ToString("X"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    CFGUtils.SaveConfig("JtjVersion", Global.JtjVersion.ToString());
                }
            }
        }
        /// <summary>
        /// 图像采集
        /// </summary>
        /// <returns></returns>
        private byte[] ReadImg(ComPort port)
        {
            try
            {

                port.Clear();
                byte[] cmd = { 0x1b, 0x1c, 0x00 };
                port.Write(cmd, 0, cmd.Length);

                Thread.Sleep(200);

                byte[] data = port.ReadData();
                if (data != null && data.Length > 5)
                {
                    //判断数据是否正确
                    long WIDTH = (UInt32)data[1] + (UInt32)(data[2] << 8);
                    long HEIGHT = (UInt32)data[3] + (UInt32)(data[4] << 8);
                    int DATA_LEN = (int)(WIDTH * HEIGHT * 2) + 5;
                    Console.WriteLine("数据长度：" + data.Length);
                    if (DATA_LEN == data.Length)
                    {
                        return data;
                    }
                    else
                    {
                        Console.WriteLine("图像采集错误");
                    }
                }
                else
                {
                    Console.WriteLine("图像采集错误");
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 查询检测模块
        /// </summary>
        /// <param name="Ditem"></param>
        /// <returns></returns>
        private string searchmodel(string Ditem)
        {
            string model = string.Empty;
            //先从分光光度模块中对比检测项目，如果没有继续和胶体金和干化学模块对比
            for (int i = 0; i < Global.fgdItems.Count; i++)
            {
                //对比项目名称，一致的话就进行检测
                DYFGDItemPara FGDitem = Global.fgdItems[i];
                if (Ditem.Equals(FGDitem.Name))
                {
                    model = "分光光度";
                    return model;
                }
            }

            //和胶体金模块的检测项目对比
            for (int i = 0; i < Global.jtjItems.Count; i++)
            {
                DYJTJItemPara JTJitem = Global.jtjItems[i];
                if (Ditem.Equals(JTJitem.Name))
                {
                    model = "胶体金";
                    return model;
                }
            }

            //和干化学模块的检测项目对比
            for (int i = 0; i < Global.gszItems.Count; i++)
            {
                DYGSZItemPara GSZitem = Global.gszItems[i];
                if (Ditem.Equals(GSZitem.Name))
                {
                    model = "干化学";
                    return model;
                }
            }
            return model;
        }

        private bool Test(ComPort comPort, int retry)
        {
            for (int i = 0; i <= retry; ++i)
            {
                if (Test(comPort))
                    return true;
                else
                    continue;
            }
            return false;
        }

        private bool Test(ComPort comPort)
        {
            byte[] cmd = { 0x03, 0xFF, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0xD4, 0x18, 0x44, 0x45, 0x56, 0x49, 0x43, 0x45, 0x3F, 0x3F };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 2000;
            // 接收2个字节的头
            int rec_len = 12;
            byte[] rec = new byte[rec_len];
            if (rec_len == comPort.Read(rec, 0, rec_len, timeout))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TestHM(ComPort comPort)
        {
            byte[] cmd = { 0xAA, 0xBB, 0x14, 0x83, 0x01, 0x00, 0x08 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            byte[] buffer = new byte[256];
            byte[] chs = new byte[1];
            byte ch;
            int KcCmd, KcPara;
            int count = 0;
            int MaxSize = 256;
            int timeout = 5000;
            DateTime beginTime = DateTime.Now;
            while (true)
            {
                if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > timeout)
                    break;

                if (comPort.Read(chs, 0, 1, 100) > 0)
                    ch = chs[0];
                else
                {
                    continue;
                }
                if (count == 0)
                {
                    if (ch == 0xAA)
                    {
                        buffer[count++] = 0xAA;
                    }
                }
                else
                {
                    if (count >= MaxSize)
                    {
                        count = 0;
                    }
                    else
                    {
                        buffer[count++] = ch;
                    }
                    if (buffer[1] != 0xBB)
                        count = 0;
                    if (count >= 7)
                    {
                        KcCmd = (buffer[3] & 0xFF) | ((buffer[2] & 0xff) << 8);
                        KcPara = (buffer[6] & 0xFF) | ((buffer[5] & 0xFF) << 8)
                                | ((buffer[4] & 0xFF) << 16);
                        if ((KcCmd == 0x0483) && (KcPara == 0x010008))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
        private class CMD
        {
            /// <summary>
            /// 获取通讯协议数据包(如果长度为0，直接传入CMD即可)
            /// </summary>
            /// <param name="cmd">CMD</param>
            /// <param name="len">DATA长度</param>
            /// <param name="data">数据包</param>
            /// <returns>返回完整数据包</returns>
            public static byte[] GetBuffer(byte cmd, byte[] data = null)
            {
                try
                {
                    bool isCrc8 = Global.JtjVersion == 3;
                    List<byte> dataList = new List<byte>();
                    dataList.Add(0x7E);//标识头
                    dataList.Add(cmd);//CMD命令
                    dataList.Add(0x00);//len
                    dataList.Add(0x00);//len
                    //将数据包追加到List后面
                    if (data != null && data.Length > 0)
                    {
                        if (isCrc8)//胶体金3.0是低位在前
                        {
                            dataList[2] = (byte)(data.Length & 0xFF);
                            dataList[3] = (byte)((data.Length >> 8) & 0xFF);
                        }
                        else//1.0和2.0是高位在前
                        {
                            dataList[3] = (byte)(data.Length & 0xFF);
                            dataList[2] = (byte)((data.Length >> 8) & 0xFF);
                        }
                        dataList.AddRange(data);
                    }
                    //CRC8数据包长度为当前List长度-1（去掉标识头）
                    byte[] crc = new byte[dataList.Count - 1];
                    //拷贝到CRC8数据包
                    dataList.CopyTo(1, crc, 0, crc.Length);
                    dataList.Add(isCrc8 ? CRC8(crc) : CRCSUM(crc));//CRC8校验值
                    dataList.Add(0x7E);//标识尾
                    byte[] buffer = new byte[dataList.Count];
                    dataList.CopyTo(buffer);
                    return buffer;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

            /// <summary>
            /// 数据校验（标识头标识尾，和CRC8校验）
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static bool Validation(byte[] data)
            {
                if (data == null || data.Length == 0) return false;
                //标识头标识尾校验
                if (data[0] != 0x7E || data[data.Length - 1] != 0x7E)
                {
                    return false;
                }
                byte cmd = data[1];
                switch (cmd)
                {
                    case 0x18://通讯测试响应
                    case 0x23://固件下载响应
                    case 0x25://固件升级响应
                        //0x00失败，0x01成功，另外通讯测试还有个0x02是需要提示进行固件升级的
                        return data[4] == 0x01 ? true : false;
                    default:
                        break;
                }

                //CRC8数组的长度为总长度-3（去掉标识头尾和CRC8）
                byte[] crcDt = new byte[data.Length - 3];
                Array.Copy(data, 1, crcDt, 0, crcDt.Length);
                byte crc8 = CMD.CRC8(crcDt);
                byte crcsum = CMD.CRCSUM(crcDt);
                //兼容CRC8与和校验
                if (data[data.Length - 2] == crc8 || data[data.Length - 2] == crcsum)
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// CRC8校验
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static byte CRC8(byte[] data)
            {
                byte crc = 0;
                for (int j = 0; j < data.Length; j++)
                {
                    crc = (byte)(crc ^ data[j]);
                    for (int i = 8; i > 0; i--)
                    {
                        if ((crc & 0x80) == 0x80)
                        {
                            crc = (byte)((crc << 1) ^ 0x31);
                        }
                        else
                        {
                            crc = (byte)(crc << 1);
                        }
                    }
                }

                return crc;
            }

            /// <summary>
            /// CRC和校验
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static byte CRCSUM(byte[] data)
            {
                byte crc = 0;
                if (data != null && data.Length > 0)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        crc += data[i];
                    }
                }

                return crc;
            }
        }
        public class HMProto
        {
            public int RecCmd;
            public int RecPara;
            public int MaxSize = 256;
            public byte[] buffer = new byte[256];
            public ComPort comPort;
            public HMProto(ComPort comPort)
            {
                this.comPort = comPort;
            }

            public bool ProtocolPackage(int cmd, int para, byte[] senddata, int timeout)
            {
                //int datalen = null == senddata ? 7 : 7 + senddata.Length;
                //byte[] data = new byte[datalen];
                byte[] data = new byte[64];
                data[0] = 0xaa;
                data[1] = 0xbb;
                data[2] = (byte)(cmd >> 8);
                data[3] = (byte)(cmd);
                data[4] = (byte)(para >> 16);
                data[5] = (byte)(para >> 8);
                data[6] = (byte)(para);
                if (null != senddata)
                {
                    senddata.CopyTo(data, 7);
                }
                comPort.Clear();
                comPort.Write(data, 0, data.Length);
                byte[] chs = new byte[1];
                byte ch;
                int count = 0;
                RecCmd = 0;
                RecPara = 0;
                DateTime beginTime = DateTime.Now;
                while (true)
                {
                    if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > timeout)
                        break;

                    if (comPort.Read(chs, 0, 1, 100) > 0)
                    {
                        ch = chs[0];
                    }
                    else
                    {
                        continue;
                    }
                    if (count == 0)
                    {
                        if (ch == 0xAA)
                        {
                            buffer[count++] = 0xAA;
                        }
                    }
                    else
                    {
                        if (count >= MaxSize)
                        {
                            count = 0;
                        }
                        else
                        {
                            buffer[count++] = ch;
                        }
                        if (buffer[1] != 0xBB)
                            count = 0;
                        if (count >= 56)
                        {
                            RecCmd = (buffer[3] & 0xFF) | ((buffer[2] & 0xff) << 8);
                            RecPara = (buffer[6] & 0xFF) | ((buffer[5] & 0xFF) << 8)
                                    | ((buffer[4] & 0xFF) << 16);
                            return true;
                        }
                    }
                }
                return false;
            }

            public bool ReceveProtocolPackage(int timeout)
            {
                byte[] chs = new byte[1];
                byte ch;
                int count = 0;
                RecCmd = 0;
                RecPara = 0;
                DateTime beginTime = DateTime.Now;
                while (true)
                {
                    if (DateTime.Now.Subtract(beginTime).TotalMilliseconds > timeout)
                        break;
                    if (comPort.Read(chs, 0, 1, 100) > 0)
                    {
                        ch = chs[0];
                    }
                    else
                    {
                        continue;
                    }

                    if (count == 0)
                    {
                        if (ch == 0xAA)
                        {
                            buffer[count++] = 0xAA;
                        }
                    }
                    else
                    {
                        if (count >= MaxSize)
                        {
                            count = 0;
                        }
                        else
                        {
                            buffer[count++] = ch;
                        }
                        if (buffer[1] != 0xBB)
                            count = 0;
                        if (count >= 56)
                        {
                            RecCmd = (buffer[3] & 0xFF) | ((buffer[2] & 0xff) << 8);
                            RecPara = (buffer[6] & 0xFF) | ((buffer[5] & 0xFF) << 8)
                                    | ((buffer[4] & 0xFF) << 16);
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 读取AD值
        /// AD返回的格式是这样的
        /// 返回0x1B LED_ROW（多少排）
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private byte[] ReadADValue(ComPort comPort)
        {
            // 发送0x1B 0x1B
            byte[] cmd = { 0x1b, 0x1b };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 4000;
            // 接收2个字节的头
            int HDR_LEN = 2;
            byte[] header = new byte[HDR_LEN];
            if (HDR_LEN == comPort.Read(header, 0, HDR_LEN, timeout))
            {
                if (0x1b != header[0])
                    return null;
                int LED_ROW = header[1];
                int LED_NUMS = 4;
                int LED_COL = 8;
                int DATA_LEN = LED_ROW * (1 + 4 * (LED_NUMS * LED_COL + 1) + 1);
                byte[] buffer = new byte[HDR_LEN + DATA_LEN];
                Array.Copy(header, buffer, HDR_LEN);
                if (DATA_LEN == comPort.Read(buffer, HDR_LEN, DATA_LEN, timeout))
                {
                    return buffer;
                }
            }
            return null;
        }

        private byte[] ReadCam(ComPort comPort, int camID)
        {
            //最开始的命令，根据ID来读取数据，后面厂家优化后全部改为0
            //byte[] cmd = { 0x1b, 0x1c, (byte)camID };
            byte[] cmd = { 0x1b, 0x1c, 0x00 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 200;

            int HDR_LEN = 5;
            byte[] header = new byte[HDR_LEN];
            if (HDR_LEN == comPort.Read(header, 0, HDR_LEN, timeout))
            {
                if (0x1b != header[0])
                    return null;
                try
                {
                    long WIDTH = (UInt32)header[1] + (UInt32)(header[2] << 8);
                    long HEIGHT = (UInt32)header[3] + (UInt32)(header[4] << 8);
                    long BITDEPTH = 2;
                    int DATA_LEN = (int)(WIDTH * HEIGHT * BITDEPTH);
                    byte[] buffer = new byte[HDR_LEN + DATA_LEN];
                    Array.Copy(header, buffer, HDR_LEN);
                    //3秒钟超时
                    if (DATA_LEN == comPort.Read(buffer, HDR_LEN, DATA_LEN, timeout * 15))
                    {
                        return buffer;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        private void WriteData(ComPort comPort, byte[] data, int offset, int count)
        {
            comPort.Clear();
            comPort.Write(data, offset, count);
        }

        private byte[] ReadGrayValue(ComPort comPort, int camID, int height)
        {
            //byte[] cmd = { 0x1b, 0x1d, (byte)camID };
            byte[] cmd = { 0x1b, 0x1d, 0x00 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 2000;
            int DATA_LEN = height;
            byte[] buffer = new byte[DATA_LEN];
            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, timeout))
            {
                return buffer;
            }
            return null;
        }

        /// <summary>
        /// 胶体金 - 获取C/T灰阶值
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="camID"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private byte[] ReadCTGrayValue(ComPort comPort, int camID, int height)
        {
            byte[] rdCvalueCmd = { 0x1b, 0x21, 0x00 };
            byte[] rdTvalueCmd = { 0x1b, 0x21, 0x01 };
            int timeout = 1000;
            int DATA_LEN = 4;
            byte[] buffer = new byte[DATA_LEN];
            byte[] rtnValue = new byte[DATA_LEN * 2];

            comPort.Clear();
            comPort.Write(rdCvalueCmd, 0, rdCvalueCmd.Length);

            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, timeout))
            {
                Array.Copy(buffer, rtnValue, 4);
            }
            else
            {
                return null;
            }

            comPort.Write(rdTvalueCmd, 0, rdTvalueCmd.Length);
            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, timeout))
            {
                Array.ConstrainedCopy(buffer, 0, rtnValue, 4, 4);
            }
            else
            {
                return null;
            }

            return rtnValue;
        }

        private bool bWhileReadADCycle = false;
        public void BeginStartReadADCycle(Message m, ChildThread target)
        {
            bWhileReadADCycle = true; ;
            this.SendMessage(m, target);
        }

        public void BeginStopReadADCycle()
        {
            bWhileReadADCycle = false;
        }

        private byte[] ReadRGBValue(ComPort comPort, int camID)
        {
            //byte[] cmd = { 0x1b, 0x20, (byte)camID };
            byte[] cmd = { 0x1b, 0x20, 0x00 };
            comPort.Clear();
            comPort.Write(cmd, 0, cmd.Length);
            int timeout = 2000;
            int DATA_LEN = 12;
            byte[] buffer = new byte[DATA_LEN];
            if (DATA_LEN == comPort.Read(buffer, 0, DATA_LEN, timeout))
            {
                return buffer;
            }

            return null;
        }

        /// <summary>
        /// 上传
        /// 目标地址：广东省智慧食药监管平台
        /// </summary>
        /// <param name="selectedRecords"></param>
        /// <returns></returns>
        private String UploadResult(List<tlsTtResultSecond> selectedRecords)
        {
            if (selectedRecords == null || selectedRecords.Count == 0)
                return "暂无需要上传的数据";

            String outStr = String.Empty;
            Global.UploadSCount = 0;
            uploadResult.Request model = null;
            IDictionary<String, uploadResult.Request> dicModel = null;
            try
            {
                if (selectedRecords.Count > 0)
                {
                    dicModel = new Dictionary<String, uploadResult.Request>();
                    foreach (tlsTtResultSecond item in selectedRecords)
                    {
                        if (!dicModel.ContainsKey(item.CheckTotalItem))
                        {
                            model = new uploadResult.Request();
                            model.username = LoginWindow._userAccount.UserName;
                            model.itemid = item.CheckTotalItem;
                            model.deviceid = Wisdom.DeviceID;
                            model.totalnum = selectedRecords.Count;
                            model.longitude = string.Empty;
                            model.latitude = string.Empty;
                            dicModel.Add(item.CheckTotalItem, model);
                        }
                        uploadResult.Request.details details = new uploadResult.Request.details();
                        if (item.SampleId.Length > 2)
                            details.sampleid = item.SampleId.Substring(2, item.SampleId.Length - 2);
                        details.doublevalue = item.CheckValueInfo;
                        details.unit = item.ResultInfo;
                        if (item.Result.Equals("合格"))
                            details.stringvalue = "0";
                        else if (item.Result.Equals("不合格"))
                            details.stringvalue = "1";
                        else
                            details.stringvalue = "2";
                        details.time = item.CheckStartDate;
                        model.detailsList.Add(details);
                    }
                }
                if (dicModel.Count == 0)
                    return "暂无需要上传的数据";
                List<string> list = new List<string>(dicModel.Keys);
                for (int i = 0; i < list.Count; i++)
                {
                    uploadResult.Request upModel = dicModel[list[i]];
                    Wisdom.UPLOADRESULT_REQUEST = upModel;
                    String result = Wisdom.HttpPostRequest(Wisdom.UPLOADRESULT);
                    if (result.Length > 0)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        uploadResult.Response json = js.Deserialize<uploadResult.Response>(result);
                        if (json != null)
                        {
                            if (json.code.Equals("0"))
                            {
                                Global.UploadSCount += upModel.detailsList.Count;
                                string where = string.Empty;
                                for (int j = 0; j < selectedRecords.Count; j++)
                                {
                                    where += where.Length > 0 ? "," + string.Format("'{0}'", selectedRecords[j].SysCode) : string.Format("'{0}'", selectedRecords[j].SysCode);
                                }
                                if (where.Length > 0)
                                {
                                    where = string.Format("({0})", where);
                                    _resultTable.UpdateUpload(where, out result);
                                }
                            }
                            else
                            {
                                if (json.code.Equals("1"))
                                {
                                    if (json.message.Length > 0)
                                        outStr += json.message + ";";
                                    else
                                        outStr = "上传参数缺失!";
                                }
                                else if (json.code.Equals("2"))
                                {
                                    if (json.message.Length > 0)
                                        outStr += json.message + ";";
                                    else
                                        outStr = "json字符串格式不正确!";
                                }
                                else if (json.code.Equals("3"))
                                {
                                    if (json.message.Length > 0)
                                        outStr += json.message + ";";
                                    else
                                        outStr = "系统处理失败，请稍后再试!";
                                }
                            }
                        }
                    }
                }

                if (Global.UploadSCount == 0)
                {
                    return outStr;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return outStr;
        }
        /// <summary>
        /// 手动测试数据上传
        /// </summary>
        /// <param name="result"></param>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string MutestUpload(tlsTtResultSecond result, string url, string user, out string errMsg)//List<tlsTtResultSecond> 
        {
            errMsg = "";
            string rtn = string.Empty;

            try
            {
                Global.UpY = result.ID;
                QuickServerResult qsr = new QuickServerResult();

                StringBuilder sb = new StringBuilder();
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);

                DataTable dt = _bll.GetCompany("reg_name = '" + result.CheckedCompany + "' and checked='1' and delete_flag='0' ", "", 3, out errMsg);
                qsr.regId = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    qsr.regId = dt.Rows[0]["rid"].ToString();//经营户ID
                    //qsr.regUserName = dt.Rows[0]["ope_shop_name"].ToString();//档口名称
                    //qsr.regUserId = dt.Rows[0]["bid"].ToString();//档口ID
                }

                dt = _bll.GetCompany(" bid = '" + result.OpertorID + "' and checked='1' and delete_flag='0'", "", 2, out errMsg);
                qsr.regUserName = "";
                qsr.regUserId = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    //qsr.regId = dt.Rows[0]["reg_id"].ToString();//经营户ID
                    qsr.regUserName = dt.Rows[0]["ope_shop_code"].ToString().Replace("#", "%23").Trim();// dt.Rows[0]["ope_shop_name"].ToString();//档口名称
                    qsr.regUserId = dt.Rows[0]["bid"].ToString();//档口ID
                }

               

                dt = _Tskbll.GetCheckItem("detect_item_name='" + result.CheckTotalItem + "' and checked='1' and delete_flag='0' ", "", 2);//查检测项目ID
                if (dt != null && dt.Rows.Count > 0)
                {
                    qsr.itemId = dt.Rows[0]["cid"].ToString(); 
                }
                qsr.id = result.CheckNo;//仪器的检测编号
                qsr.taskId = "";//dt.Rows[0]["t_id"].ToString();//任务ID为空
                qsr.taskName = ""; //dt.Rows[0]["t_task_title"].ToString();//任务主题
                qsr.samplingId = "";//dt.Rows[0]["sampling_id"].ToString();//检测任务的样品ID;
                qsr.samplingDetailId = "";//dt.Rows[0]["tid"].ToString();//检测任务的tid
                //qsr.itemId = "";//dt.Rows[0]["item_id"].ToString();//检测任务检测项目
                qsr.departId = Global.depart_id;// dt.Rows[0]["t_task_departId"].ToString(); 
                qsr.departName = Global.d_depart_name;// "天河区食药监局";  //?
                qsr.pointId = Global.point_id;// dt.Rows[0]["s_point_id"].ToString();
                qsr.pointName = Global.p_point_name;//  检测点
                //qsr.regUserId = ""; //dt.Rows[0]["s_ope_id"].ToString();//档口ID
                //qsr.regUserName = "";//dt.Rows[0]["s_ope_shop_name"].ToString();//档口名称
                //qsr.regId = "";//dt.Rows[0]["s_reg_id"].ToString(); ;//经营户ID
                qsr.regName = result.CheckedCompany;//dt.Rows[0]["s_reg_name"].ToString(); //被检单位
                qsr.statusFalg = "0";
                qsr.remark = "";
                qsr.foodId = result.SampleCode;//dt.Rows[0]["food_id"].ToString(); 
                qsr.foodName = result.FoodName;
                qsr.foodTypeId = result.SampleCode;// result[i].SampleCode;
                qsr.foodTypeName = result.FoodType;
                qsr.checkCode = "";//dt.Rows[0]["tid"].ToString(); 
                qsr.itemName = result.CheckTotalItem;
                qsr.checkResult = result.CheckValueInfo;
                qsr.limitValue = result.StandValue;
                qsr.checkAccord = result.Standard;
                qsr.checkUnit = result.ResultInfo;
                qsr.conclusion = result.Result;
                qsr.auditorName = Global.realname;
                qsr.checkUserid = Global.id;
                qsr.checkUsername = Global.realname;//result[i].CheckUnitInfo;登录人账号
                qsr.checkAccordId = result.CheckNo;
                qsr.checkDate = result.CheckStartDate;
                qsr.uploadName = Global.realname; //result[i].CheckUnitInfo;
                qsr.auditorId = Global.id;
                qsr.uploadId = Global.id;
                qsr.uploadDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                qsr.deviceCompany = "广东达元绿洲食品安全科技股份有限公司";
                qsr.deviceId = Global.MachineNum;
                qsr.deviceName = result.CheckMachine;// "DY-3500I食品综合分析仪";
                qsr.deviceModel = "分光";//dt.Rows[0]["mokuai"].ToString();//模块 分光、胶体金、干化学
                qsr.deviceMethod = result.CheckMethod;
                qsr.dataSource = "1";
                qsr.param1 = ""; //dt.Rows[0]["param2"].ToString();//预留参数
                qsr.dataType = "0";

                string json = InterfaceHelper.ToJson(qsr);
                sb.AppendFormat("&result={0}", json);
                FileUtils.KLog(sb.ToString(), "发送", 14);
                rtn = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(rtn, "接收", 14);
                ResultData Jjresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                if (Jjresult.msg == "操作成功" || Jjresult.success == true)
                {
                    Global.UploadSCount = Global.UploadSCount + 1;
                    _bll.UpdateResult("Y", Global.UpY, out errMsg);
                }
                else
                {
                    Global.UploadFCount = Global.UploadFCount + 1;
                }
            }
            catch (Exception ex)
            {
                FileUtils.ErrorLog("上传数据时出现异常", "上传失败", ex.Message);
                return errMsg = ex.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 扫码下载检测任务
        /// </summary>
        /// <param name="result"></param>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string QuickSweenUpLoad(tlsTtResultSecond result, string url, string user, string pwd)
        {
            string err = "";
            string rtn = string.Empty;
            try
            {
                sweenUpload swd = new sweenUpload();
                StringBuilder sb = new StringBuilder();
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);

                swd.id = result.taskID;
                swd.foodName = result.FoodName;
                swd.itemName  = result.CheckTotalItem;
                swd.checkResult  = result.CheckValueInfo ;
                swd.checkUnit = result.ResultInfo;
                swd.checkDate = result.CheckStartDate;
                swd.serialNumber  = Global.MachineNum ;
                swd.conclusion = result.Result;

                List<sweenUpload> objRd = new List<sweenUpload>();
                objRd.Add(swd);
                string json = InterfaceHelper.ToJson(objRd);
                sb.AppendFormat("&results={0}", json);
                FileUtils.KLog(sb.ToString(), "发送", 24);
                rtn = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(rtn, "接收", 24);
                ResultData Jjresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                if (Jjresult.resultCode == "0X00000")
                {
                    Global.UploadSCount = Global.UploadSCount + 1;
                    _bll.UpdateResult("Y", result.ID , out err);
                }
                else
                {
                    Global.UploadFCount = Global.UploadFCount + 1;
                }
            }
            catch (Exception ex)
            {
                FileUtils.ErrorLog("上传数据时出现异常", "上传失败", ex.Message);
                return err = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 快检服务检测数据上传
        /// </summary>
        /// <param name="selectedRecords"></param>
        /// <param name="url"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string QuickUpLoad(tlsTtResultSecond result, string url, string user, string pwd)//List<tlsTtResultSecond> 
        {
            string err = "";
            string rtn = string.Empty;
            try
            {
                
                Global.UpY = result.ID;
                QuickServerResult qsr = new QuickServerResult();

                StringBuilder sb = new StringBuilder();
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);

                DataTable dt = _Tskbll.GetQtask("ID="+result.taskID, "", 1);//_Tskbll.GetQtask("food_name='" + result[i].FoodName + "' and item_name='" + result[i].CheckTotalItem + "' and s_reg_name='" + result[i].CheckedCompany + "'", "", 1);
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("本地数据库没有对应的检测任务","提示");
                    return rtn;
                }

                qsr.id = result.CheckNo;//dt.Rows[0]["tid"].ToString();
                qsr.taskId = dt.Rows[0]["t_id"].ToString();//"4028935f5e07e5b7015e07e668470002";
                qsr.taskName = dt.Rows[0]["t_task_title"].ToString();
                qsr.samplingId = dt.Rows[0]["sampling_id"].ToString();//"b44f1897a94a6a9ad6b5a3ce97cec4d9";
                qsr.samplingDetailId = dt.Rows[0]["tid"].ToString();
                qsr.itemId = dt.Rows[0]["item_id"].ToString();
                qsr.departId = Global.depart_id;// dt.Rows[0]["t_task_departId"].ToString(); 
                qsr.departName = Global.d_depart_name;// "天河区食药监局";  //?
                qsr.pointId = Global.point_id;// dt.Rows[0]["s_point_id"].ToString();
                qsr.pointName = Global.p_point_name;//  检测点
                qsr.regUserId = dt.Rows[0]["s_ope_id"].ToString();//档口ID
                qsr.regUserName = dt.Rows[0]["s_ope_shop_code"].ToString().Replace("#", "%23").Trim();//改存档口编号dt.Rows[0]["s_ope_shop_name"].ToString();//档口名称
                qsr.regId = dt.Rows[0]["s_reg_id"].ToString(); ;//"40287d815e4b8158015e4b8158fc0000";
                qsr.regName = dt.Rows[0]["s_reg_name"].ToString(); //被检单位
                qsr.statusFalg = "0";
                qsr.remark = "";
                qsr.foodId = dt.Rows[0]["food_id"].ToString(); 
                qsr.foodName = result.FoodName;
                qsr.foodTypeId = "";// result[i].SampleCode;
                qsr.foodTypeName = result.FoodType;
                qsr.checkCode = dt.Rows[0]["tid"].ToString(); 
                qsr.itemName = result.CheckTotalItem;
                qsr.checkResult = result.CheckValueInfo;
                qsr.limitValue = result.StandValue;
                qsr.checkAccord = result.Standard;
                qsr.checkUnit = result.ResultInfo;
                qsr.conclusion = result.Result;
                qsr.auditorName = Global.realname;
                qsr.checkUserid = Global.id;
                qsr.checkUsername = Global.realname;//result[i].CheckUnitInfo;登录人账号
                qsr.checkAccordId = result.CheckNo;
                qsr.checkDate = result.CheckStartDate;
                qsr.uploadName = Global.realname; //result[i].CheckUnitInfo;
                qsr.auditorId = Global.id;
                qsr.uploadId = Global.id; 
                qsr.uploadDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                qsr.deviceCompany = "广东达元绿洲食品安全科技股份有限公司";
                qsr.deviceId = Global.MachineNum;
                qsr.deviceName = result.CheckMachine;// "DY-3500I食品综合分析仪";
                qsr.deviceModel = dt.Rows[0]["mokuai"].ToString();//模块快
                qsr.deviceMethod = result.CheckMethod;
                qsr.dataSource = "1";
                qsr.param1 = dt.Rows[0]["td_id"].ToString();//dt.Rows[0]["param2"].ToString();//预留参数
                qsr.dataType = dt.Rows[0]["dataType"].ToString(); 

                string json = InterfaceHelper.ToJson(qsr);
                sb.AppendFormat("&result={0}", json);

                FileUtils.KLog(sb.ToString(), "发送", 14);

                rtn = InterfaceHelper.HttpsPost(sb.ToString());

                FileUtils.KLog(rtn, "接收", 14);
                ResultData Jjresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                if (Jjresult.msg == "操作成功" || Jjresult.success == true)
                {
                    Global.UploadSCount = Global.UploadSCount + 1;
                    _bll.UpdateResult("Y", Global.UpY, out err);
                }
                else
                {
                    Global.UploadFCount = Global.UploadFCount + 1;
                }
                   
            }
            catch (Exception ex)
            {
                FileUtils.ErrorLog("上传数据时出现异常", "上传失败", ex.Message);
                return err = ex.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 上传至快检平台
        /// </summary>
        /// <param name="server"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private string KjcUpload(CheckPointInfo server, DataTable results) 
        {
            Global.UploadSCount = 0;
            string rtn = string.Empty, upStr = string.Empty;
            kjcUploadEntity upDatas = new kjcUploadEntity();
            upDatas.result = new List<kjcUploadEntity.results>();
            kjcUploadEntity.results model = new kjcUploadEntity.results();
            tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
            DataTable dtbl = null;
            string dtValue = string.Empty, deviceModel = string.Empty;

            for (int i = 0; i < results.Rows.Count; i++)
            {
                dtbl = null;
                model = new kjcUploadEntity.results();
                //记录编号
                dtValue = results.Rows[i]["SysCode"].ToString();
                if (string.IsNullOrEmpty(dtValue) || !Global.IsGuidByReg(dtValue))
                {
                    model.sysCode = dtValue = Global.GETGUID();
                    _bll.InsertSysCode(dtValue, int.Parse(results.Rows[i]["ID"].ToString()), out dtValue);
                }
                model.sysCode = string.IsNullOrEmpty(model.sysCode) ? dtValue : model.sysCode;
                //被检样品种类
                dtValue = results.Rows[i]["FoodType"].ToString();
                model.foodType = string.IsNullOrEmpty(dtValue) ? "样品" : dtValue;
                //被样品名称
                dtValue = results.Rows[i]["FoodName"].ToString();
                model.foodName = string.IsNullOrEmpty(dtValue) ? "未知样" : dtValue;
                //抽样单号
                dtValue = results.Rows[i]["SampleId"].ToString();
                model.sampleNo = string.IsNullOrEmpty(dtValue) ? "" : dtValue;
                dtValue = "";//results.Rows[i]["Sdid"].ToString();
                if (!string.IsNullOrEmpty(dtValue)) dtbl = opr.GetAsDataTable("SampleTask", string.Format("sdid = '{0}'", dtValue), out dtValue);
                bool isSampleid = dtbl != null && dtbl.Rows.Count > 0 ? true : false;
                model.sampleNo = model.sampleNo.Length == 0 && isSampleid ? dtbl.Rows[0]["sampingNO"].ToString() : model.sampleNo;
                //样品编号(对应抽样单明细中的样品编号，当sampingNO有值时必填)
                model.foodCode = isSampleid ? dtbl.Rows[0]["SampleNO"].ToString() : results.Rows[i]["SampleFoodCode"].ToString();
                //任务编号
                model.planCode = isSampleid ? dtbl.Rows[0]["planCode"].ToString() : results.Rows[i]["CheckPlanCode"].ToString();
                //经营户ID (关联被检单位子集的cdId)
                model.cdId = isSampleid ? dtbl.Rows[0]["cdId"].ToString() : "";
                //经营户名称
                model.cdName = isSampleid ? dtbl.Rows[0]["cdName"].ToString() : "";
                //检测单位（必填）(接口1的 pointId)
                model.checkPId = Global.samplenameadapter[0].pointId;
                //检测日期
                dtValue = results.Rows[i]["CheckStartDate"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                model.checkDate = dtValue;
                //检测依据
                model.checkAccord = results.Rows[i]["Standard"].ToString();
                //检测项目
                dtValue = results.Rows[i]["CheckTotalItem"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "未知项目";
                }
                model.checkItemName = dtValue;
                //检测仪器唯一编码
                model.checkDevice = Global.DeviceID;
                //被检对象名称
                dtValue = isSampleid ? dtbl.Rows[0]["regName"].ToString() : results.Rows[i]["CheckedCompany"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "默认被检单位";
                }
                model.ckcName = dtValue;
                //监管对象ID(关联被检单位的regId)
                dtValue = isSampleid ? dtbl.Rows[0]["regId"].ToString() : "";
                model.ckcCode = isSampleid ? dtbl.Rows[0]["ckoCode"].ToString() : "";
                if (!string.IsNullOrEmpty(dtValue))
                {
                    model.regId = dtValue;
                }
                else
                {
                    dtbl = opr.GetAsDataTable("kjcCompany", string.Format("regName = '{0}'", model.ckcName), out dtValue);
                    //如果查询被检单位没有的话，则是经营户信息
                    if (dtbl != null && dtbl.Rows.Count > 0 && dtValue.Length == 0)
                    {
                        model.regId = dtbl.Rows[0]["regId"].ToString();
                        //所属行政机构编号（必填）（被检单位的organizationCode）
                        model.ckcCode = dtbl.Rows[0]["organizationCode"].ToString();
                    }
                    else
                    {
                        dtbl = opr.GetAsDataTable("kjcCompanyDeteails", string.Format("cdName = '{0}'", model.ckcName), out dtValue);
                        if (dtbl != null && dtbl.Rows.Count > 0 && dtValue.Length == 0)
                        {
                            //经营户ID (关联被检单位子集的cdId)
                            model.cdId = dtbl.Rows[0]["cdId"].ToString();
                            //经营户名称
                            model.cdName = dtbl.Rows[0]["cdName"].ToString();
                            //通过经营户信息查询被检单位
                            dtbl = opr.GetAsDataTable("kjcCompany", string.Format("regId = '{0}'", dtbl.Rows[0]["regId"].ToString()), out dtValue);
                            if (dtbl != null && dtbl.Rows.Count > 0 && dtValue.Length == 0)
                            {
                                model.ckcName = dtbl.Rows[0]["regName"].ToString();
                                model.regId = dtbl.Rows[0]["regId"].ToString();
                                //所属行政机构编号（必填）（被检单位的organizationCode）
                                model.ckcCode = dtbl.Rows[0]["organizationCode"].ToString();
                            }
                        }
                    }
                }
                //检测值单位
                dtValue = results.Rows[i]["ResultInfo"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "";
                }
                model.checkUnit = dtValue;
                //检测结果值(12%)（必填）
                dtValue = results.Rows[i]["CheckValueInfo"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "0";
                }
                deviceModel = deviceModel.Length == 0 ? results.Rows[i]["CheckMachineModel"].ToString() : deviceModel;
                model.checkResult = dtValue;
                //限定值
                dtValue = results.Rows[i]["StandValue"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "0";
                }
                model.limitValue = dtValue;
                //检测结论 （合格、不合格）
                dtValue = results.Rows[i]["Result"].ToString();
                if (dtValue == null || dtValue.Length == 0)
                {
                    dtValue = "不合格";
                }
                model.checkConclusion = dtValue;
                //检测数据状态 0未审核、1已审核
                model.dataStatus = 1;
                //数据来源 数据来源0检测工作站，1监管通app，2.仪器上传，3平台上传，4黑金刚上传
                model.dataSource = 2;
                //检测人
                model.checkUser = LoginWindow._userAccount.UserName;
                //数据上传人
                model.dataUploadUser = LoginWindow._userAccount.UserName;

                upDatas.result.Add(model);
            }
            return rtn;
        }

        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        /// <summary>
        /// 上传至食安科技监管平台
        /// </summary>
        /// <param name="server">服务器相关</param>
        /// <param name="results">上传的数据</param>
        /// <returns>结果</returns>
        private string UploadResult(CheckPointInfo server, DataTable results)
        {
            //强制执行数据完善操作
            //bool IsMandatory = true;

            string rtn = string.Empty, upStr = string.Empty;
            try
            {
                int dataLen = results.Rows.Count, pageSize = 10, pageCount = Global.PageCount(dataLen, pageSize);
                //分页索引从1开始
                DataTable upDtbl = null;
                DataSet dst = null;
                tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
                for (int i = 1; i <= pageCount; i++)
                {
                    upDtbl = Global.GetPagedTable(results, i, pageSize);
                    #region 验证必填
                    string dtValue = string.Empty;
                    for (int j = 0; j < upDtbl.Rows.Count; j++)
                    {
                        try
                        {
                            //记录编号
                            dtValue = upDtbl.Rows[j]["SysCode"].ToString();
                            if (dtValue == null || dtValue.Length == 0 || !Global.IsGuidByReg(dtValue))
                            {
                                upDtbl.Rows[j]["SysCode"] = Global.GETGUID(null, 1);
                                _bll.InsertSysCode(upDtbl.Rows[j]["SysCode"].ToString(), int.Parse(upDtbl.Rows[j]["ID"].ToString()), out dtValue);
                            }
                            //检测编号
                            dtValue = upDtbl.Rows[j]["CheckNo"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["CheckNo"] = DateTime.Now.ToString("yyyyMMddHHmmssff") + j;
                            }
                            //被检对象名称
                            dtValue = upDtbl.Rows[j]["CheckedCompany"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["CheckedCompany"] = "默认被检单位";
                            }
                            //被样品名称
                            dtValue = upDtbl.Rows[j]["FoodName"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["FoodName"] = "未知样";
                            }
                            //被检样品种类
                            dtValue = upDtbl.Rows[j]["FoodType"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["FoodType"] = "样品";
                            }
                            //检测项目
                            dtValue = upDtbl.Rows[j]["CheckTotalItem"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["CheckTotalItem"] = "未知项目";
                            }
                            //检测值
                            dtValue = upDtbl.Rows[j]["CheckValueInfo"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["CheckValueInfo"] = "0";
                            }
                            //检测标准值
                            dtValue = upDtbl.Rows[j]["StandValue"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["StandValue"] = "0";
                            }
                            //检测结论 （合格、不合格）
                            dtValue = upDtbl.Rows[j]["Result"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["Result"] = "不合格";
                            }
                            //检测值单位
                            dtValue = upDtbl.Rows[j]["ResultInfo"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["ResultInfo"] = "NA";
                            }
                            ////检测单位所属行政机构名称
                            //if (IsMandatory)
                            //{
                            //    upDtbl.Rows[j]["CheckPlace"] = Global.samplenameadapter[0].Organization;
                            //}
                            //else
                            //{
                            //    dtValue = upDtbl.Rows[j]["CheckPlace"].ToString();
                            //    if (dtValue == null || dtValue.Length == 0)
                            //    {
                            //        upDtbl.Rows[j]["CheckPlace"] = Global.samplenameadapter[0].Organization;
                            //    }
                            //}
                            
                            ////检测单位所属行政机构编号
                            //if (IsMandatory)
                            //{
                            //    upDtbl.Rows[j]["CheckPlaceCode"] = Global.samplenameadapter[0].CheckPlaceCode;
                            //}
                            //else
                            //{
                            //    dtValue = upDtbl.Rows[j]["CheckPlaceCode"].ToString();
                            //    if (dtValue == null || dtValue.Length == 0)
                            //    {
                            //        upDtbl.Rows[j]["CheckPlaceCode"] = Global.samplenameadapter[0].CheckPlaceCode;
                            //    }
                            //}
                            ////检测单位名称
                            //if (IsMandatory)
                            //{
                            //    upDtbl.Rows[j]["CheckUnitName"] = Global.samplenameadapter[0].CheckPointName;
                            //}
                            //else
                            //{
                            //    dtValue = upDtbl.Rows[j]["CheckUnitName"].ToString();
                            //    if (dtValue == null || dtValue.Length == 0)
                            //    {
                            //        upDtbl.Rows[j]["CheckUnitName"] = Global.samplenameadapter[0].CheckPointName;
                            //    }
                            //}
                            //if (IsMandatory)
                            //{
                            //    upDtbl.Rows[j]["APRACategory"] = Global.samplenameadapter[0].CheckPointType;
                            //}
                            //else
                            //{
                            //    dtValue = upDtbl.Rows[j]["APRACategory"].ToString();
                            //    if (dtValue == null || dtValue.Length == 0)
                            //    {
                            //        upDtbl.Rows[j]["APRACategory"] = Global.samplenameadapter[0].CheckPointType;
                            //    }
                            //}
                            upDtbl.Rows[j]["CKCKNAMEUSID"] = Global.samplenameadapter[0].user;
                            //基层上传人 2017年2月27日修改为上传“设置”界面的用户名
                            //dtValue = upDtbl.Rows[j]["UpLoader"].ToString();
                            //if (dtValue == null || dtValue.Length == 0)
                            //{
                            upDtbl.Rows[j]["UpLoader"] = Global.samplenameadapter[0].user;
                            //}
                            //抽检日期
                            dtValue = upDtbl.Rows[j]["TakeDate"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["TakeDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                            }
                            //检测依据或标准
                            dtValue = upDtbl.Rows[j]["Standard"].ToString();
                            if (dtValue == null || dtValue.Length == 0)
                            {
                                upDtbl.Rows[j]["Standard"] = "NA";
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    #endregion
                    dst = new DataSet("UpdateResult");
                    dst.Tables.Add(upDtbl.Copy());
                    string str = dst.GetXml();
                    rtn = Global.Upload(str, server.user, server.pwd, server.pointNum, server.url);
                    dst = new DataSet();
                    using (StringReader sr = new StringReader(rtn))
                    {
                        dst.ReadXml(sr);
                    }
                    string error = string.Empty;
                    DataTable rtnDtbl = dst.Tables["Result"];
                    string result = Global.GetResultByCode(rtnDtbl.Rows[0]["ResultCode"].ToString());
                    string where = string.Empty;
                    if (result.Equals("1"))
                    {
                        if (upDtbl != null && upDtbl.Rows.Count > 0)
                        {
                            for (int j = 0; j < upDtbl.Rows.Count; j++)
                            {
                                where += where.Length > 0 ? "," + string.Format("'{0}'", upDtbl.Rows[j]["SysCode"]) : string.Format("'{0}'", upDtbl.Rows[j]["SysCode"]);
                            }
                            if (where.Length > 0)
                            {
                                where = string.Format("({0})", where);
                                _resultTable.UpdateUpload(where, out error);
                                if (error.Length == 0) Global.UploadSCount++;
                            }
                        }
                    }
                    else if (result.Equals("2"))
                    {
                        where = string.Empty;
                        result = rtnDtbl.Rows[0]["ResultInfo"].ToString();
                        //数组下标 0为add成功的部分；1为update成功的部分；2为不成功的部分
                        string[] ResultInfo = result.Split('|');
                        string[] addResult = null, updateResult = null, otherResult = null;

                        //add成功的部分
                        if (ResultInfo[0].Length > 0)
                        {
                            addResult = ResultInfo[0].Split(',');
                            if (addResult.Length > 0)
                            {
                                for (int j = 0; j < addResult.Length; j++)
                                {
                                    if (addResult[j].Length > 0)
                                    {
                                        where += where.Length > 0 ? "," + string.Format("'{0}'", addResult[j]) : string.Format("'{0}'", addResult[j]);
                                    }
                                }
                                if (where.Length > 0)
                                {
                                    where = string.Format("({0})", where);
                                    _resultTable.UpdateUpload(where, out error);
                                    if (error.Length == 0) Global.UploadSCount++;
                                }
                            }
                        }
                        //update成功的部分
                        if (ResultInfo[1].Length > 0)
                        {
                            where = string.Empty;
                            updateResult = ResultInfo[1].Split(',');
                            if (updateResult.Length > 0)
                            {
                                for (int j = 0; j < updateResult.Length; j++)
                                {
                                    if (updateResult[j].Length > 0)
                                    {
                                        where += where.Length > 0 ? "," + string.Format("'{0}'", updateResult[j]) : string.Format("'{0}'", updateResult[j]);
                                    }
                                }
                                if (where.Length > 0)
                                {
                                    where = string.Format("({0})", where);
                                    _resultTable.UpdateUpload(where, out error);
                                    if (error.Length == 0) Global.UploadSCount++;
                                }
                            }
                        }
                        //失败的部分
                        if (ResultInfo[2].Length > 0)
                        {
                            rtn = rtnDtbl.Rows[0]["ResultDesc"].ToString();
                            otherResult = ResultInfo[2].Split(',');
                            if (otherResult.Length > 0)
                            {
                                for (int j = 0; j < otherResult.Length; j++)
                                {
                                    if (otherResult[j].Length > 0)
                                    {
                                        Global.UploadFCount++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        rtn = rtnDtbl.Rows[0]["ResultDesc"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return (Global.UploadSCount > 0 && Global.UploadFCount == 0) ? string.Empty : rtn;
        }

        /// <summary>
        /// 新方法 验证连接地址，用户和密码的正确性
        /// 2016年12月22日 wenj update
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="password">密码,需要MD5加密</param>
        /// <returns>json</returns>
        public String checkUserConnection(string url, String user, String password)
        {
            string rtn = string.Empty;
            NewInterface.dataSync ds = new NewInterface.dataSync();
            ds.Url = url;
            rtn = ds.checkUserConnection(user, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToString());
            return rtn;
        }

        /// <summary>
        /// 全部数据下载
        /// 2016年12月22日 wenj update 新版本接口
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="checkNumber">检测点编号</param>
        /// <param name="checkName">检测点名称</param>
        /// <param name="checkType">检测点类型</param>
        /// <param name="checkOrg">所属行政机构</param>
        /// <param name="user">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns>结果</returns>
        private string DownLoadAllData(string url, string checkNumber, string checkName, string checkType, string checkOrg, string user, string pwd, string downType)
        {
            string UDate = string.Empty, rtn = string.Empty, sign = string.Empty,
                version = Global.Version.Equals("XZ") ? "行政版" : "企业版", outErr = string.Empty;
            DataTable dtUDate = null;
            try
            {
                //检测任务下载
                sign = "CheckPlan";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    //检测任务需要实时更新，每次下载时先清空数据库
                    _clsCompanyOpr.Delete(string.Empty, out outErr, "tTask");
                    UDate = string.Empty;
                    _CheckItemStandard = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }

                //被检单位下载
                sign = "Company";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    //被检单位需每次重新下载，无需增量下载，且每次都需要将数据清空
                    _clsCompanyOpr.Delete(string.Empty, out outErr, "TCOMPANY");
                    UDate = string.Empty;
                    _DownCompany = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                    //FileUtils.ErrorLog("SettingWindow", "DownLoadDatas", _DownCompany);
                }

                //样品检测项目和对应检测标准数据下载
                sign = "SelectItem";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 2);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _SampleStandardAgainDisplay = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }

                //检测标准下载
                sign = "Standard";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 4);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsStandard> listSample = (List<clsStandard>)IListDataSet.DataTableToIList<clsStandard>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _Standard = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }

                //检测项目下载
                sign = "CheckItem";
                if (sign.Equals(downType) || downType.Equals("all"))
                {
                    dtUDate = null;
                    dtUDate = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 3);
                    if (dtUDate != null && dtUDate.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtUDate, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _CheckItems = Global.GetXmlByService(version, url, user, pwd, sign, UDate);
                }

                return "true";
            }
            catch (Exception ex)
            {
                FileUtils.ErrorLog("SettingWindow", "DownLoadDatas", ex.ToString());
                return ex.Message;
            }
        }

        /// <summary>
        /// 检测标准下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string DownStandard(string url, string checkNumber, string checkName, string checkType, string checkOrg, string user, string pwd)
        {
            string UDate = string.Empty, rtn = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = url;
                rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, user, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    DataTable dtSample = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 4);
                    if (dtSample != null && dtSample.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtSample, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _Standard = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "Standard", UDate);
                }
            }
            catch (Exception)
            {
                return "false";
            }
            return rtn;
        }

        /// <summary>
        /// 检测项目下载
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="user">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        private string DownCheckItem(string url, string checkNumber, string checkName, string checkType, string checkOrg, string user, string pwd)
        {
            string UDate = string.Empty, rtn = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = url;
                rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, user, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    DataTable dtSample = _clsttStandardDecideOpr.GetAsDataTable(string.Empty, string.Empty, 3);
                    if (dtSample != null && dtSample.Rows.Count > 0)
                    {
                        List<clsttStandardDecide> listSample = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dtSample, 1);
                        UDate = listSample[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _CheckItems = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", user, pwd, "CheckItem", UDate);
                }
            }
            catch (Exception)
            {
                return "false";
            }
            return rtn;
        }

        /// <summary>
        /// 仪器检测项目下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string DownItems(string url, string checkNumber, string checkName, string checkType, string checkOrg, string username, string pwd)
        {
            string rtn = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = url;
                rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, username, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    _CheckItemSecondDisplay = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", username, pwd, "Instrumen,DY3000", string.Empty);
                }
                return rtn;
            }
            catch (Exception)
            {
                return "false";
            }
        }

        /// <summary>
        /// 被检单位下载
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="checkNumber"></param>
        /// <param name="checkName"></param>
        /// <param name="checkType"></param>
        /// <param name="checkOrg"></param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="UDate">更新时间</param>
        /// <returns></returns>
        private string DownCopany(string url, string checkNumber, string checkName, string checkType, string checkOrg, string username, string pwd)
        {
            string rtn = string.Empty, UDate = string.Empty, sign = "Company";
            DataTable dt = _clsCompanyOpr.GetAsDataTable(string.Empty, string.Empty, 7);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dt, 1);
                UDate = listCompany[0].UDate;
            }
            rtn = Global.GetXmlByService(Global.Version.Equals("XZ") ? "行政版" : "企业版", url, username, pwd, sign, UDate);
            return rtn;
            #region 旧版本接口
            //string UDate = string.Empty, rtn = string.Empty;
            //try
            //{
            //    FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
            //    ws.Url = url;
            //    rtn = ws.CheckChePoint(checkNumber, checkName, checkType, checkOrg, username, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
            //    if ("true".Equals(rtn))
            //    {
            //        DataTable dt = _clsCompanyOpr.GetAsDataTable(string.Empty, string.Empty, 7);
            //        if (dt != null && dt.Rows.Count > 0)
            //        {
            //            List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dt, 1);
            //            UDate = listCompany[0].UDate;
            //        }
            //        _DownCompany = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", username, pwd, "Company", UDate);
            //    }
            //    return rtn;
            //}
            //catch (Exception)
            //{
            //    return "false";
            //}
            #endregion
        }

        /// <summary>
        /// 任务下载
        /// </summary>
        /// <param name="server"></param>
        /// <returns>结果</returns>
        private string DownLoadTask(CheckPointInfo server)
        {
            string UDate = string.Empty, rtn = string.Empty;
            try
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                ws.Url = server.url;
                rtn = ws.CheckChePoint(server.pointNum, server.pointName, server.pointType, server.orgName, server.user, FormsAuthentication.HashPasswordForStoringInConfigFile(server.pwd, "MD5").ToString());
                if ("true".Equals(rtn))
                {
                    //string data_Task = ws.GetDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版", server.CheckPlaceCode, server.CheckPointID, server.RegisterID, FormsAuthentication.HashPasswordForStoringInConfigFile(server.RegisterPassword, "MD5").ToString(), "CheckPlan");
                    DataTable dt = _clsCompanyOpr.GetAsDataTable(string.Empty, string.Empty, 12);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<clsCompany> listCompany = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dt, 1);
                        UDate = listCompany[0].UDate;
                    }
                    else
                        UDate = string.Empty;
                    _CheckItemStandard = ws.downLoadDataDriverBySign(Global.Version.Equals("XZ") ? "行政版" : "企业版".Equals("XZ") ? "行政版" : "企业版", server.user, server.pwd, "CheckPlan", UDate);
                }
            }
            catch (Exception)
            {
                return "false";
            }
            return rtn;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _port.Dispose();
            }
            _disposed = true;
        }
        /// <summary>
        /// 系统权限
        /// </summary>
        /// <param name="right"></param>
        private void sysRights(string right)
        {
            string[] Aright = right.Split(',');
            Global.Detectionbtn = false;
            Global.redetectionbtn = false;
            Global.DataCenterbtn = false;
            Global.Receivetask = false;
            Global.Setting = false;
            Global.testitembtn = false;
            Global.foodtypebtn = false;
            Global.machineitembtn = false;
            Global.samplestdbtn = false;
            Global.lawsbtn = false;
            Global.testdatabtn = false;
            Global.sysupdatebtn = false;
            Global.checkcompanuy = false;
            Global.ShoudongTest = false;
            Global.print = false;
            Global.Uploadd = true;
            Global.edition = false;
            Global.Input = false;
            Global.output = false;
            Global.GenerateReport = false;
            Global.PrintReport = false;
            Global.ResetCompany = false;
            Global.ResetItem = false;
            Global.ResetStandard = false ;
            Global.ResetSampleType = false;
            Global.ResetMachineItem = false;
            Global.ResetSampleStd = false ;
            Global.ResetLaws = false;
            Global.Refusetask = false;
            Global.ManageTask = false;

            for (int j = 0; j < Aright.Length; j++)
            {
                if (Aright[j] == "1301-1")//检测
                {
                    Global.Detectionbtn = true;
                }
               
                if (Aright[j] == "1301-2")//重检
                {
                    Global.redetectionbtn = true;
                }

                if (Aright[j] == "1303")//数据中心
                {
                    Global.DataCenterbtn = true;
                }

                if (Aright[j] == "1304")//拒绝任务
                {
                    Global.Refusetask = true;
                }

                if (Aright[j] == "1305")//接收任务
                {
                    Global.Receivetask = true;
                }
                
                if (Aright[j] == "1306")//设置
                {
                    Global.Setting = true;
                }
              
                if (Aright[j] == "1303-1")//检测项目
                {
                    Global.testitembtn = true;
                }

                if (Aright[j] == "1303-2")//检测标准
                {
                    Global.teststandbtn = true;
                }

                if (Aright[j] == "1303-3")//食品种类
                {
                    Global.foodtypebtn = true;
                }
              
                if (Aright[j] == "1303-4")//仪器检测项目
                {
                    Global.machineitembtn = true;
                }
               

                if (Aright[j] == "1303-5")//样品检测标准
                {
                    Global.samplestdbtn = true;
                }
              
                if (Aright[j] == "1303-6")//法律法规
                {
                    Global.lawsbtn = true;
                }
                
                if (Aright[j] == "1303-7")//检测数据、记录
                {
                    Global.testdatabtn = true;
                }
               
                if (Aright[j] == "1306-1")//系统升级
                {
                    Global.sysupdatebtn = true;
                }
               
                if (Aright[j] == "1303-8")//被检单位
                {
                    Global.checkcompanuy = true;
                }
                
                if (Aright[j] == "1328")//手动检测
                {
                    Global.ShoudongTest = true;
                }
                
                if (Aright[j] == "1303-9")//打印
                {
                    Global.print = true;
                }
               
                if (Aright[j] == "1303-10")//上传
                {
                    Global.Uploadd = true;
                }
               
                if (Aright[j] == "1303-11")//编辑
                {
                    Global.edition = true;
                }
               
                if (Aright[j] == "1303-12")//导入
                {
                    Global.Input = true;
                }
              
                if (Aright[j] == "1303-13")//导出
                {
                    Global.output = true;
                }
                
                if (Aright[j] == "1303-14")//生成报告
                {
                    Global.GenerateReport = true;
                }
               
                if (Aright[j] == "1303-15")//打印报告
                {
                    Global.PrintReport = true;
                }
                else if (Aright[j] == "1328-1")//视频教程
                {
                    Global.vedioTV = true;
                }
                else if (Aright[j] == "1328-2")//操作说明
                {
                    Global.Instruction = true;
                }
                else if (Aright[j] == "1303-16")//被检单位重置
                {
                    Global.ResetCompany = true;
                }
                else if (Aright[j] == "1303-17")//检测项目重置
                {
                    Global.ResetItem = true;
                }
                else if (Aright[j] == "1303-18")//检测标准重置
                {
                    Global.ResetStandard = true;
                }
                else if (Aright[j] == "1303-19")//食品种类重置
                {
                    Global.ResetSampleType = true;
                }
                else if (Aright[j] == "1303-110")//仪器检测项目重置
                {
                    Global.ResetMachineItem = true;
                }
                else if (Aright[j] == "1303-111")//样品检测标准重置
                {
                    Global.ResetSampleStd = true;
                }
                else if (Aright[j] == "1303-112")//法律法规重置
                {
                    Global.ResetLaws = true;
                }
                else if (Aright[j] == "1377")//任务管理
                {
                    Global.ManageTask = true;
                }
                
            }
        }
    }
}