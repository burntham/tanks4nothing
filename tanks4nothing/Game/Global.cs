﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tanks4nothing.Game.Sound;

namespace tanks4nothing.Game
{
    //Global variables and stuffs - this includes 
    public class Global
    {
        public static AudioManager AudioPlayer;
        public static int[] PlayerScores;
        public static float[] pitch = {0.0f,0.0f,0.0f};
        public static int timeLeft=90;
    }
}
