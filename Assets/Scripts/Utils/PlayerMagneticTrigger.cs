using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagneticTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableCoin i = other.transform.GetComponent<ItemCollectableCoin>();
        if (i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }

    }
}
