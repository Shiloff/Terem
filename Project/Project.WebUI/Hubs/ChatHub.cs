using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Globalization;
using Business.DataAccess.Private.Repository.Specific;
using Business.DataAccess.Public.Entities;
using Project.WebUI.Models;

namespace Project.WebUI.Hubs
{
   
    public class ChatHub : Hub
    {
        //IChatRepository ChatRepository;
        //public ChatHub(IChatRepository chatRepository)
        //{
        //    ChatRepository = chatRepository;
        //}
        //TODO Reconnect HUB
        //EFMessageRepository MessageRepository = new EFMessageRepository();
        //EFChatRepository ChatRepository = new EFChatRepository();
        ////TODO починить репозитории
        //EFProfileRepository ProfileRepository = new EFProfileRepository();
        //public string CreateHtmlMessage(Profile profile, string message, DateTime dT, bool my = false, long messageid = 0)
        //{
        //    TagBuilder tagContent = new TagBuilder("span");
        //    tagContent.AddCssClass("message-content");
        //    tagContent.InnerHtml = message;

        //    TagBuilder tagDate = new TagBuilder("span");
        //    tagDate.AddCssClass("message-date");
        //    tagDate.InnerHtml = dT.ToString();

        //    TagBuilder tagAuthor = new TagBuilder("a");
        //    var url = @"/Profile/Index/" + profile.ProfileId;
        //    tagAuthor.MergeAttribute("href", url);
        //    tagAuthor.AddCssClass("message-author");
        //    tagAuthor.InnerHtml = profile.FirstName + " " + profile.LastName;

        //    TagBuilder tagMessage = new TagBuilder("div");
        //    tagMessage.AddCssClass("message");
        //    tagMessage.InnerHtml = tagAuthor.ToString() + tagDate.ToString() + tagContent.ToString();

        //    TagBuilder tagAvatar = new TagBuilder("img");
        //    tagAvatar.AddCssClass("message-avatar");
        //    tagAvatar.MergeAttribute("src", profile.GetImageAvatarLink);

        //    TagBuilder tagChatMessage = new TagBuilder("div");
        //    if (my) {
        //        tagChatMessage.AddCssClass("chat-message left chat-message-unread");
        //    }
        //    else
        //    {
        //        tagChatMessage.AddCssClass("chat-message left chat-message-unread");
        //    }
        //    tagChatMessage.MergeAttribute("id", "messageid_" + messageid);
        //    tagChatMessage.InnerHtml = tagAvatar.ToString() + tagMessage.ToString();
        //    return tagChatMessage.ToString();
        //}

        //public void Send(dynamic message, long from, long to)
        //{
        //    var chats = ChatRepository.GetChats(to);
        //    var myChats = ChatRepository.GetChats(from);
        //    ChatSendResult Message = new ChatSendResult();
        //    ChatSendResult MyMessage = new ChatSendResult();
        //    var profile = ProfileRepository.Profiles.FirstOrDefault(m => m.ProfileId == from);
        //    DateTime DT = DateTime.Now;

        //    long messageId = MessageRepository.AddMessage(from, to, message, DT);
        //    Message.Message = CreateHtmlMessage(profile, message, DT, false, messageId);
        //    Message.MessageId = messageId;
        //    Message.From = new ChatSendProfile();
        //    Message.From.FromId = from;
        //    MyMessage.Message = CreateHtmlMessage(profile, message, DT, true, messageId);
        //    MyMessage.MessageId = 0;
        //    foreach (var chat in myChats)
        //    {
        //        Clients.Client(chat.ConnectionId).SendMessage(MyMessage);
        //    }
        //    foreach (var chat in chats)
        //    {

        //        Clients.Client(chat.ConnectionId).SendMessage(Message);
        //    }
            
        //}

        //public void Register(long userId)
        //{
        //    ChatRepository.ConnectToChat(userId, Context.ConnectionId);
        //    Groups.Add(Context.ConnectionId, userId.ToString(CultureInfo.InvariantCulture));
           
        //}
        //public Task ToGroup(dynamic from, dynamic to, string message)
        //{
        //    return Clients.Group(to.ToString()).SendMessage(message);
        //}
        //public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        //{
        //    ChatRepository.DisconnectFromChat(Context.ConnectionId);
        //    return base.OnDisconnected(stopCalled);
        //}
        //public void ReadMessage(List<long> messageIds, long profileId, long profileIdTo)
        //{
        //    foreach (long id in messageIds)
        //    {
        //        MessageRepository.ReadMessage(id, profileId);
        //    }
        //    MessageRepository.Commit();
        //    var chats = ChatRepository.GetChats(profileIdTo);
        //    var myChats = ChatRepository.GetChats(profileId);
        //    foreach (var chat in myChats)
        //    {
        //        Clients.Client(chat.ConnectionId).SendReadMessage(messageIds);
        //    }
        //    foreach (var chat in chats)
        //    {

        //        Clients.Client(chat.ConnectionId).SendReadMessage(messageIds);
        //    }
        //}
    }
}