using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

class DepartmentsMenuActions
{
    private readonly UserInfo dbUser;
    private readonly TextBox dbDepartment;
    private readonly TextBox resultsBox;

    public DepartmentsMenuActions(TextBox dbDepartment, TextBox resultsBox, UserInfo dbUser)
    {
        this.dbDepartment = dbDepartment;
        this.resultsBox = resultsBox;
        this.dbUser = dbUser;
    }

    public void AddDepartment(object sender, EventArgs e)
    {
        resultsBox.Clear();
        string dptText = dbDepartment.Text.Trim();

        if (IsTextNull(dptText))
            return;

        if(DepartmentHelper.HasDepartment(dptText))
        {
            ChangeTextAndColor(resultsBox, "That department already exists", Color.OrangeRed);
        }
        else
        {
            DepartmentHelper.AddDepartment(dptText);
            ChangeTextAndColor(resultsBox, "Successfully added department", Color.Green);
        }
    }

    public void GetDatabaseItems(object sender, EventArgs e)
    {
        resultsBox.Clear();
        String dptText = dbDepartment.Text.Trim();
        if (IsTextNull(dptText))
            return;
        if(!DepartmentHelper.HasDepartment(dptText))
        {
            ChangeTextAndColor(resultsBox, "Department does not exist. Cannot get items from it.", Color.OrangeRed);
            return;
        }

        List<string> databaseItemInformation = DepartmentHelper.GetDepartmentItems(dptText);
        resultsBox.Clear();
        resultsBox.ForeColor = Color.Black;

        if (databaseItemInformation.Count == 0)
        {
            ChangeTextAndColor(resultsBox, "No items found for that department", Color.OrangeRed);
            return;
        }

        foreach (string item in databaseItemInformation)
        {
            resultsBox.AppendText(item + Environment.NewLine);
        }
    }

    public void GetDepartmentList(object sender, EventArgs e)
    {
        resultsBox.Clear();
        List<String> departments = DepartmentHelper.GetDepartments();
        String textToAdd = "List of departments: \n";
        for(int i = 0; i < departments.Count; i++)
        {
            textToAdd += departments[i];
            if(i + 1 < departments.Count)
                textToAdd += ", ";
        }
        resultsBox.AppendText(textToAdd + "\r\n");
    }

    public void GetDepartmentHead(object sender, EventArgs e)
    {
        String dptText = dbDepartment.Text.Trim();
        if (IsTextNull(dptText))
            return;
        if(!DepartmentHelper.HasDepartment(dptText))
        {
            ChangeTextAndColor(resultsBox, "Department does not exist. Cannot get items from it.", Color.OrangeRed);
            return;
        }

        String head = DepartmentHelper.GetHead(dptText);
        resultsBox.Clear();
        ChangeTextAndColor(resultsBox, head, Color.Black);
    }

    private bool IsTextNull(string text)
    {
        if (String.IsNullOrWhiteSpace(text))
        {
            ChangeTextAndColor(resultsBox, "Please enter a database name.", Color.Yellow);
            return true;
        }
        return false;
    }

    private void ChangeTextAndColor(TextBox results, string text, Color color)
    {
        results.Clear();
        results.AppendText(text);
        results.ForeColor = color;
    }
}