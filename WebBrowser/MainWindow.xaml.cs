using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WebBrowser.ViewModels;

namespace WebBrowser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PC pc;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            webBrowser.Address = Environment.CurrentDirectory.Replace(@"\", @"/") + "/html/index.html";
            //SetHook();
            pc = new PC
            {
                proc = GetHardwareInfo("Win32_Processor", "Name"),
                video = GetHardwareInfo("Win32_VideoController", "Name"),
                disk = GetHardwareInfo("Win32_DiskDrive", "Caption"),
                sizeDiskGb = Math.Round(GetHardwareInfoInt("Win32_DiskDrive", "Size") / 1024, 2),
                ram = GetHardwareInfo("Win32_PhysicalMemory", "Manufacturer"),
                ramSize = GetHardwareInfoInt("Win32_PhysicalMemory", "Capacity"),
                monitorName = GetHardwareInfo("Win32_DesktopMonitor", "Name"),
                screenSize = GetHardwareInfo("Win32_DesktopMonitor", "ScreenWidth") + "x" + GetHardwareInfo("Win32_DesktopMonitor", "ScreenHeight"),
                keyboardName = GetHardwareInfo("Win32_Keyboard", "Name"),
                mouseName = null,
                motherboardName = GetHardwareInfo("Win32_MotherboardDevice", "Name")
            };
            DataContext = new MainWindowViewModel(pc);
        }
 


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private static string GetHardwareInfo(string WIN32_Class, string ClassItemField)
        {
            string result = null;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);

            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result += $"{obj[ClassItemField].ToString().Trim()}, ";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
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


        private const int WH_KEYBOARD_LL = 13;//Keyboard hook;

        //Keys data structure
        [System.Runtime.InteropServices.StructLayout(
        System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
        }

        //Using callbacks
        private LowLevelKeyboardProcDelegate m_callback;
        private LowLevelKeyboardProcDelegate m_callback_1;
        private LowLevelKeyboardProcDelegate m_callback_2;
        private LowLevelKeyboardProcDelegate m_callback_3;
        private LowLevelKeyboardProcDelegate m_callback_4;
        private LowLevelKeyboardProcDelegate m_callback_5;
        private LowLevelKeyboardProcDelegate m_callback_6;
        private LowLevelKeyboardProcDelegate m_callback_7;

        //переменные для разблокировки клавиатуры
        private IntPtr m_hHook;
        private IntPtr m_hHook_1;
        private IntPtr m_hHook_2;
        private IntPtr m_hHook_3;
        private IntPtr m_hHook_4;
        private IntPtr m_hHook_5;
        private IntPtr m_hHook_6;
        private IntPtr m_hHook_7;

        //Установка перехвата клавиатуры
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(
        int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, int dwThreadId);

        //Разблокировка клавиатуры
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //Hook handle
        [System.Runtime.InteropServices.DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        //Вызов следующего хука
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(
        IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        //Захват winkey
        public IntPtr LowLevelKeyboardHookProc_win(
        int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.RWin ||
                objKeyInfo.key == Keys.LWin)
                {
                    return (IntPtr)1;//winkey
                }
            }
            return CallNextHookEx(m_hHook_1, nCode, wParam, lParam);
        }

        //Захват delete
        public IntPtr LowLevelKeyboardHookProc_del(
        int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {

                KBDLLHOOKSTRUCT objKeyInfo =
                (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Delete)
                {
                    return (IntPtr)1;//delete
                }
            }
            return CallNextHookEx(m_hHook_3, nCode, wParam, lParam);
        }

        //Захват control
        public IntPtr LowLevelKeyboardHookProc_ctrl(
        int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.RControlKey ||
                objKeyInfo.key == Keys.LControlKey)
                {
                    return (IntPtr)1;//control
                }
            }
            return CallNextHookEx(m_hHook_2, nCode, wParam, lParam);
        }

        //Захват alt
        public IntPtr LowLevelKeyboardHookProc_alt(
        int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Alt)
                {
                    return (IntPtr)1;//alt
                }
            }
            return CallNextHookEx(m_hHook_4, nCode, wParam, lParam);
        }

        //Блокировка alt+tab
        public IntPtr LowLevelKeyboardHookProc_alt_tab(
        int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Alt ||
                objKeyInfo.key == Keys.Tab)
                {
                    return (IntPtr)1;//alt+tab
                }
            }
            return CallNextHookEx(m_hHook, nCode, wParam, lParam);
        }

        //Блокировка alt+space
        public IntPtr LowLevelKeyboardHookProc_alt_space(
        int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Alt ||
                objKeyInfo.key == Keys.Space)
                {
                    return (IntPtr)1;//alt+space
                }
            }
            return CallNextHookEx(m_hHook_5, nCode, wParam, lParam);
        }

        //Блокировка control+shift+escape
        public IntPtr LowLevelKeyboardHookProc_control_shift_escape(int nCode,
        IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.LControlKey ||
                objKeyInfo.key == Keys.RControlKey ||
                objKeyInfo.key == Keys.LShiftKey ||
                objKeyInfo.key == Keys.RShiftKey ||
                objKeyInfo.key == Keys.Escape)
                {
                    return (IntPtr)1;//control+shift+escape
                }
            }
            return CallNextHookEx(m_hHook_6, nCode, wParam, lParam);
        }

        //Блокировка control+alt+del
        public IntPtr LowLevelKeyboardHookProc_control_alt_del(int nCode,
        IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.LControlKey ||
                //objKeyInfo.key == Keys.RControlKey ||
                //objKeyInfo.key == Keys.Control ||
                //objKeyInfo.key == Keys.ControlKey ||
                objKeyInfo.key == Keys.Alt ||
                //objKeyInfo.key == Keys.LMenu ||
                //objKeyInfo.key == Keys.Menu ||
                //objKeyInfo.key == Keys.RMenu ||
                objKeyInfo.key == Keys.Delete)
                {
                    return (IntPtr)1;//control+alt+del
                }
            }
            return CallNextHookEx(m_hHook_7, nCode, wParam, lParam);
        }

        private delegate IntPtr LowLevelKeyboardProcDelegate(
        int nCode, IntPtr wParam, IntPtr lParam);

        //Настройки хука
        public void SetHook()
        {
            m_callback = LowLevelKeyboardHookProc_win;
            m_callback_1 = LowLevelKeyboardHookProc_alt_tab;
            m_callback_2 = LowLevelKeyboardHookProc_ctrl;
            m_callback_3 = LowLevelKeyboardHookProc_del;
            m_callback_4 = LowLevelKeyboardHookProc_alt;
            m_callback_5 = LowLevelKeyboardHookProc_alt_space;
            m_callback_6 = LowLevelKeyboardHookProc_control_shift_escape;
            m_callback_7 = LowLevelKeyboardHookProc_control_alt_del;

            //Настройки перехвата
            m_hHook = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback,
            GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_1 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_1,
            GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_2 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_2,
            GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_3 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_3,
            GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_4 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_4,
            GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_5 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_5,
            GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_6 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_6,
            GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_7 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_7,
            GetModuleHandle(IntPtr.Zero), 0);
        }

        //Блокировка сочетаний группой
        public void Unhook()
        {
            UnhookWindowsHookEx(m_hHook);
            UnhookWindowsHookEx(m_hHook_1);
            UnhookWindowsHookEx(m_hHook_2);
            UnhookWindowsHookEx(m_hHook_3);
            UnhookWindowsHookEx(m_hHook_4);
            UnhookWindowsHookEx(m_hHook_5);
            UnhookWindowsHookEx(m_hHook_6);
            UnhookWindowsHookEx(m_hHook_7);
        }
    }
}
