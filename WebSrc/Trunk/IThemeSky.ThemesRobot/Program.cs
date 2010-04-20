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
            //MBThemeRobot robot = new MBThemeRobot();
            //for (int i = 29; i <= 36; i++)       //34
            //{
            //    robot.AnalyseList(i);
            //}
            //robot.AnalyseFromErrorUrl();
            //ThemeFileChecker.Check();
            //MRobot r = new MRobot();
            //r.Start();
            //RepairTheme rt = new RepairTheme();
            //rt.RepairCode();
            ThemeFileChecker.Check();
            Console.WriteLine("bingo");
            Console.Read();
        }
    }
}











