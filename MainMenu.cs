using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

public class MainMenu : Form
{
    private static readonly string DBPath = @"TestDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";
    public MainMenu()
    {
        this.Text = "Test form.";
        this.Size = new Size(500, 300);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label myLabel = new Label();
        myLabel.Text = "See if item is in store: ";
        myLabel.Location = new Point(10, 20);
        myLabel.AutoSize = true;
        this.Controls.Add(myLabel);

        TextBox itemToCheck = new TextBox();
        itemToCheck.Location = new Point(150, 18);
        itemToCheck.Width = 200;
        this.Controls.Add(itemToCheck);

        Button checkItemBtn = new Button();
        checkItemBtn.Text = "Check Item";
        checkItemBtn.Location = new Point(360, 18);
        checkItemBtn.Width = 75;
        checkItemBtn.Click += CheckItemExists;
        this.Controls.Add(checkItemBtn);

        Label label2 = new Label
        {
            Name = "InformationLabel",
            Text = "",
            Location = new System.Drawing.Point(150, 200),
            AutoSize = true
        };
        this.Controls.Add(label2);

    }

    private void CheckItemExists(object sender, EventArgs e)
    {
        MessageBox.Show("Button was clicked, but task has not been implemented yet.");
        this.Controls["InformationLabel"].Text = "Button has been clicked!";
        this.Controls["InformationLabel"].BackColor = Color.Green;
        String tableName = "GroceryStore";
        String columnName = "Item";

        try
        {
            using(OleDbConnection conn = new OleDbConnection(ConnString))
            {
                conn.Open();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            this.Controls["InformationLabel"].Text = "An Error has occured " + ex.Message;
            this.Controls["InformationLabel"].BackColor = Color.Red;
        }
    }

    [STAThread]
    public static void Main()
    {

        Application.EnableVisualStyles();
        Application.Run(new MainMenu());
    }
}