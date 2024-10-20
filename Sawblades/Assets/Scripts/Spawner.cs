
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnable; //!!! Must have a RigidBody2D !!!    
    
    [SerializeField] private float minTimeInterval = 2f;
    [SerializeField] private float maxTimeInterval = 2.5f;
    private void Start()
    {
        StartCoroutine(Sleep(Random.value * (maxTimeInterval - minTimeInterval) + minTimeInterval));
    }

    private IEnumerator Sleep(float waitTime)
    {
        while (true)
        {
            
            if (Time.timeSinceLevelLoad < 6f)
            {
                minTimeInterval = 3f;
                maxTimeInterval = 5f;
            }else if (Time.timeSinceLevelLoad < 12f)
            {
                minTimeInterval = 1f;
                maxTimeInterval = 3f;
            }else if (Time.timeSinceLevelLoad < 30f)
            {
                minTimeInterval = 0.5f;
                maxTimeInterval = 2f;
            }

            float randomRange = Random.value*4f-2f;
            
            Vector2 randomSpawnPosition = new Vector2(randomRange, 6);

            GameObject spawnableInstance = Instantiate(spawnable, randomSpawnPosition, Quaternion.identity);

            spawnableInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.value < 0.5 ? 3 : -3, -3);

            if (Random.Range(-3, 10) < 0)
            {
                
                randomSpawnPosition = new Vector2(Random.value * randomRange, 6);

                spawnableInstance = Instantiate(spawnable, randomSpawnPosition, Quaternion.identity);

                spawnableInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.value < 0.5 ? 3 : -3, -3);

            }
            yield return new WaitForSeconds(Random.value * (maxTimeInterval - minTimeInterval) + minTimeInterval);
        }
    }
}
