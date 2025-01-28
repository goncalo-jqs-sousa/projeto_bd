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
    public partial class visualizar_torneioUC : UserControl
    {
        private SqlConnection CN;
        public visualizar_torneioUC()
        {
            InitializeComponent();
        }

        private void Visualizar_Load(object sender, EventArgs e)
        {
            ExibirDadosTabela();
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

        private void ExibirDadosTabela()
        {
            dataGridView1.Rows.Clear();

            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    string query = "SELECT * FROM FGC.Torneio";
                    SqlCommand command = new SqlCommand(query, CN);
                    SqlDataReader reader = command.ExecuteReader();

                    // Adiciona as colunas ao DataGridView
                    dataGridView1.Columns.Add("nome", "Nome");
                    dataGridView1.Columns.Add("bilhetes_vendidos", "Bilhetes Vendidos");
                    dataGridView1.Columns.Add("prize_pool", "Valor de Prémio");
                    dataGridView1.Columns.Add("valor_anuncios", "Valor de Anúncios");
                    dataGridView1.Columns.Add("inscricoes", "Inscrições");
                    dataGridView1.Columns.Add("nif_staff", "NIF de Staff");


                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader["nome"], reader["bilhetes_vendidos"], reader["prize_pool"], reader["valor_anuncios"], reader["inscricoes"], reader["nif_staff"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao exibir os dados: " + ex.Message);
                }
                finally
                {
                    CN.Close();
                }
            }
        }
    }
}
