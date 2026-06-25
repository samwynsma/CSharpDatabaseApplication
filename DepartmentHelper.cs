using System;
using System.Collections.Generic;
using System.Security;
using System.Data.OleDb;

public static class DepartmentHelper
{
    private static readonly string DBPath = @"TestDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";

    public static List<string> GetDepartments()
    {
        String query = "SELECT Department FROM [Department]";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            conn.Open();

            List<string> dptList = new List<string>();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        dptList.Add(reader.GetString(0).Trim());
                    }
                }
            }
            return dptList;
        }
    }

    public static bool HasDepartment(string departmentText)
    {
        String query = "SELECT Count(*) FROM [Department] AS D WHERE [D.Department] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", departmentText);
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

    public static void AddDepartment(string departmentText)
    {
        String query = "INSERT INTO [Department] (Department, Head) VALUES (?, null)";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@department", departmentText);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}