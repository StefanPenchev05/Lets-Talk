import * as signalR from "@microsoft/signalr";

export default class SignalRConnection{
    private connection: signalR.HubConnection;

    constructor(url: string){
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5295" + url)
            .build();
    }

    async start(){
        try{
            await this.connection.start();
            console.log("SignalR is connected");
        }catch(err){
            console.log("Error during connecting signalR " + err);
        }
    }

    JoinRoom(roomId: string){
        this.connection.invoke("JoinRoom", roomId)
            .catch(err => {
                console.log(err);
            });
    }
}