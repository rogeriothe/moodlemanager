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
    public partial class FCategoryInc : FBase
    {

        public int id = 0;
        Categories category = new Categories();
        public FCategoryInc()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            category.name = txtFirstName.Text;


            if (id == 0)
            {

                int moodle_id = CategoryApi.create_category(category);
                category.id = moodle_id;

            }
            else
            {
                CategoryApi.update_category(category);
            }

            db.Categories.AddOrUpdate(category);
            db.SaveChanges();
            Close();
        }

        private void FCategoryInc_Load(object sender, EventArgs e)
        {
            if (id != 0)
            {
                category = db.Categories.FirstOrDefault(a => a.id == id);
                txtFirstName.Text = category.name;

            }
        }
    }
}
