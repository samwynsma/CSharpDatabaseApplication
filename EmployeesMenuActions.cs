using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class EmployeesMenuActions
{
    private readonly TextBox dbEmployeeFirst;
    private readonly TextBox dbEmployeeLast;
    private readonly TextBox dbDepartment;
    private readonly TextBox resultsBox;
    private readonly UserInfo dbUser;

    public EmployeesMenuActions(
        TextBox dbEmployeeFirst,
        TextBox dbEmployeeLast,
        TextBox dbDepartment,
        TextBox resultsBox,
        UserInfo dbUser)
    {
        this.dbEmployeeFirst = dbEmployeeFirst;
        this.dbEmployeeLast = dbEmployeeLast;
        this.dbDepartment = dbDepartment;
        this.resultsBox = resultsBox;
        this.dbUser = dbUser;
    }

    public void GetAllEmployees(object sender, EventArgs e)
    {
        resultsBox.Clear();
        List<String> employees = EmployeeHelper.GetAllEmployees();
        if(employees.Count == 0)
        {
            ChangeTextAndColor(resultsBox, "There are no employees in the company", Color.OrangeRed);
            return;
        }
        resultsBox.AppendText("List of employees: " + Environment.NewLine);
        for(int i = 0; i < employees.Count; i++)
        {
            resultsBox.AppendText(employees.get(i) + Environment.NewLine);
        }
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