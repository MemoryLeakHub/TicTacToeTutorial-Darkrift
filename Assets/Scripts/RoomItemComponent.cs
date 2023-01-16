using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoomItemComponent : MonoBehaviour
{
    public TMP_Text room;

    public int roomID;
    public void SetRoom(string name, int id) {

        SetRoomID(id);
        SetRoomName(name);
    }
    public void SetRoomName(string name) {
        room.text = name;
    }
    public void SetRoomID(int id) {
        roomID = id;
    }

    public void OnRoomClick() {

        GlobalCallbacks.SetOnJoinRoom(roomID);
    }
}
