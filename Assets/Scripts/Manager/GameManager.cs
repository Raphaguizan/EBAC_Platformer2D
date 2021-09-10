using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Game.Util;

namespace Game.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public void ChangeSnapShot(AudioMixerSnapshot snapShot)
        {
            snapShot.TransitionTo(.1f);
        }
    }
}

