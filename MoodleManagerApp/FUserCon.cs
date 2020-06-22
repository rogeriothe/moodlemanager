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

            if (txtName.Text.Length > 3)
            {

                var qr = from p in db.Users
                         where p.firstname.Contains(txtName.Text) || p.username.Contains(txtName.Text)
                         select p;

                grid.DataSource = qr.ToList();
            }
            else
            {
                
                var qr = db.Users.ToList();
                grid.DataSource = qr;
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var usersMoodle = UserApi.get_users();

            foreach (var item in usersMoodle)
            {
                if (db.Users.FirstOrDefault(a=>a.username == item.username) == null)
                {
                    Users user = new Users();
                    user.email = item.email;
                    user.firstname = item.firstname;
                    user.lastname = item.lastname;
                    user.password = item.username;
                    user.username = item.username;
                    user.id = item.id;
                    user.suspended = item.suspended;
                    db.Users.Add(user);
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

            int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value.ToString());
            var user = db.Users.FirstOrDefault(a => a.id == id);
            UserApi.delete_user(user);
            db.Users.Remove(user);
            db.SaveChanges();
            loadGrid();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
