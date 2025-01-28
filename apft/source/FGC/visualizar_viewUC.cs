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
    public partial class visualizar_viewUC : UserControl
    {
        private SqlConnection CN;
        public visualizar_viewUC()
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

        public void ConsultarViewProPlayerEquipa()
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    string query = "SELECT * FROM vw_ProPlayerEquipa";

                    using (SqlCommand command = new SqlCommand(query, CN))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nif = reader["nif"].ToString();
                                string nomeJogador = reader["nome"].ToString();
                                string prizeMoney = reader["prize_money"].ToString();
                                string gamesPlayed = reader["games_played"].ToString();
                                string nomeEquipa = reader["nif_equipa"].ToString();

                                MessageBox.Show($"NIF: {nif}, Jogador: {nomeJogador}, Prize Money: {prizeMoney}, Games Played: {gamesPlayed}, Equipa: {nomeEquipa}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao consultar a view vw_ProPlayerEquipa: " + ex.Message);
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

        public void ConsultarViewProPlayerCharsPlayed()
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    string query = "SELECT * FROM vw_ProPlayerCharsPlayed";

                    using (SqlCommand command = new SqlCommand(query, CN))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nif = reader["nif"].ToString();
                                string nomeJogador = reader["nome_jogador"].ToString();
                                string charsPlayed = reader["chars_played"].ToString();

                                MessageBox.Show($"NIF: {nif}, Jogador: {nomeJogador}, Chars Played: {charsPlayed}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao consultar a view vw_ProPlayerCharsPlayed: " + ex.Message);
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

        public void ConsultarViewEquipaPatrocinador()
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    string query = "SELECT * FROM vw_EquipaPatrocinador";

                    using (SqlCommand command = new SqlCommand(query, CN))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nomeEquipa = reader["nome_equipa"].ToString();
                                string nomePatrocinador = reader["nome_patrocinador"].ToString();
                                string valorPatrocinado = reader["valor_patrocinado"].ToString();

                                MessageBox.Show($"Equipa: {nomeEquipa}, Patrocinador: {nomePatrocinador}, Valor Patrocinado: {valorPatrocinado}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao consultar a view vw_EquipaPatrocinador: " + ex.Message);
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

        public void ConsultarViewTorneioJogadores()
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    string query = "SELECT * FROM vw_TorneioJogadores";

                    using (SqlCommand command = new SqlCommand(query, CN))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nomeTorneio = reader["nome_torneio"].ToString();
                                string nomeJogador = reader["nome_jogador"].ToString();

                                MessageBox.Show($"Torneio: {nomeTorneio}, Jogador: {nomeJogador}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao consultar a view vw_TorneioJogadores: " + ex.Message);
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

        public void ConsultarViewTorneioEquipaDesenvolvedores()
        {
            bool temp = verifySGBDConnection();
            CN.Close();
            if (temp)
            {
                try
                {
                    CN.Open();

                    string query = "SELECT * FROM vw_TorneioEquipaDesenvolvedores";

                    using (SqlCommand command = new SqlCommand(query, CN))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nomeTorneio = reader["nome_torneio"].ToString();
                                string nomeEquipaDesenvolvedores = reader["nome_equipa_desenvolvedores"].ToString();

                                MessageBox.Show($"Torneio: {nomeTorneio}, Equipa Desenvolvedores: {nomeEquipaDesenvolvedores}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao consultar a view vw_TorneioEquipaDesenvolvedores: " + ex.Message);
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

        private void button5_Click(object sender, EventArgs e)
        {
            ConsultarViewTorneioEquipaDesenvolvedores();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarViewProPlayerEquipa();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsultarViewProPlayerCharsPlayed();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConsultarViewEquipaPatrocinador();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConsultarViewTorneioJogadores();
        }
    }
}
