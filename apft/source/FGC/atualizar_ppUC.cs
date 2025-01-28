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
    public partial class atualizar_ppUC : UserControl
    {
        private SqlConnection CN;
        public atualizar_ppUC()
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
                String prize_money = (String)textBox3.Text;
                String games_played = (String)textBox4.Text;
                String nif_equipa = comboBox1.SelectedItem.ToString();


                if (string.IsNullOrWhiteSpace(nif) || string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(games_played) || string.IsNullOrWhiteSpace(nif_equipa))
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
                        string queryInserirEquipa = "UPDATE FGC.ProPlayer SET nome = @nome, prize_money = @pm, games_played = @gp, nif_equipa = @nifeq WHERE nif = @nif;";
                        SqlCommand cmdInserirEquipa = new SqlCommand(queryInserirEquipa, CN);
                        cmdInserirEquipa.Parameters.AddWithValue("@nif", nif);
                        cmdInserirEquipa.Parameters.AddWithValue("@nome", nome);
                        cmdInserirEquipa.Parameters.AddWithValue("@pm", prize_money);
                        cmdInserirEquipa.Parameters.AddWithValue("@gp", games_played);
                        cmdInserirEquipa.Parameters.AddWithValue("@nifeq", nif_equipa);
                        cmdInserirEquipa.ExecuteNonQuery();

                        MessageBox.Show("Pro player atualizado com sucesso!");

                        CN.Close();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao atualizar o pro player: " + ex.Message);
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
                string queryEquipas = "SELECT nif FROM FGC.Equipa";
                SqlCommand cmdEquipas = new SqlCommand(queryEquipas, CN);
                SqlDataReader readerEquipas = cmdEquipas.ExecuteReader();

                comboBox1.Items.Clear();
                while (readerEquipas.Read())
                {
                    string nomeDoutor = readerEquipas["nif"].ToString();
                    comboBox1.Items.Add(nomeDoutor);
                }
                readerEquipas.Close();

                string queryEquipas2 = "SELECT nif FROM FGC.ProPlayer";
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
                MessageBox.Show("Ocorreu um erro ao consultar as equipas: " + ex.Message);
            }
            finally
            {
                CN.Close(); // Certifique-se de fechar a conexão no evento Load
            }
        }
    }
}
