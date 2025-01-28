using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGC
{
    public partial class remover_staffUC : UserControl
    {
        private SqlConnection CN;
        public remover_staffUC()
        {
            InitializeComponent();
        }
        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source = " + AppData.DB_STRING + " ;" + "Initial Catalog = " + AppData.username + "; uid = " + AppData.username + ";" + "password = " + AppData.password);
        }

        private bool verifySGBDConnection()
        {
            if (CN == null)
                CN = getSGBDConnection();

            if (CN.State != ConnectionState.Open)
                CN.Open();

            return CN.State == ConnectionState.Open;
        }

        private void Remover_Click(object sender, EventArgs e)
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {

                String nif = comboBox1.SelectedItem.ToString();


                if (string.IsNullOrWhiteSpace(nif))
                {
                    MessageBox.Show("Por favor, insira os dados completos");
                    return; // Aborta a execução do evento
                }
                else
                {
                    // Remover DADOS NA BD
                    try
                    {
                        CN.Open();

                        // Remover dados na tabela FGC.Equipa
                        string queryRemoverEquipa = "DELETE FROM FGC.Staff WHERE nif = @nif";
                        SqlCommand cmdRemoverEquipa = new SqlCommand(queryRemoverEquipa, CN);
                        cmdRemoverEquipa.Parameters.AddWithValue("@nif", nif);
                        cmdRemoverEquipa.ExecuteNonQuery();

                        MessageBox.Show("Staff removido com sucesso!");

                        CN.Close();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao remover o Staff: " + ex.Message);
                    }
                }
            }

            else if (!verifySGBDConnection())
            {
                MessageBox.Show("FAILED TO OPEN CONNECTION TO DATABASE", "Connection Test", MessageBoxButtons.OK);
                return;
            }
        }
        private void Equipa_Load(object sender, EventArgs e)
        {
            CN = getSGBDConnection(); // Obter uma nova instância de conexão
            try
            {
                CN.Open();

                // Consultar os nifs das equipas e adicionar ao comboBox1
                string queryEquipas = "SELECT nif FROM FGC.Staff";
                SqlCommand cmdEquipas = new SqlCommand(queryEquipas, CN);
                SqlDataReader readerEquipas = cmdEquipas.ExecuteReader();

                comboBox1.Items.Clear();
                while (readerEquipas.Read())
                {
                    string nomeDoutor = readerEquipas["nif"].ToString();
                    comboBox1.Items.Add(nomeDoutor);
                }
                readerEquipas.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao consultar os staffs: " + ex.Message);
            }
            finally
            {
                CN.Close(); // Certifique-se de fechar a conexão no evento Load
            }
        }
    }
}
