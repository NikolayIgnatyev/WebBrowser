using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace WebBrowser
{
    public class PCInfo
    {
        //Свойства называют с большой буквы !
        public string Proc { get; set; }
        public string Video { get; set; }
        public string Disk { get; set; }
        public double SizeDiskGb { get; set; }
        public string Ram { get; set; }
        public double RamSize { get; set; }
        public string MonitorName { get; set; }
        public string KeyboardName { get; set; }
        public string MouseName { get; set; }
        public string MotherboardName { get; set; }
        public string PcName { get; set; }
        public PCInfo()
        {
            Proc = GetHardwareInfo("Win32_Processor", "Name");
            Video = GetHardwareInfo("Win32_VideoController", "Name");
            Disk = GetHardwareInfo("Win32_DiskDrive", "Caption");
            SizeDiskGb = Math.Round(GetHardwareInfoInt("Win32_DiskDrive", "Size") / 1024, 2);
            Ram = GetHardwareInfo("Win32_PhysicalMemory", "Manufacturer");
            RamSize = GetHardwareInfoInt("Win32_PhysicalMemory", "Capacity");
            MonitorName = GetHardwareInfo("Win32_DesktopMonitor", "Name");
            KeyboardName = GetHardwareInfo("Win32_Keyboard", "Description");
            MouseName = GetHardwareInfo("Win32_PointingDevice", "Description");
            MotherboardName = GetHardwareInfo("Win32_BaseBoard", "Manufacturer") + " " + GetHardwareInfo("Win32_BaseBoard", "Product");


            /// возможно это имя хоста, но пишут про эти несколько вариантов
            //PcName = Environment.MachineName;
            //PcName = System.Net.Dns.GetHostName();

            PcName = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
        }




        private static string GetHardwareInfo(string WIN32_Class, string ClassItemField)
        {
            string result = null;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + WIN32_Class);

            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result += $"{obj[ClassItemField].ToString().Trim()} ";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return WordsDistinct(result);
        }

        /// <summary>
        /// Удаление дубликатов ?
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        static string WordsDistinct(string src)
        {
            var split = src.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            var buffer = new int[split.Length];
            var index = 0;

            foreach (var item in split)
            {
                var word = "";
                var puctuationIndex = FirstIndexOfPunctuation(item);
                if (puctuationIndex != -1)
                {
                    word = item.Substring(0, puctuationIndex);
                }
                else
                {
                    word = item;
                }

                var hash = word.ToLower().GetHashCode();
                if (Array.IndexOf(buffer, hash) == -1)
                {
                    buffer[index++] = hash;
                    sb.Append(item);
                    sb.Append(" ");
                }

            }

            return sb.ToString().TrimEnd();
        }
        /// <summary>
        /// подпишешь
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        static int FirstIndexOfPunctuation(string src)
        {
            for (int i = 0; i < src.Length; i++)
            {
                if (char.IsPunctuation(src[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        private double GetHardwareInfoInt(string WIN32_Class, string ClassItemField)
        {
            double result = 0;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);

            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result += Math.Round(Convert.ToDouble(obj[ClassItemField].ToString().Trim()) / 1024 / 1024 / 1024, 2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
