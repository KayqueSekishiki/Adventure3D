using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colidir com Algo");

        Player p = collision.transform.GetComponent<Player>();
        if (p != null)
        {
            Debug.Log("Colidir com o Player");
            p.AddExternalVelocity(bounceForce, Vector3.up);
        }
    }
}
