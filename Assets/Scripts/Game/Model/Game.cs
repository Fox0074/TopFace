using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FizerFox.Game
{
    public class Game
    {
        public bool Started { get; set; }
        public bool Finished { get; set; }
        public LevelData Level { get; set; }
        public int Stars { get; set; }
        public int MissesCount { get; set; }

    }
}