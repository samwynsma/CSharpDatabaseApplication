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
        return;
    }
}