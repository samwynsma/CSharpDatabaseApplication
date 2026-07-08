using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class SignUpMenu : Form
{
    public SignUpMenu()
    {
        this.Text = $"Welcome, new user!";
        this.Size = new Size(300, 350);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label userNameLabel = new Label();
        userNameLabel.Text = "Username:";
        userNameLabel.Location = new Point(10, 20);
        userNameLabel.AutoSize = true;
        this.Controls.Add(userNameLabel);

        Button closePageBtn = new Button();
        closePageBtn.Text = "Close";
        closePageBtn.Location = new Point(120, 300);
        closePageBtn.Width = 100;
        closePageBtn.Click += new EventHandler(CloseWindow);
        this.Controls.Add(closePageBtn);
    }
    private void CloseWindow(object sender, EventArgs e)
    {
        this.Close();
    }
}