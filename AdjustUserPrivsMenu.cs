using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class AdjustUserPrivsMenu : Form
{
    private readonly UserInfo dbUser;
    private readonly ComboBox dbUserToChange;
    private readonly GroupBox IsAdminCheck;
    private readonly GroupBox IsAddDelete;
    private readonly GroupBox IsFireHire;
    private readonly GroupBox IsChangeUser;
    public AdjustUserPrivsMenu(UserInfo dbUser)
    {
        List<string> boxItems = UserDBHelper.GetAllUsers(dbUser.Username);
        object[] boxItemsObject = new object[boxItems.Count];
        for(int i = 0; i < boxItems.Count; i++)
        {
            boxItemsObject[i] = boxItems[i];
        }

        this.dbUser = dbUser;
        this.Text = $"User Privileges Menu, signed in as {dbUser.Username}";
        this.Size = new Size(500, 550);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label userLabel = new Label();
        userLabel.Text = "Enter Username Here:";
        userLabel.Location = new Point(100, 20);
        userLabel.AutoSize = true;
        this.Controls.Add(userLabel);

        dbUserToChange = new ComboBox();
        dbUserToChange.Location = new Point(250, 18);
        dbUserToChange.Width = 100;
        dbUserToChange.Items.AddRange(boxItemsObject);
        this.Controls.Add(dbUserToChange);

        Label adminLabel = new Label();
        adminLabel.Text = "Admin Privileges:";
        adminLabel.Location = new Point(100, 50);
        adminLabel.AutoSize = true;
        this.Controls.Add(adminLabel);

        IsAdminCheck = new GroupBox();
        IsAdminCheck.Location = new Point(100, 70);
        IsAdminCheck.Size = new Size(220, 60);
        IsAdminCheck.Text = string.Empty;
        this.Controls.Add(IsAdminCheck);

        RadioButton adminYes = new RadioButton();
        adminYes.Text = "Yes";
        adminYes.Location = new Point(20, 20);
        adminYes.AutoSize = true;
        adminYes.Checked = true;

        RadioButton adminNo = new RadioButton();
        adminNo.Text = "No";
        adminNo.Location = new Point(90, 20);
        adminNo.AutoSize = true;

        IsAdminCheck.Controls.Add(adminYes);
        IsAdminCheck.Controls.Add(adminNo);

        Button closePageBtn = new Button();
        closePageBtn.Text = "Close";
        closePageBtn.Location = new Point(200, 460);
        closePageBtn.Width = 100;
        closePageBtn.Click += new EventHandler(CloseWindow);
        this.Controls.Add(closePageBtn);
    }

    private void CloseWindow(object sender, EventArgs e)
    {
        this.Close();
    }
}