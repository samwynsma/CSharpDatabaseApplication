using System;
using System.Data.OleDb;

public class UserDBHelper
{
    private static readonly string DBPath = @"TestDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";

    public static bool GetAdmin(String userName)
    {
        String query = $"SELECT Admin FROM [Users] WHERE Username = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", userName);
            conn.Open();

            object result = cmd.ExecuteScalar();
            bool isAdmin = false;

            if (result != null && result != DBNull.Value)
            {
                if (string.Equals(result.ToString(), "YES", StringComparison.OrdinalIgnoreCase))
                    isAdmin = true;
            }

            return isAdmin;
        }
    }

    public static bool GetAdd(string username)
    {
        return false;
    }

    public static bool GetAddDelete(string username)
    {
        return false;
    }

    public static bool GetFireHire(string username)
    {
        return false;
    }
}