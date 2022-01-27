using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using registroCoche.modelos;

namespace registroCoche
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            refrescar();
        }

        private void refrescar() {

            using (aplicacion1Entities DB = new aplicacion1Entities())
            {
                var lst = from d in DB.Coches select d;

                dataGridView1.DataSource = lst.ToList();
            }

        }

        private void Añadir_Click(object sender, EventArgs e)
        {
            ventanas.frmAñadir ofrmAñadir = new ventanas.frmAñadir();
            ofrmAñadir.ShowDialog();

            refrescar();
        }

        private int? getID() {
            try { 
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch {
                return null;
            }
        }

        private void editar_Click(object sender, EventArgs e)
        {
            int? id = getID();
            if (id != null) {
                ventanas.frmAñadir oFrmEditar = new ventanas.frmAñadir(id);
                oFrmEditar.ShowDialog();
                refrescar();
            }
        }

        private void borrar_Click(object sender, EventArgs e)
        {
            int? id = getID();
            if (id != null)
            {
                using (aplicacion1Entities DB = new aplicacion1Entities()) {
                    Coches oCoches = DB.Coches.Find(id);
                    DB.Coches.Remove(oCoches);
                    DB.SaveChanges();
                }
                refrescar();
            }
        }
    }
}
