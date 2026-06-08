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
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainMenu());
    }
}