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
    public partial class FTurmaCon : FBase
    {
        public FTurmaCon()
        {
            InitializeComponent();
        }

        private void FTurmaCon_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void loadGrid()
        {
            db = new moodleEntities();

            if (txtName.Text.Length > 3)
            {

                var qr = from p in db.Groups
                         where p.name.Contains(txtName.Text)
                         select new
                         {
                             p.id,
                             p.name,
                             Categoria = p.Categories.name
                         };

                grid.DataSource = qr.ToList();
            }
            else
            {

                var qr = from p in db.Groups                         
                         select new
                         {
                             p.id,
                             p.name,
                             Categoria = p.Categories.name
                         };

                grid.DataSource = qr.ToList();
            }

            this.grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (FTurmaInc f = new FTurmaInc())
            {
                f.ShowDialog();
            }

            loadGrid();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            

            using (FTurmaInc f = new FTurmaInc())
            {
                int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value.ToString());
                f.id = id;
                f.ShowDialog();
            }

            loadGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Funcoes.Pergunta("Excluir turma? Esta operação é irreversível.", "Excluir?"))
            {
                int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value.ToString());
                var group = db.Groups.FirstOrDefault(a => a.id == id);
                db.Groups.Remove(group);
                db.SaveChanges();
                loadGrid();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
        }
    }
}
