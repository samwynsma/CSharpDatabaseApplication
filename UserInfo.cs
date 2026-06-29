using System;

public class UserInfo
{
    public string Username { get; }
    public bool IsAdmin { get; }
    public bool CanAddDelete { get; }
    public bool CanHireFire { get; }
    public bool HasAddedPrivs { get; }
    public bool HasUserPrivs {get;}

    public UserInfo(string user, bool admin, bool addDelete, bool canHireFire, bool additional, bool userPrivs)
    {
        Username = user;
        IsAdmin = admin;
        CanAddDelete = addDelete;
        CanHireFire = canHireFire;
        HasAddedPrivs = additional;
        HasUserPrivs = userPrivs;
    }
}