using Microsoft.Win32;
using System;
namespace QInstaller
{
    class Program
    {

        static void Main(string[] args)
        {
            string str = "";
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            if (args.Length > 0)
            {
                str = "adb install " + args[0];
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序

                //版权
                Console.Write("QInstaller on Windows [版本 1.0.0]\n");
                Console.Write("(c) 2021 JackuXL 保留所有权利。\n");
                Console.Write("交流论坛：wearbbs.cn\n\n");

                //向cmd窗口发送输入信息
                p.StandardInput.WriteLine("@echo off");
                p.StandardInput.WriteLine(str + "&exit");
                p.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完成退出进程
                p.Close();


                output = output.Substring(output.LastIndexOf("exit"), output.Length - 1 - output.LastIndexOf("exit"));
                output = output.Replace("exit", "");
                output = output.Replace("\n", "\nADB> [Log] ");
                Console.WriteLine("QInstaller> [Info] 安装日志：" + output);
                Console.Write("QInstaller> [Info] 安装完成，请按任意键结束...");
                Console.ReadKey();
            }
            else
            {
                Console.Write("QInstaller on Windows [版本 1.0.0]\n");
                Console.Write("(c) 2021 JackuXL 保留所有权利。\n");
                Console.Write("交流论坛：wearbbs.cn\n\n");
                Console.Write("QInstaller> [Error] 没有检测到APK文件，请使用APK文件唤起软件\n");
                Console.Write("QInstaller> [Info] 请按任意键结束...");
                Console.ReadKey();
            }
        }
    }
}
