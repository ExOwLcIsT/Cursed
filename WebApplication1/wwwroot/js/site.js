var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

//Disable the send button until connection is established.

connection.on("ReceiveMessage", function (user, message) {
});

connection.start()
function addPfP(x, y) {
    document.getElementById(`x ${x} y ${y}`).innerHTML = `<div class="pfp" ></div>`
    connection.invoke("sendMessage", String(x), String(y)).catch(function (err) {
        return console.error(err.toString());
    });
}