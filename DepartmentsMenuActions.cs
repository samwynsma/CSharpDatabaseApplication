using System;
using System.Windows.Forms;

class DepartmentsMenuActions
{
    private readonly UserInfo dbUser;
    private readonly TextBox dbDepartment;
    private readonly TextBox resultsBox;

    public DepartmentsMenuActions(TextBox dbDepartment, TextBox resultsBox, UserInfo dbUser)
    {
        this.dbDepartment = dbDepartment;
        this.resultsBox = resultsBox;
        this.dbUser = dbUser;
    }

    public void AddDepartment(object sender, EventArgs e)
    {
        resultsBox.Clear();
        resultsBox.AppendText("AddDepartment: not implemented yet.\r\n");
    }

    public void GetDatabaseItems(object sender, EventArgs e)
    {
        resultsBox.Clear();
        resultsBox.AppendText("GetDatabaseItems: query results would appear here.\r\n");
    }

    public void GetDepartmentList(object sender, EventArgs e)
    {
        resultsBox.Clear();
        resultsBox.AppendText("GetDepartmentList: department list would appear here.\r\n");
    }
}