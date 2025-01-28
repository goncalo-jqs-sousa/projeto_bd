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
    public partial class atualizar_fgUC : UserControl
    {
        private SqlConnection CN;
        public atualizar_fgUC()
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

                String nome = comboBox2.SelectedItem.ToString();
                String num_vendas = (String)textBox2.Text;
                String active_players = (String)textBox3.Text;
                String player_peak = (String)textBox4.Text;
                String nif_equipa_desenvolvedores = comboBox1.SelectedItem.ToString();


                if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(num_vendas) || string.IsNullOrWhiteSpace(active_players) || string.IsNullOrWhiteSpace(player_peak) || string.IsNullOrWhiteSpace(nif_equipa_desenvolvedores))
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
                        string queryInserirEquipa = "UPDATE FGC.FightingGame SET num_vendas = @nv, active_players = @ap, player_peak = @pp, nif_equipa_desenvolvedores = @nif_ed WHERE nome = @nome;";
                        SqlCommand cmdInserirEquipa = new SqlCommand(queryInserirEquipa, CN);
                        cmdInserirEquipa.Parameters.AddWithValue("@nome", nome);
                        cmdInserirEquipa.Parameters.AddWithValue("@nv", num_vendas);
                        cmdInserirEquipa.Parameters.AddWithValue("@ap", active_players);
                        cmdInserirEquipa.Parameters.AddWithValue("@pp", player_peak);
                        cmdInserirEquipa.Parameters.AddWithValue("@nif_ed", nif_equipa_desenvolvedores);
                        cmdInserirEquipa.ExecuteNonQuery();

                        MessageBox.Show("Fighting game atualizado com sucesso!");

                        CN.Close();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao atualizar o fighting game: " + ex.Message);
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
                string queryEquipas = "SELECT nif FROM FGC.EquipaDesenvolvedores";
                SqlCommand cmdEquipas = new SqlCommand(queryEquipas, CN);
                SqlDataReader readerEquipas = cmdEquipas.ExecuteReader();

                comboBox1.Items.Clear();
                while (readerEquipas.Read())
                {
                    string nomeDoutor = readerEquipas["nif"].ToString();
                    comboBox1.Items.Add(nomeDoutor);
                }
                readerEquipas.Close();

                string queryEquipas2 = "SELECT nome FROM FGC.FightingGame";
                SqlCommand cmdEquipas2 = new SqlCommand(queryEquipas2, CN);
                SqlDataReader readerEquipas2 = cmdEquipas2.ExecuteReader();

                comboBox2.Items.Clear();
                while (readerEquipas2.Read())
                {
                    string nomeDoutor = readerEquipas2["nome"].ToString();
                    comboBox2.Items.Add(nomeDoutor);
                }
                readerEquipas2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao consultar os fighting games: " + ex.Message);
            }
            finally
            {
                CN.Close(); // Certifique-se de fechar a conexão no evento Load
            }
        }
    }
}
