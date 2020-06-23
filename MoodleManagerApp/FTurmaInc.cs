using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoodleManagerApp
{
    public partial class FTurmaInc : MoodleManagerApp.FBase
    {
        Groups group = new Groups();
        public int id = 0;
        public FTurmaInc()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                group = new Groups();
            }

            group.category_id = ((Categories)cbCategoria.SelectedItem).id;
            group.name = txtName.Text;

            db.Groups.AddOrUpdate(group);
            db.SaveChanges();

            Close();
        }

        private void FTurmaInc_Load(object sender, EventArgs e)
        {
            var categories = db.Categories.ToList();
            cbCategoria.DataSource = categories;
            cbCategoria.DisplayMember = "name";
            cbCategoria.ValueMember = "id";

            group = db.Groups.FirstOrDefault(a=>a.id == id);

            if (id != 0)
            {
                cbCategoria.SelectedItem = group.Categories;
                txtName.Text = group.name;
            }
        }
    }
}
