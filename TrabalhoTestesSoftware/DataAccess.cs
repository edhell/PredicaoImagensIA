using System.Data.SQLite;

namespace TrabalhoTestesSoftware
{
    public class DataAccess
    {
        //public static string connString = "Data Source=banco.db";
        //public SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source = banco.db");
        public static string dbPath = Path.Combine(Environment.CurrentDirectory, "banco.db");

        public static void InitializeDatabase()
        {
            //dbPath = Path.Combine(Environment.CurrentDirectory, "banco.db");
            SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=banco.db;");

            try
            {
                // Cria o arquivo caso não exista:
                if (!System.IO.File.Exists(dbPath))
                {
                    Console.WriteLine("Criar BD");
                    SQLiteConnection.CreateFile(dbPath);
                }

                // Cria a tabela caso não exista:
                using (var sqlite2 = new SQLiteConnection(@"Data Source=" + dbPath + ""))
                {
                    sqlite2.Open();
                    string sql = "CREATE TABLE IF NOT EXISTS Entradas (id INTEGER PRIMARY KEY AUTOINCREMENT, texto TEXT)";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite2);
                    command.ExecuteNonQuery();
                }

                // Abre o banco de dados:
                conn.Open();
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERRO ao inicializar banco de dados: " + ex.Message);
            }
        }

        public static long AddData(string inputText)
        {
            try
            {
                SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=banco.db;");

                conn.Open();

                using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    comm.CommandText = "INSERT INTO Entradas (texto) VALUES ('" + inputText + "'); ";
                    comm.ExecuteNonQueryAsync();
                    
                    comm.CommandText = "SELECT last_insert_rowid();";
                    return (long)comm.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO ao adicionar as informações: " + ex.Message);
                return 0;
            }

        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();
            try
            {
                SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=banco.db;");

                conn.Open();

                using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    comm.CommandText = "SELECT * FROM Entradas ORDER BY id DESC;";

                    using (var reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add("(" + reader["id"].ToString() + ") " + reader["texto"].ToString());
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO ao buscar as informações: " + ex.Message);
            }

            return entries;

        }

        public static void UpdateData(long id, string inputText)
        {
            try
            {
                SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=banco.db;");

                conn.Open();

                using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    comm.CommandText = "UPDATE Entradas SET texto = '" + inputText + "' WHERE id = " + id + ";";
                    comm.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO ao atualizar dados: " + ex.Message);
            }
        }

        public static void DellAllData()
        {
            try
            {
                SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=banco.db;");

                conn.Open();

                using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    comm.CommandText = "DELETE FROM Entradas";
                    comm.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO ao excluir dados: " + ex.Message);
            }
        }

        public static void DellData(long id)
        {
            try
            {
                SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=banco.db;");

                conn.Open();

                using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    comm.CommandText = "DELETE FROM Entradas WHERE id = " + id + ";";
                    comm.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO ao excluir dado: " + ex.Message);
            }
        }


    }
}
