using System;
using System.Diagnostics;

namespace com.lvrenyang
{
    public class CmdUnity
    {

        private static Process proc = new Process();

        /// <summary> 
        /// 执行CMD语句 
        /// </summary> 
        /// <param name="cmd">要执行的CMD命令</param> 
        public void RunCmd(string cmd)
        {
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine(cmd);
            proc.Close();
        }

        /// <summary> 
        /// 打开软件并执行命令 
        /// </summary> 
        /// <param name="programName">软件路径加名称（.exe文件）</param> 
        /// <param name="cmd">要执行的命令</param> 
        public void RunProgram(string programName, string cmd)
        {
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = programName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            if (cmd.Length != 0)
            {
                proc.StandardInput.WriteLine(cmd);
            }
            proc.Close();
        }

        /// <summary> 
        /// 打开软件 
        /// </summary> 
        /// <param name="programName">软件路径加名称（.exe文件）</param> 
        public void RunProgram(string programName)
        {
            this.RunProgram(programName, "");

        }

        public static void ExecuteProcess(string filename, string arguments, int timeout)
        {
            using (Process cmd = new Process())
            {
                Console.WriteLine("Executing: {0} {1}", filename, arguments);
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.FileName = filename;
                cmd.StartInfo.Arguments = arguments;
                cmd.Start();
                cmd.WaitForExit(timeout);
                if (!cmd.HasExited)
                {
                    cmd.Kill();
                    Console.WriteLine(string.Format("ExecuteProcess stdout: {0}", cmd.StandardOutput.ReadToEnd()));
                    Console.WriteLine(string.Format("ExecuteProcess stderr: {0}", cmd.StandardError.ReadToEnd()));
                    throw new Exception("ExecuteProcess times out.");
                }
                if (0 != cmd.ExitCode)
                {
                    Console.WriteLine(string.Format("ExecuteProcess stdout: {0}", cmd.StandardOutput.ReadToEnd()));
                    Console.WriteLine(string.Format("ExecuteProcess stderr: {0}", cmd.StandardError.ReadToEnd()));
                    throw new Exception("ExecuteProcess fails.");
                }
            }
        }

    }
}