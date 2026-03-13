using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] prefabs;

    [Header("Spawn Settings")]
    public float spawnRate = 2f;
    public float minY = -4f;
    public float maxY = 4f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        int index = Random.Range(0, prefabs.Length);

        Vector3 pos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0);

        Instantiate(prefabs[index], pos, Quaternion.identity);
    }
}