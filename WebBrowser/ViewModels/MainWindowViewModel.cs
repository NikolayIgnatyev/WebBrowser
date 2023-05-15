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


        string Proc
        {
            get { return _proc; }
            set 
            { 
                _proc = value; 
                OnPropertyChanged(nameof(Proc));
            }
        }
        string Video
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
        string VolumeRam
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
