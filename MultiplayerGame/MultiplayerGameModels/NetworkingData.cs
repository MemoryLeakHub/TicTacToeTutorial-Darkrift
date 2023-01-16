using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerGameModels
{
    public class Chat : IDarkRiftSerializable
    {
        public String chatMessage;
        public void Deserialize(DeserializeEvent e)
        {
            chatMessage = e.Reader.ReadString();

        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(chatMessage);
        }
    }
    public class UserLogin : IDarkRiftSerializable
    {
        public String username;
        public void Deserialize(DeserializeEvent e)
        {
            username = e.Reader.ReadString();

        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(username);
        }
    }
    public class UserLoginSuccessful : IDarkRiftSerializable
    {
        public void Deserialize(DeserializeEvent e)
        {

        }
        public void Serialize(SerializeEvent e)
        {
        }
    }
    public class CreateRoom : IDarkRiftSerializable
    {
        public String name;
        public void Deserialize(DeserializeEvent e)
        {
            name = e.Reader.ReadString();
        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(name);
        }
    }
    public class CreateRoomSuccessful : IDarkRiftSerializable
    {
        public String name;
        public int roomID;
        public void Deserialize(DeserializeEvent e)
        {
            name = e.Reader.ReadString();
            roomID = e.Reader.ReadInt32();
        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(name);
            e.Writer.Write(roomID);
        }
    }
    public class JoinRoomSuccessful : IDarkRiftSerializable
    {
        public String name;
        public int roomID;
        public int state;
        public void Deserialize(DeserializeEvent e)
        {
            name = e.Reader.ReadString();
            roomID = e.Reader.ReadInt32();
            state = e.Reader.ReadInt32();
        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(name);
            e.Writer.Write(roomID);
            e.Writer.Write(state);
        }
    }
    public class JoinRoom : IDarkRiftSerializable
    {
        public int roomID;
        public void Deserialize(DeserializeEvent e)
        {
            roomID = e.Reader.ReadInt32();
        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(roomID);
        }
    }
    public class RoomData : IDarkRiftSerializable
    {
        public int roomID;
        public void Deserialize(DeserializeEvent e)
        {
            roomID = e.Reader.ReadInt32();
        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(roomID);
        }
    }
    public class RoomDataSuccess : IDarkRiftSerializable
    {
        public string player1Name;
        public string player2Name;
        public bool isRoomFull;
        public void Deserialize(DeserializeEvent e)
        {
            player1Name = e.Reader.ReadString();
            player2Name = e.Reader.ReadString();
            isRoomFull = e.Reader.ReadBoolean();
        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(player1Name);
            e.Writer.Write(player2Name);
            e.Writer.Write(isRoomFull);
        }
    }
}
