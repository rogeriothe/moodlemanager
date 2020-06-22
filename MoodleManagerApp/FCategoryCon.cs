using MoodleManagerApp.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoodleManagerApp
{
    public partial class FCategoryCon : MoodleManagerApp.FBase
    {
        public FCategoryCon()
        {
            InitializeComponent();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var categoriesMoodle = CategoryApi.get_categories();

            foreach (var item in categoriesMoodle)
            {
                if (db.Categories.FirstOrDefault(a => a.name == item.name) == null)
                {

                    Categories category = new Categories();

                    category.id = item.id;
                    category.name = item.name;
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
            }

            //loadGrid();
        }
    }
}
