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
        resultsBox.AppendText("GetDatabaseItems: query results would appear here.\r\n");
    }

    public void GetDepartmentList(object sender, EventArgs e)
    {
        List<String> departments = DepartmentHelper.GetDepartments();
        resultsBox.AppendText("GetDepartmentList: department list would appear here.\r\n");
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
        results.ForeColor = color;
        results.AppendText(text);
    }
}