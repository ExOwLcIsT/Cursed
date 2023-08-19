var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
var enemy = ""
var online = true;
$("#connectbtn").on('click', () => {
    enemy = $("#cid").val()
})
//Disable the send button until connection is established.

//connection.on("ReceiveMessage", function (x, y) {
//    document.getElementById(`x ${x} y ${y}`).innerHTML = `<div class="chip pl2" ></div>`
//});
connection.on("ReceiveMessage", function (x, y) {
    addPfP(x, y)
});
connection.on("ReceiveConnectionId", function (s) {
    $("#cid").text(s)
});

connection.start()
function addPfP(x, y) {
    document.getElementById(`x ${x} y ${y}`).innerHTML = `<div class="chip pl1" ></div>`
    if (online)
        connection.invoke("sendMessage", String(x), String(y), enemy).catch(function (err) {
            return console.error(err.toString());
        });
}
$("#offlinebtn").on('click', () => {
    online = false
    $("#p1").html(player1code)
})
$("#loginbtn").on('click', loginuser)
function loginuser() {
    let l = $("#login").val()
    let p = $("#password").val()
    $.ajax({
        async: true,
        url: "/api/account/login",
        method: "POST",
        data: JSON.stringify({
            "UserName": l,
            "Password": p
        }),
        headers: {
            "content-type": "application/json;odata=verbose"
        },
        success: function (data) {
            if (data) {
                $("#p1").html(player1code)
                $("#nick1").text(l)
                connection.invoke("GetConnectionId", "tur").catch(function (err) {
                    return console.error(err.toString());
                });
            }
            else {
                console.log("no")
            }
        },
        error: function (error) { console.log("error: " + JSON.stringify(error)); }
    }
    );
}
function registeruser() {
    let l = $("#login").val()
    let p = $("#password").val()
    let pc = $("#passwordconfirm").val()
    console.log(p)
    console.log(pc)
    if (p === pc) {
        $.ajax({
            async: true,
            url: "/api/account/register",
            method: "POST",
            data: JSON.stringify({
                "UserName": l,
                "Password": p
            }),
            headers: {
                "content-type": "application/json;odata=verbose"
            },
            success: function (data) {
                if (data) {
                    $("#p1").html(player1code)
                    $("#nick1").html(l)
                }
                else {
                    console.log("no")
                }
            },
            error: function (error) { console.log("error: " + JSON.stringify(error)); }
        }
        );
    }
}
$("#registerbtn").on('click', prepareregister)

function prepareregister() {
    $("#p1").html(registercode)
    $("#loginbtn").on('click', preparelogin)
    $("#registerbtn").on('click', registeruser)
}
function preparelogin() {
    $("#p1").html(logincode)
    $("#registerbtn").on('click', prepareregister)
    $("#loginbtn").on('click', loginuser)
}
let player1code = `
<div class="pic" id="pic1"></div>
<div class="nick" id="nick1">
    Player 1
        </div>
        <div class="cid" id="cid">
   
        </div>`
let player2code = `
<div class="pic" id="pic2"></div>
<div class="nick" id="nick2">
    Player 2
        </div>`
let registercode = `
<input id="login" type="text" placeholder="username"/>
        <input id="password" type="password" placeholder="password"/>
         <input id="passwordconfirm" type="password" placeholder="password confirm" />
        <input id="registerbtn" type="button" value="register user"/>
        <input id="loginbtn" type="button" value="back to logging in"/>`
let logincode =
    ` <input id="login" type="text" placeholder="username" />
        <input id="password" type="password" placeholder="password" />
        <input id="loginbtn" type="button" value="log in" />
        <input id="registerbtn" type="button" value="registration" />
        <input id="offlinebtn" type="button" value="play locally" />
        `