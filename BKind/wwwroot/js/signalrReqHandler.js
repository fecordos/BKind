var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Chat/Index')
    .build();

// Client listening for server event called 'ReceiveMessage'
connection.on('ReceiveMessage', appendMessage);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToServer(message) {
    log("sendMessage", "signalrReqHandler.js", message.text);

    // Invoke a server method called 'BroadcastMessage'
    connection.invoke('BroadcastMessage', message).catch(err => console.error(err.toString()));
}