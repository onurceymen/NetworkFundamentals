using NetworkFundamentals;

var webSocketServer = new WebSocketServer("http://localhost:5000/ws/");
await webSocketServer.StartAsync();