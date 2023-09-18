using UnityEngine;


/// <summary>
/// Play PacStudent's Death at game start
/// </summary>
public class PreviewPacStudentDeath : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().Play("PacStudentDeath");
    }
}
