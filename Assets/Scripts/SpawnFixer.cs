using UnityEngine;

public class SpawnFixer : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning("SpawnFixer: Nenhum spawnPoint definido!");
        }
    }
}