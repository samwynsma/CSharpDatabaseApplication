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
    private readonly TextBox dbDepartment;
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
        dbEmployeeFirst.Location = new Point(150, 18);
        dbEmployeeFirst.Width = 100;
        this.Controls.Add(dbEmployeeFirst);

        Label userLNLabel = new Label();
        userLNLabel.Text = "Employee Last Name:";
        userLNLabel.Location = new Point(10, 40);
        userLNLabel.AutoSize = true;
        this.Controls.Add(userLNLabel);

        dbEmployeeLast = new TextBox();
        dbEmployeeLast.Location = new Point(150, 38);
        dbEmployeeLast.Width = 100;
        this.Controls.Add(dbEmployeeLast);

        Label departmentLabel = new Label();
        departmentLabel.Text = "Department:";
        departmentLabel.Location = new Point(10, 60);
        departmentLabel.AutoSize = true;
        this.Controls.Add(departmentLabel);

        dbDepartment = new TextBox();
        dbDepartment.Location = new Point(150, 58);
        dbDepartment.Width = 100;
        this.Controls.Add(dbDepartment);

        resultsBox = new TextBox();
        resultsBox.Location = new Point(10, 140);
        resultsBox.Size = new Size(460, 300);
        resultsBox.Multiline = true;
        resultsBox.ScrollBars = ScrollBars.Vertical;
        resultsBox.ReadOnly = true;
        // resultsBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Controls.Add(resultsBox);

        actions = new EmployeesMenuActions(dbEmployeeFirst, dbEmployeeLast, dbDepartment, resultsBox, dbUser);

        Button getAllEmpsBtn = new Button();
        getAllEmpsBtn.Text = "All Emps";
        getAllEmpsBtn.Location = new Point(20, 100);
        getAllEmpsBtn.Width = 85;
        getAllEmpsBtn.Click += new EventHandler(actions.GetAllEmployees);
        this.Controls.Add(getAllEmpsBtn);

        Button getEmpInfoBtn = new Button();
        getEmpInfoBtn.Text = "Employee Info";
        getEmpInfoBtn.Location = new Point(105, 100);
        getEmpInfoBtn.Width = 85;
        getEmpInfoBtn.Click += new EventHandler(actions.GetEmployeeInfo);
        this.Controls.Add(getEmpInfoBtn);

        Button closePageBtn = new Button();
        closePageBtn.Text = "Close";
        closePageBtn.Location = new Point(200, 460);
        closePageBtn.Width = 100;
        closePageBtn.Click += new EventHandler(CloseWindow);
        this.Controls.Add(closePageBtn);
    }

    private void CloseWindow(object sender, EventArgs e)
    {
        this.Close();
    }

}