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
                    User user = new User();
                    user.email = item.email;
                    user.firstname = item.firstname;
                    user.lastname = item.lastname;
                    user.password = item.password;
                    user.username = item.username;
                    user.moodle_id = item.moodle_id;
                    db.User.Add(user);
                    db.SaveChanges();
                }
            }

            loadGrid();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (FUserAdd f = new FUserAdd())
            {
                f.ShowDialog();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (FUserAdd f = new FUserAdd())
            {
                int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value.ToString());
                f.id = id;
                f.ShowDialog();
            }

            loadGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.moodle_id = 7;
            UserApi.delete_user(user);

            //int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value.ToString());
            //var user = db.User.FirstOrDefault(a => a.id == id);
            //UserApi.delete_user(user);
            //db.User.Remove(user);
            //db.SaveChanges();
            //loadGrid();
        }
    }
}
