using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class MainMenu : Form
{
    private readonly TextBox dbItem;
    private readonly TextBox dbQuantity;
    private readonly TextBox dbCost;
    private readonly TextBox dbDepartment;
    private readonly MainMenuActions actions;
    private readonly UserInfo dbUser;

    public MainMenu(UserInfo dbUser)
    {
        this.dbUser = dbUser;
        this.Text = $"Signed in as {dbUser.Username}";
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

        Label label2 = new Label
        {
            Name = "InformationLabel",
            Text = "",
            Location = new System.Drawing.Point(100, 200),
            AutoSize = true
        };
        this.Controls.Add(label2);

        actions = new MainMenuActions(dbItem, dbQuantity, dbCost, dbDepartment, label2, dbUser);

        Button checkItemBtn = new Button();
        checkItemBtn.Text = "Check Item";
        checkItemBtn.Location = new Point(20, 100);
        checkItemBtn.Width = 85;
        checkItemBtn.Click += new EventHandler(actions.CheckItemExists);
        this.Controls.Add(checkItemBtn);

        Button addItemBtn = new Button();
        addItemBtn.Text = "Add Item";
        addItemBtn.Location = new Point(105, 100);
        addItemBtn.Width = 85;
        addItemBtn.Click += new EventHandler(actions.AddItemToDatabase);
        this.Controls.Add(addItemBtn);

        Button stockItemBtn = new Button();
        stockItemBtn.Text = "Stock Item";
        stockItemBtn.Location = new Point(190, 100);
        stockItemBtn.Width = 85;
        stockItemBtn.Click += new EventHandler(actions.StockItemInDatabase);
        this.Controls.Add(stockItemBtn);

        Button sellItemBtn = new Button();
        sellItemBtn.Text = "Sell Item";
        sellItemBtn.Location = new Point(275, 100);
        sellItemBtn.Width = 85;
        sellItemBtn.Click += new EventHandler(actions.SellItemInDatabase);
        this.Controls.Add(sellItemBtn);

        Button removeItemBtn = new Button();
        removeItemBtn.Text = "Remove Item";
        removeItemBtn.Location = new Point(360, 100);
        removeItemBtn.Width = 85;
        removeItemBtn.Click += new EventHandler(actions.RemoveItemFromDatabase);
        this.Controls.Add(removeItemBtn);

        Button itemCountBtn = new Button();
        itemCountBtn.Text = "Item Count";
        itemCountBtn.Location = new Point(20, 125);
        itemCountBtn.Width = 85;
        itemCountBtn.Click += new EventHandler(actions.GetItemCount);
        this.Controls.Add(itemCountBtn);

        Button quantityCostBtn = new Button();
        quantityCostBtn.Text = "Cost to Buy";
        quantityCostBtn.Location = new Point(105, 125);
        quantityCostBtn.Width = 85;
        quantityCostBtn.Click += new EventHandler(actions.GetQuantityCost);
        this.Controls.Add(quantityCostBtn);

        Button ShowPrivelegesBtn = new Button();
        ShowPrivelegesBtn.Text = "Priveleges";
        ShowPrivelegesBtn.Location = new Point(190, 125);
        ShowPrivelegesBtn.Width = 85;
        ShowPrivelegesBtn.Click += new EventHandler(actions.GetPriveleges);
        this.Controls.Add(ShowPrivelegesBtn);

        Button closeProgramBtn = new Button();
        closeProgramBtn.Text = "Quit";
        closeProgramBtn.Location = new Point(200, 250);
        closeProgramBtn.Width = 85;
        closeProgramBtn.Click += new EventHandler(CloseProgram);
        this.Controls.Add(closeProgramBtn);


    }

    private void CloseProgram(object sender, EventArgs e)
    {
        this.Close();
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
        Application.SetCompatibleTextRenderingDefault(false);

        UserInfo dbUser;
        using (SignInForm signIn = new SignInForm())
        {
            var dlg = signIn.ShowDialog();
            if (dlg != DialogResult.OK)
            {
                // user cancelled sign-in
                return;
            }
            if (signIn.IsGuest)
            {
                dbUser = new UserInfo("Guest", false, false, false, false);
            }
            else
            {
                dbUser = new UserInfo(signIn.Username,
                    UserDBHelper.GetAdmin(signIn.Username),
                    UserDBHelper.GetAddDelete(signIn.Username),
                    UserDBHelper.GetFireHire(signIn.Username),
                    UserDBHelper.GetAdd(signIn.Username)
                );
            }
        }

        Application.Run(new MainMenu(dbUser));
    }
}