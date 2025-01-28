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
    public partial class atualizar_torneioUC : UserControl
    {
        private SqlConnection CN;
        public atualizar_torneioUC()
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
                String bilhetes_vendidos = (String)textBox2.Text;
                String prize_pool = (String)textBox3.Text;
                String valor_anuncios = (String)textBox4.Text;
                String inscricoes = (String)textBox5.Text;
                String nif_staff = comboBox1.SelectedItem.ToString();


                if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(bilhetes_vendidos) || string.IsNullOrWhiteSpace(prize_pool) || string.IsNullOrWhiteSpace(valor_anuncios) || string.IsNullOrWhiteSpace(inscricoes) || string.IsNullOrWhiteSpace(nif_staff))
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
                        string queryInserirEquipa = "UPDATE FGC.Torneio SET bilhetes_vendidos = @bv, prize_pool = @pp, valor_anuncios = @va, inscricoes = @inscr, nif_staff = @nif_staff WHERE nome = @nome;";
                        SqlCommand cmdInserirEquipa = new SqlCommand(queryInserirEquipa, CN);
                        cmdInserirEquipa.Parameters.AddWithValue("@nome", nome);
                        cmdInserirEquipa.Parameters.AddWithValue("@bv", bilhetes_vendidos);
                        cmdInserirEquipa.Parameters.AddWithValue("@pp", prize_pool);
                        cmdInserirEquipa.Parameters.AddWithValue("@va", valor_anuncios);
                        cmdInserirEquipa.Parameters.AddWithValue("@inscr", inscricoes);
                        cmdInserirEquipa.Parameters.AddWithValue("@nif_staff", nif_staff);
                        cmdInserirEquipa.ExecuteNonQuery();

                        MessageBox.Show("Torneio atualizado com sucesso!");

                        CN.Close();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao atualizar o Torneio: " + ex.Message);
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

                string queryEquipas2 = "SELECT nome FROM FGC.Torneio";
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
                MessageBox.Show("Ocorreu um erro ao consultar os torneios: " + ex.Message);
            }
            finally
            {
                CN.Close(); // Certifique-se de fechar a conexão no evento Load
            }
        }
    }
}
