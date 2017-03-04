using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;

namespace Business.DataAccess.Private.Repository.Specific
{
    public class EFChatRepository : IChatRepository
    {
        EFDBContext context = new EFDBContext();

        public IQueryable<Chat> Chat
        {
            get
            {
                return context.Chat;
            }
        }
        public void ConnectToChat(long profileId, string connectionId)
        {
            context.Chat.Add(new Chat { ProfileId = profileId, ConnectionId = connectionId });
            context.SaveChanges();
        }
        public void DisconnectFromChat(string connectionId)
        {
            Chat chatDisconnet = Chat.Where(m => m.ConnectionId == connectionId).FirstOrDefault();
            if (chatDisconnet != null)
            {
                context.Chat.Remove(chatDisconnet);
                context.SaveChanges();
            }
        }
        public List<Chat> GetChats(long profileId)
        {
            List<Chat> result = Chat.Where(m => m.ProfileId == profileId).ToList();
            return result;
        }
    }
}
