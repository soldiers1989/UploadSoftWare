using System;
using System.Collections.Generic;
using System.IO;
using com.lvrenyang;

namespace AIO
{

    public class Record
    {
        public const string keyHole = "检测孔";
        public const string keyCategory = "检测类别";
        public const string keyItem = "检测项目";
        public const string keyResult = "检测结果";
        public const string keyUnit = "检测单位";
        public const string keyDate = "检测日期";
        public const string keyUser = "检测人员";
        public const string keyMethod = "检测方法";
        public const string keySampleName = "样品名称";
        public const string keySampleNum = "样品编号";
        //new String
        public const string keyJudgmentStandard = "判定结果";
        public string Hole { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public string Date { get; set; }
        public string User { get; set; }
        public string Method { get; set; }
        public string SampleName { get; set; }
        public string SampleNum { get; set; }
        public string JudgmentStandard { get; set; }

        public void Add(string key, string value)
        {
            if (keyHole.Equals(key))
            {
                Hole = value;
            }
            else if (keyCategory.Equals(key))
            {
                Category = value;
            }
            else if (keyItem.Equals(key))
            {
                Item = value;
            }
            else if (keyResult.Equals(key))
            {
                Result = value;
            }
            else if (keyUnit.Equals(key))
            {
                Unit = value;
            }
            else if (keyDate.Equals(key))
            {
                Date = value;
            }
            else if (keyUser.Equals(key))
            {
                User = value;
            }
            else if (keyMethod.Equals(key))
            {
                Method = value;
            }
            else if (keySampleName.Equals(key))
            {
                SampleName = value;
            }
            else if (keySampleNum.Equals(key))
            {
                SampleNum = value;
            }
            else if (keyJudgmentStandard.Equals(key))
            {
                if (value != "")
                {
                    //string oot = "";
                }
                JudgmentStandard = value;
            }
        }
    }

    public class RecordHelper
    {
        public static string RecordsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DY-Detector\\Records";

        public static void SaveRecord(string strDirName, string strFileName, string strContent)
        {
            FileUtils.AddToFile(strDirName, strFileName, strContent);
        }

        // 调用generateRecordString生成记录字符串，将记录字符串保存
        public static string GenerateRecordString(string hole, string category, string item, string method, string result, string unit, string datetime, string user, string samplename, string judgmentValue, string StandardValue, string samplenum)
        {
            return
                hole + "#" +
                category + "#" +
                item + "#" +
                result + "#" +
                unit + "#" +
                datetime + "#" +
                user + "#" +
                method + "#" +
                judgmentValue + "#" +
                samplename + "#" +
                StandardValue + "#" +
                samplenum;

            //Record.keyHole + "#" + hole + "|" +
            //Record.keyCategory + "#" + category + "|" +
            //Record.keyItem + "#" + item + "|" +
            //Record.keyResult + "#" + result + "|" +
            //Record.keyUnit + "#" + unit + "|" +
            //Record.keyDate + "#" + datetime + "|" +
            //Record.keyUser + "#" + user + "|" +
            //Record.keyMethod + "#" + method + "|" +
            //Record.keyJudgmentStandard + "#" + judgmentValue + "|" +
            //Record.keySampleName + "#" + samplename + "|" +
            //Record.keySampleNum + "#" + samplenum;
        }

        // 读取记录 返回的数据都会存起来
        public static List<Record> ReadRecord(string strDirName)
        {
            List<Record> records = new List<Record>();
            if (!Directory.Exists(strDirName))
                return records;
            List<string> files = new List<string>();
            FileUtils.EnmurateFile(strDirName, files);
            foreach (string file in files)
            {
                List<string> strs = FileUtils.ReadStringsFromFile(file);
                foreach (string str in strs)
                {   // 每一行，都是一条记录
                    Record record = new Record();
                    string[] splitStrs = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string splitStr in splitStrs)
                    {
                        string[] keyvalue = splitStr.Split(new char[] { '#' });
                        record.Add(keyvalue[0], keyvalue[1]);
                    }
                    records.Add(record);
                }
            }

            return records;
        }

    }
}
