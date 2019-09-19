using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DY.FoodClientLib;
using System.Text.RegularExpressions;

namespace FoodClient
{
    /// <summary>
    /// frmCheckedComSelect ��ժҪ˵����
    /// </summary>
    public class frmCheckedComSelect : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button btnLocation;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnNext;
        private System.ComponentModel.Container components = null;

        private TreeNode[] prevNodes;
        private string tag;
        private string _code;
        private string _type;
        private readonly clsCompanyOpr companyBll;
        private readonly clsCompanyKindOpr companyKindBLL = new clsCompanyKindOpr();

        /// <summary>
        /// ��ǰ�����
        /// </summary>
        public string sNodeTag = string.Empty;

        /// <summary>
        /// ��ǰ�������
        /// </summary>
        public string sNodeName = string.Empty;

        /// <summary>
        /// ������λ��������
        /// </summary>
        public string sParentCompanyName = string.Empty;

        /// <summary>
        /// ������λ������
        /// </summary>
        public string sParentCompanyTag = string.Empty;

        /// <summary>
        /// ������λ������Ϣ
        /// </summary>
        public string sNodeCompanyInfo = string.Empty;

        /// <summary>
        /// ���쵥λ��ǩ
        /// </summary>
        private string companyType = ShareOption.CompanyProperty1;


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pCode"></param>
        public frmCheckedComSelect(string pType, string pCode)
        {
            InitializeComponent();

            companyBll = new clsCompanyOpr();

            _type = pType;
            _code = pCode;
            prevNodes = new TreeNode[ShareOption.MaxLevel];
            for (int i = 0; i < ShareOption.MaxLevel; i++)
            {
                prevNodes[i] = new TreeNode();
            }
        }

