using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

namespace Game.Manager
{
    public class VFXManager : Singleton<VFXManager>
    {
        public List<VFXSetup> vfxList;

        public static void PlayVFX(VFXType type, Vector3 position)
        {
            var aux = Instance.vfxList.Find(vfx => vfx.vfxType == type);
            aux.emissor.transform.position = position;
            aux.emissor.Play();
        }
    }

    public enum VFXType
    {
        JUMP,
        COIN,
        POWER_UP
    }

    [System.Serializable]
    public class VFXSetup
    {
        public VFXType vfxType;
        public ParticleSystem emissor;

        public VFXSetup(VFXType type, ParticleSystem emissor)
        {
            this.vfxType = type;
            this.emissor = emissor;
        }
    }
}

