var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
var enemy = ""
var me = ""
var online = true;
$("#connectbtn").on('click', () => {
    connection.invoke("checkId", me, $("#enemyid").val());
})
//Disable the send button until connection is established.

//connection.on("ReceiveMessage", function (x, y) {
//    document.getElementById(`x ${x} y ${y}`).innerHTML = `<div class="chip pl2" ></div>`
//});
connection.on("ReceiveMessage", function (x, y) {
    addEnemyPfP(x, y)
});
connection.on("CheckId", function (f) {
    if (f) {
        enemy = $("#enemyid").val();
        $("#p2").html(player2code);
    }
});
connection.on("ReceiveConnectionId", function (s) {
    $("#cid").text(s)
    me = s;
    console.log(s)
});

connection.start()
function addPfP(x, y) {
    document.getElementById(`x ${x} y ${y}`).innerHTML = `<div class="chip pl1" ></div>`;
    $(`#x ${x} y ${y}`).on('click', () => { });
    if (online) {
        for (let i = 0; i < 10; i++) {
            for (let j = 0; j < 10; j++) {
                document.getElementById(`x ${j} y ${i}`).style.pointerEvents = 'none';
            }
        }

        connection.invoke("sendMessage", String(x), String(y), enemy).catch(function (err) {
            return console.error(err.toString());
        });
    }
    checkField(x, y, `<div class="chip pl1" ></div>`);
} function addEnemyPfP(x, y) {
    document.getElementById(`x ${x} y ${y}`).innerHTML = `<div class="chip pl2" ></div>`;
    $(`#x ${x} y ${y}`).on('click', () => { });
    if (online) {
        for (let i = 0; i < 10; i++) {
            for (let j = 0; j < 10; j++) {
                document.getElementById(`x ${j} y ${i}`).style.pointerEvents = 'auto';
            }
        }
    }
    checkField(x, y, `<div class="chip pl2" ></div>`);
}
function checkField(x, y, playercode) {
    let countToWin = 1;
    let x1 = x - 1, y1 = y - 1;
    //перевірка по діагоналі (ліво верх)

    while (x1 >= 0 && y1 >= 0 && $(`#x ${x1} y ${y1}`).html() == playercode) {
        countToWin++;
        if (countToWin == 5) { console.log("win") }
        x1--;
        y1--;
    }
    x1 = x + 1;
    y1 = y + 1;
    while (x1 <= 9 && y1 <= 9 && $(`#x ${x1} y ${y1}`).html() == playercode) {
        countToWin++;
        if (countToWin == 5) { console.log("win") }

        x1++;
        y1++;
    }
    //перевірка по діагоналі (право верх)
    countToWin = 1;
    x1 = x + 1;
    y1 = y - 1;
    while (x1 <= 9 && y1 >= 0 && $(`#x ${x1} y ${y1}`).html() == playercode) {
        countToWin++;
        if (countToWin == 5) { console.log("win") }
        x1++;
        y1--;
    }
    x1 = x - 1;
    y1 = y + 1;
    while (x1 >= 0 && y1 <= 9 && $(`#x ${x1} y ${y1}`).html() == playercode) {
        countToWin++;
        if (countToWin == 5) { console.log("win") }
        x1--;
        y1++;
    }
    //перевірка по вертикалі
    countToWin = 1;
    x1 = x;
    y1 = y - 1;
    while (y1 >= 0 && $(`#x ${x1} y ${y1}`).html() == playercode) {
        countToWin++;
        if (countToWin == 5) { console.log("win") }
        y1--;
    }
    y1 = y + 1;
    while (y1 <= 9 && $(`#x ${x1} y ${y1}`).html() == playercode) {
        countToWin++;
        if (countToWin == 5) { console.log("win") }
        y1++;
    }
    //перевірка по горизонталі
    countToWin = 1;
    x1 = x - 1;
    y1 = y;
    while (x1 >= 0 && $(`#x ${x1} y ${y1}`).html() == playercode) {
        countToWin++;
        if (countToWin == 5) { console.log("win") }
        x1--;
    }
    x1 = x + 1;
    while (x1 <= 9 && $(`#x ${x1} y ${y1}`).html() == playercode) {
        countToWin++;
        if (countToWin == 5) { console.log("win") }
        x1++;
    };
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