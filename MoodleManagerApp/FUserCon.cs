using MoodleManagerApp.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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

            this.grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            grid.Columns[2].Visible = false;
            grid.Columns[6].Visible = false;


        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            

            
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
            if (Funcoes.Pergunta("Excluir usuário? Esta operação é irreversível.","Excluir?"))
            {
                int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value.ToString());
                var user = db.Users.FirstOrDefault(a => a.id == id);
                UserApi.delete_user(user);
                db.Users.Remove(user);
                db.SaveChanges();
                loadGrid();
            }


        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
