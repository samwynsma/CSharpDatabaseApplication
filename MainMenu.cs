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
    private readonly TextBox checkItem;

    private readonly TextBox dbItemAdd;

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

        checkItem = new TextBox();
        checkItem.Location = new Point(150, 18);
        checkItem.Width = 200;
        this.Controls.Add(checkItem);

        Button checkItemBtn = new Button();
        checkItemBtn.Text = "Check Item";
        checkItemBtn.Location = new Point(360, 18);
        checkItemBtn.Width = 75;
        checkItemBtn.Click += CheckItemExists;
        this.Controls.Add(checkItemBtn);


        Label addLabel = new Label();
        addLabel.Text = "Add item to store";
        addLabel.Location = new Point(10, 40);
        addLabel.AutoSize = true;
        this.Controls.Add(addLabel);

        dbItemAdd = new TextBox();
        dbItemAdd.Location = new Point(150, 38);
        dbItemAdd.AutoSize = true;
        this.Controls.Add(dbItemAdd);

        Button addItemBtn = new Button();
        addItemBtn.Text = "Add Item";
        addItemBtn.Location = new Point(360, 38);
        addItemBtn.Width = 75;
        addItemBtn.Click += AddItemToDatabase;
        this.Controls.Add(addItemBtn);

        Label label2 = new Label
        {
            Name = "InformationLabel",
            Text = "",
            Location = new System.Drawing.Point(150, 200),
            AutoSize = true
        };
        this.Controls.Add(label2);

    }

    private bool DoesItemExist(string tableName, string columnName, string itemText)
    {
        string query = $"SELECT COUNT(*) FROM [{tableName}] WHERE [{columnName}] = ?";

        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();

            object result = cmd.ExecuteScalar();
            int count = 0;

            if (result != null && result != DBNull.Value)
            {
                count = Convert.ToInt32(result);
            }

            return count > 0;
        }
    }

    private int GetItemCount(String tableName, String itemText)
    {
        return 0;
    }

    private void AddDBRow(String tableName, String itemText)
    {
        
    }

    private void CheckItemExists(object sender, EventArgs e)
    {
        string tableName = "GroceryStore";
        string columnName = "Item";
        string itemText = checkItem.Text.Trim();

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if (string.IsNullOrWhiteSpace(itemText))
        {
            infoLabel.Text = "Please enter an item name.";
            infoLabel.BackColor = Color.Yellow;
            return;
        }

        try
        {
            bool exists = DoesItemExist(tableName, columnName, itemText);

            if (exists)
            {
                infoLabel.Text = $"'{itemText}' was found in the {tableName} table.";
                infoLabel.BackColor = Color.LightGreen;
            }
            else
            {
                infoLabel.Text = $"'{itemText}' was not found in the {tableName} table.";
                infoLabel.BackColor = Color.LightSalmon;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            infoLabel.Text = "An error occurred: " + ex.Message;
            infoLabel.BackColor = Color.Red;
        }
    }

    private void AddItemToDatabase(object sender, EventArgs e)
    {
        string tableName = "GroceryStore";
        string quantityCol = "Quantity";
        string itemCol = "Item";
        string itemText = dbItemAdd.Text.Trim();

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if (string.IsNullOrWhiteSpace(itemText))
        {
            infoLabel.Text = "Please enter an item name.";
            infoLabel.BackColor = Color.Yellow;
            return;
        }
        try
        {
            bool exists = DoesItemExist(tableName, itemCol, itemText);
            if(!exists)
            {
                AddDBRow(tableName, itemText);
            }
            else
            {
                int itemCount = GetItemCount(tableName, itemText);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            infoLabel.Text = "An error occurred: " + ex.Message;
            infoLabel.BackColor = Color.Red;
        }
    }

    [STAThread]
    public static void Main()
    {

        Application.EnableVisualStyles();
        Application.Run(new MainMenu());
    }
}