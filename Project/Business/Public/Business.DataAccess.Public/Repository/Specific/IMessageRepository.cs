using System;
using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Public.Entities;

namespace Business.DataAccess.Public.Repository.Specific
{
    public interface IMessageRepository
    {
        /// <summary>
        /// IQueryable сообщений
        /// </summary>
        /// <param name="data"></param>
        IQueryable<Message> Messages { get; }
        /// <summary>
        /// IQueryable последних сообщений
        /// </summary>
        IQueryable<MessageLast> MessagesLast { get; }
        /// <summary>
        /// Получить сообщение        
        /// </summary>
        /// <param name="MessageId">MessageId сообщения</param>
        Message GetMessage(long MessageId);
        /// <summary>
        /// Получить диалог
        /// <param name="ProfileIdFrom">ProfileId профиля</param>
        /// </summary>
        ICollection<Message> GetDialog(long ProfileIdFrom, long ProfileIdTo, int count);
        int GetDialogNewMessagesCount(long ProfileIdFrom, long ProfileIdTo);
        int GetAllDialogNewMessagesCount(long ProfileIdFrom);
        DateTime? GetDialogLastMessageTime(long ProfileIdFrom, long ProfileIdTo);
        List<Profile> GetMyDialogs(long ProfileIdFrom);
        long AddMessage(long profileIdFrom, long profileIdTo, string message, DateTime dT);
        void ReadMessage(long messageId, long profileId);

        void Commit();
    }
}
