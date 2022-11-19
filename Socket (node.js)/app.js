const express = require('express')
var net = require('net')
const app = express()
const port = 3000
var server = net.createServer(function (socket) {

    // data event： 到收到資料傳輸時觸發事件 ， argument 為對象傳輸的物件
    socket.on('data', function (data) {

        // write event: 傳輸資料的事件
        socket.write('hi', function () {
            console.log('server:收到 client端 傳輸資料為' + data + '回應 hi')
        })

    })
})


app.post('/food_1', function (req, res) {
    console.log(req.body);
});

app.listen(port, () => {
    console.log(`Example app listening at http://localhost:${port}`)
})



//使用 connection 事件
server.on('connection', function () {
    console.log('server端：收到 client 端連線請求，並通知 client 端可以開始傳送資料')

})


//用 net method listen() 方法讓 本機的 5000 port  給此 TCP server 使用
server.listen(5000, function () {
    console.log('TCP Server start')
})

