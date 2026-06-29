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

    public static List<string> GetDepartmentItems(string departmentText)
    {
        String query = "SELECT Item, Quantity, Cost FROM [GroceryStore] WHERE [Department] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@department", departmentText);
            conn.Open();

            List<string> items = new List<string>();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string itemName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0).Trim();
                    int quantity = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1));
                    decimal price = reader.IsDBNull(2) ? 0m : Convert.ToDecimal(reader.GetValue(2));
                    items.Add($"{itemName} | Qty: {quantity} | Cost: {price:C}");
                }
            }

            return items;
        }
    }

    internal static string GetHead(string dptText)
    {
        String query = "SELECT Head FROM [Department] AS D WHERE [D.Department] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@department", dptText);
            conn.Open();

            object result = cmd.ExecuteScalar();
            String dptHead = "";

            if (result != null && result != DBNull.Value)
            {
                dptHead = Convert.ToString(result);
            }
            return dptHead;
        }
    }
}