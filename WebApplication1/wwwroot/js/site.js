var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
var enemy = ""
var userConnectionid = ""
var online = false;
var userColor = 'blue';
var enemyColor = 'red';
var userPicture = "../images/Default_pfp.jpg";
var enemyPicture = "../images/Default_pfp.jpg";
var currentStep = 1;
connection.start()
function block() {
    for (let i = 0; i < 10; i++) {
        for (let j = 0; j < 10; j++) {
            document.getElementById(`x ${j} y ${i}`).style.pointerEvents = 'none';
        }
    }
}
function unblock() {
    for (let i = 0; i < 10; i++) {
        for (let j = 0; j < 10; j++) {
            document.getElementById(`x ${j} y ${i}`).style.pointerEvents = 'auto';
        }
    }
}
$("#connectbtn").on('click', () => {
    connection.invoke("checkId", userConnectionid, $("#enemyid").val(), userPicture, $("#nick1").text());
})
connection.on("ReceiveStep", function (x, y) {
    addEnemyPfP(x, y)
    currentStep = 1;
});
connection.on("CheckId", function () {
    enemy = $("#enemyid").val();
    $("#p2").html(player2code);
    online = true;
    cleanField();
    connection.invoke("getMove", userConnectionid, enemy);
});
connection.on("ReceiveConnectionId", function (s) {
    $("#cid").text(s)
    userConnectionid = s;

});
connection.on("ReceiveMove", function (s) {
    currentStep = s;
    if (currentStep === 2)
        block()
    else
        unblock();
});
connection.on("ReceiveEnemy", function (pfp, nick, fl, en) {
    $("#p2").html(player2code);
    enemyPicture = pfp;
    enemy = en;
    online = true;
    $("#nick2").text(nick)
    $("#pic2").css('background-image', `url(${enemyPicture})`);
    if (fl === true)
        connection.invoke("sendEnemy", userConnectionid, enemy, userPicture, $("#nick1").text());
});
function addPfP(x, y) {
    if (currentStep === 2) {
        addEnemyPfP(x, y)
        currentStep = 1;
        return;
    }
    document.getElementById(`x ${x} y ${y}`).innerHTML = `<div class="chip pl1"  style = "border-color:${userColor};"></div>`;
    $(`#x ${x} y ${y}`).on('click', () => { });
    $(".pl1").css('background-image', `url(${userPicture})`);
    $(`#x ${x} y ${y}`).on('click', () => { });
    if (online) {
        block()
        connection.invoke("sendMessage", String(x), String(y), enemy).catch(function (err) {
            return console.error(err.toString());
        });
    }
    currentStep = 2;
    checkField(x, y);
} function addEnemyPfP(x, y) {
    document.getElementById(`x ${x} y ${y}`).innerHTML = `<div class="chip pl2"></div>`;
    $(`#x ${x} y ${y}`).on('click', () => { });
    $(".pl2").css('background-image', `url(${enemyPicture})`);
    if (online) {
        unblock()
    }
    currentStep = 1;
    checkField(x, y);
}
function win() {
    block()
    let vinner = $("#nick" + currentStep).text()
    alert(vinner + " has won")
}
function checkField(x, y) {
    let countToWin = 1;
    let x1 = x - 1, y1 = y - 1;
    //перевірка по діагоналі (ліво верх)
    let thisplayer = document.getElementById(`x ${x} y ${y}`).innerHTML;
    while (x1 >= 0 && y1 >= 0 && document.getElementById(`x ${x1} y ${y1}`).innerHTML === thisplayer) {
        countToWin++;
        if (countToWin == 5) { win() }
        x1--;
        y1--;
    }
    x1 = x + 1;
    y1 = y + 1;
    while (x1 <= 9 && y1 <= 9 && document.getElementById(`x ${x1} y ${y1}`).innerHTML === thisplayer) {
        countToWin++;
        if (countToWin == 5) { win() }

        x1++;
        y1++;
    }
    //перевірка по діагоналі (право верх)
    countToWin = 1;
    x1 = x + 1;
    y1 = y - 1;
    while (x1 <= 9 && y1 >= 0 && document.getElementById(`x ${x1} y ${y1}`).innerHTML === thisplayer) {
        countToWin++;
        if (countToWin == 5) { win() }
        x1++;
        y1--;
    }
    x1 = x - 1;
    y1 = y + 1;
    while (x1 >= 0 && y1 <= 9 && document.getElementById(`x ${x1} y ${y1}`).innerHTML === thisplayer) {
        countToWin++;
        if (countToWin == 5) { win() }
        x1--;
        y1++;
    }
    //перевірка по вертикалі
    countToWin = 1;
    x1 = x;
    y1 = y - 1;
    while (y1 >= 0 && document.getElementById(`x ${x1} y ${y1}`).innerHTML === thisplayer) {
        countToWin++;
        if (countToWin == 5) { win() }
        y1--;
    }
    y1 = y + 1;
    while (y1 <= 9 && document.getElementById(`x ${x1} y ${y1}`).innerHTML === thisplayer) {
        countToWin++;
        if (countToWin == 5) { win() }
        y1++;
    }
    //перевірка по горизонталі
    countToWin = 1;
    x1 = x - 1;
    y1 = y;
    while (x1 >= 0 && document.getElementById(`x ${x1} y ${y1}`).innerHTML === thisplayer) {
        countToWin++;
        if (countToWin == 5) { win() }
        x1--;
    }
    x1 = x + 1;
    while (x1 <= 9 && document.getElementById(`x ${x1} y ${y1}`).innerHTML === thisplayer) {
        countToWin++;
        if (countToWin == 5) { win() }
        x1++;
    };
}

