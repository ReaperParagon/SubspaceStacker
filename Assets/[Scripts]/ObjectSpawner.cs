using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private LayerMask platformLayerMask;

    [SerializeField]
    private GameObject spawnableObject;

    [SerializeField]
    private float timer = 1.0f;

    private BoxCollider spawnArea;
    private IEnumerator spawnCoroutine;

    void OnEnable()
    {
        spawnArea = GetComponent<BoxCollider>();

        spawnCoroutine = SpawnObjectCoroutine();
        StartCoroutine(spawnCoroutine);
    }

    private void OnDisable()
    {
        if (spawnCoroutine == null)
            return;

        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    /// Functions ///

    private Vector3 GetRandomPointInSpawnArea()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        return new Vector3(x, spawnArea.bounds.center.y, z);
    }

    private Vector3 GetSpawnPoint()
    {
        // Get a point inside the box collider
        Vector3 point = GetRandomPointInSpawnArea();

        // Raycast check against platform
        if (Physics.Raycast(point, Vector3.down, 100.0f, platformLayerMask))
            return point;

        return GetSpawnPoint();
    }


    private void SpawnObject()
    {
        GameObject obj = Instantiate(spawnableObject, transform, true);
        obj.transform.position = GetSpawnPoint();
    }

    /// Coroutines ///

    IEnumerator SpawnObjectCoroutine()
    {
        yield return new WaitForSeconds(timer);
        SpawnObject();

        spawnCoroutine = SpawnObjectCoroutine();
        StartCoroutine(spawnCoroutine);
    }

}
