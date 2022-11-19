const ms = require('ms');

const app = require('express')();
const http = require('http').createServer(app);
const io = require('socket.io')(http);
var user = [];
let player_a = false;
let player_b = false;
// app.get('/', (req, res) => {
//     // res.sendFile(__dirname + '/index.html');
// });
let count = 0;
io.on('connection', (socket) => {

    if (player_a == true) {
        io.emit('A_check', 'A')
    }
    if (player_b == true) {
        io.emit('B_check', 'B')
    }
    console.log('a user connected');

    socket.on('user_data', (msg) => {

        check_update(JSON.parse(msg));
        console.log(compare());
    })
    socket.on("user_rank", () => {
        io.emit('user_rank', user);
        console.log(JSON.stringify(user).toString());
    });

    socket.on("select_A", (msg) => {
        if (msg == 'A') {
            player_a = true;
            io.emit('A_check', 'A');
        }
    });

    socket.on("select_B", (msg) => {
        if (msg == 'B') {
            player_b = true;
            io.emit('B_check', 'B');
        }
    });

    socket.on("GoPressed", (msg) => {
        if (msg == 'GameStart') {
            io.emit("GoPressed", "GameStart");
        }
    });

    socket.on("A=>B", (msg) => {
        socket.emit("A=>B", msg);
    });

    socket.on('disconnect', () => {
        console.log('user disconnected');
    });
});
http.listen(5000, () => {
    console.log('Connected at 3000');
});


function check_update(msg) {
    same = false;
    for (let i = 0; i < user.length; i++) {
        if (msg.name == user[i].name) {
            user[i].score = msg.score;
            same = true;
        }
    }
    if (same == false) {
        user.push(msg);
    }

}

function compare() {

    user.sort(function (a, b) {
        // boolean false == 0; true == 1
        return a.score - b.score;
    });
    return user;
    // 順序依序會是 Mike -> Jimmy -> Judy
}
//JSON.stringify(user)
