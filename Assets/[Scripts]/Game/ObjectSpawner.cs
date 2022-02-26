using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public delegate void OnObjectSpawnEvent();
    public static event OnObjectSpawnEvent OnObjectSpawn;

    [SerializeField]
    private LayerMask platformLayerMask;

    [SerializeField]
    private Collection spawnableObjects;

    [SerializeField]
    private float timer = 1.0f;

    private int currentIndex = -1;
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

    private Quaternion GetRandomRotation()
    {
        float rotation = Random.Range(0.0f, 360.0f);

        return Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    private void SpawnObject()
    {
        GameObject prefab = spawnableObjects.GetObject();

        GameObject obj = Instantiate(prefab, transform, true);
        obj.transform.position = GetSpawnPoint();
        obj.transform.rotation = GetRandomRotation();

        OnObjectSpawn?.Invoke();
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
