using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;

        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        //private Texture2D _defaultTexture;

        //public Texture2D defaultTexture { get { return _defaultTexture; } }

        //private void Awake()
        //{
        //    _defaultTexture = (Texture2D)mesh.materials[0].GetTexture(shaderIdName);
        //}

        private void Start()
        {
            LoadClothFromSave();
        }

        private void LoadClothFromSave()
        {
            // _defaultTexture = SaveManager.Instance.Setup.currentCloth;
            ClothSetup setup = ClothManager.Instance.GetSetupByType(SaveManager.Instance.Setup.currentClothType);
            Player.Instance.ChangeTexture(setup, 4f);
        }

        //[NaughtyAttributes.Button]
        //private void ChangeTexture()
        //{
        //    mesh.materials[0].SetTexture(shaderIdName, texture);
        //}

        public void ChangeTexture(ClothSetup setup)
        {
            mesh.materials[0].SetTexture(shaderIdName, setup.texture);
        }

        public void ResetTexture()
        {
            var defaultTexture = ClothManager.Instance.GetSetupByType(ClothType.DEFAULT).texture;
            mesh.materials[0].SetTexture(shaderIdName, defaultTexture);
        }
    }
}