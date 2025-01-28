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
    public partial class visualizar_ppUC : UserControl
    {
        private SqlConnection CN;
        public visualizar_ppUC()
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

                    string query = "SELECT * FROM FGC.ProPlayer";
                    SqlCommand command = new SqlCommand(query, CN);
                    SqlDataReader reader = command.ExecuteReader();

                    // Adiciona as colunas ao DataGridView
                    dataGridView1.Columns.Add("nif", "NIF");
                    dataGridView1.Columns.Add("nome", "Nome");
                    dataGridView1.Columns.Add("prize_money", "Valor de Prémio");
                    dataGridView1.Columns.Add("games_played", "Número de Jogos Jogados");
                    dataGridView1.Columns.Add("nif_equipa", "nif da equipa");


                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader["nif"], reader["nome"], reader["prize_money"], reader["games_played"], reader["nif_equipa"]);
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
