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
    public partial class FTurmaInc : MoodleManagerApp.FBase
    {
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
            Close();
        }

        private void FTurmaInc_Load(object sender, EventArgs e)
        {
            var categories = db.Categories.ToList();
            cbCategoria.DataSource = categories;
            cbCategoria.DisplayMember = "name";
            cbCategoria.ValueMember = "id";
        }
    }
}
