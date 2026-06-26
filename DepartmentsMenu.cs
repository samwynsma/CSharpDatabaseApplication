using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.ApplicationServices;

public class DepartmentsMenu : Form
{
    private readonly UserInfo dbUser;
    private readonly TextBox dbDepartment;
    private readonly TextBox resultsBox;
    private readonly DepartmentsMenuActions actions;
    public DepartmentsMenu(UserInfo dbUser)
    {
        this.dbUser = dbUser;
        this.Text = $"Departments Menu. Signed in as {dbUser.Username}";
        this.Size = new Size(500, 550);
        this.StartPosition = FormStartPosition.CenterScreen;

        Label myLabel = new Label();
        myLabel.Text = "Department: ";
        myLabel.Location = new Point(10, 20);
        myLabel.AutoSize = true;
        this.Controls.Add(myLabel);

        dbDepartment = new TextBox();
        dbDepartment.Location = new Point(100, 18);
        dbDepartment.Width = 100;
        this.Controls.Add(dbDepartment);

        // Results box: multiline, read-only, vertical scrollbar
        resultsBox = new TextBox();
        resultsBox.Location = new Point(10, 140);
        resultsBox.Size = new Size(460, 300);
        resultsBox.Multiline = true;
        resultsBox.ScrollBars = ScrollBars.Vertical;
        resultsBox.ReadOnly = true;
        // resultsBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Controls.Add(resultsBox);

        actions = new DepartmentsMenuActions(dbDepartment, resultsBox, dbUser);

        Button addDptButton = new Button();
        addDptButton.Text = "Add Department";
        addDptButton.Location = new Point(20, 100);
        addDptButton.Width = 85;
        addDptButton.Click += new EventHandler(actions.AddDepartment);
        this.Controls.Add(addDptButton);

        Button getItemDetailsBtn = new Button();
        getItemDetailsBtn.Text = "Dpt Items";
        getItemDetailsBtn.Location = new Point(105, 100);
        getItemDetailsBtn.Width = 85;
        getItemDetailsBtn.Click += new EventHandler(actions.GetDatabaseItems);
        this.Controls.Add(getItemDetailsBtn);

        Button dptListBtn = new Button();
        dptListBtn.Text = "Dpt List";
        dptListBtn.Location = new Point(190, 100);
        dptListBtn.Width = 85;
        dptListBtn.Click += new EventHandler(actions.GetDepartmentList);
        this.Controls.Add(dptListBtn);

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