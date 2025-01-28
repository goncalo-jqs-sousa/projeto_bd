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
    public partial class atualizarUC : UserControl
    {
        public atualizarUC()
        {
            InitializeComponent();
            entryUC uc = new entryUC();
            addUserControl(uc);
        }

        private void addUserControl(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(uc);
            uc.BringToFront();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String opcao = comboBox1.SelectedItem.ToString();

            switch (opcao)
            {
                case "Equipa":
                    atualizar_equipaUC uc1 = new atualizar_equipaUC();
                    addUserControl(uc1);
                    break;

                case "Pro_Player":
                    atualizar_ppUC uc2 = new atualizar_ppUC();
                    addUserControl(uc2);
                    break;

                case "Patrocinador":
                    atualizar_patroUC uc3 = new atualizar_patroUC();
                    addUserControl(uc3);
                    break;

                case "Staff":
                    atualizar_staffUC uc4 = new atualizar_staffUC();
                    addUserControl(uc4);
                    break;

                case "Equipa_Desenvolvedores":
                    atualizar_edUC uc5 = new atualizar_edUC();
                    addUserControl(uc5);
                    break;

                case "Fighting_Game":
                    atualizar_fgUC uc6 = new atualizar_fgUC();
                    addUserControl(uc6);
                    break;

                case "Torneio":
                    atualizar_torneioUC uc7 = new atualizar_torneioUC();
                    addUserControl(uc7);
                    break;

                default:
                    // Opção padrão, se nenhuma correspondência for encontrada
                    break;
            }
        }
    }
}
