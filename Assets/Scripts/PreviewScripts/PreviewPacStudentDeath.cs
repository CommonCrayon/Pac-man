using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPacStudentDeath : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().Play("PacStudentDeath");
    }
}
