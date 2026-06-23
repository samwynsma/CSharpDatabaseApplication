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
    }
}