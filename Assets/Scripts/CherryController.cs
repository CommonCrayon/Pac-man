using System.Collections;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] private GameObject Cherry;
    private Vector3 cherryPosition = Vector3.zero;

    private float spawnInterval = 10.0f; // Time interval in seconds and lerp speed


    private void Start()
    {
        InvokeRepeating("Spawn", 0.0f, spawnInterval);
    }

    /// <summary>
    /// Spawns the Cherry at a random side of the screen
    /// </summary>
    private void Spawn()
    {
        // Calculate the screen boundaries
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize;

        // To pick random side
        int randomSide = Random.Range(0, 4);

        if (randomSide == 0)
        {
            cherryPosition = new Vector3(Random.Range(-screenWidth, screenWidth), screenHeight + 1, 0.0f);

        }
        else if (randomSide == 1)
        {
            cherryPosition = new Vector3(Random.Range(-screenWidth, screenWidth), -screenHeight - 1, 0.0f);
        }
        else if (randomSide == 2)
        {
            cherryPosition = new Vector3(screenWidth + 1, Random.Range(-screenHeight, screenHeight), 0.0f);
        }
        else
        {
            cherryPosition = new Vector3(-screenWidth - 1, Random.Range(-screenHeight, screenHeight), 0.0f);
        }

        GameObject instantiatedCherry = Instantiate(Cherry, cherryPosition, Quaternion.identity);

        // Start the interpolation from the current position to the target position
        StartCoroutine(LerpCherry(new Vector3(cherryPosition.x * -1, cherryPosition.y * -1, 0.0f), instantiatedCherry));
    }


    /// <summary>
    /// Lerps to the other side of the screen 
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <param name="instantiatedCherry"></param>
    /// <returns></returns>
    private IEnumerator LerpCherry(Vector3 targetPosition, GameObject instantiatedCherry)
    {
        float elapsedTime = 0;

        Vector3 initialPosition = instantiatedCherry.transform.position;

        while (elapsedTime < spawnInterval)
        {
            if (instantiatedCherry == null) yield break;

            instantiatedCherry.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / spawnInterval);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (instantiatedCherry != null)
        {
            Destroy(instantiatedCherry);
        }
    }

}