$("#offlinebtn").on('click', () => {
    online = false
    $("#p1").html(player1code)
    $("#p2").html(player2code)
    $("#idhint").hide()
    unblock()
})
$("#loginbtn").on('click', loginuser)
function setSkin(userName) {
    $.ajax({
        async: true,
        url: `/api/skin/getskin?userName=${userName}`,
        method: "GET",
        headers: {
            "content-type": "application/json;odata=verbose"
        },
        success: function (data) {
            $("body").css("background", `linear-gradient(180deg, rgba(0,0,0,1) 0%, ${data.backgroundColor} 100%, ${data.backgroundColor} 100%)`)
            $("td").css("background-color", `${data.fieldColor}`)
            $(".pl1").css("border-color", `${data.borderColor}`)
            $("#pic1").css('background-image', `url(${data.picturePath})`);
            userColor = data.borderColor;
            userPicture = data.picturePath;
        },
        error: function (error) { console.log("error: " + JSON.stringify(error)); }
    }
    );
}
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
            if (data.success) {
                $("#p1").html(player1code)
                $("#nick1").text(l)
                setSkin(l);
                unblock()
                connection.invoke("GetConnectionId", "tur").catch(function (err) {
                    return console.error(err.toString());
                });
            }
            else {
                $("#loginerror").html(data.errorMessage)
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
    let borderclr = $("#borderclr").val()
    let fieldclr = $("#fieldclr").val()
    let bgclr = $("#backgroundclr").val()
    let filesend = $("#pfpinput").prop('files')[0]
    let form = new FormData()
    form.append('ff', filesend)
    form.append('user', l)
    console.log(form)
    if (p === pc) {
        $.ajax({
            async: true,
            url: "/api/account/register",
            method: "POST",
            data: JSON.stringify({
                "UserName": l,
                "Password": p,
                "BorderColor": borderclr,
                "FieldColor": fieldclr,
                "BackgroundColor": bgclr
            }),
            headers: {
                "content-type": "application/json;odata=verbose"
            },
            success: function (data) {
                if (data.success) {
                    $("#p1").html(player1code)
                    $("#nick1").html(l)
                    unblock()
                    $.ajax({
                        async: true,
                        url: "/api/account/addpicture",
                        method: "POST",
                        data: form,
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function () {
                            setSkin(l);
                            connection.invoke("GetConnectionId", "tur").catch(function (err) {
                                return console.error(err.toString());
                            });
                        },
                        error: function (error) { console.log("error: " + JSON.stringify(error)); }
                    }
                    );
                }
                else {
                    $("#registererror").text(data.errorMessage)
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
        <label id = 'idhint'>Your connection Id (give it to another player)</label>
        <div class="cid" id="cid">   
        </div>`
let player2code = `
<div class="pic" id="pic2"></div>
<div class="nick" id="nick2">
    Player 2
        </div>`
let registercode = `
<input id="login" type="text" placeholder="username" required/>
        <input id="password" type="password" placeholder="password" required/>
         <input id="passwordconfirm" type="password" placeholder="password confirm" required/>
            <label>Choose your profile picture</label>
         <input id="pfpinput" type="file" accept=".jpg, .png, .jpeg, .gif, .bmp, .tif, .tiff|image/*">
         <label>Choose your chip's border color</label>
         <input id="borderclr" type="color" value="#0000FF"/>
         <label>Choose your field's color</label>
        <input id="fieldclr" type="color" value="#000000"/>
        <label>Choose your background color</label>
        <input id="backgroundclr" type="color" value="#FF0000"/> 
        <input id="registerbtn" type="button" value="register user"/>
        <input id="loginbtn" type="button" value="back to logging in"/>
        <label id = 'registererror'></label>
        `
let logincode =
    ` <input id="login" type="text" placeholder="username" />
        <input id="password" type="password" placeholder="password" />
        <input id="loginbtn" type="button" value="log in" />
        <input id="registerbtn" type="button" value="registration" />
        <input id="offlinebtn" type="button" value="play locally" />
        <label id = 'loginerror'></label>
        `
function cleanField() {
    for (let i = 0; i < 10; i++) {
        for (let j = 0; j < 10; j++) {
            document.getElementById(`x ${j} y ${i}`).innerHTML = '';
        }
    }
}