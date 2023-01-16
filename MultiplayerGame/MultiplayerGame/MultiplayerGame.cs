using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkRift.Server;
using DarkRift;
using MultiplayerGameModels;
using MultiplayerGame.models;

namespace MultiplayerGame
{
    public class MultiplayerGame : Plugin
    {
        public override bool ThreadSafe => false;

        public override Version Version => new Version(1, 0, 0);
        public Dictionary<int, User> clientIDtoPlayer = new Dictionary<int, User>();
        public Dictionary<int, Room> roomIDtoRoom = new Dictionary<int, Room>();
        public int roomID = 1;
        public int maxNumberOfPlayers = 2;
        public MultiplayerGame(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            ClientManager.ClientConnected += OnClientConnected;
            ClientManager.ClientDisconnected += OnClientDisconnected;
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Console.WriteLine("Player Connected");
            e.Client.MessageReceived += OnMessageReceived;
            if (!clientIDtoPlayer.ContainsKey(e.Client.ID))
            {
                User user = new User();
                user.client = e.Client;
                clientIDtoPlayer.Add(e.Client.ID, user);
            }
        }

        private void OnClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Player Disconnected");

            if (clientIDtoPlayer.ContainsKey(e.Client.ID))
            {
                clientIDtoPlayer.Remove(e.Client.ID);
            }
        }

        private void OnUserLoginSuccessful(IClient client)
        {
            UserLoginSuccessful userLoginSuccessful = new UserLoginSuccessful();

            using (Message message = Message.Create((ushort)Tags.Tag.USER_LOGIN_SUCCESSFUL, userLoginSuccessful))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }
        private void OnCreateRoomSuccessful(CreateRoomSuccessful createRoomSuccessful, IClient client)
        {
            using (Message message = Message.Create((ushort)Tags.Tag.CREATE_ROOM_SUCCESS, createRoomSuccessful))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }

        private void OnJoinRoomSuccessful(JoinRoomSuccessful createRoomSuccessful, IClient client)
        {
            using (Message message = Message.Create((ushort)Tags.Tag.JOIN_ROOM_SUCCESS, createRoomSuccessful))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }
        private void RoomDataSuccess(RoomDataSuccess m, IClient client)
        {
            using (Message message = Message.Create((ushort)Tags.Tag.ROOM_DATA_SUCCESS, m))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }
        private void PlayerJoinRoom(User user, Room room)
        {
            room.numberOfPlayers++;

            user.room = room;

            JoinRoomSuccessful joinRoomSuccessful = new JoinRoomSuccessful();
            joinRoomSuccessful.name = room.name;
            joinRoomSuccessful.roomID = room.id;
            
            if (room.numberOfPlayers > maxNumberOfPlayers)
            {
                joinRoomSuccessful.state = (int) Tags.JoinRoomState.ROOM_IS_FULL;
            } else
            {
                joinRoomSuccessful.state = (int) Tags.JoinRoomState.SUCCESS;
                room.users.Add(user);
            }

            OnJoinRoomSuccessful(joinRoomSuccessful, user.client);
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                using (DarkRiftReader reader = message.GetReader())
                {
                    switch (message.Tag)
                    {
                        case (ushort)Tags.Tag.TEST_MESSAGE_2:
                   
                            Chat chat = reader.ReadSerializable<Chat>();
                            Console.WriteLine(chat.chatMessage);
                  
                            break;
                        case (ushort)Tags.Tag.USER_LOGIN:
                       
                                UserLogin userLogin = reader.ReadSerializable<UserLogin>();

                                if (clientIDtoPlayer.ContainsKey(e.Client.ID))
                                {
                                    User user = clientIDtoPlayer[e.Client.ID];
                                    user.username = userLogin.username;
                                }

                                Console.WriteLine("--------------------");
                                foreach (KeyValuePair<int, User> pair in clientIDtoPlayer)
                                {
                                    Console.WriteLine(pair.Value.username);
                                }
                                Console.WriteLine("--------------------");


                                OnUserLoginSuccessful(e.Client);


                            break;

                        case (ushort)Tags.Tag.CREATE_ROOM:

                            CreateRoom createRoom = reader.ReadSerializable<CreateRoom>();
                            Room room = new Room();
                            room.name = createRoom.name;
                            room.id = roomID;

                            roomIDtoRoom.Add(room.id, room);

                            PlayerJoinRoom(clientIDtoPlayer[e.Client.ID], room);

                            foreach (KeyValuePair<int, User> pair in clientIDtoPlayer)
                            {
                                Room userRoom = pair.Value.room;

                                if (userRoom == null)
                                {
                                    CreateRoomSuccessful createRoomSuccessful = new CreateRoomSuccessful();
                                    createRoomSuccessful.name = room.name;
                                    createRoomSuccessful.roomID = room.id;

                                    OnCreateRoomSuccessful(createRoomSuccessful, pair.Value.client);
                                }
                            }


                            roomID++;

                            break;

                        case (ushort)Tags.Tag.JOIN_ROOM:

                            JoinRoom joinRoom = reader.ReadSerializable<JoinRoom>();

                            if (roomIDtoRoom.ContainsKey(joinRoom.roomID))
                            {
                                Room userRoom = roomIDtoRoom[joinRoom.roomID];
                                PlayerJoinRoom(clientIDtoPlayer[e.Client.ID], userRoom);
                            }
                            break;
                        case (ushort)Tags.Tag.ROOM_DATA:

                            RoomData roomData = reader.ReadSerializable<RoomData>();

                            if (roomIDtoRoom.ContainsKey(roomData.roomID))
                            {
                               Room userRoom = roomIDtoRoom[roomData.roomID];

                                string username1 = "";
                                string username2 = "";
                                bool isRoomFull = false;
                                if (userRoom.users.Count > 1)
                                {
                                    username1 = userRoom.users[0].username;
                                    username2 = userRoom.users[1].username;
                                    isRoomFull = true;
                                } else
                                {
                                    username1 = userRoom.users[0].username;
                                }

                                Console.WriteLine("username1: " + username1);
                                Console.WriteLine("username2: " + username2);
                                Console.WriteLine("isRoomFull: " + isRoomFull);
                                foreach (User user in userRoom.users)
                                {
                                    RoomDataSuccess roomDataSuccess = new RoomDataSuccess();
                                    roomDataSuccess.player1Name = username1;
                                    roomDataSuccess.player2Name = username2;
                                    roomDataSuccess.isRoomFull = isRoomFull;

                                    RoomDataSuccess(roomDataSuccess, user.client);
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
