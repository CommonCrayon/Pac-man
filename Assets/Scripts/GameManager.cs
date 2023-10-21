using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private HudManager hudManager;

    public double Score = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // If an instance already exists, destroy the new one to maintain the singleton pattern.
            Destroy(gameObject);
        }
    }

    public void AddScore(double value)
    {
        Score += value;
        hudManager.UpdateScore(Score.ToString());
    }
}
