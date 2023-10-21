using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PacStudent"))
        {
            GameManager.instance.KillPacStudent();

        }
    }
}
