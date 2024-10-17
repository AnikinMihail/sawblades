
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
            
            if (Time.time < 6f)
            {
                minTimeInterval = 4f;
                maxTimeInterval = 6f;
            }else if (Time.time < 12f)
            {
                minTimeInterval = 2f;
                maxTimeInterval = 4f;
            }else if (Time.time < 30f)
            {
                minTimeInterval = 0.5f;
                maxTimeInterval = 2.5f;
            }

            int randomRange = Random.Range(-2, 2);
            
            Vector2 randomSpawnPosition = new Vector2(randomRange, 6);

            GameObject spawnableInstance = Instantiate(spawnable, randomSpawnPosition, Quaternion.identity);

            spawnableInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.value < 0.5 ? 3 : -3, -3);

            if (Random.Range(-3, 10) < 0)
            {
                
                randomSpawnPosition = new Vector2(Random.Range(Math.Abs(randomRange)-2, Math.Abs(randomRange)-1), 6);

                spawnableInstance = Instantiate(spawnable, randomSpawnPosition, Quaternion.identity);

                spawnableInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.value < 0.5 ? 3 : -3, -3);

            }
            yield return new WaitForSeconds(Random.value * (maxTimeInterval - minTimeInterval) + minTimeInterval);
        }
    }
}
