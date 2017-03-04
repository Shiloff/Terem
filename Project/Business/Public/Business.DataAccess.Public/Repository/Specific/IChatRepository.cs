using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Public.Entities;

namespace Business.DataAccess.Public.Repository.Specific
{
    public interface IChatRepository
    {
        IQueryable<Chat> Chat { get; }
        void ConnectToChat(long ProfileId, string ConnectionId);
        void DisconnectFromChat(string ConnectionId);
        List<Chat> GetChats(long ProfileId);
    }
}
