using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PacStudent"))
        {
            GameManager.instance.AddScore(100);
            Destroy(gameObject);
        }
    }
}
