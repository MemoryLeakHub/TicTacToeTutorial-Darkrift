﻿
using DarkRift.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerGame.models
{
    public class Room
    {
        public int id { get; set; }
        public String name { get; set; }

        public List<User> users = new List<User>();

        public int numberOfPlayers = 0;

    }
}
