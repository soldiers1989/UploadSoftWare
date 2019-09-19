using System;
using System.Data;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Security;
using System.Windows.Forms;
using FoodClient;

namespace DY.FoodClientLib
{
    /// <summary>
    /// ����������,ԭ����:PublicOperation 
    /// 2011-06-16,public�ǹؼ���,�����������ƣ��������淶��
    /// �����޸�ΪCommonOperation
    /// </summary>
    public class CommonOperation
    {
        /// <summary>
        /// ����������
        /// </summary>
        private CommonOperation()
        {

        }

        /// <summary>
        /// ���Ѿ����ڵİ汾�ж�
        /// </summary>
        /// <returns></returns>
        public static string ExistVersion()
        {
            string errMsg = string.Empty;
            string sql = "SELECT OPTVALUE FROM TSYSOPT WHERE SYSCODE='030102'";
            object obj = DataBase.GetOneValue(sql, out errMsg);
            if (obj == null)
            {
                return "false";
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// ��ȡ���ǰ׺,Ч���д��Ľ�
        /// </summary>
        /// <param name="format">��Ÿ�ʽ</param>
        /// <param name="companyCode">��λ����</param>
        /// <param name="userCode">�û�����</param>
        /// <param name="nLen">����</param>
        /// <returns></returns>
        public static string GetPreCode(string format, string companyCode, string userCode, out int nLen)
        {
            string areaCode = string.Empty;
            string resultStr = string.Empty;
            string splitStr = "+";
            char[] splitChar = splitStr.ToCharArray();
            string[] OkStr = format.Split(splitChar);
            int LevelNum = OkStr.GetUpperBound(0);
            nLen = 0;
            for (int i = 0; i <= LevelNum; i++)
            {
                if (!OkStr[i].Substring(0, 1).Equals("{"))
                {
                    resultStr += OkStr[i];
                }
                else
                {
                    switch (OkStr[i].Substring(0, 3).ToUpper())
                    {
                        //ʱ�䣺yy-2λ��ݣ�yyyy-4λ���
                        //M-1λ�·ݣ�MM-2λ�·�
                        //d-1λ���ڣ�dd-2λ����
                        //��ͬ�����ڸ�ʽ�仯

                        case "{D:":
                            if (OkStr[i].Length > 4)
                            {
                                //����д��Y�滻��Сд��y����Сд��m�滻�ɴ�д��m������д��D�滻��Сд��d
                                string sinput = OkStr[i].Substring(3, OkStr[i].Length - 4);
                                sinput = sinput.Replace("Y", "y");
                                sinput = sinput.Replace("m", "M");
                                sinput = sinput.Replace("D", "d");
                                resultStr += DateTime.Now.ToString(sinput);
                            }
                            break;

                        case "{N:":
                            if (OkStr[i].Length > 4)
                            {
                                nLen = int.Parse(OkStr[i].Substring(3, OkStr[i].Length - 4));
                            }
                            break;

                        //��ǰ��λ���
                        case "{C}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += companyCode;
                            }
                            break;

                        //��ǰ���Ա���
                        case "{U}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += userCode;
                            }
                            break;

                        //��ǰ�������
                        case "{A}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += areaCode;
                            }
                            break;
                    }
                }
            }
            return resultStr;
        }

        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        /// <param name="format">��ʽ��</param>
        /// <param name="maxNum"></param>
        /// <param name="companyCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public static string GetCodeString(string format, int maxNum, string companyCode, string userCode)
        {
            string areaCode = string.Empty;
            string resultStr = string.Empty;
            string splitStr = "+";
            char[] splitChar = splitStr.ToCharArray();
            string[] OkStr = format.Split(splitChar);
            int LevelNum = OkStr.GetUpperBound(0);
            for (int i = 0; i <= LevelNum; i++)
            {
                if (!OkStr[i].Substring(0, 1).Equals("{"))
                {
                    resultStr = resultStr + OkStr[i];
                }
                else
                {
                    switch (OkStr[i].Substring(0, 3).ToUpper())
                    {
                        //ʱ�䣺yy-2λ��ݣ�yyyy-4λ���
                        //      M-1λ�·ݣ�MM-2λ�·�
                        //      d-1λ���ڣ�dd-2λ����
                        //��ͬ�����ڸ�ʽ�仯
                        case "{D:":
                            if (OkStr[i].Length > 4)
                            {
                                //����д��Y�滻��Сд��y����Сд��m�滻�ɴ�д��m������д��D�滻��Сд��d
                                string sinput = OkStr[i].Substring(3, OkStr[i].Length - 4);
                                sinput = sinput.Replace("Y", "y");
                                sinput = sinput.Replace("m", "M");
                                sinput = sinput.Replace("D", "d");
                                resultStr += DateTime.Now.ToString(OkStr[i].Substring(3, OkStr[i].Length - 4));
                            }
                            break;
                        case "{N:":
                            if (OkStr[i].Length > 4)
                            {
                                resultStr += (maxNum + 1).ToString().PadLeft(int.Parse(OkStr[i].Substring(3, OkStr[i].Length - 4)), '0');
                            }
                            break;
                        //��ǰ��λ���
                        case "{C}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += companyCode;
                            }
                            break;
                        //��ǰ���Ա���
                        case "{U}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += userCode;
                            }
                            break;
                        //��ǰ�������
                        case "{A}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += areaCode;
                            }
                            break;
                    }
                }
            }
            return resultStr;
        }

        /// <summary>
        /// ��ȡ������Ϣ��ÿ��������Ӧ��ͬ�ģ����룬�����Ŀ����׼���˿�
        /// </summary>
        /// <param name="machinModel"></param>
        public static void GetMachineSetting(string machinModel)
        {
            string query = string.Empty;
            DataTable dtbl = null;
            if (string.IsNullOrEmpty(machinModel))
            {
                MessageBox.Show("ϵͳ�������������ô���", "���ش���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            machinModel = machinModel.ToUpper();
            string temp = string.Empty;
            //�޸����ַ�ʽ���Ƚ�����չ��������ʱ�����޸Ĵ˴� ֻҪ���չ�����д
            //�磺������������д�������ͺŴ���:(DY3900DY)����չ���:RS232DY3900DY ��
            if (machinModel.IndexOf("DYRSY2") >= 0)//����Ǿɰ�����ˮ���� ���⴦��
            {
                query = "Protocol='RS232ˮ���ǲ��' AND MachineModel='DYRSY2'";
            }
            else if (machinModel.IndexOf("0DY") > 0)//������°�DY3000(����DY)ϵ����ɰ�DY3000����
            {
                temp = "DY3000DY";
                if (machinModel.Equals("LZ3000DY") || machinModel.Equals("TL310DY"))
                {
                    query = string.Format("Protocol='RS232{0}' AND MachineModel='{1}'", temp, machinModel.Replace("DY", ""));
                }
                else
                {
                    query = string.Format("Protocol='RS232{0}' AND MachineModel='DY{1}'", temp, machinModel.Replace("DY", ""));
                }
            }
            else if (machinModel.IndexOf("0LD") > 0)//������׶�ϵ�а汾��:DY5000LD,DY5500LD
            {
                temp = "DY5000LD";
                query = string.Format("Protocol='RS232{0}' AND MachineModel='{1}'", temp, machinModel.Replace("LD", ""));
            }
            else if (machinModel.Equals("LZ4000LZ"))
            {
                query = string.Format("Protocol='RS232LZ4000TDY' AND MachineModel='LZ4000'");
            }
            else if (machinModel.Equals("LZ4000TLZ"))
            {
                query = string.Format("Protocol='RS232LZ4000TDY' AND MachineModel='LZ4000T'");
            }
            else
            {
                query = string.Format("Protocol='RS232{0}' AND MachineModel='{0}'", machinModel);
            }
            clsMachineOpr bll = new clsMachineOpr();
            dtbl = bll.GetAsDataTable(query, "SysCode", 2);

            if (bll == null || dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("ϵͳ�������������ô���", "���ش���", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int i = 0;
                ShareOption.DefaultMachineCode = dtbl.Rows[i]["SysCode"].ToString();
                ShareOption.DefaultCheckItemCode = dtbl.Rows[i]["LinkStdCode"].ToString();
                ShareOption.ComPort = "COM" + dtbl.Rows[i]["LinkComNo"].ToString() + ":";
                ShareOption.DefaultLimitValue = Convert.ToDecimal(dtbl.Rows[i]["TestValue"].ToString());
            }
        }

        /// <summary>
        /// ���л���
        /// </summary>
        /// <param name="iType"></param>
        public static void RunExeCache(int iType)
        {
            clsDistrictOpr oprDistrict = new clsDistrictOpr();
            clsUserUnitOpr oprUserUnit = new clsUserUnitOpr();
            clsUserInfoOpr oprUserInfo = new clsUserInfoOpr();
            switch (iType)
            {
                case 1:
                    ShareOption.DtblDistrict = oprDistrict.GetAsDataTable("IsLock=false", "SysCode", 1);
                    break;
                case 2:
                    ShareOption.DtblCheckCompany = oprUserUnit.GetAsDataTable("IsLock=false", "SysCode", 1);
                    break;
                case 3:
                    ShareOption.DtblChecker = oprUserInfo.GetAsDataTable("IsLock=false", "UserCode", 1);
                    break;
                case 10:
                    ShareOption.DtblDistrict = oprDistrict.GetAsDataTable("IsLock=false", "SysCode", 1);
                    ShareOption.DtblCheckCompany = oprUserUnit.GetAsDataTable("IsLock=false", "SysCode", 1);
                    ShareOption.DtblChecker = oprUserInfo.GetAsDataTable("IsLock=false", "UserCode", 1);//�����
                    break;
            }
            ShareOption.IsRunCache = true;
        }

        /// <summary>
        /// ���ø��ֱ�������
        /// </summary>
        public static void GetTitleSet()
        {
            DataTable dtbl = null;
            ShareOption.AreaTitle = "������֯";// "�����г�";
            ShareOption.NameTitle = "�ܼ���/��λ";
            ShareOption.DomainTitle = "����/����/���ƺ�";// "����/����/���ƺ�";
            ShareOption.SampleTitle = "��Ʒ";//���� "��Ʒ����";
            try
            {
                clsCheckComTypeOpr bll = new clsCheckComTypeOpr();
                dtbl = bll.GetAsDataTable(string.Empty, "ID", 2);
                object obj = null;
                if (dtbl != null && dtbl.Rows.Count >= 1)
                {
                    obj = dtbl.Rows[0]["AreaTitle"];
                    if (obj != null && obj.ToString().Trim() != string.Empty)
                    {
                        ShareOption.AreaTitle = obj.ToString();
                    }
                    obj = dtbl.Rows[0]["NameTitle"];
                    if (obj != null && obj.ToString().Trim() != string.Empty)
                    {
                        ShareOption.NameTitle = obj.ToString();
                    }
                    obj = dtbl.Rows[0]["DomainTitle"];
                    if (obj != null && obj.ToString().Trim() != string.Empty)
                    {
                        ShareOption.DomainTitle = obj.ToString();
                    }
                    obj = dtbl.Rows[0]["SampleTitle"];
                    if (obj != null && obj.ToString().Trim() != string.Empty)
                    {
                        ShareOption.SampleTitle = obj.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ô��ڴ���" + ex.Message);
            }
        }

        /// <summary>
        /// ����J2EE�ӿڲ�����
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        /// <param name="sign">��ʶ</param>
        /// <returns></returns>
        public static DataSet GetJ2EEData(string stdCode, string districtCode, string sign)
        {
            DataSet dst = null;
            FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
            ws.Url = ShareOption.SysServerIP;
            string ret = ws.GetDataDriverBySign(ShareOption.SystemVersion, districtCode, stdCode, ShareOption.SysServerID, FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPass, "MD5").ToString(), sign);

            if (ret.Length >= 10 && ret.Substring(0, 10).Equals("errorInfo:"))
            {
                throw new Exception("ͬ������ʧ�ܣ�ʧ��ԭ��" + ret.Substring(10, ret.Length - 10));
            }
            else
            {
                dst = new DataSet();
                using (StringReader sr = new StringReader(ret))
                {
                    dst.ReadXml(sr);
                }
            }
            return dst;
        }

        /// <summary>
        /// ����ʳƷ����
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadFoodClass(string stdCode, string districtCode)
        {
            string sign = "FoodClass";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];

            string delErr = string.Empty;
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();


            //ʳƷ����
            clsFoodClassOpr bll = new clsFoodClassOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "������Ʒ��������";
            }
            int len = dtbl.Rows.Count;
            clsFoodClass foodmodel = new clsFoodClass();
            for (int i = 0; i < len; i++)
            {
                err = string.Empty;

                foodmodel.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                foodmodel.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                foodmodel.Name = dtbl.Rows[i]["Name"].ToString();
                foodmodel.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                foodmodel.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                foodmodel.CheckItemCodes = dtbl.Rows[i]["CheckItemCodes"].ToString();
                foodmodel.CheckItemValue = dtbl.Rows[i]["CheckItemValue"].ToString();
                foodmodel.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                foodmodel.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                foodmodel.Remark = dtbl.Rows[i]["Remark"].ToString();
                bll.Insert(foodmodel, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.Append(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}����Ʒ��������", len.ToString());
        }

        /// <summary>
        /// ���ؼ��㵥λ���
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadCheckComTypeOpr(string stdCode, string districtCode)
        {
            string sign = "CheckComTypeOpr";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];

            string delErr = string.Empty;
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            clsCheckComTypeOpr bll = new clsCheckComTypeOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "���޼�����������";
            }
            int len = dtbl.Rows.Count;
            clsCheckComType model = new clsCheckComType();

            for (int i = 0; i < len; i++)
            {
                err = string.Empty;

                model.TypeName = dtbl.Rows[i]["TypeName"].ToString();
                model.NameCall = dtbl.Rows[i]["NameCall"].ToString();
                model.AreaCall = dtbl.Rows[i]["AreaCall"].ToString();
                model.VerType = dtbl.Rows[i]["VerType"].ToString();
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.ComKind = dtbl.Rows[i]["ComKind"].ToString();

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.Append(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}��������������", len.ToString());
        }

        /// <summary>
        /// ���ؼ���׼����
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadStandardType(string stdCode, string districtCode)
        {
            string sign = "StandardType";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            clsStandardTypeOpr bll = new clsStandardTypeOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "���ޱ�׼��������";
            }
            int len = dtbl.Rows.Count;
            clsStandardType model = new clsStandardType();

            for (int i = 0; i < len; i++)
            {
                err = string.Empty;

                model.StdName = dtbl.Rows[i]["StdName"].ToString();
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.Remark = string.Empty;

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.Append(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}������׼�������", len.ToString());
        }

        /// <summary>
        /// ���ؼ���׼
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadStandard(string stdCode, string districtCode)
        {
            string sign = "Standard";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            clsStandardOpr bll = new clsStandardOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "���޼���׼����";
            }
            int len = dtbl.Rows.Count;
            clsStandard model = new clsStandard();

            for (int i = 0; i < len; i++)
            {
                err = string.Empty;
                model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                model.StdDes = dtbl.Rows[i]["StdDes"].ToString();
                model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                model.StdInfo = dtbl.Rows[i]["StdInfo"].ToString();
                model.StdType = dtbl.Rows[i]["StdType"].ToString();
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.Remark = dtbl.Rows[i]["Remark"].ToString();

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.AppendLine(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}������׼����", len.ToString());
        }

        /// <summary>
        /// ���ؼ����Ŀ
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadCheckItem(string stdCode, string districtCode)
        {
            string sign = "CheckItem";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;
            clsCheckItemOpr bll = new clsCheckItemOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            clsCheckItem model = new clsCheckItem();
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "���޼����Ŀ����";
            }
            int len = dtbl.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                err = string.Empty;
                model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                model.ItemDes = dtbl.Rows[i]["ItemDes"].ToString();
                model.CheckType = dtbl.Rows[i]["CheckType"].ToString();
                model.StandardCode = dtbl.Rows[i]["StandardCode"].ToString();
                model.StandardValue = dtbl.Rows[i]["StandardValue"].ToString();
                model.Unit = dtbl.Rows[i]["Unit"].ToString();
                model.DemarcateInfo = dtbl.Rows[i]["DemarcateInfo"].ToString();
                model.TestValue = dtbl.Rows[i]["TestValue"].ToString();
                model.OperateHelp = dtbl.Rows[i]["OperateHelp"].ToString();
                model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.Remark = dtbl.Rows[i]["Remark"].ToString();

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.AppendLine(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}�������Ŀ����", len.ToString());
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadDistrict(string stdCode, string districtCode)
        {
            string sign = "District";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            clsDistrictOpr bll = new clsDistrictOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "����������������";
            }
            int len = dtbl.Rows.Count;

            clsDistrict model = new clsDistrict();

            for (int i = 0; i < len; i++)
            {
                err = string.Empty;
                model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                model.Name = dtbl.Rows[i]["Name"].ToString();
                model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                model.DistrictIndex = Convert.ToInt64(dtbl.Rows[i]["DistrictIndex"]);
                model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                model.IsLocal = Convert.ToBoolean(dtbl.Rows[i]["IsLocal"]);
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.Remark = dtbl.Rows[i]["Remark"].ToString();

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.AppendLine(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}��������������", len.ToString());
        }


        /// <summary>
        /// ���ز�Ʒ����
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadProduceArea(string stdCode, string districtCode)
        {
            string sign = "ProduceArea";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            clsProduceAreaOpr bll = new clsProduceAreaOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "���޲�Ʒ��������";
            }
            int len = dtbl.Rows.Count;

            clsProduceArea model = new clsProduceArea();

            for (int i = 0; i < len; i++)
            {
                err = string.Empty;
                model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                model.Name = dtbl.Rows[i]["Name"].ToString();
                model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                model.DistrictIndex = Convert.ToInt64(dtbl.Rows[i]["DistrictIndex"]);
                model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                model.IsLocal = Convert.ToBoolean(dtbl.Rows[i]["IsLocal"]);
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.Remark = string.Empty;

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.AppendLine(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}����Ʒ��������", len.ToString());
        }

        /// <summary>
        /// ���ص�λ���
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadCompanyKind(string stdCode, string districtCode)
        {
            string sign = "CompanyKind";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            clsCompanyKindOpr bll = new clsCompanyKindOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "���޵�λ�������";
            }
            int len = dtbl.Rows.Count;
            clsCompanyKind model = new clsCompanyKind();
            for (int i = 0; i < len; i++)
            {
                err = string.Empty;

                model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                model.Name = dtbl.Rows[i]["Name"].ToString();
                model.CompanyProperty = dtbl.Rows[i]["CompanyProperty"].ToString();
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.Remark = dtbl.Rows[i]["Remark"].ToString();
                model.Ksign = dtbl.Rows[i]["Ksign"].ToString();

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.AppendLine(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}����λ�������", len.ToString());
        }

        /// <summary>
        /// ���ص�λ��Ϣ
        /// </summary>
        /// <param name="stdCode">��׼����</param>
        /// <param name="districtCode">�������</param>
        public static string DownloadCompany(string stdCode, string districtCode)
        {
            string sign = "Company";
            DataTable dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;
            clsCompanyOpr bll = new clsCompanyOpr();
            #region
            //if (ShareOption.SystemVersion.Equals(ShareOption.LocalBaseVersion))//������
            //{
            //    bll.Delete("IsReadOnly=true", out delErr);
            //}
            //else
            //{
            //    bll.Delete(string.Format("IsReadOnly=true AND Property='{0}'", ShareOption.CompanyProperty1), out delErr);//��ҵ��
            //}
            //��ɾ��������λ��ֻɾ�����쵥λ
            //bll.Delete(string.Format("IsReadOnly=true AND Property='{0}'", ShareOption.CompanyProperty1), out delErr);
            #endregion
            bll.Delete("IsReadOnly=true and TSign<>'������'", out delErr);
            sb.Append(delErr);
            if (dtbl == null)
            {
                return "���ޱ��쵥λ����";
            }
            int len = dtbl.Rows.Count;
            clsCompany model = new clsCompany();
            for (int i = 0; i < len; i++)
            {
                #region
                //model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                //model.OtherCodeInfo = dtbl.Rows[i]["OtherCodeInfo"].ToString();
                //model.ShortName = dtbl.Rows[i]["ShortName"].ToString();
                //model.CAllow = dtbl.Rows[i]["LICENSEID"].ToString();
                //model.ISSUEAGENCY = dtbl.Rows[i]["ISSUEAGENCY"].ToString();
                //model.ISSUEDATE = dtbl.Rows[i]["ISSUEDATE"].ToString();
                //model.PERIODSTART = dtbl.Rows[i]["PERIODSTART"].ToString();
                //model.PERIODEND = dtbl.Rows[i]["PERIODEND"].ToString();
                //model.VIOLATENUM = dtbl.Rows[i]["VIOLATENUM"].ToString();
                //model.LONGITUDE = dtbl.Rows[i]["LONGITUDE"].ToString();
                //model.LATITUDE = dtbl.Rows[i]["LATITUDE"].ToString();
                //model.SCOPE = dtbl.Rows[i]["SCOPE"].ToString();
                //model.PUNISH = dtbl.Rows[i]["PUNISH"].ToString(); 
                #endregion
                err = string.Empty;
                model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                model.CompanyID = dtbl.Rows[i]["CompanyID"].ToString();
                model.FullName = dtbl.Rows[i]["FullName"].ToString();
                model.DisplayName = dtbl.Rows[i]["DisplayName"].ToString();

                model.Property = dtbl.Rows[i]["Property"].ToString();
                model.KindCode = dtbl.Rows[i]["KindCode"].ToString();
                model.RegCapital = Convert.ToInt64(dtbl.Rows[i]["RegCapital"]);
                model.Unit = dtbl.Rows[i]["Unit"].ToString();
                model.Incorporator = dtbl.Rows[i]["Incorporator"].ToString();
                if (!string.IsNullOrEmpty(dtbl.Rows[i]["RegDate"].ToString()))
                {
                    model.RegDate = Convert.ToDateTime(dtbl.Rows[i]["RegDate"]);
                }
                model.DistrictCode = dtbl.Rows[i]["DistrictCode"].ToString();
                model.PostCode = dtbl.Rows[i]["PostCode"].ToString();
                model.Address = dtbl.Rows[i]["Address"].ToString();
                model.LinkMan = dtbl.Rows[i]["LinkMan"].ToString();
                model.LinkInfo = dtbl.Rows[i]["LinkInfo"].ToString();
                model.CreditLevel = dtbl.Rows[i]["CreditLevel"].ToString();
                model.CreditRecord = dtbl.Rows[i]["CreditRecord"].ToString();
                model.ProductInfo = dtbl.Rows[i]["ProductInfo"].ToString();
                model.OtherInfo = dtbl.Rows[i]["OtherInfo"].ToString();
                model.FoodSafeRecord = dtbl.Rows[i]["FoodSafeRecord"].ToString();
                model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.Remark = dtbl.Rows[i]["Remark"].ToString();
                model.TSign = dtbl.Rows[i]["Sign"].ToString();

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.AppendLine(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}����λ��Ϣ����", len.ToString());
        }

        public static string DownloadProprietors(string stdCode, string districtCode)
        {
            string sign = "DEALER";
            DataTable dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            clsProprietorsOpr bll = new clsProprietorsOpr();
            bll.Delete("IsReadOnly=true ", out delErr);
            sb.Append(delErr);
            if (dtbl == null)
            {
                return "���޾�Ӫ������";
            }
            int len = dtbl.Rows.Count;
            clsProprietors model = new clsProprietors();
            for (int i = 0; i < len; i++)
            {
                err = string.Empty;
                model.Cdcode = dtbl.Rows[i]["Cdcode"].ToString();
                model.Cdbuslicence = dtbl.Rows[i]["Cdbuslicence"].ToString();
                model.Ciid = dtbl.Rows[i]["Ciid"].ToString();
                model.Ciname = dtbl.Rows[i]["Ciname"].ToString();
                model.Cdname = dtbl.Rows[i]["Cdname"].ToString();
                model.Cdcardid = dtbl.Rows[i]["Cdcardid"].ToString();
                model.DisplayName = dtbl.Rows[i]["DisplayName"].ToString();
                model.Property = dtbl.Rows[i]["Property"].ToString();
                model.KindCode = dtbl.Rows[i]["KindCode"].ToString();
                model.RegCapital = dtbl.Rows[i]["RegCapital"].ToString();
                model.Unit = dtbl.Rows[i]["Unit"].ToString();
                model.Incorporator = dtbl.Rows[i]["Incorporator"].ToString();
                if (!string.IsNullOrEmpty(dtbl.Rows[i]["RegDate"].ToString()))
                {
                    model.RegDate = Convert.ToDateTime(dtbl.Rows[i]["RegDate"]);
                }
                model.DistrictCode = dtbl.Rows[i]["DistrictCode"].ToString();
                model.PostCode = dtbl.Rows[i]["PostCode"].ToString();
                model.Address = dtbl.Rows[i]["Address"].ToString();
                model.LinkMan = dtbl.Rows[i]["LinkMan"].ToString();
                model.LinkInfo = dtbl.Rows[i]["LinkInfo"].ToString();
                model.CreditLevel = dtbl.Rows[i]["CreditLevel"].ToString();
                model.CreditRecord = dtbl.Rows[i]["CreditRecord"].ToString();
                model.ProductInfo = dtbl.Rows[i]["ProductInfo"].ToString();
                model.OtherInfo = dtbl.Rows[i]["OtherInfo"].ToString();
                model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                model.FoodSafeRecord = dtbl.Rows[i]["FoodSafeRecord"].ToString();
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.Remark = dtbl.Rows[i]["Remark"].ToString();

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.AppendLine(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("�Ѿ��ɹ�����{0}����Ӫ������", len.ToString());
        }

        /// <summary>
        /// ��������ͬ��
        /// </summary>
        /// <param name="stdCode">�������</param>
        /// <param name="sumError">������Ϣ</param>
        public static string DownloadAll(string stdCode, string districtCode, out string sumError, string sign)
        {
            sumError = string.Empty;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(DownloadFoodClass(stdCode, districtCode));//��Ʒ���

                sb.AppendLine(DownloadCheckComTypeOpr(stdCode, districtCode));//��ⵥλ���

                sb.AppendLine(DownloadStandardType(stdCode, districtCode));//����׼����

                sb.AppendLine(DownloadStandard(stdCode, districtCode));//����׼

                sb.AppendLine(DownloadCheckItem(stdCode, districtCode)); //�����Ŀ

                sb.AppendLine(DownloadCompanyKind(stdCode, districtCode)); //��λ���

                sb.AppendLine(DownloadDistrict(stdCode, districtCode)); //��֯����

                sb.AppendLine(DownloadProduceArea(stdCode, districtCode)); //����

                sb.AppendLine(DownloadCompany(stdCode, districtCode));//��λ��Ϣ

                sb.AppendLine(DownloadProprietors(stdCode, districtCode));//��Ӫ����Ϣ

                return sb.ToString();

                #region ע��
                //������
                //				clsCheckTypeOpr Opr2=new clsCheckTypeOpr();
                //				Opr2.Delete("IsReadOnly=true",out sDelErr);
                //
                //				dt=dsRt.Tables["CheckType"];
                //				for(int i=0;i<dt.Rows.Count;i++)
                //				{
                //					string sErr="";
                //
                //					clsCheckType checkType=new clsCheckType();
                //					checkType.Name=dt.Rows[i]["Name"].ToString();
                //					checkType.IsReadOnly=Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //					checkType.IsLock=Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //					checkType.Remark=dt.Rows[i]["Remark"].ToString();
                //
                //					Opr2.Insert(checkType,out sErr);
                //					if(!sErr.Equals(""))
                //					{
                //						sSumError+=sErr + "\r\n";
                //						continue;
                //					}
                //				}
                //				//��⼶��
                //				clsCheckLevelOpr Opr8=new clsCheckLevelOpr();
                //				Opr8.Delete("IsReadOnly=true",out sDelErr);
                //
                //				dt=dsRt.Tables["CheckLevel"];
                //				for(int i=0;i<dt.Rows.Count;i++)
                //				{
                //					string sErr="";
                //
                //					clsCheckLevel checkLevel=new clsCheckLevel();
                //					checkLevel.CheckLevel=dt.Rows[i]["CheckLevel"].ToString();
                //					checkLevel.IsReadOnly=Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //					checkLevel.IsLock=Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //					checkLevel.Remark=dt.Rows[i]["Remark"].ToString();
                //
                //					Opr8.Insert(checkLevel,out sErr);
                //					if(!sErr.Equals(""))
                //					{
                //						sSumError+=sErr + "\r\n";
                //						continue;
                //					}
                //				}		

                //				//���ü���
                //				clsCreditLevelOpr Opr9=new clsCreditLevelOpr();
                //				Opr9.Delete("IsReadOnly=true",out sDelErr);
                //
                //				dt=dsRt.Tables["CreditLevel"];
                //				for(int i=0;i<dt.Rows.Count;i++)
                //				{
                //					string sErr="";
                //
                //					clsCreditLevel creditLevel=new clsCreditLevel();
                //					creditLevel.CreditLevel=dt.Rows[i]["CreditLevel"].ToString();
                //					creditLevel.IsReadOnly=Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //					creditLevel.IsLock=Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //					creditLevel.Remark=dt.Rows[i]["Remark"].ToString();
                //
                //					Opr9.Insert(creditLevel,out sErr);
                //					if(!sErr.Equals(""))
                //					{
                //						sSumError+=sErr + "\r\n";
                //						continue;
                //					}
                //				}			

                //��˾����
                //				clsCompanyPropertyOpr Opr11=new clsCompanyPropertyOpr();
                //				Opr11.Delete("IsReadOnly=true",out sDelErr);
                //
                //				dt=dsRt.Tables["CompanyProperty"];
                //				for(int i=0;i<dt.Rows.Count;i++)
                //				{
                //					string sErr="";
                //
                //					clsCompanyProperty companyProperty=new clsCompanyProperty();
                //					companyProperty.CompanyProperty=dt.Rows[i]["CompanyProperty"].ToString();
                //					companyProperty.IsReadOnly=Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //					companyProperty.IsLock=Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //					companyProperty.Remark=dt.Rows[i]["Remark"].ToString();
                //
                //					Opr11.Insert(companyProperty,out sErr);
                //					if(!sErr.Equals(""))
                //					{
                //						sSumError+=sErr + "\r\n";
                //						continue;
                //					}
                //				}		
                #endregion

                #region DotNET�ӿڰ汾���ݲ�֧��
                //else if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
                //{

                //FoodClient.ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
                //ws.Url = ShareOption.SysServerIP;
                //DataSet dsRt = ws.GetDataDriver(ShareOption.SystemVersion, districtCode, stdCode, ShareOption.SysServerID, ShareOption.SysServerPass, out sSumError);
                //if (!sSumError.Equals(""))
                //{
                //    sSumError = "ͬ������ʧ�ܣ�ʧ��ԭ��" + sSumError;
                //    return;
                //}
                //DataTable dt = null;
                //string sDelErr = string.Empty;

                ////ʳƷ����
                //clsFoodClassOpr Opr1 = new clsFoodClassOpr();
                //Opr1.Delete("IsReadOnly=true", out sDelErr);
                //dt = dsRt.Tables["FoodClass"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsFoodClass foodClass = new clsFoodClass();
                //    foodClass.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    foodClass.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    foodClass.Name = dt.Rows[i]["Name"].ToString();
                //    foodClass.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    foodClass.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    foodClass.CheckItemCodes = dt.Rows[i]["CheckItemCodes"].ToString();
                //    foodClass.CheckItemValue = dt.Rows[i]["CheckItemValue"].ToString();
                //    foodClass.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    foodClass.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    foodClass.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr1.Insert(foodClass, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////������
                //clsCheckTypeOpr Opr2 = new clsCheckTypeOpr();
                //Opr2.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CheckType"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCheckType checkType = new clsCheckType();
                //    checkType.Name = dt.Rows[i]["Name"].ToString();
                //    checkType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    checkType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    checkType.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr2.Insert(checkType, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////���㵥λ���
                //clsCheckComTypeOpr Opr18 = new clsCheckComTypeOpr();
                //Opr18.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CheckComTypeOpr"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCheckComType checkcomType = new clsCheckComType();
                //    checkcomType.TypeName = dt.Rows[i]["TypeName"].ToString();
                //    checkcomType.NameCall = dt.Rows[i]["NameCall"].ToString();
                //    checkcomType.AreaCall = dt.Rows[i]["AreaCall"].ToString();
                //    checkcomType.VerType = dt.Rows[i]["VerType"].ToString();
                //    checkcomType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    checkcomType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    checkcomType.ComKind = dt.Rows[i]["ComKind"].ToString();

                //    Opr18.Insert(checkcomType, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////����׼����
                //clsStandardTypeOpr Opr7 = new clsStandardTypeOpr();
                //Opr7.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["StandardType"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsStandardType standardType = new clsStandardType();
                //    standardType.StdName = dt.Rows[i]["StdName"].ToString();
                //    standardType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    standardType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    standardType.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr7.Insert(standardType, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////����׼
                //clsStandardOpr Opr6 = new clsStandardOpr();
                //Opr6.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["Standard"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsStandard standard = new clsStandard();
                //    standard.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    standard.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    standard.StdDes = dt.Rows[i]["StdDes"].ToString();
                //    standard.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    standard.StdInfo = dt.Rows[i]["StdInfo"].ToString();
                //    standard.StdType = dt.Rows[i]["StdType"].ToString();
                //    standard.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    standard.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    standard.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr6.Insert(standard, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////�����Ŀ
                //clsCheckItemOpr Opr3 = new clsCheckItemOpr();
                //Opr3.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CheckItem"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCheckItem checkItem = new clsCheckItem();
                //    checkItem.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    checkItem.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    checkItem.ItemDes = dt.Rows[i]["ItemDes"].ToString();
                //    checkItem.CheckType = dt.Rows[i]["CheckType"].ToString();
                //    checkItem.StandardCode = dt.Rows[i]["StandardCode"].ToString();
                //    checkItem.StandardValue = dt.Rows[i]["StandardValue"].ToString();
                //    checkItem.Unit = dt.Rows[i]["Unit"].ToString();
                //    checkItem.DemarcateInfo = dt.Rows[i]["DemarcateInfo"].ToString();
                //    checkItem.TestValue = dt.Rows[i]["TestValue"].ToString();
                //    checkItem.OperateHelp = dt.Rows[i]["OperateHelp"].ToString();
                //    checkItem.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    checkItem.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    checkItem.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    checkItem.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr3.Insert(checkItem, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////��λ���
                //clsCompanyKindOpr Opr4 = new clsCompanyKindOpr();
                //Opr4.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CompanyKind"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCompanyKind companyKind = new clsCompanyKind();
                //    companyKind.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    companyKind.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    companyKind.Name = dt.Rows[i]["Name"].ToString();
                //    companyKind.CompanyProperty = dt.Rows[i]["CompanyProperty"].ToString();
                //    companyKind.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    companyKind.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    companyKind.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr4.Insert(companyKind, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////��֯����
                //clsDistrictOpr Opr5 = new clsDistrictOpr();
                //Opr5.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["District"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsDistrict district = new clsDistrict();
                //    district.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    district.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    district.Name = dt.Rows[i]["Name"].ToString();
                //    district.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    district.DistrictIndex = Convert.ToInt64(dt.Rows[i]["DistrictIndex"]);
                //    district.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    district.IsLocal = Convert.ToBoolean(dt.Rows[i]["IsLocal"]);
                //    district.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    district.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    district.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr5.Insert(district, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////����
                //clsProduceAreaOpr Opr15 = new clsProduceAreaOpr();
                //Opr15.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["ProduceArea"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsProduceArea ProduceArea = new clsProduceArea();
                //    ProduceArea.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    ProduceArea.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    ProduceArea.Name = dt.Rows[i]["Name"].ToString();
                //    ProduceArea.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    ProduceArea.DistrictIndex = Convert.ToInt64(dt.Rows[i]["DistrictIndex"]);
                //    ProduceArea.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    ProduceArea.IsLocal = Convert.ToBoolean(dt.Rows[i]["IsLocal"]);
                //    ProduceArea.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    ProduceArea.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    ProduceArea.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr15.Insert(ProduceArea, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////��⼶��
                //clsCheckLevelOpr Opr8 = new clsCheckLevelOpr();
                //Opr8.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CheckLevel"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCheckLevel checkLevel = new clsCheckLevel();
                //    checkLevel.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    checkLevel.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    checkLevel.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    checkLevel.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr8.Insert(checkLevel, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////���ü���
                //clsCreditLevelOpr Opr9 = new clsCreditLevelOpr();
                //Opr9.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CreditLevel"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCreditLevel creditLevel = new clsCreditLevel();
                //    creditLevel.CreditLevel = dt.Rows[i]["CreditLevel"].ToString();
                //    creditLevel.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    creditLevel.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    creditLevel.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr9.Insert(creditLevel, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////��˾����
                //clsCompanyPropertyOpr Opr11 = new clsCompanyPropertyOpr();
                //Opr11.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CompanyProperty"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCompanyProperty companyProperty = new clsCompanyProperty();
                //    companyProperty.CompanyProperty = dt.Rows[i]["CompanyProperty"].ToString();
                //    companyProperty.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    companyProperty.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    companyProperty.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr11.Insert(companyProperty, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////��˾
                //clsCompanyOpr Opr12 = new clsCompanyOpr();

                //if (ShareOption.SystemVersion.Equals(ShareOption.LocalBaseVersion))
                //{
                //    Opr12.Delete("IsReadOnly=true", out sDelErr);
                //}
                //else
                //{
                //    Opr12.Delete("IsReadOnly=true And Property='" + ShareOption.CompanyProperty1 + "'", out sDelErr);
                //}
                //dt = dsRt.Tables["Company"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCompany company = new clsCompany();
                //    company.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    company.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    company.CompanyID = dt.Rows[i]["CompanyID"].ToString();
                //    company.OtherCodeInfo = dt.Rows[i]["OtherCodeInfo"].ToString();
                //    company.FullName = dt.Rows[i]["FullName"].ToString();
                //    company.ShortName = dt.Rows[i]["ShortName"].ToString();
                //    company.DisplayName = dt.Rows[i]["DisplayName"].ToString();
                //    company.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    company.Property = dt.Rows[i]["Property"].ToString();
                //    company.KindCode = dt.Rows[i]["KindCode"].ToString();
                //    company.RegCapital = Convert.ToInt64(dt.Rows[i]["RegCapital"]);
                //    company.Unit = dt.Rows[i]["Unit"].ToString();
                //    company.Incorporator = dt.Rows[i]["Incorporator"].ToString();
                //    company.RegDate = Convert.ToDateTime(dt.Rows[i]["RegDate"]);
                //    company.DistrictCode = dt.Rows[i]["DistrictCode"].ToString();
                //    company.PostCode = dt.Rows[i]["PostCode"].ToString();
                //    company.Address = dt.Rows[i]["Address"].ToString();
                //    company.LinkMan = dt.Rows[i]["LinkMan"].ToString();
                //    company.LinkInfo = dt.Rows[i]["LinkInfo"].ToString();
                //    company.CreditLevel = dt.Rows[i]["CreditLevel"].ToString();
                //    company.CreditRecord = dt.Rows[i]["CreditRecord"].ToString();
                //    company.ProductInfo = dt.Rows[i]["ProductInfo"].ToString();
                //    company.OtherInfo = dt.Rows[i]["OtherInfo"].ToString();
                //    company.FoodSafeRecord = dt.Rows[i]["FoodSafeRecord"].ToString();
                //    company.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    company.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    company.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    company.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr12.Insert(company, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}
                //}
                #endregion
            }
            catch (Exception e)
            {
                sumError = "ͬ������ʧ�ܣ�ʧ��ԭ��" + e.Message;
                return string.Empty;
            }
        }


        /// <summary>
        /// ��ȡϵͳ������Ϣ
        /// </summary>
        public static void GetSystemInfo()
        {
            clsSysOptOpr sysBll = new clsSysOptOpr();
            try
            {
                DataTable dtbl = sysBll.GetColumnDataTable(0, "LEN(SysCode)=6 ORDER BY SysCode", "OptValue");
                ShareOption.SysTemperature = Convert.ToDecimal(dtbl.Rows[0]["OptValue"]);//������Ϊ0
                ShareOption.SysHumidity = Convert.ToDecimal(dtbl.Rows[1]["OptValue"]);
                ShareOption.SysUnit = dtbl.Rows[2]["OptValue"].ToString();
                ShareOption.SysAutoLogin = Convert.ToBoolean(dtbl.Rows[3]["OptValue"]);
                ShareOption.SysExitPrompt = Convert.ToBoolean(dtbl.Rows[5]["OptValue"]);
                ShareOption.AllowHandInputCheckUint = Convert.ToBoolean(dtbl.Rows[7]["OptValue"]);
                ShareOption.SysStdCodeSame = Convert.ToBoolean(dtbl.Rows[8]["OptValue"]);
                ShareOption.FormatStrMachineCode = dtbl.Rows[9]["OptValue"].ToString();
                ShareOption.FormatStandardCode = dtbl.Rows[10]["OptValue"].ToString();
                ShareOption.SysTimer1 = Convert.ToDecimal(dtbl.Rows[15]["OptValue"]);
                ShareOption.SysTimer2 = Convert.ToDecimal(dtbl.Rows[16]["OptValue"]);
                ShareOption.SysTimer3 = Convert.ToDecimal(dtbl.Rows[17]["OptValue"]);
                ShareOption.SysTimer4 = Convert.ToDecimal(dtbl.Rows[18]["OptValue"]);
                ShareOption.SysTimerEndPlayWav = dtbl.Rows[19]["OptValue"].ToString();

                //����ϵͳ�汾
                ShareOption.SystemVersion = dtbl.Rows[20]["OptValue"].ToString();
                ShareOption.SystemTitle = dtbl.Rows[22]["OptValue"].ToString();
                //����
                if (dtbl.Rows[28]["OptValue"] != null)//��ʶ�Ƿ񵥻���
                {
                    ShareOption.IsDataLink = Convert.ToBoolean(dtbl.Rows[28]["OptValue"].ToString());
                }

                if (!ShareOption.IsDataLink)//��������
                {
                    ShareOption.SysServerIP = dtbl.Rows[12]["OptValue"].ToString();
                    ShareOption.SysServerID = dtbl.Rows[13]["OptValue"].ToString();
                    ShareOption.SysServerPass = dtbl.Rows[14]["OptValue"].ToString();
                    ShareOption.InterfaceType = dtbl.Rows[29]["OptValue"].ToString();
                }
                //2011-11-18����
                ShareOption.ApplicationTag = dtbl.Rows[30]["OptValue"].ToString();//Ӧ�����Ͱ汾

                //�����ֶ�
                ShareOption.CurDY3000Tag = dtbl.Rows[31]["OptValue"].ToString();//DY3000�汾����
                ShareOption.DY5000Name = dtbl.Rows[32]["OptValue"].ToString();//DY5000�汾����
                ShareOption.ProductionUnitNameTag = dtbl.Rows[33]["OptValue"].ToString();//������λ��ǩ
            }
            catch
            {
                MessageBox.Show("ϵͳ��ʼ�����ô���", "���ش���", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// ����App.config���������ַ�
        /// </summary>
        /// <param name="serverURL">�������������URL</param>
        public static void writeWebServer(string serverURL)
        {
            //����������.config�ļ��Ƿ����
            string keyName = string.Empty;
            if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
            {
                keyName = "FoodClient.localhost.DataSyncService";

            }
            else
            {
                keyName = "FoodClient.ForNet.DataDriver";
            }
            System.Configuration.ConfigurationManager.AppSettings["InterfaceNameSpace"] = keyName;
            System.Configuration.ConfigurationManager.AppSettings[keyName] = serverURL;

            #region
            ////////////////д���´�Ѵ�����ʵ����Ϊ�˸���APP.configһ���ֶ�
            //string strFileNameFullPath=Application.ExecutablePath;
            //string strFileNamePath=Application.StartupPath;
            //if (strFileNamePath.Substring(strFileNamePath.Length-1,1)!="\\")
            //{
            //    strFileNamePath=strFileNamePath+"\\";
            //}
            //string strFileName=strFileNameFullPath.Substring(strFileNamePath.Length,strFileNameFullPath.Length-strFileNamePath.Length) + ".Config";
            //if (System.IO.File.Exists(strFileNamePath + strFileName))
            //{
            //    System.Xml.XmlDocument xmlDoc = new XmlDocument();
            //    xmlDoc.Load(strFileNamePath + strFileName);
            //    XmlElement xmlRootElement;
            //    xmlRootElement = xmlDoc.DocumentElement["appSettings"];
            //    if (xmlRootElement.ChildNodes[0].Name == "add")
            //    {
            //        if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
            //        {
            //            xmlRootElement.ChildNodes[0].Attributes["key"].Value = "FoodClient.localhost.DataSyncService";
            //        }
            //        else if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
            //        {
            //            xmlRootElement.ChildNodes[0].Attributes["key"].Value = "FoodClient.ForNet.DataDriver";
            //        }
            //        xmlRootElement.ChildNodes[0].Attributes["value"].Value = strWebServer;
            //    }
            //    xmlDoc.Save(strFileNamePath + strFileName);
            //}
            #endregion
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="serverIp">������������ַ</param>
        /// <param name="user">�û���</param>
        /// <param name="pwd">����</param>
        /// <returns>����true��ʾ�ɹ�������false��ʾʧ��</returns>
        public static bool CheckConnection(string serverIp, string user, string pwd)
        {
            if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
            {
                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
                //DY.WebService.ForJ2EE.DataSyncService ws = new DY.WebService.ForJ2EE.DataSyncService();
                ws.Url = serverIp;

                string blrtn = ws.CheckConnection(user, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if (blrtn.Equals("true"))
                {
                    return true;
                    //Cursor = Cursors.Default;
                    //MessageBox.Show(this, "����������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return false;
                    //Cursor = Cursors.Default;
                    //MessageBox.Show(this, "�������޷����ӣ����������ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else //if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
            {
                FoodClient.ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
                //DY.WebService.ForNet.DataDriver ws = new WebService.ForNet.DataDriver();
                ws.Url = serverIp;
                string sErr = string.Empty;
                bool blrtn = ws.CheckConnection(user, pwd, out sErr);
                if (blrtn)
                {
                    return true;
                    //Cursor = Cursors.Default;
                    //MessageBox.Show(this, "����������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return false;
                    //Cursor = Cursors.Default;
                    //MessageBox.Show(this, "�������޷����ӣ����������ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #region �ò����ĺ���
        ///// <summary>
        ///// �����ϴ��������� ��UpdateDB�޸�ΪDataUpload
        ///// �޸��ߣ��¹���
        ///// �޸�ʱ�䣺2011-08-29
        ///// </summary>
        ///// <param name="strWhere"></param>
        //public static void DataUpload(string strWhere)
        //{
        //    //DateTime dt1 = DateTime.Now;
        //    //TimeSpan ts;
        //    DataTable dtResult = new DataTable("Results");
        //    try
        //    {
        //        clsResultOpr resultBll = new clsResultOpr();
        //        string twhere = strWhere;
        //        if (!string.IsNullOrEmpty(strWhere))
        //        {
        //            twhere += string.Format(" AND ");

        //        }
        //        twhere += " A.SysCode<>'' AND A.CheckNo<>'' AND A.FoodCode<>''";
        //        dtResult= resultBll.GetUploadDataTable(twhere, "A.SysCode");//GetAsDataTable(strWhere, "SysCode");

        //        if (dtResult == null)
        //        {
        //            return;
        //        }
        //        string currentUser = CurrentUser.GetInstance().UserInfo.Name;
        //        string checkUnitName = string.Empty;
        //        string tag="";
        //        if (dtResult != null && dtResult.Rows.Count > 0)
        //        {
        //            object obj = null;
        //            for (int i = 0; i < dtResult.Rows.Count; i++)
        //            {
        //                obj = dtResult.Rows[i]["SampleCode"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["SampleCode"] = tag;
        //                }

        //                obj = dtResult.Rows[i]["CheckedCompany"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckedCompany"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckedCompanyInfo"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckedCompanyInfo"] = tag;
        //                }

        //                obj = dtResult.Rows[i]["CheckedComDis"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckedComDis"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckPlace"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckPlace"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["FoodName"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["FoodName"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["SentCompany"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["SentCompany"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["Provider"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Provider"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["ProduceDate"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["ProduceDate"] = DBNull.Value;
        //                }
        //                obj = dtResult.Rows[i]["ProduceCompany"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["ProduceCompany"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["ProducePlace"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["ProducePlace"] = tag;
        //                }

        //                dtResult.Rows[i]["UpLoader"] = currentUser;
        //                obj = dtResult.Rows[i]["ProducePlaceInfo"];
        //                if (obj != null && (obj.ToString().IndexOf("null") >= 0))
        //                {
        //                    dtResult.Rows[i]["ProducePlaceInfo"] = DBNull.Value;
        //                }

        //                obj = dtResult.Rows[i]["Unit"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Unit"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["SampleUnit"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["SampleUnit"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["SampleModel"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["SampleModel"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["SampleLevel"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["SampleLevel"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["SampleState"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["SampleState"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["Standard"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Standard"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckMachine"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckMachine"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckMachineModel"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckMachineModel"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["MachineCompany"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["MachineCompany"] = tag;
        //                }

        //                obj = dtResult.Rows[i]["CheckValueInfo"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckValueInfo"] = tag;
        //                }

        //                obj = dtResult.Rows[i]["StandValue"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["StandValue"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["Result"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Result"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["ResultInfo"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["ResultInfo"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["UpperCompany"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["UpperCompany"] = tag;
        //                }

        //                obj = dtResult.Rows[i]["CheckType"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckType"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckUnitName"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckUnitName"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckUnitInfo"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckUnitInfo"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["Checker"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Checker"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["Assessor"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Assessor"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["Organizer"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Organizer"] = tag;
        //                }

        //                obj = dtResult.Rows[i]["Remark"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Remark"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckPlanCode"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckPlanCode"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckederVal"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckederVal"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["IsSentCheck"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["IsSentCheck"] = tag;
        //                }
        //                obj = dtResult.Rows[i]["CheckederRemark"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["CheckederRemark"] = tag;
        //                }

        //                obj = dtResult.Rows[i]["Notes"];
        //                if (obj == null || obj == DBNull.Value)
        //                {
        //                    dtResult.Rows[i]["Notes"] = tag;
        //                }
        //            }

        //            checkUnitName = dtResult.Rows[0]["CheckUnitName"].ToString();
        //            DataSet dst = new DataSet("UpdateResult");
        //            dst.Tables.Add(dtResult.Copy());

        //            //dst.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "text.xml");
        //            //dst.Tables.Add(dtResult);
        //            if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
        //            {
        //                FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
        //                ws.Url = ShareOption.SysServerIP;
        //                string ret = ws.SetDataDriver(dst.GetXml(), ShareOption.SysServerID, FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPass, "MD5").ToString(), checkUnitName, 3);//�Ӽ�������

        //                if (ret.IndexOf("errofInfo") >= 0)//���ڴ���
        //                {
        //                    MessageBox.Show("�ϴ�����ʧ�� " + ret);
        //                    return;
        //                }
        //                clsResult model = null;
        //                string user = CurrentUser.GetInstance().UserInfo.UserCode;
        //                string err = string.Empty;
        //                string reSendTag = "��";
        //                int len = 0;
        //                int ind = ret.IndexOf("|");
        //                if (ind >= 0)//����Ǵ��ݲ������� ���ݸ�ʽ�磺 2|29393939,112030283  2��ʾ�Ѿ��ɹ��������������ʾδ�ɹ��ı����"��"����
        //                {
        //                    string first = ret.Substring(0, ind);
        //                    if (first.Trim() == "0")
        //                    {
        //                        MessageBox.Show("�ϴ�����ʧ�� ");
        //                        return;
        //                    }
        //                    string retTemp = ret.Substring(ind + 1, ret.Length - ind - 1);
        //                    string[] tArray = retTemp.Split(',');
        //                    len = tArray.Length;
        //                    System.Collections.Hashtable htbl = new System.Collections.Hashtable();
        //                    if (len > 0)
        //                    {
        //                        for (int i = 0; i < len; i++)//���ɹ��ı��
        //                        {
        //                            htbl.Add(tArray[i], "0");
        //                        }
        //                        string temp = string.Empty;
        //                        int j = 0;
        //                        for (int i = 0; i < dtResult.Rows.Count; i++)
        //                        {
        //                            temp = dtResult.Rows[i]["SysCode"].ToString();
        //                            if (htbl[temp] != null && htbl[temp].ToString() == "0")
        //                            {
        //                                continue;
        //                            }
        //                            model = new clsResult();
        //                            model.SysCode = temp;
        //                            model.IsSended = true;
        //                            reSendTag = dtResult.Rows[i]["IsReSended"].ToString();
        //                            if (reSendTag == "��")
        //                            {
        //                                model.IsReSended = true;
        //                            }
        //                            else
        //                            {
        //                                model.IsReSended = false;
        //                            }
        //                            model.SendedDate = DateTime.Now;
        //                            model.Sender = user;
        //                            resultBll.SetUploadFlag(model, out err);
        //                            j++;
        //                        }
        //                        MessageBox.Show("���ݲ����ϴ��ɹ������ϴ�" + j + "����¼");
        //                    }
        //                }
        //                else //ȫ���ɹ�
        //                {
        //                    len = Convert.ToInt32(ret);
        //                    if (len <= 0)
        //                    {
        //                        MessageBox.Show("û�з��ϵ����ݼ�¼");
        //                        return;
        //                    }
        //                    for (int i = 0; i < len; i++)
        //                    {
        //                        model = new clsResult();
        //                        model.SysCode = dtResult.Rows[i]["SysCode"].ToString();
        //                        model.IsSended = true;
        //                        reSendTag = dtResult.Rows[i]["IsReSended"].ToString();
        //                        if (reSendTag == "��")
        //                        {
        //                            model.IsReSended = true;
        //                        }
        //                        else
        //                        {
        //                            model.IsReSended = false;
        //                        }
        //                        model.SendedDate = DateTime.Now;
        //                        model.Sender = user;
        //                        resultBll.SetUploadFlag(model, out err);
        //                    }
        //                    // DateTime dt2= DateTime.Now;
        //                    //ts = dt2 - dt1;
        //                    MessageBox.Show("�ϴ����ݳɹ������ϴ�" + ret + "����¼");//,��ʱ:"+ts.TotalMilliseconds);
        //                }
        //                //�����ϴ��ı�־λ���ϴ�ʱ��
        //            }
        //            #region ע��
        //            #region ����DataTable
        //            //����һ������ƽ̨�ӿ����ݽṹ��DataTable
        //            //DataTable dtResult = new DataTable("Results");
        //            //DataColumn dataCol;
        //            //DataRow dr;

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);//System.Type.GetType("System.String");
        //            //dataCol.ColumnName = "SysCode";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "ResultType";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckNo";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "SampleCode";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "StdCode";
        //            //dtResult.Columns.Add(dataCol);

        //            ////////////////////////////////����һ����λ���
        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckedCompanyCode";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckedCompany";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckedCompanyInfo";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckedComDis";
        //            //dtResult.Columns.Add(dataCol);

        //            ////--------------------�����ӵĽڵ�����������λ���------------------------
        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckPlaceCode";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckPlace";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckPlaceInfo";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "FoodName";
        //            //dtResult.Columns.Add(dataCol);

        //            ////dataCol = new DataColumn();
        //            ////dataCol.DataType = typeof(string);
        //            ////dataCol.ColumnName = "FoodInfo";
        //            ////dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "SentCompany";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Provider";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(DateTime); //System.Type.GetType("System.DateTime");
        //            //dataCol.ColumnName = "ProduceDate";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "ProduceCompany";
        //            //dtResult.Columns.Add(dataCol);


        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "ProducePlace";
        //            //dtResult.Columns.Add(dataCol);

        //            ////dataCol = new DataColumn();
        //            ////dataCol.DataType = typeof(string);
        //            ////dataCol.ColumnName = "ProducePlaceInfo";
        //            ////dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(DateTime); //System.Type.GetType("System.DateTime");
        //            //dataCol.ColumnName = "TakeDate";//
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(DateTime);// System.Type.GetType("System.DateTime");
        //            //dataCol.ColumnName = "CheckStartDate";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(DateTime);// System.Type.GetType("System.DateTime");
        //            //dataCol.ColumnName = "CheckEndDate";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "ImportNum";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "SaveNum";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Unit";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(int); //System.Type.GetType("System.Int32");
        //            //dataCol.ColumnName = "SampleBaseNum";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(decimal);// System.Type.GetType("System.Decimal");
        //            //dataCol.ColumnName = "SampleNum";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "SampleUnit";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "SampleModel";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "SampleLevel";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "SampleState";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Standard";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckMachine";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckMachineModel";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "MachineCompany";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckTotalItem";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckValueInfo";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "StandValue";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Result";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "ResultInfo";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "OrCheckNo";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "UpperCompany";
        //            //dtResult.Columns.Add(dataCol);


        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckType";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckUnitName";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckUnitInfo";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Checker";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Assessor";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Organizer";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "UpLoader";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Remark";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckPlanCode";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckederVal";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "IsSentCheck";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "CheckederRemark";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "IsReSended";
        //            //dtResult.Columns.Add(dataCol);

        //            //dataCol = new DataColumn();
        //            //dataCol.DataType = typeof(string);
        //            //dataCol.ColumnName = "Notes";
        //            //dtResult.Columns.Add(dataCol);
        //            #endregion
        //            //string currentUser = CurrentUser.GetInstance().UserInfo.Name;
        //            //string checkPlaceCode = string.Empty;
        //            //string checkedCompanyCode = string.Empty;
        //            //string comFullName = string.Empty;
        //            //string producePlaceCode = string.Empty;
        //            //string checkUnitName = string.Empty;
        //            ////string foodCode = string.Empty;
        //            ////string checkUnitCode = string.Empty;
        //            ////string produceCompanyCode = string.Empty;
        //            ////string standardCode = string.Empty;
        //            ////string machineCode = string.Empty;
        //            ////string checkerCode = string.Empty;
        //            ////string assessorCode = string.Empty;
        //            ////string organizerCode = string.Empty;
        //            ////string sysCode = string.Empty;

        //            //for (int i = 0; i < dtbl.Rows.Count; i++)
        //            //{
        //            //    dr = dtResult.NewRow();
        //            //    dr["SysCode"] = dtbl.Rows[i]["SysCode"].ToString();
        //            //    dr["ResultType"] = dtbl.Rows[i]["ResultType"].ToString();
        //            //    dr["CheckNo"] = dtbl.Rows[i]["CheckNo"].ToString();
        //            //    dr["SampleCode"] = dtbl.Rows[i]["SampleCode"].ToString();
        //            //    dr["StdCode"] = dtbl.Rows[i]["StdCode"].ToString();

        //            //    //foodCode = dtbl.Rows[i]["FoodCode"].ToString();//ȥ��
        //            //    //dr["FoodInfo"] = clsFoodClassOpr.LevelNamesFromCode(foodCode, ShareOption.FoodCodeLevel);//��һ�����ƣ����
        //            //    dr["FoodName"] = dtbl.Rows[i]["FoodName"].ToString();// clsFoodClassOpr.NameFromCode(foodCode);//����


        //            //    checkPlaceCode = dtbl.Rows[i]["CheckPlaceCode"].ToString();
        //            //    dr["CheckPlaceCode"] = checkPlaceCode;
        //            //    dr["CheckPlace"] = dtbl.Rows[i]["CheckPlace"].ToString(); //clsDistrictOpr.NameFromCode(checkPlaceCode);

        //            //    dr["CheckPlaceInfo"] = clsDistrictOpr.LevelNamesFromCode(checkPlaceCode, ShareOption.DistrictCodeLevel);//��һ��������������

        //            //    checkedCompanyCode = dtbl.Rows[i]["CheckedCompany"].ToString();//��λ���
        //            //    comFullName = dtbl.Rows[i]["CheckedCompanyName"].ToString();

        //            //    if (dtbl.Rows[i]["UpperCompanyName"].ToString().Trim().Equals(""))
        //            //    {
        //            //        dr["CheckedCompany"] = comFullName;
        //            //        dr["CheckedCompanyInfo"] = comFullName;
        //            //    }
        //            //    else
        //            //    {
        //            //        dr["CheckedCompany"] = dtbl.Rows[i]["UpperCompanyName"].ToString();
        //            //        dr["CheckedCompanyInfo"] = comFullName;
        //            //    }

        //            //    dr["CheckedCompanyCode"] = checkedCompanyCode;
        //            //    dr["CheckedComDis"] = dtbl.Rows[i]["CheckedComDis"].ToString();
        //            //    dr["SentCompany"] = dtbl.Rows[i]["SentCompany"].ToString();
        //            //    dr["Provider"] = dtbl.Rows[i]["Provider"].ToString();

        //            //    if (dtbl.Rows[i]["ProduceDate"] != null && dtbl.Rows[i]["ProduceDate"] != DBNull.Value)
        //            //    {
        //            //        dr["ProduceDate"] = dtbl.Rows[i]["ProduceDate"];// Convert.ToDateTime();
        //            //    }
        //            //    //produceCompanyCode = dtbl.Rows[i]["ProduceCompany"].ToString();
        //            //    dr["ProduceCompany"] = dtbl.Rows[i]["ProduceCompanyName"].ToString();// clsCompanyOpr.NameFromCode(produceCompanyCode);
        //            //    //producePlaceCode = dtbl.Rows[i]["ProducePlace"].ToString();
        //            //    dr["ProducePlace"] = dtbl.Rows[i]["ProducePlaceName"].ToString();// clsProduceAreaOpr.NameFromCode(producePlaceCode);
        //            //    // dr["ProducePlaceInfo"] = clsProduceAreaOpr.LevelNamesFromCode(producePlaceCode, ShareOption.DistrictCodeLevel);//ȡ����һ��
        //            //    dr["TakeDate"] = dtbl.Rows[i]["TakeDate"];// Convert.ToDateTime(dtbl.Rows[i]["TakeDate"]);
        //            //    dr["CheckStartDate"] = dtbl.Rows[i]["CheckStartDate"];// Convert.ToDateTime();
        //            //    dr["CheckEndDate"] = dtbl.Rows[i]["CheckEndDate"];// Convert.ToDateTime();

        //            //    dr["ImportNum"] = dtbl.Rows[i]["ImportNum"];
        //            //    dr["SaveNum"] = dtbl.Rows[i]["SaveNum"];
        //            //    dr["Unit"] = dtbl.Rows[i]["Unit"].ToString();
        //            //    dr["SampleBaseNum"] = dtbl.Rows[i]["SampleBaseNum"];
        //            //    dr["SampleNum"] = dtbl.Rows[i]["SampleNum"];
        //            //    //if (dtbl.Rows[i]["ImportNum"].ToString().Equals(""))
        //            //    //{
        //            //    //    dr["ImportNum"] = DBNull.Value;
        //            //    //}
        //            //    //else
        //            //    //{
        //            //    //    dr["ImportNum"] = dtbl.Rows[i]["ImportNum"].ToString();
        //            //    //}

        //            //    //if (dtbl.Rows[i]["SaveNum"].ToString().Equals(""))
        //            //    //{
        //            //    //    dr["SaveNum"] = DBNull.Value;
        //            //    //}
        //            //    //else
        //            //    //{
        //            //    //    dr["SaveNum"] = dtbl.Rows[i]["SaveNum"].ToString();
        //            //    //}

        //            //    //if (dtbl.Rows[i]["SampleBaseNum"] == DBNull.Value)
        //            //    //{
        //            //    //    dr["SampleBaseNum"] = DBNull.Value;
        //            //    //}
        //            //    //else
        //            //    //{
        //            //    //    dr["SampleBaseNum"] = dtbl.Rows[i]["SampleBaseNum"];// Convert.ToInt16();
        //            //    //}

        //            //    //if (dtbl.Rows[i]["SampleNum"] == DBNull.Value)
        //            //    //{
        //            //    //   dr["SampleNum"] = DBNull.Value;
        //            //    //}
        //            //    //else
        //            //    //{
        //            //    //    dr["SampleNum"] = dtbl.Rows[i]["SampleNum"];// Convert.ToDouble();
        //            //    //}
        //            //    dr["SampleUnit"] = dtbl.Rows[i]["SampleUnit"].ToString();
        //            //    dr["SampleModel"] = dtbl.Rows[i]["SampleModel"].ToString();
        //            //    dr["SampleLevel"] = dtbl.Rows[i]["SampleLevel"].ToString();
        //            //    dr["SampleState"] = dtbl.Rows[i]["SampleState"].ToString();
        //            //    // standardCode = dtbl.Rows[i]["Standard"].ToString();
        //            //    dr["Standard"] = dtbl.Rows[i]["StandardName"].ToString();
        //            //clsStandardOpr.GetNameFromCode(standardCode);
        //            //    //machineCode = dtbl.Rows[i]["CheckMachine"].ToString();
        //            //    dr["CheckMachine"] = dtbl.Rows[i]["MachineName"].ToString();// clsMachineOpr.GetMachineNameFromCode(machineCode);
        //            //    dr["CheckMachineModel"] = dtbl.Rows[i]["MachineModel"].ToString();// clsMachineOpr.GetNameFromCode("MachineModel", machineCode);
        //            //    dr["MachineCompany"] = dtbl.Rows[i]["MachineCompany"].ToString();// clsMachineOpr.GetNameFromCode("Company", machineCode);
        //            //    dr["CheckTotalItem"] = dtbl.Rows[i]["CheckTotalItemName"].ToString(); //clsCheckItemOpr.GetNameFromCode(dtbl.Rows[i]["CheckTotalItem"].ToString());
        //            //    dr["CheckValueInfo"] = dtbl.Rows[i]["CheckValueInfo"].ToString();
        //            //    dr["StandValue"] = dtbl.Rows[i]["StandValue"].ToString();
        //            //    dr["Result"] = dtbl.Rows[i]["Result"].ToString();
        //            //    dr["ResultInfo"] = dtbl.Rows[i]["ResultInfo"].ToString();
        //            //    dr["OrCheckNo"] = dtbl.Rows[i]["OrCheckNo"].ToString();
        //            //    dr["UpperCompany"] = dtbl.Rows[i]["UpperCompany"].ToString();


        //            //    dr["CheckType"] = dtbl.Rows[i]["CheckType"].ToString();
        //            //    // checkUnitCode = dtbl.Rows[i]["CheckUnitCode"].ToString();
        //            //    //checkUnitName = clsUserUnitOpr.GetNameFromCode(checkUnitCode);//��ȡ�������� 
        //            //    checkUnitName = dtbl.Rows[i]["CheckUnitName"].ToString();
        //            //    dr["CheckUnitName"] = checkUnitName;
        //            //    // dr["CheckUnitInfo"] = dtbl.Rows[i][" CheckUnitInfo"].ToString(); //clsUserUnitOpr.GetNameFromCode("ShortName", checkUnitCode);//��ȡ�������
        //            //    //checkerCode = dtbl.Rows[i]["Checker"].ToString();
        //            //    dr["Checker"] = dtbl.Rows[i]["CheckerName"].ToString();//clsUserInfoOpr.NameFromCode(checkerCode);

        //            //    //assessorCode = dtbl.Rows[i]["Assessor"].ToString();
        //            //    dr["Assessor"] = dtbl.Rows[i]["AssessorName"].ToString();// clsUserInfoOpr.NameFromCode(assessorCode);

        //            //    //organizerCode = dtbl.Rows[i]["Organizer"].ToString();
        //            //    dr["Organizer"] = dtbl.Rows[i]["OrganizerName"].ToString(); //clsUserInfoOpr.NameFromCode(organizerCode);
        //            //    dr["UpLoader"] = currentUser;
        //            //    dr["Remark"] = dtbl.Rows[i]["Remark"].ToString();
        //            //    dr["CheckPlanCode"] = dtbl.Rows[i]["CheckPlanCode"].ToString();
        //            //    dr["CheckederVal"] = dtbl.Rows[i]["CheckederVal"].ToString();
        //            //    dr["IsSentCheck"] = dtbl.Rows[i]["IsSentCheck"].ToString();
        //            //    dr["CheckederRemark"] = dtbl.Rows[i]["CheckederRemark"].ToString();
        //            //    if (Convert.ToBoolean(dtbl.Rows[i]["IsReSended"]))
        //            //    {
        //            //        dr["IsReSended"] = "��";
        //            //    }
        //            //    else
        //            //    {
        //            //        dr["IsReSended"] = "��";
        //            //    }
        //            //    dtResult.Rows.Add(dr);
        //            //    dr["Notes"] = dtbl.Rows[i]["Notes"].ToString();
        //            //}
        //            //��ʱ��֧��.NET�汾
        //            //else if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
        //            //{
        //            //    FoodClient.ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
        //            //    ws.Url = ShareOption.SysServerIP;
        //            //    int rt = ws.SetDataDriver(dst, ShareOption.SysServerID, ShareOption.SysServerPass, checkUnitName, 3);

        //            //    //�����ϴ��ı�־λ���ϴ�ʱ��
        //            //    if (rt != 0)
        //            //    {
        //            //        string sErr = string.Empty;
        //            //        for (int i = 0; i < rt; i++)
        //            //        {
        //            //            clsResult result = new clsResult();
        //            //            result.SysCode = dtbl.Rows[i]["SysCode"].ToString();
        //            //            result.IsSended = true;
        //            //            result.IsReSended = Convert.ToBoolean(dtbl.Rows[i]["IsReSended"].ToString());
        //            //            result.SendedDate = DateTime.Now;
        //            //            result.Sender = CurrentUser.GetInstance().UserInfo.UserCode;
        //            //            resultBll.SetUploadFlag(result, out sErr);
        //            //        }
        //            //    }

        //            //    MessageBox.Show("�ϴ����ݳɹ������ϴ�" + rt.ToString() + "����¼");
        //            //}
        //            #endregion
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("�ϴ�����ʧ�ܣ�ʧ��ԭ��" + e.Message);
        //    }
        //}
        ///// <summary>
        ///// �ϴ����֮�󣬸��¼���¼
        ///// </summary>
        ///// <param name="stockWhere"></param>
        ///// <param name="saleWhere"></param>
        ///// <param name="intType"></param>
        //public static void UpdateRecordDB(string stockWhere, string saleWhere, int intType)
        //{
        //    try
        //    {
        //        string sErr = string.Empty;
        //        string companyname = clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);
        //        if (intType == 1 || intType == 0)
        //        {
        //            clsStockRecordOpr opr = new clsStockRecordOpr();
        //            DataTable dt = opr.GetAsDataTable(stockWhere, "SysCode", 0);
        //            DataTable dt1 = new DataTable("StockRecord");
        //            dt1 = dt.Copy();
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataSet dsRt = new DataSet("UpdateRecord");
        //                dsRt.Tables.Add(dt1);
        //                if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
        //                {
        //                    FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
        //                    ws.Url = ShareOption.SysServerIP;
        //                    string rt = ws.SetDataDriver(dsRt.GetXml(), ShareOption.SysServerID, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPass, "MD5").ToString(), companyname, 1);
        //                    //�����ϴ��ı�־λ���ϴ�ʱ��
        //                    if (rt.Length < 11)
        //                    {

        //                        for (int i = 0; i < Convert.ToInt32(rt); i++)
        //                        {
        //                            opr.UpdateSendFlag(dt.Rows[i]["SysCode"].ToString(), out sErr);
        //                        }
        //                        MessageBox.Show("�ϴ����ݳɹ������ϴ�" + rt.ToString() + "����¼");
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("�ϴ�����ʧ�ܣ�ʧ��ԭ��" + rt.Substring(10, rt.Length - 10));
        //                    }
        //                }
        //                else //if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
        //                {
        //                    FoodClient.ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
        //                    ws.Url = ShareOption.SysServerIP;
        //                    int rt = ws.SetDataDriver(dsRt, ShareOption.SysServerID, ShareOption.SysServerPass, companyname, 1);
        //                    //�����ϴ��ı�־λ���ϴ�ʱ��
        //                    if (rt != 0)
        //                    {

        //                        for (int i = 0; i < rt; i++)
        //                        {
        //                            opr.UpdateSendFlag(dt.Rows[i]["SysCode"].ToString(), out sErr);
        //                        }
        //                    }
        //                    MessageBox.Show("�ϴ����ݳɹ������ϴ�" + rt.ToString() + "����¼");
        //                }
        //            }
        //        }
        //        if (intType == 2 || intType == 0)
        //        {
        //            clsSaleRecordOpr opr = new clsSaleRecordOpr();
        //            DataTable dt = opr.GetAsDataTable(saleWhere, "SysCode", 0);
        //            DataTable dt1 = new DataTable("StockRecord");
        //            dt1 = dt.Copy();
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataSet dsRt = new DataSet("UpdateRecord");
        //                dsRt.Tables.Add(dt1);
        //                if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
        //                {
        //                    FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
        //                    ws.Url = ShareOption.SysServerIP;
        //                    string rt = ws.SetDataDriver(dsRt.GetXml(), ShareOption.SysServerID, FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPass, "MD5").ToString(), companyname, 2);
        //                    //�����ϴ��ı�־λ���ϴ�ʱ��
        //                    if (rt.Length < 11)
        //                    {

        //                        for (int i = 0; i < Convert.ToInt32(rt); i++)
        //                        {
        //                            opr.UpdateSendFlag(dt.Rows[i]["SysCode"].ToString(), out sErr);
        //                        }
        //                        MessageBox.Show("�ϴ����ݳɹ������ϴ�" + rt.ToString() + "����¼");
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("�ϴ�����ʧ�ܣ�ʧ��ԭ��" + rt.Substring(10, rt.Length - 10));
        //                    }
        //                }
        //                else //if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
        //                {
        //                    FoodClient.ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
        //                    ws.Url = ShareOption.SysServerIP;
        //                    int rt = ws.SetDataDriver(dsRt, ShareOption.SysServerID, ShareOption.SysServerPass, companyname, 2);
        //                    //�����ϴ��ı�־λ���ϴ�ʱ��
        //                    if (rt != 0)
        //                    {

        //                        for (int i = 0; i < rt; i++)
        //                        {
        //                            opr.UpdateSendFlag(dt.Rows[i]["SysCode"].ToString(), out sErr);
        //                        }
        //                    }

        //                    MessageBox.Show("�ϴ����ݳɹ������ϴ�" + rt.ToString() + "����¼");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("�ϴ�����ʧ�ܣ�ʧ��ԭ��" + e.Message);
        //    }
        //}

        //public static void UpdateRecordDB(string StockwhereStr, string SalewhereStr, string ConnectionStr, int intType)
        //{
        //    if (intType == 1 || intType == 0)
        //    {
        //        clsStockRecordOpr opr = new clsStockRecordOpr();
        //        DataTable dt = opr.GetAsDataTable(StockwhereStr, "SysCode", 0);
        //        string strsql = string.Empty;
        //        string sErr = string.Empty;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            sErr = string.Empty;
        //            strsql = "insert into tFss_StockRecord(SysCode,StdCode,CompanyID,CompanyName,DisplayName,InputDate,FoodID,FoodName,Model,InputNumber,OutputNumber,Unit,ProduceDate,ExpirationDate,ProduceCompanyID,ProduceCompanyName,PrivoderID,PrivoderName,LinkInfo,LinkMan,CertificateType1,CertificateType2,CertificateType3,CertificateType4,CertificateType5,CertificateType6,CertificateType7,CertificateType8,CertificateType9,CertificateInfo,MakeMan,Remark,DistrictCode,CompanyInfo)"
        //                + " values('"
        //                + dt.Rows[i]["SysCode"].ToString() + "','"
        //                + dt.Rows[i]["StdCode"].ToString() + "','"
        //                + dt.Rows[i]["CompanyID"].ToString() + "','"
        //                + dt.Rows[i]["CompanyName"].ToString() + "','"
        //                + dt.Rows[i]["DisplayName"].ToString() + "','"
        //                + dt.Rows[i]["InputDate"].ToString() + "','"
        //                + dt.Rows[i]["FoodID"].ToString() + "','"
        //                + dt.Rows[i]["FoodName"].ToString() + "','"
        //                + dt.Rows[i]["Model"].ToString() + "',"
        //                + dt.Rows[i]["InputNumber"].ToString() + ","
        //                + dt.Rows[i]["OutputNumber"].ToString() + ",'"
        //                + dt.Rows[i]["Unit"].ToString() + "','"
        //                + dt.Rows[i]["ProduceDate"].ToString() + "','"
        //                + dt.Rows[i]["ExpirationDate"].ToString() + "','"
        //                + dt.Rows[i]["ProduceCompanyID"].ToString() + "','"
        //                + dt.Rows[i]["ProduceCompanyName"].ToString() + "','"
        //                + dt.Rows[i]["PrivoderID"].ToString() + "','"
        //                + dt.Rows[i]["PrivoderName"].ToString() + "','"
        //                + dt.Rows[i]["LinkInfo"].ToString() + "','"
        //                + dt.Rows[i]["LinkMan"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType1"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType2"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType3"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType4"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType5"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType6"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType7"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType8"].ToString() + "','"
        //                + dt.Rows[i]["CertificateType9"].ToString() + "','"
        //                + dt.Rows[i]["CertificateInfo"].ToString() + "','"
        //                + dt.Rows[i]["MakeMan"].ToString() + "','"
        //                + dt.Rows[i]["Remark"].ToString() + "','"
        //                + dt.Rows[i]["DistrictCode"].ToString() + "','"
        //                + clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode) + "')";
        //            DataBase.ExecuteCommand(ConnectionStr, strsql, out sErr);

        //            if (sErr.Equals(string.Empty))
        //            {
        //                opr.UpdateSendFlag(dt.Rows[i]["SysCode"].ToString(), out sErr);
        //            }
        //        }
        //    }
        //    if (intType == 2 || intType == 0)
        //    {
        //        clsSaleRecordOpr opr = new clsSaleRecordOpr();
        //        DataTable dt = opr.GetAsDataTable(SalewhereStr, "SysCode", 0);
        //        string strsql = string.Empty;
        //        string sErr = string.Empty;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            sErr = string.Empty;

        //            strsql = "insert into tFss_SaleRecord(SysCode,StdCode,CompanyID,CompanyName,DisplayName,SaleDate,FoodID,FoodName,Model,SaleNumber,Price,Unit,PurchaserID,PurchaserName,LinkInfo,LinkMan,MakeMan,Remark,DistrictCode,CompanyInfo)"
        //                + " values('"
        //                + dt.Rows[i]["SysCode"].ToString() + "','"
        //                + dt.Rows[i]["StdCode"].ToString() + "','"
        //                + dt.Rows[i]["CompanyID"].ToString() + "','"
        //                + dt.Rows[i]["CompanyName"].ToString() + "','"
        //                + dt.Rows[i]["DisplayName"].ToString() + "','"
        //                + dt.Rows[i]["SaleDate"].ToString() + "','"
        //                + dt.Rows[i]["FoodID"].ToString() + "','"
        //                + dt.Rows[i]["FoodName"].ToString() + "','"
        //                + dt.Rows[i]["Model"].ToString() + "',"
        //                + dt.Rows[i]["SaleNumber"].ToString() + ","
        //                + dt.Rows[i]["Price"].ToString() + ",'"
        //                + dt.Rows[i]["Unit"].ToString() + "','"
        //                + dt.Rows[i]["PurchaserID"].ToString() + "','"
        //                + dt.Rows[i]["PurchaserName"].ToString() + "','"
        //                + dt.Rows[i]["LinkInfo"].ToString() + "','"
        //                + dt.Rows[i]["LinkMan"].ToString() + "','"
        //                + dt.Rows[i]["MakeMan"].ToString() + "','"
        //                + dt.Rows[i]["Remark"].ToString() + "','"
        //                + dt.Rows[i]["DistrictCode"].ToString() + "','"
        //                + clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode) + "')";
        //            DataBase.ExecuteCommand(ConnectionStr, strsql, out sErr);

        //            if (sErr.Equals(string.Empty))
        //            {
        //                opr.UpdateSendFlag(dt.Rows[i]["SysCode"].ToString(), out sErr);
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// û���õ�����ע��
        ///// �¹��� 2011-08-31
        ///// ����������SQL Server�Ļ��������뱾������ͬ��һ��
        /////������string Connectionstr �����������ַ���
        /////���������������б��м�¼����һ��
        /////  �����������ݿ⣨FoodSafeSystem��     �������ݿ�(Local.mdb)
        /////  tCom_District                   --->  tDistrict
        /////  tCom_ProduceArea                --->  tProduceArea
        /////  tFss_CheckItem                  --->  tCheckItem
        /////  tFss_CheckLevel                 --->  tCheckLevel
        /////  tFss_CheckType                  --->  tCheckType  
        /////  tFss_Company                    --->  tCompany
        /////  tFss_CompanyKind                --->  tCompanyKind
        /////  tFss_CompanyProperty            --->  tCompanyProperty
        /////  tFss_CreditLevel                --->  tCreditLevel 
        /////  tFss_FoodClass                  --->  tFoodClass
        /////  tFss_Standard                   --->  tStandard
        /////  tFss_StandardType               --->  tStandardType
        /////  tFss_CheckComType               --->  tCheckComType
        ///// </summary>
        ///// <param name="connectionString"></param>
        ///// <param name="stdCode"></param>
        ///// <param name="sumError"></param>
        //public static void UpdateLocalDB(string connectionString, string stdCode, out string sumError)
        //{
        //    DataTable dt = null;
        //    sumError = string.Empty;
        //    string whereStr = string.Empty;
        //    string delErr = string.Empty;

        //    //ʳƷ����
        //    clsFoodClassOpr Opr1 = new clsFoodClassOpr();
        //    Opr1.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_FoodClassOpr opr = new clsFss_FoodClassOpr(connectionString);
        //    dt = opr.GetAsDataTable(whereStr, "SysCode");
        //    string sErr = string.Empty;
        //    clsFoodClass model = null;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = string.Empty;

        //        model = new clsFoodClass();
        //        model.SysCode = dt.Rows[i]["SysCode"].ToString();
        //        model.StdCode = dt.Rows[i]["StdCode"].ToString();
        //        model.Name = dt.Rows[i]["Name"].ToString();
        //        model.ShortCut = dt.Rows[i]["ShortCut"].ToString();
        //        model.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
        //        model.CheckItemCodes = dt.Rows[i]["CheckItemCodes"].ToString();
        //        model.CheckItemValue = dt.Rows[i]["CheckItemValue"].ToString();
        //        model.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        model.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        model.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr1.Insert(model, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //������
        //    clsCheckTypeOpr Opr2 = new clsCheckTypeOpr();
        //    Opr2.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_CheckTypeOpr opr2 = new clsFss_CheckTypeOpr(connectionString);
        //    dt = opr2.GetAsDataTable(whereStr, "Name");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsCheckType checkType = new clsCheckType();
        //        checkType.Name = dt.Rows[i]["Name"].ToString();
        //        checkType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        checkType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        checkType.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr2.Insert(checkType, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //���㵥λ���
        //    clsCheckComTypeOpr Opr18 = new clsCheckComTypeOpr();
        //    Opr18.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_CheckComTypeOpr opr18 = new clsFss_CheckComTypeOpr(connectionString);
        //    dt = opr18.GetAsDataTable(whereStr, "ID", 0);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsCheckComType checkcomType = new clsCheckComType();
        //        checkcomType.TypeName = dt.Rows[i]["TypeName"].ToString();
        //        checkcomType.NameCall = dt.Rows[i]["NameCall"].ToString();
        //        checkcomType.AreaCall = dt.Rows[i]["AreaCall"].ToString();
        //        checkcomType.VerType = dt.Rows[i]["VerType"].ToString();
        //        checkcomType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        checkcomType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        checkcomType.ComKind = dt.Rows[i]["ComKind"].ToString();

        //        Opr18.Insert(checkcomType, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //����׼����
        //    clsStandardTypeOpr Opr7 = new clsStandardTypeOpr();
        //    Opr7.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_StandardTypeOpr opr7 = new clsFss_StandardTypeOpr(connectionString);
        //    dt = opr7.GetAsDataTable(whereStr, "StdName");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsStandardType standardType = new clsStandardType();
        //        standardType.StdName = dt.Rows[i]["StdName"].ToString();
        //        standardType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        standardType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        standardType.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr7.Insert(standardType, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //����׼
        //    clsStandardOpr Opr6 = new clsStandardOpr();
        //    Opr6.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_StandardOpr opr6 = new clsFss_StandardOpr(connectionString);
        //    dt = opr6.GetAsDataTable(whereStr, "SysCode");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsStandard standard = new clsStandard();
        //        standard.SysCode = dt.Rows[i]["SysCode"].ToString();
        //        standard.StdCode = dt.Rows[i]["StdCode"].ToString();
        //        standard.StdDes = dt.Rows[i]["StdDes"].ToString();
        //        standard.ShortCut = dt.Rows[i]["ShortCut"].ToString();
        //        standard.StdInfo = dt.Rows[i]["StdInfo"].ToString();
        //        standard.StdType = dt.Rows[i]["StdType"].ToString();
        //        standard.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        standard.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        standard.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr6.Insert(standard, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //�����Ŀ
        //    clsCheckItemOpr Opr3 = new clsCheckItemOpr();
        //    Opr3.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_CheckItemOpr opr3 = new clsFss_CheckItemOpr(connectionString);
        //    dt = opr3.GetAsDataTable(whereStr, "SysCode");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsCheckItem checkItem = new clsCheckItem();
        //        checkItem.SysCode = dt.Rows[i]["SysCode"].ToString();
        //        checkItem.StdCode = dt.Rows[i]["StdCode"].ToString();
        //        checkItem.ItemDes = dt.Rows[i]["ItemDes"].ToString();
        //        checkItem.CheckType = dt.Rows[i]["CheckType"].ToString();
        //        checkItem.StandardCode = dt.Rows[i]["StandardCode"].ToString();
        //        checkItem.StandardValue = dt.Rows[i]["StandardValue"].ToString();
        //        checkItem.Unit = dt.Rows[i]["Unit"].ToString();
        //        checkItem.DemarcateInfo = dt.Rows[i]["DemarcateInfo"].ToString();
        //        checkItem.TestValue = dt.Rows[i]["TestValue"].ToString();
        //        checkItem.OperateHelp = dt.Rows[i]["OperateHelp"].ToString();
        //        checkItem.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
        //        checkItem.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        checkItem.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        checkItem.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr3.Insert(checkItem, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //��λ���
        //    clsCompanyKindOpr Opr4 = new clsCompanyKindOpr();
        //    Opr4.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_CompanyKindOpr opr4 = new clsFss_CompanyKindOpr(connectionString);
        //    dt = opr4.GetAsDataTable(whereStr, "SysCode");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsCompanyKind companyKind = new clsCompanyKind();
        //        companyKind.SysCode = dt.Rows[i]["SysCode"].ToString();
        //        companyKind.StdCode = dt.Rows[i]["StdCode"].ToString();
        //        companyKind.Name = dt.Rows[i]["Name"].ToString();
        //        companyKind.CompanyProperty = dt.Rows[i]["CompanyProperty"].ToString();
        //        companyKind.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        companyKind.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        companyKind.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr4.Insert(companyKind, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //��֯����
        //    clsDistrictOpr Opr5 = new clsDistrictOpr();
        //    Opr5.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsCom_DistrictOpr opr5 = new clsCom_DistrictOpr(connectionString);
        //    dt = opr5.GetAsDataTable(whereStr, "SysCode");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsDistrict district = new clsDistrict();
        //        district.SysCode = dt.Rows[i]["SysCode"].ToString();
        //        district.StdCode = dt.Rows[i]["StdCode"].ToString();
        //        district.Name = dt.Rows[i]["Name"].ToString();
        //        district.ShortCut = dt.Rows[i]["ShortCut"].ToString();
        //        district.DistrictIndex = Convert.ToInt64(dt.Rows[i]["DistrictIndex"]);
        //        district.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
        //        district.IsLocal = Convert.ToBoolean(dt.Rows[i]["IsLocal"]);
        //        district.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        district.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        district.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr5.Insert(district, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //����
        //    clsProduceAreaOpr Opr15 = new clsProduceAreaOpr();
        //    Opr15.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsCom_ProduceAreaOpr opr15 = new clsCom_ProduceAreaOpr(connectionString);
        //    dt = opr15.GetAsDataTable(whereStr, "SysCode");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsProduceArea ProduceArea = new clsProduceArea();
        //        ProduceArea.SysCode = dt.Rows[i]["SysCode"].ToString();
        //        ProduceArea.StdCode = dt.Rows[i]["StdCode"].ToString();
        //        ProduceArea.Name = dt.Rows[i]["Name"].ToString();
        //        ProduceArea.ShortCut = dt.Rows[i]["ShortCut"].ToString();
        //        ProduceArea.DistrictIndex = Convert.ToInt64(dt.Rows[i]["DistrictIndex"]);
        //        ProduceArea.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
        //        ProduceArea.IsLocal = Convert.ToBoolean(dt.Rows[i]["IsLocal"]);
        //        ProduceArea.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        ProduceArea.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        ProduceArea.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr15.Insert(ProduceArea, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //��⼶��
        //    clsCheckLevelOpr Opr8 = new clsCheckLevelOpr();
        //    Opr8.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_CheckLevelOpr opr8 = new clsFss_CheckLevelOpr(connectionString);
        //    dt = opr8.GetAsDataTable(whereStr, "CheckLevel");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsCheckLevel checkLevel = new clsCheckLevel();
        //        checkLevel.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
        //        checkLevel.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        checkLevel.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        checkLevel.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr8.Insert(checkLevel, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //���ü���
        //    clsCreditLevelOpr Opr9 = new clsCreditLevelOpr();
        //    Opr9.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_CreditLevelOpr opr9 = new clsFss_CreditLevelOpr(connectionString);
        //    dt = opr9.GetAsDataTable(whereStr, "CreditLevel");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsCreditLevel creditLevel = new clsCreditLevel();
        //        creditLevel.CreditLevel = dt.Rows[i]["CreditLevel"].ToString();
        //        creditLevel.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        creditLevel.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        creditLevel.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr9.Insert(creditLevel, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //��˾����
        //    clsCompanyPropertyOpr Opr11 = new clsCompanyPropertyOpr();
        //    Opr11.Delete("IsReadOnly=true", out delErr);

        //    whereStr = "IsReadOnly=1";
        //    clsFss_CompanyPropertyOpr opr11 = new clsFss_CompanyPropertyOpr(connectionString);
        //    dt = opr11.GetAsDataTable(whereStr, "CompanyProperty");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsCompanyProperty companyProperty = new clsCompanyProperty();
        //        companyProperty.CompanyProperty = dt.Rows[i]["CompanyProperty"].ToString();
        //        companyProperty.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        companyProperty.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        companyProperty.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr11.Insert(companyProperty, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }

        //    //��˾
        //    clsFss_CompanyOpr opr12 = new clsFss_CompanyOpr(connectionString);
        //    clsCompanyOpr Opr12 = new clsCompanyOpr();

        //    if (ShareOption.SystemVersion.Equals(ShareOption.LocalBaseVersion))
        //    {
        //        Opr12.Delete("IsReadOnly=true", out delErr);

        //        string sDistrictCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", ShareOption.DefaultUserUnitCode);

        //        whereStr = "A.Property='" + ShareOption.CompanyProperty1 + "'"
        //            + " and A.DistrictCode like '" + sDistrictCode + "%'";
        //        whereStr = "(" + whereStr + ")" + " and A.IsReadOnly=1";
        //    }
        //    else
        //    {

        //        Opr12.Delete("IsReadOnly=true And Property='" + ShareOption.CompanyProperty1 + "'", out delErr);

        //        clsCom_UserUnitOpr com_uuOpr = new clsCom_UserUnitOpr(connectionString);
        //        string sDistrictCode = com_uuOpr.GetDistrictCode(stdCode);

        //        whereStr = "A.Property='" + ShareOption.CompanyProperty1 + "'"
        //            + " and A.DistrictCode like '" + sDistrictCode + "%'"
        //            + " And A.StdCode Like '" + stdCode + "%'";

        //    }
        //    dt = opr12.GetAsDataTable(whereStr, "SysCode");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sErr = "";

        //        clsCompany company = new clsCompany();
        //        company.SysCode = dt.Rows[i]["SysCode"].ToString();
        //        company.StdCode = dt.Rows[i]["StdCode"].ToString();
        //        company.CompanyID = dt.Rows[i]["CompanyID"].ToString();
        //        company.OtherCodeInfo = dt.Rows[i]["OtherCodeInfo"].ToString();
        //        company.FullName = dt.Rows[i]["FullName"].ToString();
        //        company.ShortName = dt.Rows[i]["ShortName"].ToString();
        //        company.DisplayName = dt.Rows[i]["DisplayName"].ToString();
        //        company.ShortCut = dt.Rows[i]["ShortCut"].ToString();
        //        company.Property = dt.Rows[i]["Property"].ToString();
        //        company.KindCode = dt.Rows[i]["KindCode"].ToString();
        //        company.RegCapital = Convert.ToInt64(dt.Rows[i]["RegCapital"]);
        //        company.Unit = dt.Rows[i]["Unit"].ToString();
        //        company.Incorporator = dt.Rows[i]["Incorporator"].ToString();
        //        company.RegDate = Convert.ToDateTime(dt.Rows[i]["RegDate"]);
        //        company.DistrictCode = dt.Rows[i]["DistrictCode"].ToString();
        //        company.PostCode = dt.Rows[i]["PostCode"].ToString();
        //        company.Address = dt.Rows[i]["Address"].ToString();
        //        company.LinkMan = dt.Rows[i]["LinkMan"].ToString();
        //        company.LinkInfo = dt.Rows[i]["LinkInfo"].ToString();
        //        company.CreditLevel = dt.Rows[i]["CreditLevel"].ToString();
        //        company.CreditRecord = dt.Rows[i]["CreditRecord"].ToString();
        //        company.ProductInfo = dt.Rows[i]["ProductInfo"].ToString();
        //        company.OtherInfo = dt.Rows[i]["OtherInfo"].ToString();
        //        company.FoodSafeRecord = dt.Rows[i]["FoodSafeRecord"].ToString();
        //        company.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
        //        company.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
        //        company.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
        //        company.Remark = dt.Rows[i]["Remark"].ToString();

        //        Opr12.Insert(company, out sErr);
        //        if (!sErr.Equals(""))
        //        {
        //            sumError += sErr + "\r\n";
        //            continue;
        //        }
        //    }
        //}
        ///// <summary>
        ///// û���õ��ĵط�����ע����
        ///// �¹��� 2011-08-31
        ///// </summary>
        ///// <param name="strWhere"></param>
        ///// <param name="connectionString"></param>
        //public static void UpdateDB(string strWhere, string connectionString)
        //{
        //    clsUpdate_ResultOpr Opr1 = new clsUpdate_ResultOpr(connectionString);
        //    clsResultOpr opr = new clsResultOpr();
        //    clsUpdate_Result model = null;
        //    string sysCode = string.Empty;
        //    DataTable dtbl = opr.GetAsDataTable(strWhere, "SysCode");
        //    string sErr = string.Empty;
        //    string foodCode = string.Empty;
        //    string checkPlaceCode = string.Empty;
        //    string checkedCompanyCode = string.Empty;
        //    string comFullName = string.Empty;
        //    string proCompanyCode = string.Empty;
        //    string proPlaceCode = string.Empty;
        //    string standardCode = string.Empty;
        //    string machineCode = string.Empty;
        //    string checkUnitCode = string.Empty;
        //    string checkerCode = string.Empty;
        //    string assessorCode = string.Empty;
        //    string organizerCode = string.Empty;
        //    for (int i = 0; i < dtbl.Rows.Count; i++)
        //    {
        //        sErr = string.Empty;
        //        if (dtbl.Rows[i]["SysCode"].ToString().Equals("")
        //            || dtbl.Rows[i]["CheckNo"].ToString().Equals("")
        //            || dtbl.Rows[i]["FoodCode"].ToString().Equals(""))
        //        {
        //            continue;
        //        }
        //        sysCode = dtbl.Rows[i]["SysCode"].ToString();
        //        foodCode = dtbl.Rows[i]["FoodCode"].ToString();
        //        checkPlaceCode = dtbl.Rows[i]["CheckPlaceCode"].ToString();
        //        checkedCompanyCode = dtbl.Rows[i]["CheckedCompany"].ToString();
        //        comFullName = dtbl.Rows[i]["CheckedCompanyName"].ToString();
        //        proCompanyCode = dtbl.Rows[i]["ProduceCompany"].ToString();
        //        proPlaceCode = dtbl.Rows[i]["ProducePlace"].ToString();
        //        standardCode = dtbl.Rows[i]["Standard"].ToString();
        //        machineCode = dtbl.Rows[i]["CheckMachine"].ToString();
        //        checkUnitCode = dtbl.Rows[i]["CheckUnitCode"].ToString();
        //        checkerCode = dtbl.Rows[i]["Checker"].ToString();
        //        assessorCode = dtbl.Rows[i]["Assessor"].ToString();
        //        organizerCode = dtbl.Rows[i]["Organizer"].ToString();

        //        model = new clsUpdate_Result();
        //        model.SysCode = sysCode;
        //        model.ResultType = dtbl.Rows[i]["ResultType"].ToString();
        //        model.CheckNo = dtbl.Rows[i]["CheckNo"].ToString();
        //        model.SampleCode = dtbl.Rows[i]["SampleCode"].ToString();
        //        model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
        //        model.FoodName = clsFoodClassOpr.NameFromCode(foodCode);
        //        model.FoodInfo = clsFoodClassOpr.LevelNamesFromCode(foodCode, ShareOption.FoodCodeLevel);
        //        model.CheckPlace = clsDistrictOpr.NameFromCode(checkPlaceCode);
        //        model.CheckPlaceInfo = clsDistrictOpr.LevelNamesFromCode(checkPlaceCode, ShareOption.DistrictCodeLevel);

        //        if (dtbl.Rows[i]["UpperCompanyName"].ToString().Trim().Equals(""))
        //        {
        //            model.CheckedCompany = comFullName;
        //            model.CheckedCompanyInfo = comFullName;
        //        }
        //        else
        //        {
        //            model.CheckedCompany = dtbl.Rows[i]["UpperCompanyName"].ToString();
        //            model.CheckedCompanyInfo = comFullName;
        //        }
        //        model.CheckedComDis = dtbl.Rows[i]["CheckedComDis"].ToString();
        //        model.SentCompany = dtbl.Rows[i]["SentCompany"].ToString();
        //        model.Provider = dtbl.Rows[i]["Provider"].ToString();
        //        model.ProduceDate = Convert.ToDateTime(dtbl.Rows[i]["ProduceDate"]);

        //        model.ProduceCompany = clsCompanyOpr.NameFromCode(proCompanyCode);

        //        model.ProducePlace = clsProduceAreaOpr.NameFromCode(proPlaceCode);
        //        model.ProducePlaceInfo = clsProduceAreaOpr.LevelNamesFromCode(proPlaceCode, ShareOption.DistrictCodeLevel);
        //        model.TakeDate = Convert.ToDateTime(dtbl.Rows[i]["TakeDate"]);
        //        model.CheckStartDate = Convert.ToDateTime(dtbl.Rows[i]["CheckStartDate"]);
        //        model.CheckEndDate = Convert.ToDateTime(dtbl.Rows[i]["CheckEndDate"]);
        //        model.ImportNum = dtbl.Rows[i]["ImportNum"].ToString();
        //        model.SaveNum = dtbl.Rows[i]["SaveNum"].ToString();
        //        model.Unit = dtbl.Rows[i]["Unit"].ToString();
        //        model.SampleBaseNum = Convert.ToInt16(dtbl.Rows[i]["SampleBaseNum"]);
        //        model.SampleNum = Convert.ToDouble(dtbl.Rows[i]["SampleNum"]);
        //        model.SampleUnit = dtbl.Rows[i]["SampleUnit"].ToString();
        //        model.SampleModel = dtbl.Rows[i]["SampleModel"].ToString();
        //        model.SampleLevel = dtbl.Rows[i]["SampleLevel"].ToString();
        //        model.SampleState = dtbl.Rows[i]["SampleState"].ToString();
        //        model.Standard = clsStandardOpr.GetNameFromCode(standardCode);
        //        model.CheckMachine = clsMachineOpr.GetMachineNameFromCode(machineCode);
        //        model.CheckMachineModel = clsMachineOpr.GetNameFromCode("MachineModel", machineCode);
        //        model.MachineCompany = clsMachineOpr.GetNameFromCode("Company", machineCode);
        //        model.CheckTotalItem = clsCheckItemOpr.GetNameFromCode(dtbl.Rows[i]["CheckTotalItem"].ToString());
        //        model.CheckValueInfo = dtbl.Rows[i]["CheckValueInfo"].ToString();
        //        model.StandValue = dtbl.Rows[i]["StandValue"].ToString();
        //        model.Result = dtbl.Rows[i]["Result"].ToString();
        //        model.ResultInfo = dtbl.Rows[i]["ResultInfo"].ToString();
        //        model.OrCheckNo = dtbl.Rows[i]["OrCheckNo"].ToString();
        //        model.UpperCompany = dtbl.Rows[i]["UpperCompany"].ToString();
        //        model.CheckType = dtbl.Rows[i]["CheckType"].ToString();
        //        model.CheckUnitName = clsUserUnitOpr.GetNameFromCode(checkUnitCode);
        //        model.CheckUnitInfo = clsUserUnitOpr.GetNameFromCode("ShortName", checkUnitCode);
        //        model.Checker = clsUserInfoOpr.NameFromCode(checkerCode);
        //        model.Assessor = clsUserInfoOpr.NameFromCode(assessorCode);
        //        model.Organizer = clsUserInfoOpr.NameFromCode(organizerCode);
        //        model.UpLoader = CurrentUser.GetInstance().UserInfo.Name;
        //        model.Remark = dtbl.Rows[i]["Remark"].ToString();

        //        Opr1.Insert(model, out sErr);
        //        if (!sErr.Equals(string.Empty))
        //        {
        //            continue;
        //        }

        //        //�����ϴ��ı�־λ���ϴ�ʱ��
        //        clsResult result = new clsResult();
        //        result.SysCode = sysCode;
        //        result.IsSended = true;
        //        result.SendedDate = DateTime.Now;
        //        result.Sender = CurrentUser.GetInstance().UserInfo.UserCode;

        //        opr.SetUploadFlag(result, out sErr);
        //    }
        //}


        ///// <summary>
        ///// ���ϵͳ���ݿ�汾
        ///// </summary>
        //public static void UpdateExeDB()
        //{
        //    string sResult = ExistVer();

        //    if (sResult.Equals("false"))
        //    {
        //        bool ret = UpdateLocalDBVer("1.2");
        //        if (ret)
        //        {
        //            ret = UpdateLocalDBVer("1.3");
        //            if (ret)
        //            {
        //                MessageBox.Show("ϵͳ���ݿ��Ѿ����µ����°汾��1.3", "ϵͳ������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("ϵͳ���ݿ�������°汾1.3ʧ�ܣ�", "ϵͳ������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("ϵͳ���ݿ�������°汾1.2ʧ�ܣ�", "ϵͳ������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    else if (sResult.Equals("1.2"))
        //    {
        //        bool ret = UpdateLocalDBVer("1.3");
        //        if (ret)
        //        {
        //            MessageBox.Show("ϵͳ���ݿ��Ѿ����µ����°汾��1.3", "ϵͳ������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("ϵͳ���ݿ�������°汾1.3ʧ�ܣ�", "ϵͳ������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}

        ///// <summary>
        ///// ���µ�ǰϵͳ���ݿ�汾,����ԭ�п�ṹ
        ///// </summary>
        ///// <param name="DBVer"></param>
        ///// <returns></returns>
        //public static bool UpdateLocalDBVer(string DBVer)
        //{
        //    string sErrMsg = string.Empty;

        //    try
        //    {
        //        bool bRet = false;
        //        string sql = string.Empty;
        //        if (DBVer.Equals("1.2"))
        //        {
        //            sql = "Create Table tStockRecord(SysCode Text(50) Not Null CONSTRAINT tStockRecordConstraint Primary Key,StdCode Text(50),CompanyID  Text(50),CompanyName  Text(200),DisplayName Text(200) ,InputDate DateTime,FoodID Text(50),FoodName Text(100),Model Text(200) ,InputNumber FLOAT Not Null,OutputNumber FLOAT Not Null ,Unit Text(50),ProduceDate DateTime,ExpirationDate Text(50),ProduceCompanyID Text(50),ProduceCompanyName Text(200),PrivoderID Text(50),PrivoderName Memo, LinkInfo Text(250),LinkMan Text(50),CertificateType1 Text(250) ,CertificateType2 Text(250),CertificateType3 Text(250),CertificateType4 Text(250),CertificateType5 Text(250),CertificateType6 Text(250) ,CertificateType7 Text(250),CertificateType8 Text(250),CertificateType9 Text(250),CertificateInfo Text(250),MakeMan Text(50),Remark Memo ,DistrictCode Text(50),IsSended bit,SentDate DateTime) ";
        //            bRet = DataBase.ExecuteCommand(sql, out sErrMsg);
        //            if (bRet)
        //            {
        //                sql = "Create Table tSaleRecord(SysCode Text(50) Not Null CONSTRAINT tSaleRecordConstraint Primary Key,StdCode Text(50),CompanyID  Text(50),CompanyName  Text(200),DisplayName Text(200) ,SaleDate DateTime,FoodID Text(50),FoodName Text(100),Model Text(200) ,SaleNumber FLOAT Not Null,Price FLOAT Not Null ,Unit Text(50),PurchaserID Text(50),PurchaserName Text(200), LinkInfo Text(250),LinkMan Text(50),MakeMan Text(50),Remark Memo ,DistrictCode Text(50),IsSended bit,SentDate DateTime)";
        //                bRet = DataBase.ExecuteCommand(sql, out sErrMsg);
        //                if (bRet)
        //                {
        //                    sql = "Insert Into tSysOpt(SysCode,OptDes,OptValue) Values('030102','�汾��','1.2')";
        //                    bRet = DataBase.ExecuteCommand(sql, out sErrMsg);
        //                    if (bRet)
        //                    {
        //                        sql = "Update tMachine Set IsSupport=False";
        //                        bRet = DataBase.ExecuteCommand(sql, out sErrMsg);

        //                        sql = "Insert Into tMachine(SysCode,MachineName,MachineModel,Company,Protocol,LinkComNo,IsSupport,TestValue,TestSign,LinkStdCode) Values('008','NC-800ũҩ�ж����ٲⶨ��','NC-800','���ݷ�����������','NC-800ũ���ǲ��',1,True,50,'<','00003')";
        //                        bRet = DataBase.ExecuteCommand(sql, out sErrMsg);
        //                    }
        //                }
        //            }
        //            return bRet;
        //        }
        //        else if (DBVer.Equals("1.3"))
        //        {

        //            //��������
        //            sql = "Alter Table tResult ADD COLUMN CheckedCompanyName Text(250), CheckedComDis Text(200),UpperCompanyName Text(200)";
        //            bRet = DataBase.ExecuteCommand(sql, out sErrMsg);
        //            if (bRet)
        //            {
        //                sql = "Update tSysOpt Set OptValue='1.3' Where SysCode='030102'";
        //                bRet = DataBase.ExecuteCommand(sql, out sErrMsg);
        //                if (clsResultOpr.UpdateOldResult(out sErrMsg) != 1)
        //                {
        //                    return false;
        //                }
        //            }
        //            return bRet;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        sErrMsg = e.Message;
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// ��������
        ///// </summary>
        ///// <param name="whereStr"></param>
        ///// <param name="MDBFile"></param>
        //public static void SaveSendDB(string whereStr, string MDBFile)
        //{
        //    clsSend_ResultOpr Opr1 = new clsSend_ResultOpr(MDBFile);
        //    clsResultOpr opr = new clsResultOpr();
        //    clsSend_Result model = null;
        //    DataTable dtbl = opr.GetAsDataTable(whereStr, "SysCode");
        //    string sErr = string.Empty;
        //    string syscode = string.Empty;
        //    string foodcode = string.Empty;
        //    string checkplacecode = string.Empty;
        //    string checkedcompanycode = string.Empty;
        //    string comFullName = string.Empty;
        //    string ProduceCompanycode = string.Empty;
        //    string produceplacecode = string.Empty;
        //    string standardcode = string.Empty;
        //    string machinecode = string.Empty;
        //    string checkunitcode = string.Empty;
        //    string checkercode = string.Empty;
        //    string assessorcode = string.Empty;
        //    string Organizercode = string.Empty;
        //    for (int i = 0; i < dtbl.Rows.Count; i++)
        //    {
        //        sErr = string.Empty;

        //        if (dtbl.Rows[i]["SysCode"].ToString().Equals(string.Empty)
        //            || dtbl.Rows[i]["CheckNo"].ToString().Equals(string.Empty)
        //            || dtbl.Rows[i]["FoodCode"].ToString().Equals(string.Empty))
        //        {
        //            continue;
        //        }
        //        syscode = dtbl.Rows[i]["SysCode"].ToString();
        //        foodcode = dtbl.Rows[i]["FoodCode"].ToString();
        //        checkplacecode = dtbl.Rows[i]["CheckPlaceCode"].ToString();
        //        checkedcompanycode = dtbl.Rows[i]["CheckedCompany"].ToString();
        //        comFullName = dtbl.Rows[i]["CheckedCompanyName"].ToString();
        //        ProduceCompanycode = dtbl.Rows[i]["ProduceCompany"].ToString();
        //        produceplacecode = dtbl.Rows[i]["ProducePlace"].ToString();
        //        standardcode = dtbl.Rows[i]["Standard"].ToString();
        //        machinecode = dtbl.Rows[i]["CheckMachine"].ToString();
        //        checkunitcode = dtbl.Rows[i]["CheckUnitCode"].ToString();
        //        checkercode = dtbl.Rows[i]["Checker"].ToString();
        //        assessorcode = dtbl.Rows[i]["Assessor"].ToString();
        //        Organizercode = dtbl.Rows[i]["Organizer"].ToString();
        //        model = new clsSend_Result();
        //        model.SysCode = syscode;
        //        model.ResultType = dtbl.Rows[i]["ResultType"].ToString();
        //        model.CheckNo = dtbl.Rows[i]["CheckNo"].ToString();
        //        model.SampleCode = dtbl.Rows[i]["SampleCode"].ToString();
        //        model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
        //        model.FoodName = clsFoodClassOpr.NameFromCode(foodcode);
        //        model.FoodInfo = clsFoodClassOpr.LevelNamesFromCode(foodcode, ShareOption.FoodCodeLevel);
        //        model.CheckPlace = clsDistrictOpr.NameFromCode(checkplacecode);
        //        model.CheckPlaceInfo = clsDistrictOpr.LevelNamesFromCode(checkplacecode, ShareOption.DistrictCodeLevel);

        //        if (dtbl.Rows[i]["UpperCompanyName"].ToString().Trim().Equals(""))
        //        {
        //            model.CheckedCompany = comFullName;
        //            model.CheckedCompanyInfo = comFullName;
        //        }
        //        else
        //        {
        //            model.CheckedCompany = dtbl.Rows[i]["UpperCompanyName"].ToString();
        //            model.CheckedCompanyInfo = comFullName;
        //        }
        //        model.CheckedComDis = dtbl.Rows[i]["CheckedComDis"].ToString();
        //        model.SentCompany = dtbl.Rows[i]["SentCompany"].ToString();
        //        model.Provider = dtbl.Rows[i]["Provider"].ToString();
        //        model.ProduceDate = Convert.ToDateTime(dtbl.Rows[i]["ProduceDate"]);

        //        model.ProduceCompany = clsCompanyOpr.NameFromCode(ProduceCompanycode);
        //        model.ProducePlace = clsProduceAreaOpr.NameFromCode(produceplacecode);
        //        model.ProducePlaceInfo = clsProduceAreaOpr.LevelNamesFromCode(produceplacecode, ShareOption.DistrictCodeLevel);
        //        model.TakeDate = Convert.ToDateTime(dtbl.Rows[i]["TakeDate"]);
        //        model.CheckStartDate = Convert.ToDateTime(dtbl.Rows[i]["CheckStartDate"]);
        //        model.CheckEndDate = Convert.ToDateTime(dtbl.Rows[i]["CheckEndDate"]);
        //        model.ImportNum = dtbl.Rows[i]["ImportNum"].ToString();
        //        model.SaveNum = dtbl.Rows[i]["SaveNum"].ToString();
        //        model.Unit = dtbl.Rows[i]["Unit"].ToString();
        //        model.SampleBaseNum = Convert.ToInt16(dtbl.Rows[i]["SampleBaseNum"]);
        //        model.SampleNum = Convert.ToDouble(dtbl.Rows[i]["SampleNum"]);
        //        model.SampleUnit = dtbl.Rows[i]["SampleUnit"].ToString();
        //        model.SampleModel = dtbl.Rows[i]["SampleModel"].ToString();
        //        model.SampleLevel = dtbl.Rows[i]["SampleLevel"].ToString();
        //        model.SampleState = dtbl.Rows[i]["SampleState"].ToString();
        //        model.Standard = clsStandardOpr.GetNameFromCode(standardcode);

        //        model.CheckMachine = clsMachineOpr.GetMachineNameFromCode(machinecode);
        //        model.CheckMachineModel = clsMachineOpr.GetNameFromCode("MachineModel", machinecode);
        //        model.MachineCompany = clsMachineOpr.GetNameFromCode("Company", machinecode);
        //        model.CheckTotalItem = dtbl.Rows[i]["CheckTotalItem"].ToString();
        //        model.CheckValueInfo = dtbl.Rows[i]["CheckValueInfo"].ToString();
        //        model.StandValue = dtbl.Rows[i]["StandValue"].ToString();
        //        model.Result = dtbl.Rows[i]["Result"].ToString();
        //        model.ResultInfo = dtbl.Rows[i]["ResultInfo"].ToString();
        //        model.OrCheckNo = dtbl.Rows[i]["OrCheckNo"].ToString();
        //        model.UpperCompany = dtbl.Rows[i]["UpperCompany"].ToString();
        //        model.CheckType = dtbl.Rows[i]["CheckType"].ToString();
        //        model.Checker = clsUserInfoOpr.NameFromCode(checkercode);
        //        model.CheckUnitName = clsUserUnitOpr.GetNameFromCode(checkunitcode);
        //        model.CheckUnitInfo = clsUserUnitOpr.GetNameFromCode("ShortName", checkunitcode);
        //        model.Assessor = clsUserInfoOpr.NameFromCode(assessorcode);

        //        model.Organizer = clsUserInfoOpr.NameFromCode(Organizercode);
        //        model.UpLoader = CurrentUser.GetInstance().UserInfo.Name;
        //        model.Remark = dtbl.Rows[i]["Remark"].ToString();

        //        Opr1.Insert(model, out sErr);

        //        if (!sErr.Equals(string.Empty))
        //        {
        //            continue;
        //        }

        //        //�����ϴ��ı�־λ���ϴ�ʱ��
        //        clsResult result = new clsResult();
        //        result.SysCode = syscode;
        //        result.IsSended = true;
        //        result.SendedDate = DateTime.Now;
        //        result.Sender = CurrentUser.GetInstance().UserInfo.UserCode;
        //        opr.SetUploadFlag(result, out sErr);
        //    }
        //}
        //public static string sumError { get; set; }  
        #endregion
    }
}
