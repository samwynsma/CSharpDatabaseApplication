using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class EmployeesMenu : Form
{
    private readonly UserInfo dbUser;
    private readonly TextBox dbEmployeeFirst;
    private readonly TextBox dbEmployeeLast;
    private readonly TextBox resultsBox;
    private readonly EmployeesMenuActions actions;
    
    public EmployeesMenu(UserInfo dbUser)
    {
        this.dbUser = dbUser;
        this.Text = $"Employees Menu, signed in as {dbUser.Username}";
        this.Size = new Size(500, 550);
        this.StartPosition = FormStartPosition.CenterScreen;
    }

}