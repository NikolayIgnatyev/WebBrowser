using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebBrowser.ViewModels
{
    public class MainWindowViewModel:BaseViewModel
    {
        string _proc;
        string _video;
        string _volumeRam;
        string _volumeDisk;
        string _Disk;
        string _Ram;
        string _monitorName;
        string _keyboardName;
        string _mother;
        string _mouse;
        string _pcName;

        public string PcName
        {
            get
            {
                return "Имя компьютера: " + PC.PcName;
            }
            set
            {
                _pcName = value;
                OnPropertyChanged(nameof(PcName));
            }
        }
        public PCInfo _pc = new PCInfo();
        public PCInfo PC
        {
            get
            {
                return _pc;
            }
            set
            {
                _pc = value;
                OnPropertyChanged(nameof(PC));
            }
        }
        public string Mouse
        {
            get
            {
                return "Мышь: " + PC.MouseName;
            }
            set
            {
                _mouse =  value;
                OnPropertyChanged(nameof(Mouse));
            }
        }
        public string KeyboardName
        {
            get
            {
                return "Клавиатура: " + PC.KeyboardName;
            }
            set
            {
                _keyboardName = value;
                OnPropertyChanged(nameof(KeyboardName));
            }
        }
        public string Mother
        {
            get
            {
                return "Материнская плата: " + PC.MotherboardName;
            }
            set
            {
                _mother = value;
                OnPropertyChanged(nameof(Mother));
            }
        }
        public string MonitorName
        {
            get
            {
                return "Монитор: " + PC.MonitorName;
            }
            set
            {
                _monitorName = value;
                OnPropertyChanged(nameof(MonitorName));
            }
        }
        public string Ram
        {
            get
            {
                return "Оперативная память: " + PC.Ram;
            }
            set
            {
                _Ram = value;
                OnPropertyChanged(nameof(Ram));
            }
        }
        public string Disk
        {
            get
            {
                return "Диск: " + PC.Disk;
            }
            set
            {
                _Disk = value;
                OnPropertyChanged(nameof(Disk));
            }
        }
        public string VolumeDisk
        {
            get
            {
                return "Объём диска: " + PC.SizeDiskGb + " ТБ";
            }
            set
            {
                _volumeDisk = value;
                OnPropertyChanged(nameof(VolumeDisk));
            }
        }
        public string Proc
        {
            get 
            { 
                return "Процессор: " + PC.Proc; 
            }
            set 
            { 
                _proc = value; 
                OnPropertyChanged(nameof(Proc));
            }
        }
        public string Video
        {
            get
            {
                return "Видеокарта: " + PC.Video;
            }
            set
            {
                _video = value;
                OnPropertyChanged(nameof(Video));
            }
        }
        public string VolumeRam
        {
            get
            {
                return "Объем оперативной памяти: " + PC.RamSize + " ГБ";
            }
            set
            {
                _volumeRam = value;
                OnPropertyChanged(nameof(VolumeRam));
            }
        }
    }
}
