using MoodleManagerApp.Repositorio;
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
            timer_site.Enabled = true;
            Checksite();
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

        private void timer_site_Tick(object sender, EventArgs e)
        {
            Checksite();
        }

        private void Checksite()
        {
            if (Funcoes.get_site_info())
            {
                lbStatus.Text = "Moodle On-line";
            }
            else
            {
                lbStatus.Text = "Moodle Off-line";
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (FTurmaCon f = new FTurmaCon())
            {
                f.ShowDialog();
            }
        }
    }
}
