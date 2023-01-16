using System.Collections;
using System.Collections.Generic;
using MultiplayerGameModels;
using UnityEngine;

public static class GlobalCallbacks 
{
   
    public static int roomID = -1;

   public delegate void OnCreateRoom(string name);
   public static OnCreateRoom Callback_OnCreateRoom;
   public static void SetOnCreateRoom(string name) {
       if (Callback_OnCreateRoom != null) {
           Callback_OnCreateRoom(name);
       }
   }

   
   public delegate void OnCreateRoomSuccess(CreateRoomSuccessful message);
   public static OnCreateRoomSuccess Callback_OnCreateRoomSuccess;
   public static void SetOnCreateRoomSuccess(CreateRoomSuccessful message) {
       if (Callback_OnCreateRoomSuccess != null) {
           Callback_OnCreateRoomSuccess(message);
       }
   }

   
   public delegate void OnJoinRoom(int id);
   public static OnJoinRoom Callback_OnJoinRoom;
   public static void SetOnJoinRoom(int id) {
       if (Callback_OnJoinRoom != null) {
           Callback_OnJoinRoom(id);
       }
   }


   public delegate void OnJoinRoomSuccess(JoinRoomSuccessful message);
   public static OnJoinRoomSuccess Callback_OnJoinRoomSuccess;
   public static void SetOnJoinRoomSuccess(JoinRoomSuccessful message) {
       if (Callback_OnJoinRoomSuccess != null) {
           Callback_OnJoinRoomSuccess(message);
       }
   }
   
   public delegate void RoomData(int id);
   public static RoomData Callback_OnRoomData;
   public static void SetOnRoomData(int id) {
       if (Callback_OnRoomData != null) {
           Callback_OnRoomData(id);
       }
   }
   public delegate void OnRomDataSuccess(RoomDataSuccess message);
   public static OnRomDataSuccess Callback_OnRomDataSuccess;
   public static void SetOnRomDataSuccess(RoomDataSuccess message) {
       if (Callback_OnRomDataSuccess != null) {
           Callback_OnRomDataSuccess(message);
       }
   }
}
