using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class UserActions
{
    private readonly TextBox username;
    private readonly TextBox password;
    private readonly TextBox verifyPassword;

    public UserActions(TextBox username, TextBox password, TextBox verifyPassword)
    {
        this.username = username;
        this.password = password;
        this.verifyPassword = verifyPassword;
    }

    public void AddUser(object sender, EventArgs e)
    {
        return;
    }
}