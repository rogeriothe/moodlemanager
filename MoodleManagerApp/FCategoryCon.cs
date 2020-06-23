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

                    if (item.name != "Miscellaneous")
                    {
                        Categories category = new Categories();

                        category.id = item.id;
                        category.name = item.name;
                        db.Categories.Add(category);
                        db.SaveChanges();
                    }
                    
                }
            }

            loadGrid();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (FCategoryInc f = new FCategoryInc())
            {
                f.ShowDialog();
            }
            loadGrid();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (FCategoryInc f = new FCategoryInc())
            {
                int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value.ToString());
                f.id = id;
                f.ShowDialog();
            }
            loadGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Funcoes.Pergunta("Excluir categoria? Esta operação é irreversível.", "Excluir?"))
            {
                int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value.ToString());
                var category = db.Categories.FirstOrDefault(a => a.id == id);
                CategoryApi.delete_category(category);
                db.Categories.Remove(category);
                db.SaveChanges();
                loadGrid();
            }
        }

        private void loadGrid()
        {
            db = new moodleEntities();

            if (txtName.Text.Length > 3)
            {

                var qr = from p in db.Categories
                         where p.name.Contains(txtName.Text)
                         select p;

                grid.DataSource = qr.ToList();
            }
            else
            {

                var qr = db.Categories.ToList();
                grid.DataSource = qr;
            }

            this.grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void FCategoryCon_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
