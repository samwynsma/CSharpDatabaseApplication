using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class DepartmentsMenu : Form
{
    private readonly UserInfo dbUser;
    public DepartmentsMenu(UserInfo dbUser)
    {
        this.dbUser = dbUser;
        this.Text = $"Departments Menu. Signed in as {dbUser.Username}";
        this.Size = new Size(500, 500);
        this.StartPosition = FormStartPosition.CenterScreen;
        
    }
}