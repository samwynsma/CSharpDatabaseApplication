using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class SignUpMenu : Form
{
    private readonly TextBox userToAdd;
    private readonly TextBox passwordToAdd;
    private readonly TextBox passwordToVerify;
    private readonly UserActions actions;
    private readonly User newUser;
    public SignUpMenu()
    {
        this.Text = $"Welcome, new user!";
        this.Size = new Size(300, 300);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label userNameLabel = new Label();
        userNameLabel.Text = "Username:";
        userNameLabel.Location = new Point(10, 20);
        userNameLabel.AutoSize = true;
        this.Controls.Add(userNameLabel);

        userToAdd = new TextBox();
        userToAdd.Location = new Point(100, 18);
        userToAdd.Width = 100;
        this.Controls.Add(userToAdd);

        Label passwordLabel = new Label();
        passwordLabel.Text = "Password:";
        passwordLabel.Location = new Point(10, 40);
        passwordLabel.AutoSize = true;
        this.Controls.Add(passwordLabel);

        passwordToAdd = new TextBox();
        passwordToAdd.Location = new Point(100, 38);
        passwordToAdd.Width = 100;
        this.Controls.Add(passwordToAdd);

        Label verifyLabel = new Label();
        verifyLabel.Text = "Verify Password:";
        verifyLabel.Location = new Point(10, 40);
        verifyLabel.AutoSize = true;
        this.Controls.Add(passwordLabel);

        passwordToVerify = new TextBox();
        passwordToVerify.Location = new Point(100, 38);
        passwordToVerify.Width = 100;
        this.Controls.Add(passwordToVerify);

        Button addUserBtn = new Button();
        addUserBtn.Text = "Sign Up";
        addUserBtn.Location = new Point(100, 120);
        addUserBtn.Width = 100;
        addUserBtn.Click += new EventHandler(actions.AddUser);
        this.Controls.Add(addUserBtn);

        Button closePageBtn = new Button();
        closePageBtn.Text = "Close";
        closePageBtn.Location = new Point(100, 220);
        closePageBtn.Width = 100;
        closePageBtn.Click += new EventHandler(CloseWindow);
        this.Controls.Add(closePageBtn);
    }
    private void CloseWindow(object sender, EventArgs e)
    {
        this.Close();
    }
}