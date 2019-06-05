using System;
using Microsoft.Win32;

namespace LumosAdapterConsole
{
    class Program
    {
        const int lightHour = 7;
        const int darkHour = 22;

        static void Main(string[] args)
        {
            Console.WriteLine("LumosAdapter protège vos yeux !");
            TimeSpan darkTimeSpan = TimeSpan.FromHours(darkHour);
            TimeSpan lightTimeSpan = TimeSpan.FromHours(lightHour);
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (isBetweenTwoTimeSpan(now, darkTimeSpan, lightTimeSpan))
            {
                //Switch to dark theme
                switchTheme("0");
            }
            else
            {
                //Switch to light theme
                switchTheme("1");
            }

        }

        static void switchTheme(String value)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", true);
            if (registry != null)
            {
                registry.SetValue("SystemUsesLightTheme", value, RegistryValueKind.DWord);
                registry.SetValue("AppsUseLightTheme", value, RegistryValueKind.DWord);
                registry.Close();
            }
            else
            {
                Console.WriteLine("Clef de registre introuvable");
            }

        }

        static bool isBetweenTwoTimeSpan(TimeSpan candidateTime, TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime < endTime)
            {
                // Normal case, e.g. 8am-2pm
                return startTime <= candidateTime && candidateTime < endTime;
            }
            else
            {
                // Reverse case, e.g. 10pm-2am
                return startTime <= candidateTime || candidateTime < endTime;
            }
        }
    }
}

