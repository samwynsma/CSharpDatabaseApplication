using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class AddNewUserMenu : Form
{
    private readonly TextBox userToAdd;
    private readonly TextBox passwordToAdd;
    private readonly TextBox passwordToVerify;
    private readonly UserActions actions;
    private readonly UserInfo dbUser;
    public AddNewUserMenu()
    {
        this.Text = $"Add New User Menu, signed in as {dbUser.Username}";
        this.Size = new Size(300, 300);
        this.StartPosition = FormStartPosition.CenterScreen;
    }
    private void CloseWindow(object sender, EventArgs e)
    {
        this.Close();
    }
}