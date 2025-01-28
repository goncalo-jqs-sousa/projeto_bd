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
    public partial class atualizar_staffUC : UserControl
    {
        private SqlConnection CN;
        public atualizar_staffUC()
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

        private void Atualizar_Click(object sender, EventArgs e)
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {

                String nif = comboBox2.SelectedItem.ToString();
                String nome = (String)textBox2.Text;
                String num_staff = (String)textBox3.Text;
                String salario = (String)textBox4.Text;


                if (string.IsNullOrWhiteSpace(nif) || string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(num_staff) || string.IsNullOrWhiteSpace(salario))
                {
                    MessageBox.Show("Por favor, insira os dados completos");
                    return; // Aborta a execução do evento
                }
                else
                {
                    // INSERIR DADOS NA BD
                    try
                    {
                        CN.Open();

                        // Inserir dados na tabela FGC.Equipa
                        string queryInserirEquipa = "UPDATE FGC.Staff SET nome = @nome, num_staff = @ns, salario = @salario WHERE nif = @nif;";
                        SqlCommand cmdInserirEquipa = new SqlCommand(queryInserirEquipa, CN);
                        cmdInserirEquipa.Parameters.AddWithValue("@nif", nif);
                        cmdInserirEquipa.Parameters.AddWithValue("@nome", nome);
                        cmdInserirEquipa.Parameters.AddWithValue("@ns", num_staff);
                        cmdInserirEquipa.Parameters.AddWithValue("@salario", salario);
                        cmdInserirEquipa.ExecuteNonQuery();

                        MessageBox.Show("Staff atualizado com sucesso!");

                        CN.Close();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao atualizar o staff: " + ex.Message);
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

                string queryEquipas2 = "SELECT nif FROM FGC.Staff";
                SqlCommand cmdEquipas2 = new SqlCommand(queryEquipas2, CN);
                SqlDataReader readerEquipas2 = cmdEquipas2.ExecuteReader();

                comboBox2.Items.Clear();
                while (readerEquipas2.Read())
                {
                    string nomeDoutor = readerEquipas2["nif"].ToString();
                    comboBox2.Items.Add(nomeDoutor);
                }
                readerEquipas2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao consultar o Staff: " + ex.Message);
            }
            finally
            {
                CN.Close(); // Certifique-se de fechar a conexão no evento Load
            }
        }
    }
}
