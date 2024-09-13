using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHP : MonoBehaviour
{
     GameObject health;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().RestoreHealth(100);
            Destroy(gameObject);
        }
    }
}
