using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client.Unity;
using DarkRift;
using MultiplayerGameModels;
using TMPro;
using DarkRift.Client;
using UnityEngine.SceneManagement;

public class NetworkingManager : MonoBehaviour
{

    public TMP_InputField username;

    public UnityClient client;
    

    public void Awake() {
        client.MessageReceived += OnMessageReceived;

        GlobalCallbacks.Callback_OnCreateRoom += Callback_OnCreateRoom;
        GlobalCallbacks.Callback_OnJoinRoom += Callback_OnJoinRoomClick;
        GlobalCallbacks.Callback_OnRoomData += Callback_OnRoomData;
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnDestroy() {
        client.MessageReceived -= OnMessageReceived;
        GlobalCallbacks.Callback_OnJoinRoom -= Callback_OnJoinRoomClick;
        GlobalCallbacks.Callback_OnRoomData -= Callback_OnRoomData;
    }

    public void Callback_OnRoomData(int roomID) {
        RoomData roomData = new RoomData();
        roomData.roomID = roomID;

Debug.Log("roomData : " + roomData.roomID);
        using (Message message = Message.Create((ushort) Tags.Tag.ROOM_DATA, roomData)){
            client.SendMessage(message, SendMode.Reliable);
        }
    }
    private void Callback_OnCreateRoom(string name) {
        CreateRoom createRoom = new CreateRoom();
        createRoom.name = name;

        using (Message message = Message.Create((ushort) Tags.Tag.CREATE_ROOM, createRoom)){
            client.SendMessage(message, SendMode.Reliable);
        }
    }
    public void Callback_OnJoinRoomClick(int roomID) {
        JoinRoom joinRoom = new JoinRoom();
        joinRoom.roomID = roomID;

Debug.Log("test "+ roomID);
        using (Message message = Message.Create((ushort) Tags.Tag.JOIN_ROOM, joinRoom)){
            client.SendMessage(message, SendMode.Reliable);
        }
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        using (Message message = e.GetMessage())
        {
            using (DarkRiftReader reader = message.GetReader())
            {
                switch (message.Tag)
                {
                    case (ushort) Tags.Tag.USER_LOGIN_SUCCESSFUL:
                        Debug.Log("USER_LOGIN_SUCCESSFUL");
                        SceneManager.LoadScene("Lobby");
                    break;
                    case (ushort) Tags.Tag.CREATE_ROOM_SUCCESS:

                        CreateRoomSuccessful createRoomSuccessful = reader.ReadSerializable<CreateRoomSuccessful>();
                        
                        Debug.Log("CREATE_ROOM_SUCCESS");
                        GlobalCallbacks.SetOnCreateRoomSuccess(createRoomSuccessful);
                    break;
                    case (ushort) Tags.Tag.JOIN_ROOM_SUCCESS:

                        JoinRoomSuccessful joinRoomSuccessful = reader.ReadSerializable<JoinRoomSuccessful>();
                        if (joinRoomSuccessful.state == (int) Tags.JoinRoomState.SUCCESS) {
                            Debug.Log("JOIN_ROOM_SUCCESS");
                            GlobalCallbacks.SetOnJoinRoomSuccess(joinRoomSuccessful);
                        } else {
                            Debug.Log("JOIN_ROOM_FAILED ROOM IS FULL");
                        }
                    break;
                    case (ushort) Tags.Tag.ROOM_DATA_SUCCESS:

                        RoomDataSuccess roomDataSuccess = reader.ReadSerializable<RoomDataSuccess>();
                        
                        Debug.Log("ROOM_DATA_SUCCESS");
                        GlobalCallbacks.SetOnRomDataSuccess(roomDataSuccess);
                    break;
                }
            }
        }
    }
    public void OnClickTestButton() {

        Chat chat = new Chat();
        chat.chatMessage = "test text new way";
      
        using (Message message = Message.Create((ushort) Tags.Tag.TEST_MESSAGE_2, chat)){
            client.SendMessage(message, SendMode.Reliable);
        }
    }

    public void OnLoginClick() {
        UserLogin userLogin = new UserLogin();
        userLogin.username = username.text;

        using (Message message = Message.Create((ushort) Tags.Tag.USER_LOGIN, userLogin)){
            client.SendMessage(message, SendMode.Reliable);
        }
    }
}
