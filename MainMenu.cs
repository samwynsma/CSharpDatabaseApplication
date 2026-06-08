using System;
using System.Drawing;
using System.Windows.Forms;

public class MainMenu : Form
{
    public MainMenu()
    {
        this.Text = "Test form.";
        this.Size = new Size(400, 200);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label myLabel = new Label();
        myLabel.Text = "Enter your product here.";
        myLabel.Location = new Point(10, 20);
        myLabel.AutoSize = true;
        this.Controls.Add(myLabel);
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainMenu());
    }
}