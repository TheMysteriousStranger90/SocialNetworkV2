using DAL.Entities;
using DAL.Specifications;

namespace DAL.Interfaces;

public interface IMessageRepository
{
    void AddGroup(Group group);
    void RemoveConnection(Connection connection);
    Task<Connection> GetConnection(string connectionId);
    Task<Group> GetMessageGroup(string groupName);
    Task<Group> GetGroupForConnection(string connectionId);
    void AddMessage(Message message);
    void DeleteMessage(Message message);
    Task<Message> GetMessage(int id);
    IQueryable<Message> GetMessagesForUser(MessageParams messageParams);
    Task<IEnumerable<Message>> GetMessageThread(string currentUserName, string recipientUserName); 
}