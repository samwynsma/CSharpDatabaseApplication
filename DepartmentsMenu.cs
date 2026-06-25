using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class DepartmentsMenu : Form
{
    private readonly UserInfo dbUser;
    private readonly TextBox dbDepartment;
    private readonly DepartmentsMenuActions actions;
    public DepartmentsMenu(UserInfo dbUser)
    {
        this.dbUser = dbUser;
        this.Text = $"Departments Menu. Signed in as {dbUser.Username}";
        this.Size = new Size(500, 500);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label myLabel = new Label();
        myLabel.Text = "Department: ";
        myLabel.Location = new Point(10, 20);
        myLabel.AutoSize = true;
        this.Controls.Add(myLabel);

        dbDepartment = new TextBox();
        dbDepartment.Location = new Point(100, 18);
        dbDepartment.Width = 100;
        this.Controls.Add(dbDepartment);

        actions = new DepartmentsMenuActions(dbDepartment, dbUser);

        Button addDptButton = new Button();
        addDptButton.Text = "Add Department";
        addDptButton.Location = new Point(20, 100);
        addDptButton.Width = 85;
        addDptButton.Click += new EventHandler(actions.AddDepartment);
        this.Controls.Add(addDptButton);

        Button getItemDetailsBtn = new Button();
        getItemDetailsBtn.Text = "Department Items";
        getItemDetailsBtn.Location = new Point(105, 100);
        getItemDetailsBtn.Width = 85;
        getItemDetailsBtn.Click += new EventHandler(actions.GetDatabaseItems);
        this.Controls.Add(getItemDetailsBtn);
    }
}