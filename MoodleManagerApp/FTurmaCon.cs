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
    public partial class FTurmaCon : FBase
    {
        public FTurmaCon()
        {
            InitializeComponent();
        }

        private void FTurmaCon_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (FTurmaInc f = new FTurmaInc())
            {
                f.ShowDialog();
            }
        }
    }
}
