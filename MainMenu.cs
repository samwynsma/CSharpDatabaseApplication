using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

public class MainMenu : Form
{
    private readonly TextBox dbItem;
    private readonly TextBox dbQuantity;
    private readonly TextBox dbCost;
    private readonly TextBox dbDepartment;

    public MainMenu()
    {
        this.Text = "Test form.";
        this.Size = new Size(500, 350);
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

        Label costLabel = new Label();
        costLabel.Text = "Cost: ";
        costLabel.Location = new Point(10, 60);
        costLabel.AutoSize = true;
        this.Controls.Add(costLabel);

        dbCost = new TextBox();
        dbCost.Location = new Point(100, 58);
        dbCost.Width = 100;
        dbCost.KeyPress += new KeyPressEventHandler(DbCost_KeyPress);
        dbCost.TextChanged += new EventHandler(DbCost_TextChanged);
        this.Controls.Add(dbCost);

        Label deptLabel = new Label();
        deptLabel.Text = "Department: ";
        deptLabel.Location = new Point(10, 80);
        deptLabel.AutoSize = true;
        this.Controls.Add(deptLabel);

        dbDepartment = new TextBox();
        dbDepartment.Location = new Point(100, 78);
        dbDepartment.Width = 100;
        this.Controls.Add(dbDepartment);

        Button checkItemBtn = new Button();
        checkItemBtn.Text = "Check Item";
        checkItemBtn.Location = new Point(20, 100);
        checkItemBtn.Width = 85;
        checkItemBtn.Click += new EventHandler(CheckItemExists);
        this.Controls.Add(checkItemBtn);

        Button addItemBtn = new Button();
        addItemBtn.Text = "Add Item";
        addItemBtn.Location = new Point(105, 100);
        addItemBtn.Width = 85;
        addItemBtn.Click += new EventHandler(AddItemToDatabase);
        this.Controls.Add(addItemBtn);

        Button stockItemBtn = new Button();
        stockItemBtn.Text = "Stock Item";
        stockItemBtn.Location = new Point(190, 100);
        stockItemBtn.Width = 85;
        stockItemBtn.Click += new EventHandler(StockItemInDatabase);
        this.Controls.Add(stockItemBtn);

        Button sellItemBtn = new Button();
        sellItemBtn.Text = "Sell Item";
        sellItemBtn.Location = new Point(275, 100);
        sellItemBtn.Width = 85;
        sellItemBtn.Click += new EventHandler(SellItemInDatabase);
        this.Controls.Add(sellItemBtn);

        Button removeItemBtn = new Button();
        removeItemBtn.Text = "Remove Item";
        removeItemBtn.Location = new Point(360, 100);
        removeItemBtn.Width = 85;
        removeItemBtn.Click += new EventHandler(RemoveItemFromDatabase);
        this.Controls.Add(removeItemBtn);

        Button itemCountBtn = new Button();
        itemCountBtn.Text = "Item Count";
        itemCountBtn.Location = new Point(20, 125);
        itemCountBtn.Width = 85;
        itemCountBtn.Click += new EventHandler(GetItemCount);
        this.Controls.Add(itemCountBtn);

        Button quantityCostBtn = new Button();
        quantityCostBtn.Text = "Cost to Buy";
        quantityCostBtn.Location = new Point(105, 125);
        quantityCostBtn.Width = 85;
        quantityCostBtn.Click = new EventHandler(GetQuantityCost);
        this.ControlsAdd(quantityCostBtn);

        Button closeProgramBtn = new Button();
        closeProgramBtn.Text = "Quit";
        closeProgramBtn.Location = new Point(200, 250);
        closeProgramBtn.Width = 85;
        closeProgramBtn.Click += new EventHandler(CloseProgram);
        this.Controls.Add(closeProgramBtn);

        


        Label label2 = new Label
        {
            Name = "InformationLabel",
            Text = "",
            Location = new System.Drawing.Point(150, 200),
            AutoSize = true
        };
        this.Controls.Add(label2);
    }

    private void CloseProgram(object sender, EventArgs e)
    {
        this.Close();
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

            string costText = dbCost.Text.Trim();
            string departmentText = dbDepartment.Text.Trim();

            if (String.IsNullOrWhiteSpace(departmentText))
            {
                ChangeTextAndColor(infoLabel, "Please enter a department.", Color.Yellow);
                return;
            }

            if (!Decimal.TryParse(costText, out decimal costVal) || costVal < 0)
            {
                ChangeTextAndColor(infoLabel, "Please enter a valid non-negative cost.", Color.Orange);
                return;
            }

            if(!exists)
            {
                DatabaseHelper.AddDBRow(tableName, itemText, costVal, departmentText);
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

    private void GetItemCount(object sender, EventArgs e)
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
                int val = DatabaseHelper.GetItemCount(tableName, itemText);
                ChangeTextAndColor(infoLabel, "There are " + val + " of the item " + itemText + " in the database.", Color.Green);
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    private void GetQuantityCost(object sender, EventArgs e)
    {
        string tableName = "GroceryStore";
        string itemText = dbItem.Text.Trim();
        string itemCol = "Item";

        Label infoLabel = (Label)this.Controls["InformationLabel"];

        if(IsTextNull(itemText))
            return;

        try
        {
            int items = Convert.ToInt32(dbQuantity.Text.Trim());

            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            if(!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
            }
            else
            {
                double costTotal = DatabaseHelper.GetCostValue(tableName, itemCol, itemText);
                ChangeTextAndColor(infoLabel, "The total cost of " + items + " " + itemText + " is " + costTotal + ".", Color.Green);
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

    

    private void DbCost_KeyPress(object sender, KeyPressEventArgs e)
    {
        char ch = e.KeyChar;

        // Allow control characters (backspace), digits, and one decimal point
        if (!char.IsControl(ch) && !char.IsDigit(ch) && ch != '.')
        {
            e.Handled = true;
            return;
        }

        TextBox tb = sender as TextBox;
        if (ch == '.' && tb != null && tb.Text.Contains('.'))
        {
            // only allow one decimal point
            e.Handled = true;
        }
    }

    private void DbCost_TextChanged(object sender, EventArgs e)
    {
        TextBox tb = sender as TextBox;
        if (tb == null)
            return;

        string text = tb.Text;
        int idx = text.IndexOf('.');
        if (idx >= 0)
        {
            int decimals = text.Length - idx - 1;
            if (decimals > 2)
            {
                int sel = tb.SelectionStart;
                tb.Text = text.Substring(0, idx + 3);
                // restore caret position as best effort
                tb.SelectionStart = Math.Min(tb.Text.Length, Math.Max(0, sel - (decimals - 2)));
            }
        }
    }

    [STAThread]
    public static void Main()
    {

        Application.EnableVisualStyles();
        Application.Run(new MainMenu());
    }
}
