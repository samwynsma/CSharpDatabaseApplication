using System;
using System.Data.OleDb;

public static class UserDBHelper
{
    private static readonly string DBPath = @"TestDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";

    public static bool GetAdmin(String userName)
    {
        String query = $"SELECT Admin FROM [Users] WHERE [Username] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", userName);
            conn.Open();

            object result = cmd.ExecuteScalar();
            bool isAdmin = false;

            if (result != null && result != DBNull.Value)
            {
                if (string.Equals(result.ToString(), "true", StringComparison.OrdinalIgnoreCase))
                    isAdmin = true;
            }

            return isAdmin;
        }
    }

    public static bool GetAddDelete(string userName)
    {
        String query = $"SELECT AddDeletePriv FROM [Users] WHERE [Username] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", userName);
            conn.Open();

            object result = cmd.ExecuteScalar();
            bool isAddDel = false;

            if (result != null && result != DBNull.Value)
            {
                if (string.Equals(result.ToString(), "true", StringComparison.OrdinalIgnoreCase))
                    isAddDel = true;
            }

            return isAddDel;
        }
    }

    public static bool GetFireHire(string userName)
    {
        String query = $"SELECT FireHire FROM [Users] WHERE [Username] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", userName);
            conn.Open();

            object result = cmd.ExecuteScalar();
            bool isFireHire = false;

            if (result != null && result != DBNull.Value)
            {
                if (string.Equals(result.ToString(), "true", StringComparison.OrdinalIgnoreCase))
                    isFireHire = true;
            }

            return isFireHire;
        }
    }

    public static bool GetAdd(string userName)
    {
        String query = $"SELECT Additional FROM [Users] WHERE [Username] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", userName);
            conn.Open();

            object result = cmd.ExecuteScalar();
            bool isAddit = false;

            if (result != null && result != DBNull.Value)
            {
                if (string.Equals(result.ToString(), "true", StringComparison.OrdinalIgnoreCase))
                    isAddit = true;
            }

            return isAddit;
        }
    }

    public static bool GetUserPrivs(string userName)
    {
        String query = $"SELECT ChangeUser FROM [Users] WHERE [Username] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", userName);
            conn.Open();

            object result = cmd.ExecuteScalar();
            bool isUP = false;

            if (result != null && result != DBNull.Value)
            {
                if (string.Equals(result.ToString(), "true", StringComparison.OrdinalIgnoreCase))
                    isUP = true;
            }

            return isUP;
        }
    }
}