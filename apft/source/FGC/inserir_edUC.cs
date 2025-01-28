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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FGC
{
    public partial class inserir_edUC : UserControl
    {
        private SqlConnection CN;
        public inserir_edUC()
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
                String num_desenvolvedores = (String)textBox3.Text;


                if (string.IsNullOrWhiteSpace(nif) || string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(num_desenvolvedores))
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
                        string queryInserirEquipa = "INSERT INTO FGC.EquipaDesenvolvedores (nif, nome, num_desenvolvedores) " +
                                                    "VALUES (@nif, @nome, @nd);";
                        SqlCommand cmdInserirEquipa = new SqlCommand(queryInserirEquipa, CN);
                        cmdInserirEquipa.Parameters.AddWithValue("@nif", nif);
                        cmdInserirEquipa.Parameters.AddWithValue("@nome", nome);
                        cmdInserirEquipa.Parameters.AddWithValue("@nd", num_desenvolvedores);
                        cmdInserirEquipa.ExecuteNonQuery();

                        MessageBox.Show("Equipa de desevolvedores inserida com sucesso!");

                        CN.Close();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao inserir a equipa de desevolvedores: " + ex.Message);
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
