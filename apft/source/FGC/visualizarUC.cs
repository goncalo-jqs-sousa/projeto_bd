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
    public partial class visualizarUC : UserControl
    {
        public visualizarUC()
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
                    visualizar_equipaUC uc1 = new visualizar_equipaUC();
                    addUserControl(uc1);
                    break;

                case "Pro_Player":
                    visualizar_ppUC uc2 = new visualizar_ppUC();
                    addUserControl(uc2);
                    break;

                case "Patrocinador":
                    visualizar_patroUC uc3 = new visualizar_patroUC();
                    addUserControl(uc3);
                    break;

                case "Staff":
                    visualizar_staffUC uc4 = new visualizar_staffUC();
                    addUserControl(uc4);
                    break;

                case "Equipa_Desenvolvedores":
                    visualizar_edUC uc5 = new visualizar_edUC();
                    addUserControl(uc5);
                    break;

                case "Fighting_Game":
                    visualizar_fgUC uc6 = new visualizar_fgUC();
                    addUserControl(uc6);
                    break;

                case "Torneio":
                    visualizar_torneioUC uc7 = new visualizar_torneioUC();
                    addUserControl(uc7);
                    break;

                case "View":
                    visualizar_viewUC uc8 = new visualizar_viewUC();
                    addUserControl(uc8);
                    break;

                case "SPs":
                    visualizar_spUC uc9 = new visualizar_spUC();
                    addUserControl(uc9);
                    break;

                case "UDFs":
                    visualizar_udfUC uc10 = new visualizar_udfUC();
                    addUserControl(uc10);
                    break;

                default:
                    // Opção padrão, se nenhuma correspondência for encontrada
                    break;
            }
        }
    }
}
