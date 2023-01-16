using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerGameModels
{
    public class Tags
    {
        public enum Tag
        {
            USER_LOGIN = 1,
            USER_LOGIN_SUCCESSFUL = 2,

            CREATE_ROOM = 8,
            CREATE_ROOM_SUCCESS = 9,

            JOIN_ROOM = 13,
            JOIN_ROOM_SUCCESS = 14,

            ROOM_DATA = 21,
            ROOM_DATA_SUCCESS = 22,

            TEST_MESSAGE = 4,
            TEST_MESSAGE_2 = 5
        }

        public enum JoinRoomState
        {
            SUCCESS,
            ROOM_IS_FULL
        }
    }
}
