using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LogInSystem.DB
{
    static public class Queries
    {
        static private DataTable DataTable;
        static readonly private string ConnectionString = ConfigurationManager.ConnectionStrings["DB-connection"].ConnectionString;

        public static DataTable SELECT(string query, string[] key, dynamic[] values)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable = new DataTable();
                        for (int i = 0; i < key.Length; i++)
                        {
                            if (string.IsNullOrEmpty(values[i]))
                                adapter.SelectCommand.Parameters.AddWithValue("@" + key[i], DBNull.Value);
                            else
                                adapter.SelectCommand.Parameters.AddWithValue("@" + key[i], values[i]);
                        }
                        adapter.Fill(DataTable);
                    }
                }
                catch
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            return DataTable;
        }

        public static bool INSERT_UPDATE_DELETE(string query, string[] key, dynamic[] values)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        for (int i = 0; i < key.Length; i++)
                        {
                            if(string.IsNullOrEmpty(values[i]))
                                command.Parameters.AddWithValue("@" + key[i], DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@" + key[i], values[i]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch(Exception e)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return true;
        }

        public static bool ExistParameters(string query, string[] key, dynamic[] values)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        for (int i = 0; i < key.Length; i++)
                        {
                            if (string.IsNullOrEmpty(values[i]))
                                command.Parameters.AddWithValue("@" + key[i], DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@" + key[i], values[i]);
                        }
                        SqlDataReader dr = command.ExecuteReader();
                        if (dr.HasRows == true)
                        {
                            return true;
                        }
                        else
                        {
                            connection.Close();
                            return false;
                        }
                    }

                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool Exist(string query, string key, dynamic value)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@" + key, value);
                        SqlDataReader dr = command.ExecuteReader();

                        if (dr.HasRows == true)
                        {
                            return true;
                        }
                        else
                        {
                            connection.Close();
                            return false;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}