        private void frmCheckedComSelect_Load(object sender, System.EventArgs e)
        {
            tag = this.Tag.ToString();
            string code = string.Empty;
            string sqlWhere = string.Empty;
            string sysCode = string.Empty;
            TreeNode posNode = null;
            TreeNode theNode = new TreeNode();
            TreeNode preNode = new TreeNode();
            DataTable dtblDistrict = null;
            DataTable dtblCompanyKind = null;
            clsDistrictOpr districtBll = new clsDistrictOpr();
            if (tag.Equals("Checked"))
            {
                Cursor = Cursors.WaitCursor;
                if (!ShareOption.AllowHandInputCheckUint)//��������ֹ�¼�뱻�쵥λ
                {
                    if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)//������
                    {
                        if (this.treeView1.Nodes.Count > 0)
                        {
                            while (this.treeView1.Nodes.Count > 0)
                            {
                                this.treeView1.Nodes.RemoveAt(0);
                            }
                        }
                        //�õ���ⵥλ���¼���λ
                        string districtCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", "0001");
                        //2015��12��18��wenj ������ѯ������δ���|��ͣ�õĲ�����ʾ
                        string strSql = "SysCode LIKE '" + districtCode + "_%'" + " And IsLock = False And IsReadOnly = True";
                        dtblDistrict = districtBll.GetAsDataTable(strSql, "SysCode", 0);
                        //�õ�һ����λ������ݼ�
                        sqlWhere = string.Format("IsLock=0 And CompanyProperty = '{0}' And IsLock = False And IsReadOnly = true AND Len(SysCode)=3", companyType);//
                        dtblCompanyKind = companyKindBLL.GetAsDataTable(sqlWhere, "SysCode", 1);
                        if (dtblCompanyKind == null || dtblCompanyKind.Rows.Count <= 0)
                        {
                            MessageBox.Show(this, "��λ���û�����ã�������!");
                            Cursor = Cursors.Default;
                            return;
                        }
                        string subWhere = string.Empty;
                        if (dtblDistrict.Rows.Count <= 0)//������¼���λ
                        {
                            for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
                            {
                                theNode = new TreeNode();
                                sysCode = dtblCompanyKind.Rows[j]["SysCode"].ToString();
                                theNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
                                theNode.Tag = "DWLX" + sysCode;

                                //ȡ�������������ȡ����λ����
                                getSubCompanyKindNode(sysCode, districtCode, ref theNode);
                                #region ע��
                                //if (dtblCompany != null && dtblCompany.Rows.Count > 0)
                                //{
                                //    for (int k = 0; k < dtblCompany.Rows.Count; k++)
                                //    {
                                //        comNode = new TreeNode();
                                //        sysCode = dtblCompany.Rows[k]["SysCode"].ToString();
                                //        comNode.Text = dtblCompany.Rows[k]["FullName"].ToString();
                                //        comNode.Tag = sysCode;

                                //        theNode.Nodes.Add(comNode);

                                //        sql = string.Format("Property='{0}' And IsReadOnly=true  And StdCode Like '{1}_%'", companyType, sysCode);
                                //        dtblSubCompany = companyBll.GetAsDataTable(sql, "SysCode", 1);

                                //        if (dtblSubCompany != null && dtblSubCompany.Rows.Count > 0)
                                //        {
                                //            for (int l = 0; l < dtblSubCompany.Rows.Count; l++)
                                //            {
                                //                comsNode = new TreeNode();
                                //                comsNode.Text = dtblSubCompany.Rows[l]["FullName"].ToString();
                                //                comsNode.Tag = dtblSubCompany.Rows[l]["SysCode"].ToString();
                                //                comNode.Nodes.Add(comsNode);
                                //            }
                                //        }
                                //    }
                                //}
                                #endregion
                                this.treeView1.Nodes.Add(theNode);
                            }
                            if (this.treeView1.Nodes.Count >= 1)
                            {
                                this.treeView1.Nodes[0].Expand();
                            }
                        }
                        else   //�����¼���λ
                        {
                            for (int i = 0; i < dtblDistrict.Rows.Count; i++)
                            {
                                code = dtblDistrict.Rows[i]["SysCode"].ToString();

                                theNode = new TreeNode();
                                theNode.Text = dtblDistrict.Rows[i]["Name"].ToString();
                                theNode.Tag = "XZJG" + code;

                                //���ýڵ��ǲ�����ϸ�ӽڵ㣬�Ǿ����ӵ�λ���ͽڵ�
                                if (clsDistrictOpr.DistrictIsMX(code))
                                {
                                    for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
                                    {
                                        TreeNode childCKNode = new TreeNode();
                                        childCKNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
                                        sysCode = dtblCompanyKind.Rows[j]["SysCode"].ToString();
                                        childCKNode.Tag = "DWLX" + sysCode;
                                        theNode.Nodes.Add(childCKNode);
                                        //ȡ�������������ȡ����λ����
                                        getSubCompanyKindNode(sysCode, code, ref childCKNode);

                                        #region ע��
                                        //if (dtblCompany != null && dtblCompany.Rows.Count > 0)
                                        //{
                                        //    for (int k = 0; k < dtblCompany.Rows.Count; k++)
                                        //    {
                                        //        comNode = new TreeNode();
                                        //        comNode.Text = dtblCompany.Rows[k]["FullName"].ToString();
                                        //        comNode.Tag = dtblCompany.Rows[k]["SysCode"].ToString();
                                        //        childCKNode.Nodes.Add(comNode);

                                        //        sql = string.Format("Property='{0}' And IsReadOnly=true And StdCode Like '{1}_%'",companyType, dtblCompany.Rows[k]["StdCode"].ToString());
                                        //        dtblSubCompany = companyBll.GetAsDataTable(sql, "SysCode", 1);

                                        //        if (dtblSubCompany != null && dtblSubCompany.Rows.Count > 0)
                                        //        {
                                        //            for (int l = 0; l < dtblSubCompany.Rows.Count; l++)
                                        //            {
                                        //                comsNode = new TreeNode();
                                        //                comsNode.Text = dtblSubCompany.Rows[l]["FullName"].ToString();
                                        //                comsNode.Tag = dtblSubCompany.Rows[l]["SysCode"].ToString();
                                        //                comNode.Nodes.Add(comsNode);
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                        #endregion
                                    }
                                }
                                addTreeNode(code, districtCode, theNode);
                                #region ע��
                                //int iMod = (code.Length - districtCode.Length) / ShareOption.DistrictCodeLevel;
                                //if (iMod == 1)
                                //{
                                //    this.treeView1.Nodes.Add(theNode);
                                //}
                                //else if (iMod >= 2)
                                //{
                                //    prevNodes[iMod - 2].Nodes.Add(theNode);
                                //}

                                //if (iMod >= 1)
                                //{
                                //    prevNodes[iMod - 1] = theNode;
                                //}

                                //if (this.treeView1.Nodes.Count >= 1)
                                //{
                                //    this.treeView1.Nodes[0].Expand();
                                //}
                                #endregion
                            }
                        }
                    }

                    else  //��ҵ��
                    {
                        string tempTag = string.Empty;
                        theNode = new TreeNode();
                        theNode.Text = clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);

                        if (!ShareOption.IsDataLink)//����������
                        {
                            tempTag = clsUserUnitOpr.GetNameFromCode("CompanyId", ShareOption.DefaultUserUnitCode);
                        }
                        else
                        {
                            tempTag = clsUserUnitOpr.GetStdCode(ShareOption.DefaultUserUnitCode);//ͨ���û���λ�����ȡ��ҵ��׼��
                        }
                        theNode.Tag = tempTag;
                        sqlWhere = string.Format("ISLOCK=FALSE AND ISREADONLY=TRUE AND Property='{0}' AND StdCode LIKE '{1}%'", companyType, tempTag);

                        dtblDistrict = companyBll.GetAsDataTable(sqlWhere, "SysCode", 1);//000128001

                        if (dtblDistrict != null && dtblDistrict.Rows.Count > 0)
                        {
                            TreeNode childNode = null;
                            foreach (DataRow dr in dtblDistrict.Rows)
                            {
                                code = dr["SysCode"].ToString();
                                childNode = new TreeNode();
                                childNode.Text = dr["FullName"].ToString();
                                childNode.Tag = code;

                                if (_code.Equals(code))
                                {
                                    posNode = childNode;
                                }
                                theNode.Nodes.Add(childNode);
                            }
                        }
                        treeView1.Nodes.Add(theNode);

                    }
                    FrmMain.IsLoadCheckedComSel = true;
                }
                else
                {
                    TreeNode comsNode = null;
                    theNode = new TreeNode();
                    theNode.Text = clsCompanyOpr.NameFromCode(_code);
                    theNode.Tag = _code;

                    sqlWhere = string.Format("Property='{0}' And IsReadOnly=true And StdCode Like '{1}_%'", companyType, clsCompanyOpr.StdCodeFromCode(_code));

                    dtblDistrict = companyBll.GetAsDataTable(sqlWhere, "SysCode", 1);

                    if (dtblDistrict != null && dtblDistrict.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtblDistrict.Rows.Count; i++)
                        {
                            comsNode = new TreeNode();
                            comsNode.Text = dtblDistrict.Rows[i]["FullName"].ToString();
                            comsNode.Tag = dtblDistrict.Rows[i]["SysCode"].ToString();
                            theNode.Nodes.Add(comsNode);
                        }
                    }

