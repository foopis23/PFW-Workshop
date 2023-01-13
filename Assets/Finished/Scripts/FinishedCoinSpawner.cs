using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedCoinSpawner : MonoBehaviour
{
    public AnimationCurve fallVelocityOverTime;
    public AnimationCurve spawnRateOverTime;
    public GameObject coinPrefab;

    public float spawnWidth;

    public float minFallVelocity = 1.0f;
    public float maxFallVelocity = 3.0f;
    public float minSpawnRate = 4.0f;
    public float maxSpawnRate = 1.5f;
    public float minTime = 0;
    public float maxTime = 120;

    private Camera cam;
    private float _lastSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.right * spawnWidth);
        Debug.DrawRay(transform.position, transform.right * -1 * spawnWidth);

        var time = Mathf.Clamp(Time.time / maxTime, 0, 1);
        var spawnRate = spawnRateOverTime.Evaluate(time) * (maxSpawnRate - minSpawnRate) + minSpawnRate;
        var fallVelocity = fallVelocityOverTime.Evaluate(time) * (maxFallVelocity - minFallVelocity) + minFallVelocity;

        if (Time.time - _lastSpawn >= spawnRate)
        {
            _lastSpawn = Time.time;

            var dx = Random.Range(spawnWidth * -1, spawnWidth);

            GameObject newObject = Instantiate(coinPrefab);
            newObject.transform.position = gameObject.transform.position + new Vector3(dx, 0, 0);
            newObject.GetComponent<FinishedCoinController>().fallVelocity = fallVelocity;
        }
    }
}
