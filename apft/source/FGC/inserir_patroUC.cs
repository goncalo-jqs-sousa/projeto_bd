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
    public partial class inserir_patroUC : UserControl
    {
        private SqlConnection CN;
        public inserir_patroUC()
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
                String telefone = (String)textBox2.Text;
                String nome = (String)textBox3.Text;
                String email = (String)textBox4.Text;


                if (string.IsNullOrWhiteSpace(nif) || string.IsNullOrWhiteSpace(telefone) || string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email))
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
                        string queryInserirPatrocinador = "INSERT INTO FGC.Patrocinador (nif, telefone, nome, email) " +
                                                    "VALUES (@nif, @telefone, @nome, @email);";
                        SqlCommand cmdInserirPatrocinador = new SqlCommand(queryInserirPatrocinador, CN);
                        cmdInserirPatrocinador.Parameters.AddWithValue("@nif", nif);
                        cmdInserirPatrocinador.Parameters.AddWithValue("@telefone", telefone);
                        cmdInserirPatrocinador.Parameters.AddWithValue("@nome", nome);
                        cmdInserirPatrocinador.Parameters.AddWithValue("@email", email);
                        cmdInserirPatrocinador.ExecuteNonQuery();

                        MessageBox.Show("Patrocinador inserido com sucesso!");

                        CN.Close();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao inserir o patrocinador: " + ex.Message);
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
