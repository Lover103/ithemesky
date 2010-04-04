using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IThemeSky.ThemesRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            MBThemeRobot robot = new MBThemeRobot();
            //for (int i = 29; i <= 36; i++)       //34
            //{
            //    robot.AnalyseList(i);
            //}
            //robot.AnalyseFromErrorUrl();
            ThemeFileChecker.Check();
            //Console.WriteLine("完成所有抓取！！！！！！！！BinGo!");
            Console.Read();
        }
    }
}
