using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoodleManagerApp
{
    public partial class FMain : FBase
    {
        public FMain()
        {            
            InitializeComponent();
            this.ShowInTaskbar = true;
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (FUserCon f = new FUserCon())
            {
                f.ShowDialog();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (FCategoryCon f = new FCategoryCon())
            {
                f.ShowDialog();
            }
        }
    }
}
