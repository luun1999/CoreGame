using System.Collections;
using System.Collections.Generic;
using System;

namespace VNEagleEngine.Events.Player
{
    public static class PlayerEvents
    {
        public static Action<int> PLAYER_GOT_DAMAGE;
        public static Action PLAYER_HEAL;
        public static Action PLAYER_GAME_OVER;
    }
}

