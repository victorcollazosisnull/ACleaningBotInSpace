using System.Collections;
using UnityEngine;

public class PointsSpawner : MonoBehaviour
{
    public GameObject pointPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 1.5f;
    public float spawnY = 6.6f;

    [Header("Left Area")]
    public float leftMinX = -3.38f;
    public float leftMaxX = -1.5f;

    [Header("Right Area")]
    public float rightMinX = 1.5f;
    public float rightMaxX = 3.38f;

    [Header("Pattern Settings")]
    public float gap = 0.5f;

    private Vector2[][] patterns;

    void Start()
    {
        InitializePatterns();
        StartCoroutine(SpawnPoints());
    }

    void InitializePatterns()
    {
        Vector2[] horizontal =
        {
            new Vector2(0,0),
            new Vector2(gap,0),
            new Vector2(gap*2,0),
            new Vector2(gap*3,0),
            new Vector2(gap*4,0)
        };

        Vector2[] vertical =
        {
            new Vector2(0,0),
            new Vector2(0,-gap),
            new Vector2(0,-gap*2),
            new Vector2(0,-gap*3),
            new Vector2(0,-gap*4)
        };

        Vector2[] square =
        {
            new Vector2(0,0),
            new Vector2(gap,0),
            new Vector2(gap*2,0),

            new Vector2(0,-gap),
            new Vector2(gap,-gap),
            new Vector2(gap*2,-gap)
        };

        Vector2[] diamond =
        {
            new Vector2(0,0),
            new Vector2(-gap,-gap),
            new Vector2(gap,-gap),
            new Vector2(0,-gap*2)
        };

        Vector2[] zigzag =
        {
            new Vector2(0,0),
            new Vector2(gap,-gap),
            new Vector2(0,-gap*2),
            new Vector2(gap,-gap*3),
            new Vector2(0,-gap*4)
        };

        Vector2[] triangle =
        {
            new Vector2(0,0),

            new Vector2(-gap,-gap),
            new Vector2(gap,-gap),

            new Vector2(-gap*2,-gap*2),
            new Vector2(0,-gap*2),
            new Vector2(gap*2,-gap*2)
        };

        Vector2[] bigBlock =
        {
            new Vector2(-gap,0),
            new Vector2(0,0),
            new Vector2(gap,0),

            new Vector2(-gap,-gap),
            new Vector2(0,-gap),
            new Vector2(gap,-gap),

            new Vector2(-gap,-gap*2),
            new Vector2(0,-gap*2),
            new Vector2(gap,-gap*2)
        };

        Vector2[] bigRectangle =
        {
            new Vector2(-gap*2,0),
            new Vector2(-gap,0),
            new Vector2(0,0),
            new Vector2(gap,0),
            new Vector2(gap*2,0),

            new Vector2(-gap*2,-gap),
            new Vector2(-gap,-gap),
            new Vector2(0,-gap),
            new Vector2(gap,-gap),
            new Vector2(gap*2,-gap)
        };

        Vector2[] frame =
        {
            new Vector2(-gap,0),
            new Vector2(0,0),
            new Vector2(gap,0),

            new Vector2(-gap,-gap),
            new Vector2(gap,-gap),

            new Vector2(-gap,-gap*2),
            new Vector2(0,-gap*2),
            new Vector2(gap,-gap*2)
        };

        patterns = new Vector2[][]
        {
            horizontal,
            vertical,
            square,
            diamond,
            zigzag,
            triangle,
            bigBlock,
            bigRectangle,
            frame
        };
    }

    IEnumerator SpawnPoints()
    {
        while (true)
        {
            bool spawnLeft = Random.value > 0.5f;

            float x;

            if (spawnLeft)
                x = Random.Range(leftMinX, leftMaxX);
            else
                x = Random.Range(rightMinX, rightMaxX);

            Vector3 spawnPos = new Vector3(x, spawnY, 0);

            int randomPattern = Random.Range(0, patterns.Length);

            SpawnPattern(spawnPos, patterns[randomPattern]);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnPattern(Vector3 startPos, Vector2[] pattern)
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            Vector3 pos = startPos + (Vector3)pattern[i];
            Instantiate(pointPrefab, pos, Quaternion.identity);
        }
    }
}