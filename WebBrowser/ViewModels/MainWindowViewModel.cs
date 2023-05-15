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
        string _screenSize;
        string _mouse;
        public MainWindowViewModel(PC pc)
        {
            Proc = pc.proc;
            Video = pc.video;

            Mother = pc.motherboardName;
            ScreenSize = pc.screenSize;
            Mouse = pc.mouseName;
            MonitorName = pc.monitorName;

            KeyboardName = pc.keyboardName;
            Disk = pc.disk;
            VolumeDisk = pc.sizeDiskGb.ToString();
            Ram = pc.ram;
            VolumeRam = pc.ramSize.ToString();

        }
        public string Mouse
        {
            get
            {
                return _mouse;
            }
            set
            {
                _mouse = "Мышь: " + value;
                OnPropertyChanged(nameof(Mouse));
            }
        }
        public string ScreenSize
        {
            get
            {
                return _screenSize;
            }
            set
            {
                _screenSize = "Разрешение экрана: " + value;
                OnPropertyChanged(nameof(ScreenSize));
            }
        }
        public string KeyboardName
        {
            get
            {
                return _keyboardName;
            }
            set
            {
                _keyboardName ="Клавиатура: "+ value;
                OnPropertyChanged(nameof(KeyboardName));
            }
        }
        public string Mother
        {
            get
            {
                return _mother;
            }
            set
            {
                _mother = "Материнская плата: " + value;
                OnPropertyChanged(nameof(Mother));
            }
        }
        public string MonitorName
        {
            get
            {
                return _monitorName;
            }
            set
            {
                _monitorName = "Монитор: " + value;
                OnPropertyChanged(nameof(MonitorName));
            }
        }
        public string Ram
        {
            get
            {
                return _Ram;
            }
            set
            {
                _Ram = "Оперативная память: " + value;
                OnPropertyChanged(nameof(Ram));
            }
        }
        public string Disk
        {
            get
            {
                return _Disk;
            }
            set
            {
                _Disk ="Диск: " + value;
                OnPropertyChanged(nameof(Disk));
            }
        }
        public string VolumeDisk
        {
            get
            {
                return _volumeDisk;
            }
            set
            {
                _volumeDisk = "Объём диска: " + value + " ТБ";
                OnPropertyChanged(nameof(VolumeDisk));
            }
        }
        public string Proc
        {
            get { return _proc; }
            set 
            { 
                _proc = "Процессор: " + value; 
                OnPropertyChanged(nameof(Proc));
            }
        }
        public string Video
        {
            get
            {
                return _video;
            }
            set
            {
                _video = "Видеокарта: " +value;
                OnPropertyChanged(nameof(Video));
            }
        }
        public string VolumeRam
        {
            get
            {
                return "Объем оперативной памяти: " + _volumeRam + " ГБ";
            }
            set
            {
                _volumeRam = value;
                OnPropertyChanged(nameof(VolumeRam));
            }
        }
    }
}
