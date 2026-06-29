using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class MainMenuActions
{
    private readonly TextBox dbItem;
    private readonly TextBox dbQuantity;
    private readonly TextBox dbCost;
    private readonly TextBox dbDepartment;
    private readonly Label infoLabel;
    private readonly UserInfo dbUser;

    public MainMenuActions(TextBox dbItem, TextBox dbQuantity, TextBox dbCost, TextBox dbDepartment, Label infoLabel, UserInfo dbUser)
    {
        this.dbItem = dbItem;
        this.dbQuantity = dbQuantity;
        this.dbCost = dbCost;
        this.dbDepartment = dbDepartment;
        this.infoLabel = infoLabel;
        this.dbUser = dbUser;
    }

    public void CheckItemExists(object sender, EventArgs e)
    {
        const string tableName = "GroceryStore";
        const string columnName = "Item";
        string itemText = dbItem.Text.Trim();

        if (IsTextNull(itemText))
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

    public void AddItemToDatabase(object sender, EventArgs e)
    {
        const string tableName = "GroceryStore";
        const string itemCol = "Item";
        string itemText = dbItem.Text.Trim();

        if (IsTextNull(itemText) || !CheckAddDeletePriv(infoLabel))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            string costText = dbCost.Text.Trim();
            string departmentText = dbDepartment.Text.Trim();
            bool departmentExists = DepartmentHelper.HasDepartment(departmentText);

            if (String.IsNullOrWhiteSpace(departmentText))
            {
                ChangeTextAndColor(infoLabel, "Please enter a department.", Color.Yellow);
                return;
            }

            if(!departmentExists)
            {
                ChangeTextAndColor(infoLabel, "The department that you entered does not exist.", Color.Yellow);
                return;
            }

            if (!Decimal.TryParse(costText, out decimal costVal) || costVal < 0)
            {
                ChangeTextAndColor(infoLabel, "Please enter a valid non-negative cost.", Color.Orange);
                return;
            }

            if (!exists)
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

    public void StockItemInDatabase(object sender, EventArgs e)
    {
        const string tableName = "GroceryStore";
        const string itemCol = "Item";
        string itemText = dbItem.Text.Trim();

        if (IsTextNull(itemText))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            int quantityText = Convert.ToInt32(dbQuantity.Text.Trim());

            if (!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
            }
            else if (quantityText <= 0)
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

    public void SellItemInDatabase(object sender, EventArgs e)
    {
        const string tableName = "GroceryStore";
        const string itemCol = "Item";
        string itemText = dbItem.Text.Trim();

        if (IsTextNull(itemText))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            int quantityText = Convert.ToInt32(dbQuantity.Text.Trim());

            if (!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
            }
            else if (quantityText <= 0)
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

    public void RemoveItemFromDatabase(object sender, EventArgs e)
    {
        const string tableName = "GroceryStore";
        const string itemCol = "Item";
        string itemText = dbItem.Text.Trim();

        if (IsTextNull(itemText) || !CheckAddDeletePriv(infoLabel))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            if (!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                $"Are you sure you want to permanently delete '{itemText}' from the database?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                ChangeTextAndColor(infoLabel, itemText + " Item deletion canceled.", Color.LightGoldenrodYellow);
                return;
            }

            DatabaseHelper.DeleteDBRow(tableName, itemText);
            ChangeTextAndColor(infoLabel, itemText + " has been removed from the database.", Color.Green);
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    public void GetItemCount(object sender, EventArgs e)
    {
        const string tableName = "GroceryStore";
        const string itemCol = "Item";
        string itemText = dbItem.Text.Trim();

        if (IsTextNull(itemText))
            return;

        try
        {
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            if (!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
                return;
            }

            int val = DatabaseHelper.GetItemCount(tableName, itemText);
            ChangeTextAndColor(infoLabel, "There are " + val + " of the item " + itemText + " in the database.", Color.Green);
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    public void GetQuantityCost(object sender, EventArgs e)
    {
        const string tableName = "GroceryStore";
        const string itemCol = "Item";
        string itemText = dbItem.Text.Trim();

        if (IsTextNull(itemText))
            return;

        try
        {
            int items = Convert.ToInt32(dbQuantity.Text.Trim());
            bool exists = DatabaseHelper.DoesItemExist(tableName, itemCol, itemText);
            if (!exists)
            {
                ChangeTextAndColor(infoLabel, itemText + " is not present in the database", Color.Yellow);
                return;
            }

            double costTotal = DatabaseHelper.GetCostValue(tableName, itemText, items);
            ChangeTextAndColor(infoLabel, "The total cost of " + items + " " + itemText + " is " + costTotal.ToString("C2") + ".", Color.Green);
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }
    }

    public void GetPriveleges(object sender, EventArgs e)
    {
        List<string> privs = new List<string>();
        if(dbUser.IsAdmin)
        {
            privs.Add("Admin");
        }
        if(dbUser.HasAddedPrivs)
        {
            privs.Add("Additional");
        }
        if(dbUser.CanAddDelete)
        {
            privs.Add("Add/Remove Items");
        }
        if(dbUser.CanHireFire)
        {
            privs.Add("Fire/Hire");
        }
        if(privs.Count == 0)
        {
            ChangeTextAndColor(infoLabel, "This user has no privleges.", Color.Orange);
        }
        else
        {
            String privStr = "This user has the following privleges: ";
            for(int i = 0; i < privs.Count; i++)
            {
                privStr += privs[i];
                if(i + 1 < privs.Count)
                {
                    privStr += ", ";
                }
            }
            ChangeTextAndColor(infoLabel, privStr, Color.Green);
        }
    }

    public void OpenDepartments(object sender, EventArgs e)
    {
        if(!dbUser.IsAdmin)
        {
            ChangeTextAndColor(infoLabel, "You do not have permission to do that.", Color.OrangeRed);
            return;
        }
        new DepartmentsMenu(dbUser).ShowDialog();
    }

    public void OpenEmployees(object sender, EventArgs e)
    {
        return;
    }

    private void DisplayErrorMessage(string message)
    {
        Console.WriteLine("Unexpected error: " + message);
        ChangeTextAndColor(infoLabel, "An error occurred: " + message, Color.Red);
    }

    private bool IsTextNull(string text)
    {
        if (String.IsNullOrWhiteSpace(text))
        {
            ChangeTextAndColor(infoLabel, "Please enter an item name.", Color.Yellow);
            return true;
        }
        return false;
    }

    private bool CheckAddDeletePriv(Label label)
    {
        if(!dbUser.CanAddDelete)
        {
            ChangeTextAndColor(label, "User does not have the authority to add or remove items", Color.OrangeRed);
        }
        return dbUser.CanAddDelete;
    }

    private bool CheckAdmin(Label label)
    {
        if(!dbUser.IsAdmin)
        {
            ChangeTextAndColor(label, "Admin privelges are required to continue", Color.OrangeRed);
        }
        return dbUser.IsAdmin;
    }

    private void ChangeTextAndColor(Label label, string text, Color color)
    {
        label.Text = text;
        label.BackColor = color;
    }

    
}
