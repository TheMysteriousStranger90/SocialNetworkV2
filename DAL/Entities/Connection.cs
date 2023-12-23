namespace DAL.Entities;

public class Connection
{
    public Connection()
    {
    }

    public Connection(string connectionId, string username)
    {
        ConnectionId = connectionId;
        UserName = username;
    }

    public string ConnectionId { get; set; }
    public string UserName { get; set; }
}