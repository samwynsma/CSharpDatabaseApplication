using System;
using System.Drawing;
using System.Windows.Forms;

public class SignInForm : Form
{
    private TextBox usernameText;
    private TextBox passwordText;
    public bool IsGuest { get; private set; } = false;
    public string Username => usernameText?.Text ?? string.Empty;

    public SignInForm()
    {
        this.Text = "Sign In";
        this.Size = new Size(320, 160);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ShowInTaskbar = false;

        Label userLabel = new Label { Text = "Username:", Location = new Point(10, 12), AutoSize = true };
        this.Controls.Add(userLabel);

        usernameText = new TextBox { Location = new Point(100, 10), Width = 190 };
        this.Controls.Add(usernameText);

        Label passLabel = new Label { Text = "Password:", Location = new Point(10, 42), AutoSize = true };
        this.Controls.Add(passLabel);

        passwordText = new TextBox { Location = new Point(100, 40), Width = 190, UseSystemPasswordChar = true };
        this.Controls.Add(passwordText);

        Button signInBtn = new Button { Text = "Sign In", Location = new Point(20, 80), Width = 80 };
        signInBtn.Click += SignInBtn_Click;
        this.Controls.Add(signInBtn);

        Button guestBtn = new Button { Text = "Guest", Location = new Point(110, 80), Width = 80 };
        guestBtn.Click += GuestBtn_Click;
        this.Controls.Add(guestBtn);

        Button cancelBtn = new Button { Text = "Cancel", Location = new Point(200, 80), Width = 80 };
        cancelBtn.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        this.Controls.Add(cancelBtn);
    }

    private void GuestBtn_Click(object sender, EventArgs e)
    {
        IsGuest = true;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void SignInBtn_Click(object sender, EventArgs e)
    {
        string user = usernameText.Text?.Trim();
        string pass = passwordText.Text ?? string.Empty;

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            MessageBox.Show("Please enter username and password.", "Sign In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            bool ok = DatabaseHelper.AuthenticateUser(user, pass);
            if (ok)
            {
                IsGuest = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Sign In Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error checking credentials: {ex.Message}", "Sign In Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
