using System;
using System.Windows.Forms;

namespace DY.FoodClientLib
{
	/// <summary>
	///检测是否包含特殊字符，用于过滤SQL关键字符注入
	/// </summary>
	public class RegularCheck
	{
        //private	static char[] specChars=new char[]{'\''};

		private RegularCheck()
        {
		}

        /// <summary>
        /// 查检整个表单的控件
        /// </summary>
        /// <param name="theForm"></param>
        /// <param name="posControl"></param>
        /// <returns></returns>
        public static bool HaveSpecChar(Form theForm, out Control posControl)
        {
            string sText = string.Empty;

            foreach (Control con in theForm.Controls)
            {
                if (con is Label || con is CheckBox || con is Button)
                {
                    continue;
                }
                if (con is TextBox)
                {
                    sText = ((TextBox)con).Text.Trim();
                }
                if(con is C1.Win.C1List.C1Combo)
                {
                    sText = ((C1.Win.C1List.C1Combo)con).Text.Trim();
                }
                posControl = con;
                return IsIllegalString(sText);


                //Type curType = con.GetType();
                //if (curType.FullName.Equals("System.Windows.Forms.TextBox"))
                //{
                //    sText = ((TextBox)con).Text.Trim();
                //}
                //else if (curType.FullName.Equals("C1.Win.C1List.C1Combo"))
                //{
                //    sText = ((C1.Win.C1List.C1Combo)con).Text.Trim();
                //}
                //else if (curType.FullName.Equals("System.Windows.Forms.Label")
                //    || curType.FullName.Equals("System.Windows.Forms.CheckBox")
                //    || curType.FullName.Equals("System.Windows.Forms.Button"))
                //{
                //    continue;
                //}
            
                //for (int i = 0; i < specChars.Length; i++)
                //{
                //    int iPos = sText.IndexOf(specChars[i]);
                //    if (iPos != -1)
                //    {
                //        posControl = con;
                //        return true;
                //    }
                //}
            }

            posControl = null;
            return false;
        }

        /// <summary>
        ///  单个控件检查
        /// </summary>
        /// <param name="posControl"></param>
        /// <returns></returns>
        public static bool HaveSpecChar(Control posControl)
        {
            string sText = posControl.Text.Trim();

            return IsIllegalString(sText);
            //for (int i = 0; i < specChars.Length; i++)
            //{
            //    int iPos = sText.IndexOf(specChars[i]);
            //    if (iPos != -1)
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        private static char[] chArray = "!@#$%^&*()_+=~`[]\\;',./{}|:\"<>?".ToCharArray();

        /// <summary>
        /// 检查是否存在非法字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns>如果存在返回true,否则返回false</returns>
        public static bool IsIllegalString(string input)
        {
            bool tag = false;
            foreach (char ch in chArray)
            {
                if (input.IndexOf(ch) >= 0)
                {
                    tag = true;
                    break;
                }
            }
            return tag;
        }

	}
}
