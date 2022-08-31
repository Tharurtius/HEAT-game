/* File SpawnManager.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 30 August 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private GameObject objContainer;
    [SerializeField] private float spawnRate = 3f;

    public Color GizmosColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);

    void Start()
    {
        StartCoroutine(SpawnObj());
    }

    private IEnumerator SpawnObj()
    {
        while (true)
        {
            // Position logic here
            var spawnOrigin = new Vector3(0, 0, 0);
            var spawnRange = new Vector3(0, 0, 0);
            var spawnRandomRange = new Vector3(0, 0, 0);

            spawnOrigin = objContainer.transform.position;
            spawnRange = objContainer.transform.localScale / 2.0f;
            spawnRandomRange = new Vector3(Random.Range(-spawnRange.x, spawnRange.x), 0, Random.Range(-spawnRange.z, spawnRange.z)); // Don't randomly spawn on Y-axis
            Vector3 randomCoordinate = spawnOrigin + spawnRandomRange;

            // Spawn the object then parent it to the container
            var newObj = Instantiate(objPrefab, randomCoordinate, Quaternion.identity);
            newObj.transform.parent = objContainer.transform;
            yield return new WaitForSeconds(spawnRate);
        }

    }

    // Editor visualisation
    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
