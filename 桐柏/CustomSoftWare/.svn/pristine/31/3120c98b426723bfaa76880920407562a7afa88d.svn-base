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
	/// frmCheckedComSelect 的摘要说明。
	/// </summary>
	public class frmCheckedCompany : TitleBarBase
	{
		private System.Windows.Forms.Button btnLocation;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TreeView typeTree;
		private System.ComponentModel.Container components = null;

		private TreeNode[] prevNodes;
		private string tag;
		private string _code;
		private string _type;
		private readonly clsCompanyOpr companyBll;
		private readonly clsCompanyKindOpr companyKindBLL = new clsCompanyKindOpr();
		private bool m_IsFirst = true;
		private TreeNode m_SelectedNode = null;

		/// <summary>
		/// 当前类别编号
		/// </summary>
		public string sNodeTag = string.Empty;

		/// <summary>
		/// 当前类别名称
		/// </summary>
		public string sNodeName = string.Empty;
        /// <summary>
        /// 档口/店面/车牌号
        /// </summary>
        public string sDisplayName = string.Empty;
		/// <summary>
		/// 所属单位父类名称
		/// </summary>
		public string sParentCompanyName = string.Empty;

		/// <summary>
		/// 所属单位父类编号
		/// </summary>
		public string sParentCompanyTag = string.Empty;

		/// <summary>
		/// 所属单位父类信息
		/// </summary>
		public string sNodeCompanyInfo = string.Empty;
		protected C1.Win.C1FlexGrid.C1FlexGrid flexGridCompany;
        private Button btnShowAll;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        protected C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid02;

		/// <summary>
		/// 被检单位标签
		/// </summary>
		private string companyType = ShareOption.CompanyProperty1;


		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pType"></param>
		/// <param name="pCode"></param>
		public frmCheckedCompany(string pType, string pCode)
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
			flexGridCompany.Styles.Normal.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Raised;

			tag = this.Tag.ToString();
			string code = string.Empty;
			string sqlWhere = string.Empty;
			string sysCode = string.Empty;

			TreeNode posNode = null;
			TreeNode theNode = new TreeNode();
			TreeNode preNode = new TreeNode();

			DataTable dtblfirstDistrict = null;
			DataTable dtblsecondDistrict = null;
			DataTable dtblCompanyKind = null;

			clsDistrictOpr districtBll = new clsDistrictOpr();



			if (tag.Equals("Checked"))
			{
                if (FrmMain.IsLoadCheckedComSel && FrmMain .AllownChange )
				{
					if (this.typeTree.Nodes.Count >= 1)
					{
						this.typeTree.Nodes[0].Expand();
					}
					return;
				}

				clsUserUnitOpr userOperation = new clsUserUnitOpr();
				DataTable userInfo = userOperation.GetAsDataTable("A.SysCode = '0001'", string.Empty, 2);
				string areaCode = userInfo.Rows[0]["DistrictCode"].ToString();
				string name = userInfo.Rows[0]["name"].ToString();
				preNode.Text = name;
				preNode.Tag = "XZJG" + areaCode;
				typeTree.Nodes.Add(preNode);

				Cursor = Cursors.WaitCursor;

				if (!ShareOption.AllowHandInputCheckUint || ShareOption.AllowHandInputCheckUint)//如果不是手工录入被检单位
				{
					if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)//行政版
					{
						//得到检测单位的下级单位
						string districtCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", "0001");
						dtblfirstDistrict = districtBll.GetAsDataTable("SysCode LIKE '" + districtCode + "___'", "SysCode", 0);

						//得到一级单位类别数据集
                        //sqlWhere = string.Format("IsLock=0 And CompanyProperty='{0}' And IsReadOnly=true AND Len(SysCode)=3", companyType);
                        sqlWhere = string.Format("IsLock=0 And (CompanyProperty='{0}'or CompanyProperty='两者都是') And IsReadOnly=true ", companyType);
						dtblCompanyKind = companyKindBLL.GetAsDataTable(sqlWhere, "SysCode", 1);

						if (dtblCompanyKind == null || dtblCompanyKind.Rows.Count <= 0)
						{
							MessageBox.Show(this, "单位类别没有设置，请设置!");
							Cursor = Cursors.Default;
							return;
						}
						string subWhere = string.Empty;
						//DataTable dtblCompany = companyBll.GetAsDataTable("1=1", "SysCode", 1);
						if (dtblfirstDistrict.Rows.Count <= 0)//如果无下级单位
						{
							for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
							{
								theNode = new TreeNode();
								sysCode = dtblCompanyKind.Rows[j]["SysCode"].ToString();
								theNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
								theNode.Tag = "DWLX" + sysCode;

								//取出二级分类后，再取出单位名称
								//getSubCompanyKindNode(sysCode, districtCode, ref theNode, dtblCompany);   //districtCode="001"   syscode="001"

								//this.typeTree.Nodes[0].Add(theNode);
								typeTree.Nodes[0].Nodes.Add(theNode);
							}
							if (this.typeTree.Nodes[0].Nodes.Count >= 1)
							{
								this.typeTree.Nodes[0].Expand();
							}
						}
						else   //若有下级单位
						{
							code = dtblfirstDistrict.Rows[0]["SysCode"].ToString();
							bool detailed = clsDistrictOpr.DistrictIsMX(code);

							for (int i = 0; i < dtblfirstDistrict.Rows.Count; i++)
							{
								code = dtblfirstDistrict.Rows[i]["SysCode"].ToString();   //code="001001"

								theNode = new TreeNode();
								theNode.Text = dtblfirstDistrict.Rows[i]["Name"].ToString();
								theNode.Tag = "XZJG" + code;

								//检查该节点是不是明细子节点，是就增加单位类型节点
								if (detailed)
								{
									for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
									{
										TreeNode childCKNode = new TreeNode();
										childCKNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
										sysCode = dtblCompanyKind.Rows[j]["SysCode"].ToString();
										childCKNode.Tag = "DWLX" + sysCode;
										theNode.Nodes.Add(childCKNode);
										//取出二级分类后，再取出单位名称
										//getSubCompanyKindNode(sysCode, code, ref childCKNode, dtblCompany);  //sysCode="001"  code="001001"
									}
								}
								else
								{
									typeTree.Nodes[0].Nodes.Add(theNode);
									dtblsecondDistrict = districtBll.GetAsDataTable("SysCode LIKE '" + code + "___'", "SysCode", 0);
									if (dtblsecondDistrict.Rows.Count <= 0)
									{
										for (int j = 0; j < dtblCompanyKind.Rows.Count; j++)
										{
											TreeNode childCKNode = new TreeNode();
											childCKNode.Text = dtblCompanyKind.Rows[j]["Name"].ToString();
											sysCode = dtblCompanyKind.Rows[j]["SysCode"].ToString();
											childCKNode.Tag = "DWLX" + sysCode;
											theNode.Nodes.Add(childCKNode);
										}
									}
									for (int index = 0; index < dtblsecondDistrict.Rows.Count; index++)
									{
										TreeNode secondLevelNode = new TreeNode();
										secondLevelNode.Text = dtblsecondDistrict.Rows[index]["Name"].ToString();
										secondLevelNode.Tag = "XZJG" + dtblsecondDistrict.Rows[index]["SysCode"].ToString();

										for (int rowIndex = 0; rowIndex < dtblCompanyKind.Rows.Count; rowIndex++)
										{
											TreeNode childNode = new TreeNode();
											childNode.Text = dtblCompanyKind.Rows[rowIndex]["Name"].ToString();
											sysCode = dtblCompanyKind.Rows[rowIndex]["SysCode"].ToString();
											childNode.Tag = "DWLX" + sysCode;
											secondLevelNode.Nodes.Add(childNode);
										}

										theNode.Nodes.Add(secondLevelNode);
									}
								}
								if (detailed)
								{
									typeTree.Nodes[0].Nodes.Add(theNode);
								}

							}

							if (this.typeTree.Nodes[0].Nodes.Count >= 1)
							{
								this.typeTree.Nodes[0].Expand();
							}
						}
					}

					else  //企业版
					{
						string tempTag = string.Empty;
						theNode = new TreeNode();
						theNode.Text = clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);

						if (!ShareOption.IsDataLink)//如果是网络版
						{
							tempTag = clsUserUnitOpr.GetNameFromCode("CompanyId", ShareOption.DefaultUserUnitCode);
						}
						else
						{
							tempTag = clsUserUnitOpr.GetStdCode(ShareOption.DefaultUserUnitCode);//通过用户单位编码获取企业标准码
						}
						theNode.Tag = tempTag;
						sqlWhere = string.Format("ISLOCK=FALSE AND ISREADONLY=TRUE AND Property='{0}' AND StdCode LIKE '{1}%'", companyType, tempTag);

						dtblfirstDistrict = companyBll.GetAsDataTable(sqlWhere, "SysCode", 1);//000128001

						if (dtblfirstDistrict != null && dtblfirstDistrict.Rows.Count > 0)
						{
							TreeNode childNode = null;
							foreach (DataRow dr in dtblfirstDistrict.Rows)
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
						typeTree.Nodes.Add(theNode);

					}
					FrmMain.IsLoadCheckedComSel = false ;
                    FrmMain.AllownChange = true;
				}
				else
				{
					TreeNode comsNode = null;
					theNode = new TreeNode();
					theNode.Text = clsCompanyOpr.NameFromCode(_code);
					theNode.Tag = _code;

					sqlWhere = string.Format("Property='{0}' And IsReadOnly=true And StdCode Like '{1}_%'", companyType, clsCompanyOpr.StdCodeFromCode(_code));

					dtblfirstDistrict = companyBll.GetAsDataTable(sqlWhere, "SysCode", 1);

					if (dtblfirstDistrict != null && dtblfirstDistrict.Rows.Count > 0)
					{
						for (int i = 0; i < dtblfirstDistrict.Rows.Count; i++)
						{
							comsNode = new TreeNode();
							comsNode.Text = dtblfirstDistrict.Rows[i]["FullName"].ToString();
							comsNode.Tag = dtblfirstDistrict.Rows[i]["SysCode"].ToString();
							theNode.Nodes.Add(comsNode);
						}
					}

					typeTree.Nodes.Add(theNode);

					if (typeTree.Nodes.Count >= 1)
					{
						typeTree.Nodes[0].Expand();
					}
					FrmMain.IsLoadCheckedComSel = true ;
                    FrmMain.AllownChange = false;
				}
			}

			else if (tag.Equals("UpperChecked"))//上级单位
			{
				if (FrmMain.IsLoadCheckedUpperComSel)
				{
					return;
				}
				this.Text = "选择" + ShareOption.AreaTitle;
				this.label3.Text = ShareOption.AreaTitle + "：";//所属市场

				Cursor = Cursors.WaitCursor;

				//如果是行政版
				if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
				{
					//得到检测单位的下级单位
					string districtCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", "0001");
					dtblfirstDistrict = districtBll.GetAsDataTable(string.Format("SysCode Like '{0}%'", districtCode), "SysCode", 0);

					dtblCompanyKind = companyKindBLL.GetAsDataTable(string.Format("IsLock=0 And IsReadOnly=true And CompanyProperty='{0}'", companyType), "SysCode", 1);
					if (dtblCompanyKind == null || dtblCompanyKind.Rows.Count <= 0)
					{
						MessageBox.Show(this, "单位类别没有设置，请设置!");
						Cursor = Cursors.Default;
						return;
					}

					if (dtblfirstDistrict.Rows.Count <= 0)
					{
						getUpperCompanyTreeNode(dtblCompanyKind, districtCode, true, ref theNode);
						#region 注掉
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
						if (this.typeTree.Nodes.Count >= 1)
						{
							this.typeTree.Nodes[0].Expand();
						}
					}
					else
					{
						for (int i = 0; i < dtblfirstDistrict.Rows.Count; i++)
						{
							code = dtblfirstDistrict.Rows[i]["SysCode"].ToString();
							theNode = new TreeNode();
							theNode.Text = dtblfirstDistrict.Rows[i]["Name"].ToString();
							theNode.Tag = "XZJG" + code;

							//检查该节点是不是明细子节点，是就增加单位类型节点
							if (clsDistrictOpr.DistrictIsMX(code))
							{
								getUpperCompanyTreeNode(dtblCompanyKind, code, false, ref theNode);

								#region 注掉
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
							//addTreeNode(code, districtCode, theNode);

							#region 注掉重复代码
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
			else  //生产单位
			{
				if (tag.Equals("Produce"))
				{
					this.Text = "选择生产/加工单位/个人";
					this.label3.Text = ShareOption.ProductionUnitNameTag + "："; //配置不同的标签
				}
				else
				{
					this.Text = "选择供货单位/个人";
					this.label3.Text = "供货单位：";
				}

				dtblfirstDistrict = companyBll.GetAsDataTable("IsLock=false And IsReadOnly=true And Property='生产单位' ", "SysCode", 1);
				if (dtblfirstDistrict != null)
				{
					foreach (DataRow dr in dtblfirstDistrict.Rows)
					{
						code = dr["SysCode"].ToString();

						TreeNode childNode = new TreeNode();
						childNode.Text = dr["FullName"].ToString();
						childNode.Tag = code;
						if (_code.Equals(code))
						{
							posNode = childNode;
						}
						this.typeTree.Nodes.Add(childNode);
					}
				}
			}

			//定位treeview所在节点
			//if (posNode != null)
			//{
			//    this.treeView1.SelectedNode = posNode;
			//    posNode.EnsureVisible();
			//}
			//else
			//{
			//    this.treeView1.ExpandAll();
			//}


			//TitleBarText = this.Text;

			//TreeNode CurrentNode = null;
			//if (this.treeView1.Nodes.Count > 0)
			//{
			//    CurrentNode = this.treeView1.Nodes[0];
			//    while (CurrentNode != null)
			//    {
			//        if (CurrentNode.Tag.ToString().Equals(_code))
			//        {
			//            this.treeView1.SelectedNode = CurrentNode;
			//            CurrentNode.EnsureVisible();
			//            CurrentNode.Expand();
			//            Cursor = Cursors.Default;
			//            return;
			//        }
			//        else
			//        {
			//            CurrentNode = this.GetNextNode(CurrentNode, false);
			//            if (CurrentNode == null)
			//            {
			//                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
			//                this.treeView1.ExpandAll();
			//                Cursor = Cursors.Default;
			//                return;
			//            }
			//        }
			//    }
			//}
			TitleBarText = this.Text;

			//if (this.treeView1.Nodes.Count <= 0)
			//{
			//    this.btnOK.Enabled = false;
			//    this.txtName.Enabled = false;
			//}
			//Cursor = Cursors.Default;

			//TitleBarText = this.Text;
			Cursor = Cursors.Default;

		}

		/// <summary>
		/// 获取二级单位类别及所下属单位目录树
		/// </summary>
		/// <param name="sysCode"></param>
		/// <param name="theNode"></param>
		private void getSubCompanyKindNode(string sysCode, string districtCode, ref TreeNode theNode)
		{
			string sqlWhere = string.Format("IsLock=0 And CompanyProperty='{0}' And IsReadOnly=true AND Mid(SysCode,1,3)='{1}' AND LEN(SysCode)=6", companyType, sysCode);
			DataTable dtblSubCK = companyKindBLL.GetAsDataTable(sqlWhere, "SysCode", 1);
			DataTable dtblCompany = null;
			if (dtblSubCK != null && dtblSubCK.Rows.Count >= 1)//如果有二级单位类别
			{

				for (int n = 0; n < dtblSubCK.Rows.Count; n++)
				{
					TreeNode childCK = new TreeNode();
					string tcode = dtblSubCK.Rows[n]["SysCode"].ToString();
					childCK.Text = dtblSubCK.Rows[n]["Name"].ToString();
					childCK.Tag = tcode;
					theNode.Nodes.Add(childCK);

					//取出对应类别下的单位
					sqlWhere = string.Format("Property='{0}' And IsReadOnly=true and KindCode = '{1}' And DistrictCode='{2}' And Len(StdCode)={3}", companyType, tcode, districtCode, ShareOption.CompanyStdCodeLen);

					dtblCompany = companyBll.GetAsDataTable(sqlWhere, "SysCode", 1);
					if (dtblCompany.Rows.Count <= 0)
					{
						continue;
					}
					getCompanyTreeNode(dtblCompany, ref childCK);
				}
			}
			else //如果没有二级单位类别
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
		/// 处理最后节点值问题
		/// </summary>
		/// <param name="code"></param>
		/// <param name="districtCode"></param>
		/// <param name="theNode"></param>
		private void addTreeNode(string code, string districtCode, TreeNode theNode)
		{
			//int iMod = (code.Length - districtCode.Length) / ShareOption.DistrictCodeLevel;
			//if (iMod <= 1)// (iMod == 1)
			//{
			//    this.typeTree.Nodes.Add(theNode);
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
			//typeTree.Nodes[0].Nodes.Add(theNode);
			//if (this.typeTree.Nodes.Count >= 1)
			//{
			//    this.typeTree.Nodes[0].Expand();
			//}
		}

		/// <summary>
		/// 获取两级单位树结构
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
		/// 获取上属单位目录树
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
					this.typeTree.Nodes.Add(childNode);
				}
			}
		}

		/// <summary>
		/// 设置值
		/// </summary>
		/// <param name="pType"></param>
		/// <param name="pCode"></param>
		public void SetFormValues(string pType, string pCode)
		{
			_type = pType;
			_code = pCode;
			TreeNode CurrentNode = null;
			if (this.typeTree.Nodes.Count > 0)
			{
				CurrentNode = this.typeTree.Nodes[0];
				while (CurrentNode != null)
				{
					if (CurrentNode.Tag.ToString().Equals(_code))
					{
						this.typeTree.SelectedNode = CurrentNode;
						CurrentNode.EnsureVisible();
						CurrentNode.Expand();
						return;
					}
					else
					{
						CurrentNode = this.GetNextNode(CurrentNode, false);
						if (CurrentNode == null)
						{
							this.typeTree.SelectedNode = this.typeTree.Nodes[0];
							// this.typeTree.ExpandAll();
							return;
						}
					}
				}
			}
		}
		
		/// <summary>
		/// 清理所有正在使用的资源。
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckedCompany));
            this.btnLocation = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.typeTree = new System.Windows.Forms.TreeView();
            this.flexGridCompany = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.c1FlexGrid02 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.flexGridCompany)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid02)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLocation
            // 
            this.btnLocation.Location = new System.Drawing.Point(548, 43);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(50, 24);
            this.btnLocation.TabIndex = 21;
            this.btnLocation.Text = "查找";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(402, 44);
            this.txtName.MaxLength = 30;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(128, 21);
            this.txtName.TabIndex = 19;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(312, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 21);
            this.label3.TabIndex = 20;
            this.label3.Text = "受检人/单位：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(728, 533);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(526, 533);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "选择";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // typeTree
            // 
            this.typeTree.HideSelection = false;
            this.typeTree.Location = new System.Drawing.Point(8, 42);
            this.typeTree.Name = "typeTree";
            this.typeTree.Size = new System.Drawing.Size(298, 515);
            this.typeTree.TabIndex = 16;
            this.typeTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.typeTree_BeforeSelect);
            this.typeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.typeTree_AfterSelect);
            this.typeTree.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // flexGridCompany
            // 
            this.flexGridCompany.AllowEditing = false;
            this.flexGridCompany.BackColor = System.Drawing.Color.White;
            this.flexGridCompany.ColumnInfo = resources.GetString("flexGridCompany.ColumnInfo");
            this.flexGridCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flexGridCompany.ForeColor = System.Drawing.Color.Black;
            this.flexGridCompany.Location = new System.Drawing.Point(0, 0);
            this.flexGridCompany.Name = "flexGridCompany";
            this.flexGridCompany.Rows.Count = 1;
            this.flexGridCompany.Rows.DefaultSize = 18;
            this.flexGridCompany.Rows.MaxSize = 200;
            this.flexGridCompany.Rows.MinSize = 20;
            this.flexGridCompany.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flexGridCompany.Size = new System.Drawing.Size(551, 434);
            this.flexGridCompany.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("flexGridCompany.Styles"));
            this.flexGridCompany.TabIndex = 23;
            this.flexGridCompany.Click += new System.EventHandler(this.flexGridCompany_Click);
            this.flexGridCompany.DoubleClick += new System.EventHandler(this.flexGridCompany_DoubleClick);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(617, 43);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(50, 23);
            this.btnShowAll.TabIndex = 24;
            this.btnShowAll.Text = "全部";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flexGridCompany);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.c1FlexGrid02);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(551, 434);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 26;
            // 
            // c1FlexGrid02
            // 
            this.c1FlexGrid02.AllowEditing = false;
            this.c1FlexGrid02.BackColor = System.Drawing.Color.White;
            this.c1FlexGrid02.ColumnInfo = resources.GetString("c1FlexGrid02.ColumnInfo");
            this.c1FlexGrid02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid02.ForeColor = System.Drawing.Color.Black;
            this.c1FlexGrid02.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid02.Name = "c1FlexGrid02";
            this.c1FlexGrid02.Rows.Count = 1;
            this.c1FlexGrid02.Rows.DefaultSize = 18;
            this.c1FlexGrid02.Rows.MaxSize = 200;
            this.c1FlexGrid02.Rows.MinSize = 20;
            this.c1FlexGrid02.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid02.Size = new System.Drawing.Size(96, 100);
            this.c1FlexGrid02.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid02.Styles"));
            this.c1FlexGrid02.TabIndex = 24;
            this.c1FlexGrid02.Click += new System.EventHandler(this.c1FlexGrid02_Click);
            this.c1FlexGrid02.DoubleClick += new System.EventHandler(this.c1FlexGrid02_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(314, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 454);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "被检单位选择";
            // 
            // frmCheckedCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(890, 569);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.typeTree);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckedCompany";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择受检人/单位";
            this.Load += new System.EventHandler(this.frmCheckedComSelect_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.typeTree, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.btnLocation, 0);
            this.Controls.SetChildIndex(this.btnShowAll, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.flexGridCompany)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid02)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnLocation_Click(object sender, System.EventArgs e)
		{
			string companyName = txtName.Text.Trim();

			SearchCompaniesByNode(companyName,2);
		}

		private void selectNode(string sNodeName, bool IsNext)
		{
			bool IsReRun = false;
			TreeNode CurrentNode = null;
			if (!IsNext)
			{
				CurrentNode = this.typeTree.Nodes[0];
			}
			else
			{
				CurrentNode = this.GetNextNode(this.typeTree.SelectedNode, true);
			}
			Regex r = new Regex(sNodeName, RegexOptions.IgnoreCase);
			while (CurrentNode != null)
			{
				Match m = r.Match(CurrentNode.Text);
				if (m.Success)
				{
					this.typeTree.SelectedNode = CurrentNode;
					CurrentNode.EnsureVisible();
					return;
				}
				else
				{
					if (IsReRun && IsNext && CurrentNode.Equals(this.typeTree.SelectedNode)) return;
					CurrentNode = this.GetNextNode(CurrentNode, false);
					if (CurrentNode == null && IsNext)
					{
						IsReRun = true;
						CurrentNode = this.typeTree.Nodes[0];
					}
				}
			}
		}

		private TreeNode GetNextNode(TreeNode CurrentNode, bool IsReRun)
		{
			TreeNode NextNode = null;
			//得到下一个节点
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
            if (c1FlexGrid02.Rows.Count-1 <= 0)
            {
                flexGridCompany_DoubleClick(null, null);
                //DialogResult = DialogResult.OK;
            }
            else
            {
                c1FlexGrid02_DoubleClick(null, null);
            }
			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			sNodeTag = string.Empty;
			sNodeName = string.Empty;
		}

		private void treeView1_DoubleClick(object sender, System.EventArgs e)
		{
			//sNodeTag = this.typeTree.SelectedNode.Tag.ToString();
			//sNodeName = this.typeTree.SelectedNode.Text;
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
			//        //行政版
			//        if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
			//        {
			//            string sTag = this.typeTree.SelectedNode.Parent.Tag.ToString();

			//            if (!sTag.Substring(0, 4).Equals("DWLX") && ShareOption.ApplicationTag != ShareOption.FDAppTag)//食药系统单位只有一级分类
			//            {
			//                sParentCompanyName = this.typeTree.SelectedNode.Parent.Text;
			//                sParentCompanyTag = sTag;
			//                this.sNodeCompanyInfo = clsCompanyOpr.DisplayNameFromCode(sNodeTag);
			//            }
			//            else
			//            {
			//                sParentCompanyName = this.typeTree.SelectedNode.Text;
			//                sParentCompanyTag = this.typeTree.SelectedNode.Tag.ToString();
			//                this.sNodeCompanyInfo = "";
			//            }
			//        }
			//        //企业版
			//        if (ShareOption.SystemVersion == ShareOption.EnterpriseVersion)
			//        {
			//            sParentCompanyName = this.typeTree.Nodes[0].Text;
			//            sParentCompanyTag = this.typeTree.Nodes[0].Tag.ToString();
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
		}

		private void BindCompanies(DataTable companies)
		{
			if (flexGridCompany.Rows.Count > 0)
			{
				flexGridCompany.Rows.RemoveRange(1, flexGridCompany.Rows.Count - 1);
			}
			if (companies == null)
			{
				return;
			}

			for (int rowIndex = 0; rowIndex < companies.Rows.Count; rowIndex++)
			{
				flexGridCompany.AddItem(new object[] {  companies.Rows[rowIndex]["fullname"].ToString(),
					                                                              companies.Rows[rowIndex]["Incorporator"].ToString(),
					                                                              companies.Rows[rowIndex]["CompanyType"].ToString(),
																				  companies.Rows[rowIndex]["OrganizationName"].ToString(),
																				  companies.Rows[rowIndex]["SysCode"].ToString()}
																				  );

				C1.Win.C1FlexGrid.CellStyle unselectedStyle = flexGridCompany.Styles.Add("unselectedStyle");
				unselectedStyle.BackColor = Color.White;
				unselectedStyle.ForeColor = Color.Black;

				flexGridCompany.Rows[rowIndex + 1].Style = unselectedStyle;

				flexGridCompany.Refresh();
			}

			flexGridCompany.AutoSizeCols();
            splitContainer1.Panel2Collapsed =true ;
            c1FlexGrid02.Refresh();
			//if (flexGridCompany.Rows.Count > 1)
			//{
			//    btnLocation.Enabled = true;
			//    btnShowAll.Enabled = true;
			//}
			//else
			//{
			//    btnLocation.Enabled = false;
			//    btnShowAll.Enabled = false;
			//}
		}

		private void SearchCompaniesByNode(string companyName,int A)
		{
			if (m_SelectedNode == null  )
			{
				MessageBox.Show(this, "请先在左边栏目中选择一个单位类别!", "单位类别", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			string tag = m_SelectedNode.Tag.ToString();

			string typeCode = string.Empty;
			string organizationCode = string.Empty;
			string sqlWhere = string.Empty;
			if (m_SelectedNode.Parent != null)
			{
				string flag = tag.Substring(0, 4);
				if (flag == "XZJG")
				{
					organizationCode = tag.Substring(4);
					if (organizationCode.Length == 6)
					{
						if (m_SelectedNode.Nodes != null && m_SelectedNode.Nodes[0].Tag.ToString().Substring(0,4) != "XZJG")
						{
							sqlWhere = string.Format("c.Property='{0}' And c.IsReadOnly=true And c.DistrictCode = '{1}' And Len(c.StdCode)={2} AND c.FullName LIKE '%{3}%'", companyType, organizationCode, ShareOption.CompanyStdCodeLen, companyName);
						}
						else
						{
							sqlWhere = string.Format("c.Property='{0}' And c.IsReadOnly=true And c.DistrictCode LIKE '{1}___' And Len(c.StdCode)={2} AND c.FullName LIKE '%{3}%'", companyType, organizationCode, ShareOption.CompanyStdCodeLen, companyName);
						}
					}
					else
					{
						sqlWhere = string.Format("c.Property='{0}' And c.IsReadOnly=true And c.DistrictCode='{1}' And Len(c.StdCode)={2} AND c.FullName LIKE '%{3}%'", companyType, organizationCode, ShareOption.CompanyStdCodeLen, companyName);
					}

				}
				else
				{
					organizationCode = typeTree.SelectedNode.Parent.Tag.ToString().Substring(4);
					typeCode = tag.Substring(4);
                    //sqlWhere = string.Format("c.Property='{0}' And c.IsReadOnly=true and c.KindCode = '{1}' And c.DistrictCode='{2}' And Len(c.StdCode)={3} AND c.FullName LIKE '%{4}%'", companyType, typeCode, organizationCode, ShareOption.CompanyStdCodeLen, companyName);
                    if (A == 1)
                    {
                        sqlWhere = string.Format("c.IsReadOnly=true  And c.DistrictCode='{0}' ", organizationCode);
                    }
                    if (A == 2)
                    {
                        sqlWhere = string.Format("c.IsReadOnly=true  And c.DistrictCode='{0}'  AND c.FullName LIKE '%{1}%'", organizationCode, companyName);
                    }
                    if (A == 0)
                    {
                        sqlWhere = string.Format("c.IsReadOnly=true and c.KindCode = '{0}' And c.DistrictCode='{1}'  AND c.FullName LIKE '%{2}%'", typeCode, organizationCode, companyName);
                    }                    
				}
			}
			else
			{
				organizationCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", "0001");
				sqlWhere = string.Format("c.Property='{0}' And c.IsReadOnly=true And c.DistrictCode LIKE '{1}%' And Len(c.StdCode)={2} AND c.FullName LIKE '%{3}%'", companyType, organizationCode, ShareOption.CompanyStdCodeLen, companyName);
			}


			clsCompanyOpr companyBll = new clsCompanyOpr();
			DataTable companies = companyBll.GetAsDataTable(sqlWhere, "c.DistrictCode ASC,c.KindCode ASC,c.FullName ASC", 2);

			BindCompanies(companies);
		}

		private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (this.txtName.Text.Trim() != "" && this.typeTree.Nodes.Count > 0 && e.KeyCode == Keys.Enter)
			{
				selectNode(this.txtName.Text.Trim(), false);
			}
		}


		private void typeTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			txtName.Text = string.Empty;

			m_SelectedNode = typeTree.SelectedNode;

			string selectedTag = m_SelectedNode.Tag.ToString();
			selectedTag = selectedTag.Substring(0, 4);
			if (selectedTag != "DWLX")
			{
				return;
			}

			SearchCompaniesByNode(string.Empty,0);
		}

		private void flexGridCompany_DoubleClick(object sender, EventArgs e)
		{
           
			int rowIndex = flexGridCompany.RowSel;
			if (rowIndex <= 0)
			{
				return;
			}
            sNodeName = flexGridCompany.Rows[rowIndex]["colName"].ToString();
            sNodeTag = flexGridCompany.Rows[rowIndex]["colComCode"].ToString();

            sParentCompanyName = flexGridCompany.Rows[rowIndex]["colName"].ToString();
            sDisplayName = string.Empty;

            DialogResult = DialogResult.OK;
		}

        private void BindCompaniesTosecond(DataTable companies)
        {
            if (c1FlexGrid02.Rows.Count > 0)
            {
                c1FlexGrid02.Rows.RemoveRange(1, c1FlexGrid02.Rows.Count - 1);
            }
            if (companies == null)
            {
                return;
            }

            for (int rowIndex = 0; rowIndex < companies.Rows.Count; rowIndex++)
            {
                c1FlexGrid02.AddItem(new object[] {  companies.Rows[rowIndex]["Cdname"].ToString(),
					                                                              companies.Rows[rowIndex]["Incorporator"].ToString(),
					                                                              companies.Rows[rowIndex]["DisplayName"].ToString(),
																				  companies.Rows[rowIndex]["Ciname"].ToString()}
                                                                                  );

                C1.Win.C1FlexGrid.CellStyle unselectedStyle = c1FlexGrid02.Styles.Add("unselectedStyle");
                unselectedStyle.BackColor = Color.White;
                unselectedStyle.ForeColor = Color.Black;

                c1FlexGrid02.Rows[rowIndex + 1].Style = unselectedStyle;

                c1FlexGrid02.Refresh();
            }

            c1FlexGrid02.AutoSizeCols();

        }

		private void flexGridCompany_Click(object sender, EventArgs e)
		{
            string SNIDENAME = string.Empty;
            string SNIDETAG = string.Empty;

			int rowIndex = flexGridCompany.RowSel;
			if (rowIndex <= 0)
			{
				return;
			}
            if (flexGridCompany.Rows[rowIndex].Style.BackColor == Color.Black)
            {
                flexGridCompany.Styles.Highlight.ForeColor = Color.Black;
            }
            else
            {
                flexGridCompany.Styles.Highlight.ForeColor = Color.Red;
            }
            SNIDENAME = flexGridCompany.Rows[rowIndex]["colName"].ToString();
            SNIDETAG = flexGridCompany.Rows[rowIndex]["colComCode"].ToString();

            string sqlWhere = string.Format("Ciname='{0}'", SNIDENAME);
            clsProprietorsOpr ProBll = new clsProprietorsOpr();
            DataTable ProTable = ProBll.GetAsDataTable(sqlWhere, "", 1);


            if (ProTable.Rows.Count <= 0)
            {
                BindCompaniesTosecond(ProTable);
                c1FlexGrid02.Refresh();
                return;
            }
            if (ProTable.Rows.Count > 0)
            {
                splitContainer1.Panel2Collapsed = false;
                BindCompaniesTosecond(ProTable);
            }



		}


		private void typeTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (m_IsFirst)
			{
				e.Cancel = true;
				m_IsFirst = false;
			}
		}

		private void btnShowAll_Click(object sender, EventArgs e)
		{

            SearchCompaniesByNode(string .Empty ,1);

		}

        private void c1FlexGrid02_DoubleClick(object sender, EventArgs e)
        {
            int rowIndex = c1FlexGrid02.RowSel;
            if (rowIndex <= 0)
            {
                return;
            }
            sNodeName = c1FlexGrid02.Rows[rowIndex]["colCdname"].ToString();
            sParentCompanyName = c1FlexGrid02.Rows[rowIndex]["colCiname"].ToString();
            sDisplayName = c1FlexGrid02.Rows[rowIndex]["colDisplayName"].ToString();
            DialogResult = DialogResult.OK;
        }

        private void c1FlexGrid02_Click(object sender, EventArgs e)
        {
            int rowIndex = c1FlexGrid02.RowSel;
            if (rowIndex <= 0)
            {
                return;
            }

            if (c1FlexGrid02.Rows[rowIndex].Style.BackColor == Color.Black)
            {
                c1FlexGrid02.Styles.Highlight.ForeColor = Color.Black;
            }
            else
            {
                c1FlexGrid02.Styles.Highlight.ForeColor = Color.Red;
            }
        }

	}
}
