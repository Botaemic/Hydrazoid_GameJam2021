using Hydrazoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerCharacter Player { get; set; }
    }
}