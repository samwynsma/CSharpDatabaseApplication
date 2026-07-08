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
    private RadioButton adminYes, adminNo;
    private RadioButton addDelYes, addDelNo;
    private RadioButton fireHireYes, fireHireNo;
    private RadioButton changeUserYes, changeUserNo;
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
        dbUserToChange.SelectedIndexChanged += new EventHandler(OnUserSelected);
        this.Controls.Add(dbUserToChange);

        // Admin Prompt
        Label adminLabel = new Label();
        adminLabel.Text = "Admin Privileges:";
        adminLabel.Location = new Point(20, 80);
        adminLabel.AutoSize = true;
        this.Controls.Add(adminLabel);

        IsAdminCheck = new GroupBox();
        IsAdminCheck.Location = new Point(150, 50);
        IsAdminCheck.Size = new Size(220, 60);
        IsAdminCheck.Text = string.Empty;
        this.Controls.Add(IsAdminCheck);

        adminYes = new RadioButton();
        adminYes.Text = "Yes";
        adminYes.Location = new Point(20, 20);
        adminYes.AutoSize = true;
        adminYes.Checked = true;

        adminNo = new RadioButton();
        adminNo.Text = "No";
        adminNo.Location = new Point(90, 20);
        adminNo.AutoSize = true;

        IsAdminCheck.Controls.Add(adminYes);
        IsAdminCheck.Controls.Add(adminNo);

        // Add/Delete Prompt
        Label addDelLabel = new Label();
        addDelLabel.Text = "Add/Delete Privileges:";
        addDelLabel.Location = new Point(20, 150);
        addDelLabel.AutoSize = true;
        this.Controls.Add(addDelLabel);

        IsAddDelete = new GroupBox();
        IsAddDelete.Location = new Point(150, 120);
        IsAddDelete.Size = new Size(220, 60);
        IsAddDelete.Text = string.Empty;
        this.Controls.Add(IsAddDelete);

        addDelYes = new RadioButton();
        addDelYes.Text = "Yes";
        addDelYes.Location = new Point(20, 20);
        addDelYes.AutoSize = true;
        addDelYes.Checked = true;

        addDelNo = new RadioButton();
        addDelNo.Text = "No";
        addDelNo.Location = new Point(90, 20);
        addDelNo.AutoSize = true;

        IsAddDelete.Controls.Add(addDelYes);
        IsAddDelete.Controls.Add(addDelNo);

        // Fire/Hire Prompt
        Label FireHireLabel = new Label();
        FireHireLabel.Text = "Fire/Hire Privileges:";
        FireHireLabel.Location = new Point(20, 220);
        FireHireLabel.AutoSize = true;
        this.Controls.Add(FireHireLabel);

        IsFireHire = new GroupBox();
        IsFireHire.Location = new Point(150, 190);
        IsFireHire.Size = new Size(220, 60);
        IsFireHire.Text = string.Empty;
        this.Controls.Add(IsFireHire);

        fireHireYes = new RadioButton();
        fireHireYes.Text = "Yes";
        fireHireYes.Location = new Point(20, 20);
        fireHireYes.AutoSize = true;
        fireHireYes.Checked = true;

        fireHireNo = new RadioButton();
        fireHireNo.Text = "No";
        fireHireNo.Location = new Point(90, 20);
        fireHireNo.AutoSize = true;

        IsFireHire.Controls.Add(fireHireYes);
        IsFireHire.Controls.Add(fireHireNo);

        // Change User Prompt
        Label ChangeUserLabel = new Label();
        ChangeUserLabel.Text = "Change User Privileges:";
        ChangeUserLabel.Location = new Point(20, 290);
        ChangeUserLabel.AutoSize = true;
        this.Controls.Add(ChangeUserLabel);

        IsChangeUser = new GroupBox();
        IsChangeUser.Location = new Point(150, 260);
        IsChangeUser.Size = new Size(220, 60);
        IsChangeUser.Text = string.Empty;
        this.Controls.Add(IsChangeUser);

        changeUserYes = new RadioButton();
        changeUserYes.Text = "Yes";
        changeUserYes.Location = new Point(20, 20);
        changeUserYes.AutoSize = true;
        changeUserYes.Checked = true;

        changeUserNo = new RadioButton();
        changeUserNo.Text = "No";
        changeUserNo.Location = new Point(90, 20);
        changeUserNo.AutoSize = true;

        IsChangeUser.Controls.Add(changeUserYes);
        IsChangeUser.Controls.Add(changeUserNo);

        Button changePrivsBtn = new Button();
        changePrivsBtn.Text = "Modify Privs";
        changePrivsBtn.Location = new Point(200, 420);
        changePrivsBtn.Width = 100;
        changePrivsBtn.Click += new EventHandler(ChangePrivileges);
        this.Controls.Add(changePrivsBtn);

        Button closePageBtn = new Button();
        closePageBtn.Text = "Close";
        closePageBtn.Location = new Point(200, 460);
        closePageBtn.Width = 100;
        closePageBtn.Click += new EventHandler(CloseWindow);
        this.Controls.Add(closePageBtn);
    }

    private void ChangePrivileges(object sender, EventArgs e)
    {
        return;
    }

    private void OnUserSelected(object sender, EventArgs e)
    {
        if (dbUserToChange.SelectedItem != null)
        {
            string selectedUser = dbUserToChange.SelectedItem.ToString();
            PopulateUserPrivileges(selectedUser);
        }
    }

    private void PopulateUserPrivileges(string username)
    {
        List<bool> privs = UserDBHelper.GetCurrentActivePrivs(username);
        
        if (privs.Count >= 4)
        {
            adminYes.Checked = privs[0];
            adminNo.Checked = !privs[0];

            addDelYes.Checked = privs[1];
            addDelNo.Checked = !privs[1];

            fireHireYes.Checked = privs[2];
            fireHireNo.Checked = !privs[2];

            changeUserYes.Checked = privs[3];
            changeUserNo.Checked = !privs[3];
        }
    }

    private void CloseWindow(object sender, EventArgs e)
    {
        this.Close();
    }
}