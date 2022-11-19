using System.Collections;
using System.Collections.Generic;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class TestObject : MonoBehaviour {
  private QSocket socket;

  void Start () {
    
    socket = IO.Socket ("http://localhost:5000");
        
    socket.On (QSocket.EVENT_CONNECT, () => {
      Debug.Log ("Connected");
      socket.Emit ("chat", "test");
        socket.Emit("aaa", "aaa");
    });

    socket.On ("chat", data => {
      Debug.Log ("data : " + data);
    });
        
     
  }

  private void OnDestroy () {
    socket.Disconnect ();
  }
}