using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

public class MainMenu : Form
{
    private static readonly string DBPath = @"GroceryStoreDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";
    public MainMenu()
    {
        this.Text = "Test form.";
        this.Size = new Size(400, 300);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label myLabel = new Label();
        myLabel.Text = "Enter your product here: ";
        myLabel.Location = new Point(10, 20);
        myLabel.AutoSize = true;
        this.Controls.Add(myLabel);

        TextBox itemToAdd = new TextBox();
        itemToAdd.Location = new Point(150, 18);
        itemToAdd.Width = 200;
        this.Controls.Add(itemToAdd);
    }

    [STAThread]
    public static void Main()
    {

        Application.EnableVisualStyles();
        Application.Run(new MainMenu());
    }
}