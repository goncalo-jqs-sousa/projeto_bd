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
    public partial class visualizar_spUC : UserControl
    {
        private SqlConnection CN;
        public visualizar_spUC()
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

        public void InserirEquipa(string nif, string nome, int num_jogadores, int num_torneios_ganhos)
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    SqlCommand command = new SqlCommand("InserirEquipa", CN);
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir os parâmetros da stored procedure
                    command.Parameters.AddWithValue("@nif", nif);
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@num_jogadores", num_jogadores);
                    command.Parameters.AddWithValue("@num_torneios_ganhos", num_torneios_ganhos);

                    command.ExecuteNonQuery(); 
                    MessageBox.Show("Equipa inserida com sucesso");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro: " + ex.Message);
                }
                finally
                {
                    CN.Close();
                }
            }
            else if (!verifySGBDConnection())
            {
                MessageBox.Show("FAILED TO OPEN CONNECTION TO DATABASE", "Connection Test", MessageBoxButtons.OK);
                return;
            }
        }

        public void GetGameDevelopersByPlayer(string playerNIF)
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    using (SqlCommand command = new SqlCommand("GetGameDevelopersByPlayer", CN))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Definir o parâmetro da stored procedure
                        command.Parameters.AddWithValue("@playerNIF", playerNIF);

                        // Executar a stored procedure
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nomeEquipaDev = reader["NomeEquipaDev"].ToString();

                                MessageBox.Show($"Equipa de desenvolvedores do jogador NIF '{playerNIF}': {nomeEquipaDev}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao executar a stored procedure GetGameDevelopersByPlayer: " + ex.Message);
                }
                finally
                {
                    CN.Close();
                }
            }
            else if (!verifySGBDConnection())
            {
                MessageBox.Show("FAILED TO OPEN CONNECTION TO DATABASE", "Connection Test", MessageBoxButtons.OK);
                return;
            }
            }

        public void GetSponsors(string entityType, string entityID)
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    using (SqlCommand command = new SqlCommand("GetSponsors", CN))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Definir os parâmetros da stored procedure
                        command.Parameters.AddWithValue("@entityType", entityType);
                        command.Parameters.AddWithValue("@entityID", entityID);

                        // Executar a stored procedure
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nomePatrocinador = reader["NomePatrocinador"].ToString();

                                MessageBox.Show($"Patrocinador do tipo '{entityType}' com ID '{entityID}': {nomePatrocinador}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao executar a stored procedure GetSponsors: " + ex.Message);
                }
                finally
                {
                    CN.Close();
                }
            }
            else if (!verifySGBDConnection())
            {
                MessageBox.Show("FAILED TO OPEN CONNECTION TO DATABASE", "Connection Test", MessageBoxButtons.OK);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String nif = (String)textBox9.Text;
            String nome = (String)textBox8.Text;
            String aux1 = (String)textBox7.Text;
            int nj = int.Parse(aux1);
            String aux2 = (String)textBox6.Text;
            int tg = int.Parse(aux2);
            InserirEquipa(nif, nome, nj, tg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String nif = (String)textBox3.Text;
            GetGameDevelopersByPlayer(nif);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String entidade = (String)textBox4.Text;
            String nif = (String)textBox5.Text;
            GetSponsors(entidade, nif);
        }
    }
}
