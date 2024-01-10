using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHOCSINHCAP3.UI
{
    public partial class uc_Chat : UserControl
    {
        private string _title { get; set; }
        public void SetName(string _name)
        {
            label1.Text = _name;
        }
        public uc_Chat()
        {
            InitializeComponent();
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; gvsend.Text = value; }
        }
    }
}
