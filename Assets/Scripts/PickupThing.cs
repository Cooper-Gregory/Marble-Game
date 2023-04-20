using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupThing : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onThingAcquired;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onThingAcquired.Raise(this, null);
            Destroy(gameObject);
        }
    }
}
