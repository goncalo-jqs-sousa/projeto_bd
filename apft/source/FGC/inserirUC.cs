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
    public partial class inserirUC : UserControl
    {
        public inserirUC()
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
                    inserir_equipaUC uc1 = new  inserir_equipaUC();
                    addUserControl(uc1);
                    break;

                case "Pro_Player":
                    inserir_ppUC uc2 = new inserir_ppUC();
                    addUserControl(uc2);
                    break;
                  
                case "Patrocinador":
                    inserir_patroUC uc3 = new inserir_patroUC();
                    addUserControl(uc3);
                    break;

                case "Staff":
                    inserir_staffUC uc4 = new inserir_staffUC();
                    addUserControl(uc4);
                    break;

                case "Equipa_Desenvolvedores":
                    inserir_edUC uc5 = new inserir_edUC();
                    addUserControl(uc5);
                    break;

                case "Fighting_Game":
                    inserir_fgUC uc6 = new inserir_fgUC();
                    addUserControl(uc6);
                    break;

                case "Torneio":
                    inserir_torneioUC uc7 = new inserir_torneioUC();
                    addUserControl(uc7);
                    break;

                default:
                    // Opção padrão, se nenhuma correspondência for encontrada
                    break;
            }
        }
    }
}
