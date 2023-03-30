using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;


namespace Cloth
{
    public enum ClothType
    {
        SPEED,
        STRONG,
        JUMPER
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothtype == clothType);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothtype;
        public Texture2D texture;
    }

}