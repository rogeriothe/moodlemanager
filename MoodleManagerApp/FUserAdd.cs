using MoodleManagerApp.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoodleManagerApp
{
    public partial class FUserAdd : FBase
    {
                
        public int id = 0;
        Users user = new Users();

        public FUserAdd()
        {
            InitializeComponent();
        }

        private void FUserAdd_Load(object sender, EventArgs e)
        {
            if (id != 0)
            {
                user = db.Users.FirstOrDefault(a => a.id == id);
                txtEmail.Text = user.email;
                txtFirstName.Text = user.firstname;
                txtLastName.Text = user.lastname;
                txtPassword.Text = user.password;
                txtUserName.Text = user.username;
                chkSuspended.Checked = (bool)user.suspended;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            user.email = txtEmail.Text;
            user.firstname = txtFirstName.Text;
            user.lastname = txtLastName.Text;
            user.password = txtPassword.Text;
            user.username = txtUserName.Text;
            user.suspended = chkSuspended.Checked;


            if (id == 0)
            {

                int moodle_id = UserApi.create_user(user);
                user.id = moodle_id;

            }else
            {
                UserApi.update_user(user);
            }

            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            Close();

        }
    }
}
