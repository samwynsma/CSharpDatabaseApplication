using System;
using System.Data.OleDb;

public static class DatabaseHelper
{
    private static readonly string DBPath = @"TestDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";

    public static bool DoesItemExist(string tableName, string columnName, string itemText)
    {
        string query = $"SELECT COUNT(*) FROM [{tableName}] WHERE [{columnName}] = ?";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", itemText);
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

    public static int GetItemCount(string tableName, string itemText)
    {
        string query = $"SELECT Quantity FROM [{tableName}] WHERE Item = ?";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();

            object result = cmd.ExecuteScalar();
            int val = 0;

            if (result != null && result != DBNull.Value)
            {
                val = Convert.ToInt32(result);
            }

            return val;
        }
    }

    public static void IncreaseItemQuantity(string tableName, string itemText, int quantityText)
    {
        string query = $"UPDATE [{tableName}] SET Quantity = Quantity + ? WHERE Item = ?";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@quantity", quantityText);
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void DecreaseItemQuantity(string tableName, string itemText, int quantityText)
    {
        int itemQuantity = GetItemCount(tableName, itemText);
        if (itemQuantity < quantityText)
        {
            throw new Exception("Cannot decrease item quantity by more than there are items");
        }

        string query = $"UPDATE [{tableName}] SET Quantity = Quantity - ? WHERE Item = ?";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@quantity", quantityText);
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void AddDBRow(string tableName, string itemText, decimal cost, string department)
    {
        string query = $"INSERT INTO [{tableName}] (Item, Quantity, Cost, Department) VALUES (?, 0, ?, ?)";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", itemText);
            cmd.Parameters.AddWithValue("@cost", cost);
            cmd.Parameters.AddWithValue("@department", department);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void DeleteDBRow(string tableName, string itemText)
    {
        string query = $"DELETE FROM [{tableName}] WHERE Item = ?";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static double GetCostValue(string tableName, string itemText, int itemCount)
    {
        string query = $"SELECT Cost * ? FROM [{tableName}] WHERE Item = ?";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();

            object result = cmd.ExecuteScalar();
            double val = 0;

            if (result != null && result != DBNull.Value)
            {
                val = Convert.ToDouble(result);
            }

            return val;
        }
    }
}
