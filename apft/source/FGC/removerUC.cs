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
    public partial class removerUC : UserControl
    {
        public removerUC()
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
                    remover_equipaUC uc = new remover_equipaUC();
                    addUserControl(uc);
                    break;

                case "Pro_Player":
                    remover_ppUC uc2 = new remover_ppUC();
                    addUserControl(uc2);
                    break;

                case "Patrocinador":
                    remover_patroUC uc3 = new remover_patroUC();
                    addUserControl(uc3);
                    break;

                case "Staff":
                    remover_staffUC uc4 = new remover_staffUC();
                    addUserControl(uc4);
                    break;

                case "Equipa_Desenvolvedores":
                    remover_edUC uc5 = new remover_edUC();
                    addUserControl(uc5);
                    break;

                case "Fighting_Game":
                    remover_fgUC uc6 = new remover_fgUC();
                    addUserControl(uc6);
                    break;

                case "Torneio":
                    remover_torneioUC uc7 = new remover_torneioUC();
                    addUserControl(uc7);
                    break;

                default:
                    // Opção padrão, se nenhuma correspondência for encontrada
                    break;
            }
        }
    }
}
