using MoodleManagerApp.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoodleManagerApp
{
    public partial class FUserCon : FBase
    {
        moodleEntities db = new moodleEntities();
        public FUserCon()
        {
            InitializeComponent();
        }

        private void FUserCon_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void loadGrid()
        {
            db = new moodleEntities();
            var qr = db.User.ToList();
            grid.DataSource = qr;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var usersMoodle = UserApi.get_users();

            foreach (var item in usersMoodle)
            {
                if (db.User.FirstOrDefault(a=>a.username == item.username) == null)
                {
                    Debug.Print(item.username);
                }
            }

            loadGrid();
        }
    }
}
