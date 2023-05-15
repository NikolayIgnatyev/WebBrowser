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

        public MainWindowViewModel(PC pc)
        {
            VolumeDisk = pc.disk;
            Proc = pc.proc;
            Video = pc.video;
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
