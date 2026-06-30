using System;
using System.Collections.Generic;
using System.Security;
using System.Data.OleDb;

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
}