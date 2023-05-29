using Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Repositories
{
    public class Repo
    {
        ObservableCollection<Author> Authors = new ObservableCollection<Author>();

        SqlConnection conn;
        string cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

        public void GetData()
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlDataReader reader = null;
                string query = "SELECT * FROM Authors";

                using (var cmd = new SqlCommand(query, conn))
                {
                    reader = cmd.ExecuteReader();

                    Authors.Clear();

                    while (reader.Read())
                    {
                        var author = new Author
                        {
                            Id = int.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                        };
                        Authors.Add(author);
                    }
                }
            }
        }

        public Repo()
        {
            GetData();
        }


        public ObservableCollection<Author> Getall()
        {
            return Authors;
        }

        public void AddAuthor(int id, string firstName, string lastName)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                string query = $@" INSERT INTO Authors(Id,FirstName,LastName)
                                VALUES(@id,@firstName,@lastName)";

                using (var command = new SqlCommand(query, conn))
                {
                    var paramId = new SqlParameter();
                    paramId.ParameterName = "@id";
                    paramId.SqlDbType = SqlDbType.Int;
                    paramId.Value = id;

                    var paramName = new SqlParameter();
                    paramName.ParameterName = "@firstName";
                    paramName.SqlDbType = SqlDbType.NVarChar;
                    paramName.Value = firstName;

                    var paramSurname = new SqlParameter();
                    paramSurname.ParameterName = "@lastName";
                    paramSurname.SqlDbType = SqlDbType.NVarChar;
                    paramSurname.Value = lastName;

                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramName);
                    command.Parameters.Add(paramSurname);

                    var result = command.ExecuteNonQuery();
                }
                Author author = new Author
                {
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName
                };
                Authors.Add(author);
            }
        }

        public void DeleteAuthor(int id)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();
                using (var command = new SqlCommand())
                {

                    string query = "DELETE FROM Authors WHERE Id=@id";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlParameter paramid = new SqlParameter();
                    if (id != -1)
                    {
                        paramid.ParameterName = "@id";
                        paramid.SqlDbType = SqlDbType.Int;
                        paramid.Value = Authors[id].Id;

                        cmd.Parameters.Add(paramid);
                        var result = cmd.ExecuteNonQuery();
                        MessageBox.Show($"{result} Deleted successfully");
                    }
                }
            }
            GetData();
        }
    }
}

