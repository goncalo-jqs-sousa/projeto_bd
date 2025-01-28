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
    public partial class visualizar_equipaUC : UserControl
    {
        private SqlConnection CN;
        public visualizar_equipaUC()
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

                    string query = "SELECT * FROM FGC.Equipa";
                    SqlCommand command = new SqlCommand(query, CN);
                    SqlDataReader reader = command.ExecuteReader();

                    // Adiciona as colunas ao DataGridView
                    dataGridView1.Columns.Add("nif", "NIF");
                    dataGridView1.Columns.Add("nome", "Nome");
                    dataGridView1.Columns.Add("num_jogadores", "Número de Jogadores");
                    dataGridView1.Columns.Add("num_torneios_ganhos", "Número de Torneios Ganhos");


                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader["nif"], reader["nome"], reader["num_jogadores"], reader["num_torneios_ganhos"]);
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
