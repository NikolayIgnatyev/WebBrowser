using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public MainWindowViewModel(PC pc)
        {
            VolumeDisk = pc.sizeDiskGb.ToString();
            Proc = pc.proc;
            Video = pc.video;
            Disk = pc.disk;
            Ram = pc.ram;
            VolumeRam = pc.ramSize.ToString();

        }
        public string Ram
        {
            get
            {
                return _Ram;
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
                return _Disk;
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
                return _volumeDisk;
            }
            set
            {
                _volumeDisk = value;
                OnPropertyChanged(nameof(VolumeDisk));
            }
        }
        public string Proc
        {
            get { return _proc; }
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
                return _video;
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
                return _volumeRam;
            }
            set
            {
                _volumeRam = value;
                OnPropertyChanged(nameof(VolumeRam));
            }
        }
    }
}
