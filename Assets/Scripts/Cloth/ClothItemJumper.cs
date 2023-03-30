using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemJumper : ClothItemBase
    {
        public float jumpForce = 25f;

        public override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeJumpForce(jumpForce, duration);
        }
    }
}
