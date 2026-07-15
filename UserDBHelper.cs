using System;
using System.Collections.Generic;
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

    public static void AddUserToSystem(string userName, string password)
    {
        String query = "INSERT INTO [Users] (Username, Password, Admin, AddDeletePriv, FireHire, Additional, ChangeUser) VALUES (?, ?, 0, 0, 0, 0, 0)";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Username", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static bool CheckUserExists(string userName)
    {
        string query = $"SELECT COUNT(*) FROM [Users] WHERE [Username] = ?";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", userName);
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

    public static List<string> GetAllUsers(string disallowed)
    {
        string query = $"SELECT [Username] FROM [Users] WHERE [Username] <> ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", disallowed);
            conn.Open();

            List<string> users = new List<string>();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(reader.GetString(0));
                }
            }
            return users;
        }
    }

    public static List<bool> GetCurrentActivePrivs(string user)
    {
        string query = $"SELECT * FROM [Users] WHERE [Username] = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", user);
            conn.Open();

            List<bool> privs = new List<bool>();
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    privs.Add(reader.GetBoolean(3));
                    privs.Add(reader.GetBoolean(4));
                    privs.Add(reader.GetBoolean(5));
                    privs.Add(reader.GetBoolean(6));
                    privs.Add(reader.GetBoolean(7));
                }
            }
            return privs;
        }
    }

    public static void AddUserToDatabase(string user, string password)
    {
        String query = $"INSERT INTO [Users] (Username, Password), VALUES ([{user}], [{password}])";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}