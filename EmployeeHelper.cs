using System;
using System.Collections.Generic;
using System.Security;
using System.Data.OleDb;
using System.CodeDom;

public static class EmployeeHelper
{
    private static readonly string DBPath = @"TestDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";

    public static List<String> GetAllEmployees()
    {
        String query = "SELECT FirstName, LastName FROM [Employee]";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            conn.Open();

            List<string> empList = new List<string>();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        empList.Add(reader.GetString(0).Trim() + " " + reader.GetString(1).Trim());
                    }
                }
            }
            return empList;
        }
    }

    public static bool HasEmployee(String firstName, String lastName)
    {
        String query = "SELECT Count(*) FROM [Employee] WHERE [FirstName] = ? AND [LastName] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@first", firstName);
            cmd.Parameters.AddWithValue("@last", lastName);
            conn.Open();

            object result = cmd.ExecuteScalar();
            int count = 0;

            if (result != null && result != DBNull.Value)
            {
                count = Convert.ToInt32(result);
            }

            return count > 0;
        }
    }

    public static string GetEmployeeDetails(String firstName, String lastName)
    {
        String query = "SELECT * FROM [Employee] WHERE [FirstName] = ? AND [LastName] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@first", firstName);
            cmd.Parameters.AddWithValue("@last", lastName);
            conn.Open();

            object result = cmd.ExecuteScalar();
            String details = "";
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    String first = reader.GetString(1).Trim();
                    String last = reader.GetString(2).Trim();
                    String hireDate = reader.GetDateTime(3).ToString().Trim();
                    decimal hourlySalary = reader.GetDecimal(4);
                    String position = reader.GetString(5).Trim();
                    details = first + " " + last + " | " + "started at " + hireDate + " | " + "salary: " + hourlySalary.ToString("C") + " | " + "position: " + position;
                }
            }
            return details;
        }
    }

    public static List<string> GetEmployeeRoles()
    {
        String query = "SELECT DISTINCT Position FROM [Employee]";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            conn.Open();

            List<string> employeeRoles = new List<string>();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    String position = reader.GetString(0);
                    employeeRoles.Add(position);
                }
            }
            return employeeRoles;
        }
    }
}