using System;
using System.Collections.Generic;
using System.Security;
using System.Data.OleDb;

public static class EmployeeHelper
{
    private static readonly string DBPath = @"TestDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";

    public static List<String> GetEmployees()
    {
        List<String> empList = new List<String>();
        return empList;
    }
}