using MoodleManagerApp.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoodleManagerApp
{
    public partial class FSync : FBase
    {
        public FSync()
        {
            InitializeComponent();
        }

        private void FSync_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var lista = CategoryApi.get_categories();

            barra.Value = 0;
            barra.Maximum = lista.Count;

            foreach (var item in lista)
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

                barra.Value++;
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var lista = UserApi.get_users();

            barra.Value = 0;
            barra.Maximum = lista.Count;

            List<Users> usuarios = db.Users.ToList();            

            foreach (var item in lista)
            {
                if (usuarios.FirstOrDefault(a => a.username == item.username) == null)
                {
                    lbInfo.Text = item.firstname + " " + item.lastname;
                    Application.DoEvents();

                    try
                    {
                        Users user = new Users();
                        user.email = item.email;
                        user.firstname = item.firstname;
                        user.lastname = item.lastname;
                        user.password = item.username + "@123";
                        user.username = item.username;
                        user.id = item.id;
                        user.suspended = item.suspended;

                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var eve in ex.EntityValidationErrors)
                        {
                            Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }


                }
                
                barra.Value++;
            }

            lbInfo.Text = "";
            MessageBox.Show("Importação de usuários concluída." + Environment.NewLine +
                barra.Maximum + " usuários sincronizados.");
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Groups> lista = GroupsApi.get_groups();

            barra.Value = 0;
            barra.Maximum = lista.Count;


            foreach (Groups item in lista)
            {
                var qr = (from p in db.Groups
                          where p.category_id == item.category_id && p.name == item.name
                          select p).ToList();

                if (qr.Count() == 0)
                {
                    db.Groups.Add(new Groups { name = item.name, category_id = item.category_id });
                    db.SaveChanges();
                }
            }

            barra.Value++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Users> usuarios = db.Users.ToList();
            barra.Value = 0;
            barra.Maximum = usuarios.Count;


            foreach (var item in usuarios)
            {
                lbInfo.Text = item.firstname + " " + item.lastname;
                Application.DoEvents();
                UserApi.create_user(item);
                UserApi.update_user(item, false);
                barra.Value++;
            }

            lbInfo.Text = "";
            MessageBox.Show("Exportação de usuários concluída." + Environment.NewLine +
                barra.Maximum + " usuários sincronizados.");
        }
    }
}
