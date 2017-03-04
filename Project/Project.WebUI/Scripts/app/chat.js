var chat;
var $ProfileId;
$(document).ready(function () {
    $.ajaxSetup({
        cache: false
    });
    var $openedChat = $("#ChatOpen");
    $ProfileId = $("#ProfileId").val();
    var userId = $ProfileId;
    // прокси
    chat = $.connection.chatHub;
    // объявляем callback, который среагирует на событие сервера
    chat.client.SendMessage = function (message) {
        // обновляем счётчик сообщений
        GetMessage(message);
    };
    chat.client.SendReadMessage = function (messageId) {
        // обновляем счётчик сообщений
        $.each(messageId, function (index, id) {
            $("#messageid_" + id).removeClass("chat-message-unread");
        })
        RefrashAllNewMessages();
    };

    // Запускаем хаб
    ShowLoading('message_entry', '#ajax_loader', 1);
    ShowLoading('dropdown_messages', '#ajax_loader2', 1);
    $.connection.hub.start().done(function () {
        // расскажем серверу кто подключился
        chat.server.register(userId);
        HideLoading('#ajax_loader');
        RefrashAllNewMessages();
        if ($openedChat.val() != 0) {
            GetChat($openedChat.val());
        }
    });
    $("#message-input").on("keyup", function (e) {
        if ((e.keyCode == 13) && ($("#message-input").val().trim() != "")) {
            //SendMessage($("#ToProfileId").val(), $("#message-input").val());
            chat.server.send($("#message-input").val(), $ProfileId, $("#ToProfileId").val());
            $("#message-input").val("");
        }
    });
    $("#sendMessage").on("click", function (e) {
        if ($("#message-input").val().trim() != "") {
            //SendMessage($("#ToProfileId").val(), $("#message-input").val());
            chat.server.send($("#message-input").val(), $ProfileId, $("#ToProfileId").val());
            $("#message-input").val("");
        }
    });


   
});

function GetChat(ProfileId)
{
    $.ajaxSetup({
        cache: false
    });
    $(".chat-discussion").load("/Messages/Dialog?ProfileId=" + ProfileId, function (response, status, xhr) {
        $(".chat-discussion").scrollTop($(".chat-discussion")[0].scrollHeight);
        var ids = [];
        $.each($(".chat-message-unread.left"), function (index, message) {
            ids.push($(message).attr("id").replace("messageid_", ""));
        });
        chat.server.readMessage(ids, $ProfileId, $("#ToProfileId").val());
        $("#chat_user_new_" + ProfileId + "").html("");
        
    });

}

function GetMessage(message) {
    
    if ((message.From != null) && (message.From.FromId != $("#ToProfileId").val())) {
        var $newMessageCountFromUser = $("#chat_user_new_" + message.From.FromId).html();
        $newMessageCountFromUser++;
        $("#chat_user_new_" + message.From.FromId).html($newMessageCountFromUser);
    }
    if ((message.From == null) || (message.From.FromId == $("#ToProfileId").val())) {
        var $chat_discussion = $(".chat-discussion");
        if ($chat_discussion[0] != undefined) {
            $chat_discussion.append(message.Message);
            $chat_discussion.scrollTop($(".chat-discussion")[0].scrollHeight);
        }
        var ids = [];
        ids.push(message.MessageId);
        chat.server.readMessage(ids, $ProfileId, $("#ToProfileId").val());
    }

}
function RefrashAllNewMessages() {
    $("#newMessageCount").load("/Messages/UnreadMessages", null, function (res) {
        RefrashAllNewMessages2();
    });
}
function RefrashAllNewMessages2() {
    $("#dropdown_messages").load("/Messages/UnreadMessagesShort", null, function (res) {
        HideLoading('#ajax_loader2');
    });
}