                    treeView1.Nodes.Add(theNode);

                    if (treeView1.Nodes.Count >= 1)
                    {
                        treeView1.Nodes[0].Expand();
                    }
                    FrmMain.IsLoadCheckedComSel = false;
                }
            }

            else if (tag.Equals("UpperChecked"))//�ϼ���λ
            {
                if (FrmMain.IsLoadCheckedUpperComSel)
                {
                    return;
                }
                this.Text = "ѡ��" + ShareOption.AreaTitle;
                this.label3.Text = ShareOption.AreaTitle + "��";//�����г�

                Cursor = Cursors.WaitCursor;

                //�����������
                if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
                {
                    //�õ���ⵥλ���¼���λ
                    string districtCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", "0001");
                    dtblDistrict = districtBll.GetAsDataTable(string.Format("SysCode Like '{0}%'", districtCode), "SysCode", 0);

                    dtblCompanyKind = companyKindBLL.GetAsDataTable(string.Format("IsLock=0 And IsReadOnly=true And CompanyProperty='{0}'", companyType), "SysCode", 1);
                    if (dtblCompanyKind == null || dtblCompanyKind.Rows.Count <= 0)
                    {
                        MessageBox.Show(this, "��λ���û�����ã�������!");
                        Cursor = Cursors.Default;
                        return;
                    }

                    if (dtblDistrict.Rows.Count <= 0)
                    {
                        getUpperCompanyTreeNode(dtblCompanyKind, districtCode, true, ref theNode);
                        #region ע��
                        //TreeNode comNode = null;
                        //for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
                        //{
                        //    theNode = new TreeNode();
                        //    theNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
                        //    sysCode = dtblCompanyKind.Rows[j]["SysCode"].ToString();
                        //    theNode.Tag = "DWLX" + sysCode;

                        //    string sql = string.Format("Property='{0}' And IsReadOnly=true and KindCode = '{1}' And DistrictCode='{2}' And Len(StdCode)={3}", companyType, sysCode, districtCode, ShareOption.CompanyStdCodeLen);

                        //    dtblCompany = companyBll.GetAsDataTable(sql, "SysCode", 1);

                        //    if (dtblCompany != null && dtblCompany.Rows.Count >= 1)
                        //    {
                        //        for (int k = 0; k < dtblCompany.Rows.Count; k++)
                        //        {
                        //            comNode = new TreeNode();
                        //            comNode.Text = dtblCompany.Rows[k]["FullName"].ToString();
                        //            comNode.Tag = dtblCompany.Rows[k]["SysCode"].ToString();
                        //            theNode.Nodes.Add(comNode);
                        //        }
                        //    }
                        //    this.treeView1.Nodes.Add(theNode);
                        //}
                        #endregion
                        if (this.treeView1.Nodes.Count >= 1)
                        {
                            this.treeView1.Nodes[0].Expand();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtblDistrict.Rows.Count; i++)
                        {
                            code = dtblDistrict.Rows[i]["SysCode"].ToString();
                            theNode = new TreeNode();
                            theNode.Text = dtblDistrict.Rows[i]["Name"].ToString();
                            theNode.Tag = "XZJG" + code;

                            //���ýڵ��ǲ�����ϸ�ӽڵ㣬�Ǿ����ӵ�λ���ͽڵ�
                            if (clsDistrictOpr.DistrictIsMX(code))
                            {
                                getUpperCompanyTreeNode(dtblCompanyKind, code, false, ref theNode);

                                #region ע��
                                //TreeNode comNode = null;
                                //string sql = string.Empty;
                                //TreeNode childNode = null;

                                //for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
                                //{
                                //    childNode = new TreeNode();
                                //    childNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
                                //    sysCode = dtblCompanyKind.Rows[j]["SysCode"].ToString();
                                //    childNode.Tag = "DWLX" + sysCode;
                                //    theNode.Nodes.Add(childNode);

                                //    sql = string.Format("Property='{0}' And IsReadOnly=true and KindCode = '{1}' And DistrictCode='{2}' And Len(StdCode)={3}", companyType, sysCode, code, ShareOption.CompanyStdCodeLen);

                                //    dtblSubCompany = companyBll.GetAsDataTable(sql, "SysCode", 1);

                                //    if (dtblSubCompany != null && dtblSubCompany.Rows.Count > 0)
                                //    {
                                //        for (int k = 0; k < dtblSubCompany.Rows.Count; k++)
                                //        {
                                //            comNode = new TreeNode();
                                //            comNode.Text = dtblSubCompany.Rows[k]["FullName"].ToString();
                                //            comNode.Tag = dtblSubCompany.Rows[k]["SysCode"].ToString();

                                //            childNode.Nodes.Add(comNode);
                                //        }
                                //    }
                                //}
                                #endregion
                            }
                            addTreeNode(code, districtCode, theNode);

                            #region ע���ظ�����
                            //int iMod = (code.Length - districtCode.Length) / ShareOption.DistrictCodeLevel;
                            //if (iMod <= 1)// (iMod == 1)
                            //{
                            //    this.treeView1.Nodes.Add(theNode);
                            //}
                            //else
                            //{
                            //    if (iMod >= 2)
                            //    {
                            //        prevNodes[iMod - 2].Nodes.Add(theNode);
                            //    }
                            //}
                            //if (iMod >= 1)
                            //{
                            //    prevNodes[iMod - 1] = theNode;
                            //}

                            //if (this.treeView1.Nodes.Count >= 1)
                            //    this.treeView1.Nodes[0].Expand();
                            #endregion
                        }
                    }
                }
                FrmMain.IsLoadCheckedUpperComSel = true;
            }
            else  //������λ
            {
                if (tag.Equals("Produce"))
                {
                    this.Text = "ѡ������/�ӹ���λ/����";
                    this.label3.Text = ShareOption.ProductionUnitNameTag + "��"; //���ò�ͬ�ı�ǩ
                }
                else
                {
                    this.Text = "ѡ�񹩻���λ/����";
                    this.label3.Text = "������λ��";
                }

                dtblDistrict = companyBll.GetAsDataTable("IsLock=false And IsReadOnly=true And Property='������λ' ", "SysCode", 1);
                if (dtblDistrict != null)
                {
                    foreach (DataRow dr in dtblDistrict.Rows)
                    {
                        code = dr["SysCode"].ToString();

                        TreeNode childNode = new TreeNode();
                        childNode.Text = dr["FullName"].ToString();
                        childNode.Tag = code;
                        if (_code.Equals(code))
                        {
                            posNode = childNode;
                        }
                        this.treeView1.Nodes.Add(childNode);
                    }
                }
            }

            //��λtreeview���ڽڵ�
            if (posNode != null)
            {
                this.treeView1.SelectedNode = posNode;
                posNode.EnsureVisible();
            }
            else
            {
                this.treeView1.ExpandAll();
            }

            TreeNode CurrentNode = null;
            if (this.treeView1.Nodes.Count > 0)
            {
                CurrentNode = this.treeView1.Nodes[0];
                while (CurrentNode != null)
                {
                    if (CurrentNode.Tag.ToString().Equals(_code))
                    {
                        this.treeView1.SelectedNode = CurrentNode;
                        CurrentNode.EnsureVisible();
                        CurrentNode.Expand();
                        Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        CurrentNode = this.GetNextNode(CurrentNode, false);
                        if (CurrentNode == null)
                        {
                            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                            this.treeView1.ExpandAll();
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
            }

            if (this.treeView1.Nodes.Count <= 0)
            {
                this.btnOK.Enabled = false;
                this.txtName.Enabled = false;
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// ��ȡ������λ�����������λĿ¼��
        /// </summary>
        /// <param name="sysCode"></param>
        /// <param name="theNode"></param>
        private void getSubCompanyKindNode(string sysCode, string districtCode, ref TreeNode theNode)
        {
            string sqlWhere = string.Format("IsLock=0 And CompanyProperty='{0}' And IsReadOnly=true AND Mid(SysCode,1,3)='{1}' AND LEN(SysCode)=6", companyType, sysCode);
            DataTable dtblSubCK = companyKindBLL.GetAsDataTable(sqlWhere, "SysCode", 1);
            DataTable dtblCompany = null;
            if (dtblSubCK != null && dtblSubCK.Rows.Count >= 1)//����ж�����λ���
            {

                for (int n = 0; n < dtblSubCK.Rows.Count; n++)
                {
                    TreeNode childCK = new TreeNode();
                    string tcode = dtblSubCK.Rows[n]["SysCode"].ToString();
                    childCK.Text = dtblSubCK.Rows[n]["Name"].ToString();
                    childCK.Tag = tcode;
                    theNode.Nodes.Add(childCK);

                    //ȡ����Ӧ����µĵ�λ
                    sqlWhere = string.Format("Property='{0}' And IsReadOnly=true and KindCode = '{1}' And DistrictCode='{2}' And Len(StdCode)={3}", companyType, tcode, districtCode, ShareOption.CompanyStdCodeLen);

                    dtblCompany = companyBll.GetAsDataTable(sqlWhere, "SysCode", 1);
                    if (dtblCompany.Rows.Count <= 0)
                    {
                        continue;
                    }
                    getCompanyTreeNode(dtblCompany, ref childCK);
                }
            }
            else //���û�ж�����λ���
            {
                sqlWhere = string.Format("Property='{0}' And IsReadOnly=true and KindCode = '{1}' And DistrictCode='{2}' And Len(StdCode)={3}", companyType, sysCode, districtCode, ShareOption.CompanyStdCodeLen);

                dtblCompany = companyBll.GetAsDataTable(sqlWhere, "SysCode", 1);
                if (dtblCompany.Rows.Count <= 0)
                {
                    return;
                }
                getCompanyTreeNode(dtblCompany, ref theNode);
            }
        }

        /// <summary>
        /// �������ڵ�ֵ����
        /// </summary>
        /// <param name="code"></param>
        /// <param name="districtCode"></param>
        /// <param name="theNode"></param>
        private void addTreeNode(string code, string districtCode, TreeNode theNode)
        {
            int iMod = (code.Length - districtCode.Length) / ShareOption.DistrictCodeLevel;
            if (iMod <= 1)// (iMod == 1)
            {
                this.treeView1.Nodes.Add(theNode);
            }
            else
            {
                if (iMod >= 2)
                {
                    prevNodes[iMod - 2].Nodes.Add(theNode);
                }
            }
            if (iMod >= 1)
            {
                prevNodes[iMod - 1] = theNode;
            }

            if (this.treeView1.Nodes.Count >= 1)
            {
                this.treeView1.Nodes[0].Expand();
            }
        }

        /// <summary>
        /// ��ȡ������λ���ṹ
        /// </summary>
        /// <param name="dtblCompany"></param>
        /// <param name="childCKNode"></param>
        private void getCompanyTreeNode(DataTable dtblCompany, ref TreeNode childCKNode)
        {
            if (dtblCompany != null && dtblCompany.Rows.Count > 0)
            {
                string sysCode = string.Empty;
                string sqlWhere = string.Empty;
                DataTable dtblSubCompany = null;
                TreeNode comNode = null;
                TreeNode comsNode = null;

                for (int k = 0; k < dtblCompany.Rows.Count; k++)
                {
                    comNode = new TreeNode();
                    comNode.Text = dtblCompany.Rows[k]["FullName"].ToString();
                    sysCode = dtblCompany.Rows[k]["SysCode"].ToString();
                    comNode.Tag = sysCode;

                    childCKNode.Nodes.Add(comNode);

                    sqlWhere = string.Format("Property='{0}' And IsReadOnly=true And StdCode Like '{1}_%'", companyType, sysCode);
                    dtblSubCompany = companyBll.GetAsDataTable(sqlWhere, "SysCode", 1);

                    if (dtblSubCompany != null && dtblSubCompany.Rows.Count > 0)
                    {
                        for (int l = 0; l < dtblSubCompany.Rows.Count; l++)
                        {
                            comsNode = new TreeNode();
                            comsNode.Text = dtblSubCompany.Rows[l]["FullName"].ToString();
                            comsNode.Tag = dtblSubCompany.Rows[l]["SysCode"].ToString();
                            comNode.Nodes.Add(comsNode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ������λĿ¼��
        /// </summary>
        /// <param name="dtblCompanyKind"></param>
        /// <param name="districtCode"></param>
        /// <param name="theNode"></param>
        private void getUpperCompanyTreeNode(DataTable dtblCompanyKind, string districtCode, bool isFirstNode, ref TreeNode theNode)//
        {
            TreeNode comNode = null;
            TreeNode childNode = null;
            string sql = string.Empty;
            string sysCode = string.Empty;
            DataTable dtblSubCompany = null;

            for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
            {
                childNode = new TreeNode();
                childNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
                sysCode = dtblCompanyKind.Rows[j]["SysCode"].ToString();
                childNode.Tag = "DWLX" + sysCode;
                if (!isFirstNode)
                {
                    theNode.Nodes.Add(childNode);
                }

                sql = string.Format("Property='{0}' And IsReadOnly=true and KindCode = '{1}' And DistrictCode='{2}' And Len(StdCode)={3}", companyType, sysCode, districtCode, ShareOption.CompanyStdCodeLen);

                dtblSubCompany = companyBll.GetAsDataTable(sql, "SysCode", 1);

                if (dtblSubCompany != null && dtblSubCompany.Rows.Count > 0)
                {
                    for (int k = 0; k < dtblSubCompany.Rows.Count; k++)
                    {
                        comNode = new TreeNode();
                        comNode.Text = dtblSubCompany.Rows[k]["FullName"].ToString();
                        comNode.Tag = dtblSubCompany.Rows[k]["SysCode"].ToString();
                        childNode.Nodes.Add(comNode);
                    }
                }
                if (isFirstNode)
                {
                    this.treeView1.Nodes.Add(childNode);
                }
            }
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pCode"></param>
        public void SetFormValues(string pType, string pCode)
        {
            _type = pType;
            _code = pCode;
            TreeNode CurrentNode = null;
            if (this.treeView1.Nodes.Count > 0)
            {
                CurrentNode = this.treeView1.Nodes[0];
                while (CurrentNode != null)
                {
                    if (CurrentNode.Tag.ToString().Equals(_code))
                    {
                        this.treeView1.SelectedNode = CurrentNode;
                        CurrentNode.EnsureVisible();
                        CurrentNode.Expand();
                        return;
                    }
                    else
                    {
                        CurrentNode = this.GetNextNode(CurrentNode, false);
                        if (CurrentNode == null)
                        {
                            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                            this.treeView1.ExpandAll();
                            return;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLocation = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLocation
            // 
            this.btnLocation.Location = new System.Drawing.Point(224, 5);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(72, 24);
            this.btnLocation.TabIndex = 21;
            this.btnLocation.Text = "��λ";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(88, 7);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(128, 21);
            this.txtName.TabIndex = 19;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(-2, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 21);
            this.label3.TabIndex = 20;
            this.label3.Text = "�ܼ���/��λ��";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(304, 343);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 343);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(8, 31);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(384, 304);
            this.treeView1.TabIndex = 16;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(304, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(72, 24);
            this.btnNext.TabIndex = 22;
            this.btnNext.Text = "��һ��";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // frmCheckedComSelect
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(400, 373);
            this.ControlBox = false;
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckedComSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ѡ���ܼ���/��λ";
            this.Load += new System.EventHandler(this.frmCheckedComSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void btnLocation_Click(object sender, System.EventArgs e)
        {
            if (this.txtName.Text.Trim() != "" && this.treeView1.Nodes.Count > 0)
            {
                selectNode(this.txtName.Text.Trim(), false);
            }
        }

        private void selectNode(string sNodeName, bool IsNext)
        {
            bool IsReRun = false;
            TreeNode CurrentNode = null;
            if (!IsNext)
            {
                CurrentNode = this.treeView1.Nodes[0];
            }
            else
            {
                CurrentNode = this.GetNextNode(this.treeView1.SelectedNode, true);
            }
            Regex r = new Regex(sNodeName, RegexOptions.IgnoreCase);
            while (CurrentNode != null)
            {
                Match m = r.Match(CurrentNode.Text);
                if (m.Success)
                {
                    this.treeView1.SelectedNode = CurrentNode;
                    CurrentNode.EnsureVisible();
                    return;
                }
                else
                {
                    if (IsReRun && IsNext && CurrentNode.Equals(this.treeView1.SelectedNode)) return;
                    CurrentNode = this.GetNextNode(CurrentNode, false);
                    if (CurrentNode == null && IsNext)
                    {
                        IsReRun = true;
                        CurrentNode = this.treeView1.Nodes[0];
                    }
                }
            }
        }

        private TreeNode GetNextNode(TreeNode CurrentNode, bool IsReRun)
        {
            TreeNode NextNode = null;
            //�õ���һ���ڵ�
            if (CurrentNode.FirstNode != null)
            {
                NextNode = CurrentNode.FirstNode;
            }
            else if (CurrentNode.NextNode != null)
            {
                NextNode = CurrentNode.NextNode;
            }
            else if (CurrentNode.Parent != null)
            {
                while (CurrentNode.Parent != null)
                {
                    CurrentNode = CurrentNode.Parent;
                    if (CurrentNode.NextNode != null)
                    {
                        NextNode = CurrentNode.NextNode;
                        return NextNode;
                    }
                }
            }
            if (IsReRun && NextNode == null)
            {
                NextNode = CurrentNode.TreeView.Nodes[0];
            }
            return NextNode;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            SelectValue();
        }

        /// <summary>
        /// ѡ���ܼ���/��λ
        /// </summary>
        private void SelectValue()
        {
            sNodeTag = this.treeView1.SelectedNode.Tag.ToString();
            sNodeName = this.treeView1.SelectedNode.Text;

            if (tag.Equals("Checked") || tag.Equals("UpperChecked"))
            {
                if (sNodeTag.Length >= 4)
                {
                    string temp = sNodeTag.Substring(0, 4);
                    if (temp.Equals("DWLX") || temp.Equals("XZJG"))
                        return;
                }
            }
            if (ShareOption.AllowHandInputCheckUint)
                this.sNodeCompanyInfo = clsCompanyOpr.DisplayNameFromCode(sNodeTag);
            else
            {
                if (tag.Equals("Checked"))
                {
                    if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
                    {
                        string sTag = this.treeView1.SelectedNode.Parent.Tag.ToString();
                        if (!sTag.Substring(0, 4).Equals("DWLX") && ShareOption.ApplicationTag != ShareOption.FDAppTag)//ʳҩϵͳ��λֻ��һ������
                        {
                            sParentCompanyName = this.treeView1.SelectedNode.Parent.Text;
                            sParentCompanyTag = sTag;
                            this.sNodeCompanyInfo = clsCompanyOpr.DisplayNameFromCode(sNodeTag);
                        }
                        else
                        {
                            sParentCompanyName = this.treeView1.SelectedNode.Text;
                            sParentCompanyTag = this.treeView1.SelectedNode.Tag.ToString();
                            this.sNodeCompanyInfo = clsCompanyOpr.DisplayNameFromCode(sNodeTag);
                        }
                    }
                    if (ShareOption.SystemVersion == ShareOption.EnterpriseVersion)
                    {
                        sParentCompanyName = this.treeView1.Nodes[0].Text;
                        sParentCompanyTag = this.treeView1.Nodes[0].Tag.ToString();
                        this.sNodeCompanyInfo = clsCompanyOpr.DisplayNameFromCode(sNodeTag);
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            sNodeTag = string.Empty;
            sNodeName = string.Empty;
        }

        private void treeView1_DoubleClick(object sender, System.EventArgs e)
        {
            SelectValue();

            #region  2015��12��22��wenj ������ͬ�ķ����϶�Ϊһ
            //sNodeTag = this.treeView1.SelectedNode.Tag.ToString();
            //sNodeName = this.treeView1.SelectedNode.Text;
            //if (tag.Equals("Checked") || tag.Equals("UpperChecked"))
            //{
            //    if (sNodeTag.Length >= 4)
            //    {
            //        string temp = sNodeTag.Substring(0, 4);
            //        if (temp.Equals("DWLX") || temp.Equals("XZJG"))
            //        {
            //            return;
            //        }
            //    }
            //}

            //if (ShareOption.AllowHandInputCheckUint)
            //{
            //    if (clsCompanyOpr.StdCodeFromCode(sNodeTag).Length == ShareOption.CompanyStdCodeLevel)
            //    {
            //        this.sNodeCompanyInfo = string.Empty;
            //    }
            //    else
            //    {
            //        this.sNodeCompanyInfo = clsCompanyOpr.DisplayNameFromCode(sNodeTag);
            //    }
            //}
            //else
            //{
            //    if (tag.Equals("Checked"))
            //    {
            //        //������
            //        if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
            //        {
            //            string sTag = this.treeView1.SelectedNode.Parent.Tag.ToString();

            //            if (!sTag.Substring(0, 4).Equals("DWLX") && ShareOption.ApplicationTag != ShareOption.FDAppTag)//ʳҩϵͳ��λֻ��һ������
            //            {
            //                sParentCompanyName = this.treeView1.SelectedNode.Parent.Text;
            //                sParentCompanyTag = sTag;
            //                this.sNodeCompanyInfo = clsCompanyOpr.DisplayNameFromCode(sNodeTag);
            //            }
            //            else
            //            {
            //                sParentCompanyName = this.treeView1.SelectedNode.Text;
            //                sParentCompanyTag = this.treeView1.SelectedNode.Tag.ToString();
            //                //sParentCompanyName = this.treeView1.SelectedNode.Parent.Text;
            //                //sParentCompanyTag = this.treeView1.SelectedNode.Parent.Tag.ToString();
            //                this.sNodeCompanyInfo = "";
            //            }
            //        }
            //        //��ҵ��
            //        if (ShareOption.SystemVersion == ShareOption.EnterpriseVersion)
            //        {
            //            sParentCompanyName = this.treeView1.Nodes[0].Text;
            //            sParentCompanyTag = this.treeView1.Nodes[0].Tag.ToString();
            //            if (sNodeTag.Equals(sParentCompanyTag))
            //            {
            //                this.sNodeCompanyInfo = string.Empty;
            //            }
            //            else
            //            {
            //                this.sNodeCompanyInfo = clsCompanyOpr.DisplayNameFromCode(sNodeTag);
            //            }
            //        }
            //    }
            //}
            //DialogResult = DialogResult.OK;
            #endregion
        }

        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.txtName.Text.Trim() != "" && this.treeView1.Nodes.Count > 0 && e.KeyCode == Keys.Enter)
            {
                selectNode(this.txtName.Text.Trim(), false);
            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            if (this.txtName.Text.Trim() != "" && this.treeView1.Nodes.Count > 0)
            {
                selectNode(this.txtName.Text.Trim(), true);
            }
        }

    }
}
