var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Chat/Index')
    .build();

// clientul 'asculta' evenimentul serverului numit'ReceiveMessage'
connection.on('ReceiveMessage', appendMessage);

connection.start() 
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToServer(message) {
    log("sendMessage", "signalrReqHandler.js", message.text);

    // invocam metoda 'GroupMessage'
    connection.invoke('GroupMessage', message).catch(err => console.error(err.toString()));
}