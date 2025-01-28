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
    public partial class inserir_equipaUC : UserControl
    {
        private SqlConnection CN;
        public inserir_equipaUC()
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

        private void Inserir_Click(object sender, EventArgs e)
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {

                String nif = (String)textBox1.Text;
                String nome = (String)textBox2.Text;
                String num_jogadores = (String)textBox3.Text;
                String num_torneios_ganhos = (String)textBox4.Text;


                if (string.IsNullOrWhiteSpace(nif) || string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(num_jogadores))
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
                        string queryInserirEquipa = "INSERT INTO FGC.Equipa (nif, nome, num_jogadores, num_torneios_ganhos) " +
                                                    "VALUES (@nif, @nome, @nj, @ntg);";
                        SqlCommand cmdInserirEquipa = new SqlCommand(queryInserirEquipa, CN);
                        cmdInserirEquipa.Parameters.AddWithValue("@nif", nif);
                        cmdInserirEquipa.Parameters.AddWithValue("@nome", nome);
                        cmdInserirEquipa.Parameters.AddWithValue("@nj", num_jogadores);
                        cmdInserirEquipa.Parameters.AddWithValue("@ntg", num_torneios_ganhos);
                        cmdInserirEquipa.ExecuteNonQuery();

                        MessageBox.Show("Equipa inserida com sucesso!");

                        CN.Close();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao inserir a equipa: " + ex.Message);
                    }
                }
            }

            else if (!verifySGBDConnection())
            {
                MessageBox.Show("FAILED TO OPEN CONNECTION TO DATABASE", "Connection Test", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
