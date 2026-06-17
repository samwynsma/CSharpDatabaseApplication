using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

public class MainMenu : Form
{
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
        checkItemBtn.Width = 85;
        checkItemBtn.Click += CheckItemExists;
        this.Controls.Add(checkItemBtn);

        Button addItemBtn = new Button();
        addItemBtn.Text = "Add Item";
        addItemBtn.Location = new Point(105, 100);
        addItemBtn.Width = 85;
        addItemBtn.Click += AddItemToDatabase;
        this.Controls.Add(addItemBtn);

        Button stockItemBtn = new Button();
        stockItemBtn.Text = "Stock Item";
        stockItemBtn.Location = new Point(190, 100);
        stockItemBtn.Width = 85;
        stockItemBtn.Click += StockItemInDatabase;
        this.Controls.Add(stockItemBtn);

        Button sellItemBtn = new Button();
        sellItemBtn.Text = "Sell Item";
        sellItemBtn.Location = new Point(275, 100);
        sellItemBtn.Width = 85;
        sellItemBtn.Click += SellItemInDatabase;
        this.Controls.Add(sellItemBtn);

        Button removeItemBtn = new Button();
        removeItemBtn.Text = "Remove Item";
        removeItemBtn.Location = new Point(360, 100);
        removeItemBtn.Width = 85;
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


    private void CheckItemExists(object sender, EventArgs e)
    {
        string tableName = "GroceryStore";
        string columnName = "Item";
        string itemText = dbItem.Text.Trim();

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if(IsTextNull(itemText))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, columnName, itemText);

            if (exists)
            {
                ChangeTextAndColor(infoLabel, $"'{itemText}' was found in the {tableName} table.", Color.LightGreen);
            }
            else
            {
                ChangeTextAndColor(infoLabel, $"'{itemText}' was not found in the {tableName} table.", Color.LightSalmon);
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    private void AddItemToDatabase(object sender, EventArgs e)
    {
        string tableName = "GroceryStore";
        string itemCol = "Item";
        string itemText = dbItem.Text.Trim();

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if(IsTextNull(itemText))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            if(!exists)
            {
                DatabaseHelper.AddDBRow(tableName, itemText);
                Console.WriteLine("New item in database: " + itemText);
                ChangeTextAndColor(infoLabel, itemText + " has been added to the database.", Color.Green);
            }
            else
            {
                ChangeTextAndColor(infoLabel, itemText + " is already present in the database", Color.Yellow);
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    private void StockItemInDatabase(object sender, EventArgs e)
    {
        String tableName = "GroceryStore";
        String itemText = dbItem.Text.Trim();
        String itemCol = "Item";

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if(IsTextNull(itemText))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            int quantityText = Convert.ToInt32(dbQuantity.Text.Trim());
            if(!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
            }
            else if(quantityText <= 0)
            {
                ChangeTextAndColor(infoLabel, "Quantity was set to " + quantityText + ". Quantity must be greater than 0.", Color.Orange);
            }
            else
            {
                DatabaseHelper.IncreaseItemQuantity(tableName, itemText, quantityText);
                ChangeTextAndColor(infoLabel, quantityText + " of the item " + itemText + " have been added to the database", Color.Green);
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    private void SellItemInDatabase(object sender, EventArgs e)
    {
        String tableName = "GroceryStore";
        String itemText = dbItem.Text.Trim();
        String itemCol = "Item";

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if(IsTextNull(itemText))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            int quantityText = Convert.ToInt32(dbQuantity.Text.Trim());
            if(!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
            }
            else if(quantityText <= 0)
            {
                ChangeTextAndColor(infoLabel, "Quantity was set to " + quantityText + ". Quantity must be greater than 0.", Color.Orange);
            }
            else
            {
                DatabaseHelper.DecreaseItemQuantity(tableName, itemText, quantityText);
                ChangeTextAndColor(infoLabel, quantityText + " of the item " + itemText + " have been sold and thus have been removed from the database.", Color.Green);
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    private void RemoveItemFromDatabase(object sender, EventArgs e)
    {
        String tableName = "GroceryStore";
        String itemText = dbItem.Text.Trim();
        String itemCol = "Item";

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if(IsTextNull(itemText))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            if(!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
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
                    ChangeTextAndColor(infoLabel, itemText + "Item deletion canceled.", Color.LightGoldenrodYellow);
                    return;
                }

                DatabaseHelper.DeleteDBRow(tableName, itemText);
                ChangeTextAndColor(infoLabel, itemText + " has been removed from the database.", Color.Green);
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    private void DisplayErrorMessage(String message)
    {
        Label infoLabel = (Label)this.Controls["InformationLabel"];

        Console.WriteLine("Unexpected error: " + message);
        ChangeTextAndColor(infoLabel, "An error occurred: " + message, Color.Red);
    }

    public bool IsTextNull(String text)
    {
        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if(String.IsNullOrWhiteSpace(text))
        {
            ChangeTextAndColor(infoLabel, "Please enter an item name.", Color.Yellow);
            return true;
        }
        return false;
    }

    public void ChangeTextAndColor(Label label, String text, Color color)
    {
        label.Text = text;
        label.BackColor = color;
    }

    [STAThread]
    public static void Main()
    {

        Application.EnableVisualStyles();
        Application.Run(new MainMenu());
    }
}
