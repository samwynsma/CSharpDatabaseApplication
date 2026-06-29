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

        Label userLabel = new Label();
        userLabel.Text = "Employee First Name:";
        userLabel.Location = new Point(10, 20);
        userLabel.AutoSize = true;
        this.Controls.Add(userLabel);

        dbEmployeeFirst = new TextBox();
        dbEmployeeFirst.Location = new Point(100, 18);
        dbEmployeeFirst.Width = 100;
        this.Controls.Add(dbEmployeeFirst);

        Label userLNLabel = new Label();
        userLNLabel.Text = "Employee Last Name:";
        userLNLabel.Location = new Point(10, 40);
        userLNLabel.AutoSize = true;
        this.Controls.Add(userLNLabel);

        dbEmployeeLast = new TextBox();
        dbEmployeeLast.Location = new Point(100, 38);
        dbEmployeeLast.Width = 100;
        this.Controls.Add(dbEmployeeLast);

        Button getAllEmpsBtn = new Button();
        getAllEmpsBtn.Text = "All Employees";
        getAllEmpsBtn.Location = new Point(20, 100);
        getAllEmpsBtn.Width = 85;
        getAllEmpsBtn.Click += new EventHandler(actions.GetAllEmployees);
        this.Controls.Add(getAllEmpsBtn);
    }

}