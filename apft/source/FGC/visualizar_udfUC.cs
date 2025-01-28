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
    public partial class visualizar_udfUC : UserControl
    {
        private SqlConnection CN;
        public visualizar_udfUC()
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

        public decimal CalculateAveragePrizeMoney()
        {
            decimal averagePrizeMoney = 0;
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    string query = "SELECT dbo.CalculateAveragePrizeMoney() AS AveragePrizeMoney";
                    using (SqlCommand command = new SqlCommand(query, CN))
                    {
                        // Executar a consulta SQL
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                averagePrizeMoney = Convert.ToDecimal(reader["AveragePrizeMoney"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao executar a User-Defined Function CalculateAveragePrizeMoney: " + ex.Message);
                }
                finally
                {
                    CN.Close();
                }
                MessageBox.Show(averagePrizeMoney.ToString());
                return averagePrizeMoney;
            }
            else
            {
                MessageBox.Show("FAILED TO OPEN CONNECTION TO DATABASE", "Connection Test", MessageBoxButtons.OK);
                return -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CalculateAveragePrizeMoney();
        }
    }
}
