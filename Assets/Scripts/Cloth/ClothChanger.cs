using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public string shaderIdName = "_EmissionMap";

        public ClothItemSpeed clothSpeed;
        public ClothItemStrong clothStrong;
        public ClothItemJumper clothJumper;


        private void Start()
        {
            LoadClothFromSave();
        }

        private void LoadClothFromSave()
        {
            ClothSetup setup = ClothManager.Instance.GetSetupByType(SaveManager.Instance.Setup.currentClothType);

            if (setup.clothType == ClothType.SPEED)
            {
                clothSpeed.Collect();
            }
            else if (setup.clothType == ClothType.STRONG)
            {
                clothStrong.Collect();
            }
            else if (setup.clothType == ClothType.JUMPER)
            {
                clothJumper.Collect();
            }
            else
            {
                Player.Instance.ChangeTexture(setup, 4f);
            }
        }



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