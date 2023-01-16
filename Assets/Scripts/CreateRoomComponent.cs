using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MultiplayerGameModels;
using UnityEngine.SceneManagement;

public class CreateRoomComponent : MonoBehaviour
{
  
    public GameObject roomList;
    public GameObject roomPrefab;
    public TMP_InputField roomName;

    public void Awake() {
        Application.runInBackground = true;
        GlobalCallbacks.Callback_OnCreateRoomSuccess += Callback_OnCreateRoomSuccess;
        GlobalCallbacks.Callback_OnJoinRoomSuccess += Callback_OnJoinRoomSuccess;
    }

    public void OnDestroy() {
        GlobalCallbacks.Callback_OnCreateRoomSuccess -= Callback_OnCreateRoomSuccess;
        GlobalCallbacks.Callback_OnJoinRoomSuccess -= Callback_OnJoinRoomSuccess;
    }

    private void Callback_OnJoinRoomSuccess(JoinRoomSuccessful message) {
        GlobalCallbacks.roomID = message.roomID;
        SceneManager.LoadScene("Game");
    }

    private void Callback_OnCreateRoomSuccess(CreateRoomSuccessful message) {
        CreateRoom(message);
    }

    public void CreateRoomOnClick() {

        GlobalCallbacks.SetOnCreateRoom(roomName.text);
    }

    private void CreateRoom(CreateRoomSuccessful message) {
        GameObject room = Instantiate(roomPrefab, roomList.transform);

        RoomItemComponent roomItemComponent = (RoomItemComponent) room.GetComponent<RoomItemComponent>();
        roomItemComponent.SetRoom(message.name, message.roomID);
    }
}
