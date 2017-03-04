using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;

namespace Business.DataAccess.Private.Repository.Specific
{
    public class EFMessageRepository : IMessageRepository
    {
        EFDBContext context = new EFDBContext();

        public IQueryable<Message> Messages
        {
            get
            {
                return context.Messages;
            }
        }
        public IQueryable<MessageLast> MessagesLast
        {
            get
            {
                return context.MessagesLast;
            }
        }
        public Message GetMessage(long MessageId)
        {
            return context.Messages.Find(MessageId);
        }
        public ICollection<Message> GetDialog(long ProfileIdFrom, long ProfileIdTo, int count = 100)
        {
            var result = context.Messages
                .Where(m => (m.ProfileIdFrom == ProfileIdFrom && m.ProfileIdTo == ProfileIdTo) || (m.ProfileIdTo == ProfileIdFrom && m.ProfileIdFrom == ProfileIdTo))
                .OrderByDescending(m => m.DT)
                .Take(count)
                .Include(m=>m.ProfileFrom)
                .Include(m => m.ProfileTo)
                .ToList();
            return result;
        }
        public int GetDialogNewMessagesCount(long ProfileIdFrom, long ProfileIdTo)
        {
            var result = context.Messages
                .Where(m => (m.ProfileIdFrom == ProfileIdFrom && m.ProfileIdTo == ProfileIdTo) && (m.Read == false || m.Read == null))
                .Count();
            return result;
        }
        public int GetAllDialogNewMessagesCount(long ProfileIdFrom)
        {
            var result = context.Messages
                .Where(m => (m.Read == false || m.Read == null) && (m.ProfileIdTo == ProfileIdFrom))
                .Count();
            return result;
        }
        public DateTime? GetDialogLastMessageTime(long ProfileIdFrom, long ProfileIdTo)
        {
            var result = context.Messages
                .Where(m => (m.ProfileIdFrom == ProfileIdFrom && m.ProfileIdTo == ProfileIdTo) || (m.ProfileIdTo == ProfileIdFrom && m.ProfileIdFrom == ProfileIdTo))
                .OrderByDescending(m => m.DT)
                .Select(m => m.DT)
                .FirstOrDefault();                
            return result;
        }
        public List<Profile> GetMyDialogs(long ProfileIdFrom)
        {
            List<Profile> result=new List<Profile>();          
            Messages.ToList();
            var FromMe = context.MessagesLast
                .Where(m => m.ProfileIdFrom == ProfileIdFrom && m.ProfileIdTo != ProfileIdFrom)
                .Where(m => m.ProfileTo.New != true)
                .OrderByDescending(m => m.DT)
                .Select(m => new { Profile = m.ProfileTo,DT=m.DT}).ToList();
            var FromTo = context.MessagesLast
                .Where(m => m.ProfileIdTo == ProfileIdFrom && m.ProfileIdFrom != ProfileIdFrom)
                .Where(m => m.ProfileFrom.New != true)
                .OrderByDescending(m => m.DT)
                .Select(m => new { Profile = m.ProfileFrom, DT = m.DT }).ToList();
            var firstResult = FromMe;
            FromMe.AddRange(FromTo);
            FromMe = FromMe.OrderByDescending(m => m.DT).ToList();            
            result = FromMe.Select(m=>m.Profile).Distinct().ToList();
            return result;
        }
        public long AddMessage(long profileIdFrom, long profileIdTo, string message, DateTime dT)
        {
            Message newMessage = new Message { ProfileIdFrom = profileIdFrom, ProfileIdTo = profileIdTo, MessageText = message, DT = dT };
            context.Messages.Add(newMessage);
            MessageLast messageLast = MessagesLast.Where(m => m.ProfileIdTo == profileIdTo && m.ProfileIdFrom == profileIdFrom).FirstOrDefault();
            if (messageLast!=null)
            {
                messageLast.DT = dT;
            }
            else
            {
                messageLast = new MessageLast { ProfileIdTo = profileIdTo, ProfileIdFrom = profileIdFrom, DT = dT };
                context.MessagesLast.Add(messageLast);

            }
            context.SaveChanges();
            return newMessage.MessageId;
        }
        public void ReadMessage(long messageId, long profileId)
        {
            Message message = Messages.Where(m => m.MessageId == messageId && m.ProfileIdTo == profileId).FirstOrDefault();
            if (message != null)
            {
                message.Read = true;
                
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
