using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace WebBrowser
{
    public class PC
    {
        public string proc { get; set; }
        public string video { get; set; }
        public string disk { get; set; }
        public double sizeDiskGb { get; set; }
        public string ram { get; set; }
        public double ramSize { get; set; }
        public string monitorName { get; set; }
        public string keyboardName { get; set; }
        public string mouseName { get; set; }
        public string motherboardName { get; set; }
        public PC() { }
    }
}
