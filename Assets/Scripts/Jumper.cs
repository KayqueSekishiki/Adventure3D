using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public SFXType sfxJump;
    public float bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.transform.GetComponent<Player>();

        if (p != null)
        {
            IAddExternalVelocity externalVelocity = collision.transform.GetComponent<IAddExternalVelocity>();
            if (externalVelocity != null)
            {
                externalVelocity.AddExternalVelocity(bounceForce, Vector3.up);
                PlaySFXJump();
            }
        }
    }

    private void PlaySFXJump()
    {
        SFXPool.Instance.PlaySFX(sfxJump);
    }
}
