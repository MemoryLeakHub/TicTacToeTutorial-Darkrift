using System.Collections;
using System.Collections.Generic;
using MultiplayerGameModels;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RoomDataComponent : MonoBehaviour
{

    public TMP_Text user1;
    public TMP_Text user2;
    public Button startGame;

    // Start is called before the first frame update
    void Start()
    {
        GlobalCallbacks.SetOnRoomData(GlobalCallbacks.roomID);
    }

    public void Awake() {
        GlobalCallbacks.Callback_OnRomDataSuccess += Callback_OnRomDataSuccess;
    }

    void OnDestroy()
    {
        GlobalCallbacks.Callback_OnRomDataSuccess -= Callback_OnRomDataSuccess;
    }

    private void Callback_OnRomDataSuccess(RoomDataSuccess message) {

        user1.text = message.player1Name;
        user2.text = message.player2Name;

        if (message.isRoomFull) {
            startGame.interactable = true;
        }
    }
}
