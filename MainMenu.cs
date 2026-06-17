using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms.VisualStyles;

public class MainMenu : Form
{
    private static readonly string DBPath = @"TestDatabase.accdb";
    private static readonly string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DBPath};Persist Security Info=False;";
    private readonly TextBox dbItem;
    private readonly TextBox dbQuantity;

    public MainMenu()
    {
        this.Text = "Test form.";
        this.Size = new Size(500, 300);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label myLabel = new Label();
        myLabel.Text = "Item: ";
        myLabel.Location = new Point(10, 20);
        myLabel.AutoSize = true;
        this.Controls.Add(myLabel);  

        dbItem = new TextBox();
        dbItem.Location = new Point(100, 18);
        dbItem.Width = 100;
        this.Controls.Add(dbItem);


        Label addLabel = new Label();
        addLabel.Text = "Quantity: ";
        addLabel.Location = new Point(10, 40);
        addLabel.AutoSize = true;
        this.Controls.Add(addLabel);

        dbQuantity = new TextBox();
        dbQuantity.Location = new Point(100, 38);
        dbQuantity.AutoSize = true;
        this.Controls.Add(dbQuantity);

        Button checkItemBtn = new Button();
        checkItemBtn.Text = "Check Item";
        checkItemBtn.Location = new Point(20, 100);
        checkItemBtn.Width = 75;
        checkItemBtn.Click += CheckItemExists;
        this.Controls.Add(checkItemBtn);

        Button addItemBtn = new Button();
        addItemBtn.Text = "Add Item";
        addItemBtn.Location = new Point(100, 100);
        addItemBtn.Width = 75;
        addItemBtn.Click += AddItemToDatabase;
        this.Controls.Add(addItemBtn);

        Button stockItemBtn = new Button();
        stockItemBtn.Text = "Stock Item";
        stockItemBtn.Location = new Point(180, 100);
        stockItemBtn.Width = 75;
        stockItemBtn.Click += StockItemInDatabase;
        this.Controls.Add(stockItemBtn);

        Button sellItemBtn = new Button();
        sellItemBtn.Text = "Sell Item";
        sellItemBtn.Location = new Point(260, 100);
        sellItemBtn.Width = 75;
        sellItemBtn.Click += SellItemInDatabase;
        this.Controls.Add(sellItemBtn);

        Button removeItemBtn = new Button();
        removeItemBtn.Text = "Remove Item";
        removeItemBtn.Location = new Point(340, 100);
        removeItemBtn.Width = 75;
        removeItemBtn.Click += RemoveItemFromDatabase;
        this.Controls.Add(removeItemBtn);

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
        String query = $"SELECT Quantity FROM [{tableName}] WHERE Item = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();
            object result = cmd.ExecuteScalar();
            int val = 0;

            if (result != null && result != DBNull.Value)
            {
                val = Convert.ToInt32(result);
            }
            return val;
        }
    }

    private void IncreaseItemQuantity(String tableName, string itemText, int quantityText)
    {
        String query = $"UPDATE [{tableName}] SET Quantity = Quantity + ? WHERE Item = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@quantity", quantityText);
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private void DecreaseItemQuantity(String tableName, string itemText, int quantityText)
    {
        int itemQuantity = GetItemCount(tableName, itemText);
        if (itemQuantity < quantityText)
        {
            throw new Exception("Cannot decrease item quantity by more than there are items");
        }
        String query = $"UPDATE [{tableName}] SET Quantity = Quantity - ? WHERE Item = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@quantity", quantityText);
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private void AddDBRow(String tableName, String itemText)
    {
        String query = $"INSERT INTO [{tableName}] (Item, Quantity) VALUES (?, 0)";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private void DeleteDBRow(String tableName, String itemText)
    {
        String query = $"DELETE FROM [{tableName}] WHERE Item = ?";
        using (OleDbConnection conn = new OleDbConnection(ConnString))
        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@item", itemText);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private void CheckItemExists(object sender, EventArgs e)
    {
        string tableName = "GroceryStore";
        string columnName = "Item";
        string itemText = dbItem.Text.Trim();

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
        string itemCol = "Item";
        string itemText = dbItem.Text.Trim();

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if (string.IsNullOrWhiteSpace(itemText))
        {
            infoLabel.Text = "Please enter an item name.";
            infoLabel.BackColor = Color.Yellow;
            return;
        }

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
                Console.WriteLine("New item in database: " + itemText);
                infoLabel.Text = itemText + " has been added to the database.";
                infoLabel.BackColor = Color.Green;
            }
            else
            {
                infoLabel.Text = itemText + " is already present in the database";
                infoLabel.BackColor = Color.Yellow;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            infoLabel.Text = "An error occurred: " + ex.Message;
            infoLabel.BackColor = Color.Red;
        }
    }

    private void StockItemInDatabase(object sender, EventArgs e)
    {
        String tableName = "GroceryStore";
        String itemText = dbItem.Text.Trim();
        String itemCol = "Item";

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
            int quantityText = Convert.ToInt32(dbQuantity.Text.Trim());
            if(!exists)
            {
                infoLabel.Text = itemText + " is not present in the database";
                infoLabel.BackColor = Color.Yellow;
            }
            else if(quantityText <= 0)
            {
                infoLabel.Text = "Quantity was set to " + quantityText + ". Quantity must be greater than 0.";
                infoLabel.BackColor = Color.Orange;
            }
            else
            {
                IncreaseItemQuantity(tableName, itemText, quantityText);
                infoLabel.Text = quantityText + " of the item " + itemText + " have been added to the database";
                infoLabel.BackColor = Color.Green;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            infoLabel.Text = "An error occurred: " + ex.Message;
            infoLabel.BackColor = Color.Red;
        }
    }

    private void SellItemInDatabase(object sender, EventArgs e)
    {
        String tableName = "GroceryStore";
        String itemText = dbItem.Text.Trim();
        String itemCol = "Item";

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
            int quantityText = Convert.ToInt32(dbQuantity.Text.Trim());
            if(!exists)
            {
                infoLabel.Text = itemText + " is not present in the database";
                infoLabel.BackColor = Color.Yellow;
            }
            else if(quantityText <= 0)
            {
                infoLabel.Text = "Quantity was set to " + quantityText + ". Quantity must be greater than 0.";
                infoLabel.BackColor = Color.Orange;
            }
            else
            {
                DecreaseItemQuantity(tableName, itemText, quantityText);
                infoLabel.Text = quantityText + " of the item " + itemText + " have been sold and thus have been removed from the database.";
                infoLabel.BackColor = Color.Green;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            infoLabel.Text = "An error occurred: " + ex.Message;
            infoLabel.BackColor = Color.Red;
        }
    }

    private void RemoveItemFromDatabase(object sender, EventArgs e)
    {
        String tableName = "GroceryStore";
        String itemText = dbItem.Text.Trim();
        String itemCol = "Item";

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
                infoLabel.Text = itemText + " is not present in the database";
                infoLabel.BackColor = Color.Yellow;
            }
            else
            {
                DialogResult confirmation = MessageBox.Show(
                    $"Are you sure you want to permanently delete '{itemText}' from the database?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmation != DialogResult.Yes)
                {
                    infoLabel.Text = "Item deletion canceled.";
                    infoLabel.BackColor = Color.LightGoldenrodYellow;
                    return;
                }

                DeleteDBRow(tableName, itemText);
                infoLabel.Text = itemText + " has been removed from the database.";
                infoLabel.BackColor = Color.Green;
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