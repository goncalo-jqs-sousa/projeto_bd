using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGC
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            Entry uc = new Entry();
            addUserControl(uc);
        }

        private void addUserControl(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(uc);
            uc.BringToFront();
        }

        private void inserir_Click(object sender, EventArgs e)
        {
            inserirUC uc = new inserirUC();
            addUserControl(uc);
        }

        private void remover_Click(object sender, EventArgs e)
        {
            removerUC uc = new removerUC();
            addUserControl(uc);
        }

        private void atualizar_Click(object sender, EventArgs e)
        {
            atualizarUC uc = new atualizarUC();
            addUserControl(uc);
        }

        private void visualizar_Click(object sender, EventArgs e)
        {
            visualizarUC uc = new visualizarUC();
            addUserControl(uc);
        }
    }
}
