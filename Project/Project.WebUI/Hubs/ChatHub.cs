using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Globalization;
using Business.DataAccess.Private.Repository.Specific;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using Project.WebUI.Models;

namespace Project.WebUI.Hubs
{
   
    public class ChatHub : Hub
    {
        //TODO Reconnect HUB
        readonly IMessageRepository _messageRepository = new EFMessageRepository();
        readonly IChatRepository _chatRepository = new EFChatRepository();
        //TODO починить репозитории
        readonly EFProfileRepository _profileRepository = new EFProfileRepository();
        public string CreateHtmlMessage(Profile profile, string message, DateTime dT, bool my = false, long messageid = 0)
        {
            var tagContent = new TagBuilder("span");
            tagContent.AddCssClass("message-content");
            tagContent.InnerHtml = message;

            var tagDate = new TagBuilder("span");
            tagDate.AddCssClass("message-date");
            tagDate.InnerHtml = dT.ToString();

            var tagAuthor = new TagBuilder("a");
            var url = @"/Profile/Index/" + profile.ProfileId;
            tagAuthor.MergeAttribute("href", url);
            tagAuthor.AddCssClass("message-author");
            tagAuthor.InnerHtml = profile.FirstName + " " + profile.LastName;

            var tagMessage = new TagBuilder("div");
            tagMessage.AddCssClass("message");
            tagMessage.InnerHtml = tagAuthor.ToString() + tagDate.ToString() + tagContent.ToString();

            var tagAvatar = new TagBuilder("img");
            tagAvatar.AddCssClass("message-avatar");
            tagAvatar.MergeAttribute("src", profile.GetImageAvatarLink);

            var tagChatMessage = new TagBuilder("div");
            if (my)
            {
                tagChatMessage.AddCssClass("chat-message left chat-message-unread");
            }
            else
            {
                tagChatMessage.AddCssClass("chat-message left chat-message-unread");
            }
            tagChatMessage.MergeAttribute("id", "messageid_" + messageid);
            tagChatMessage.InnerHtml = tagAvatar.ToString() + tagMessage.ToString();
            return tagChatMessage.ToString();
        }

        public void Send(dynamic message, long from, long to)
        {
            var chats = _chatRepository.GetChats(to);
            var myChats = _chatRepository.GetChats(from);
            var Message = new ChatSendResult();
            var MyMessage = new ChatSendResult();
            var profile = _profileRepository.Profiles.FirstOrDefault(m => m.ProfileId == from);
            var DT = DateTime.Now;

            long messageId = _messageRepository.AddMessage(from, to, message, DT);
            Message.Message = CreateHtmlMessage(profile, message, DT, false, messageId);
            Message.MessageId = messageId;
            Message.From = new ChatSendProfile {FromId = @from};
            MyMessage.Message = CreateHtmlMessage(profile, message, DT, true, messageId);
            MyMessage.MessageId = 0;
            foreach (var chat in myChats)
            {
                Clients.Client(chat.ConnectionId).SendMessage(MyMessage);
            }
            foreach (var chat in chats)
            {

                Clients.Client(chat.ConnectionId).SendMessage(Message);
            }

        }

        public void Register(long userId)
        {
            _chatRepository.ConnectToChat(userId, Context.ConnectionId);
            Groups.Add(Context.ConnectionId, userId.ToString(CultureInfo.InvariantCulture));

        }
        public Task ToGroup(dynamic from, dynamic to, string message)
        {
            return Clients.Group(to.ToString()).SendMessage(message);
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            _chatRepository.DisconnectFromChat(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
        public void ReadMessage(List<long> messageIds, long profileId, long profileIdTo)
        {
            foreach (var id in messageIds)
            {
                _messageRepository.ReadMessage(id, profileId);
            }
            _messageRepository.Commit();
            var chats = _chatRepository.GetChats(profileIdTo);
            var myChats = _chatRepository.GetChats(profileId);
            foreach (var chat in myChats)
            {
                Clients.Client(chat.ConnectionId).SendReadMessage(messageIds);
            }
            foreach (var chat in chats)
            {

                Clients.Client(chat.ConnectionId).SendReadMessage(messageIds);
            }
        }
    }
}