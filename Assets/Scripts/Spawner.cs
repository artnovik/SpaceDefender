using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("GameObject which will spawn")]
    [SerializeField]
    private GameObject _spawnObject;

    //[Tooltip("List of spawned objects")]
    //private List<GameObject> _spawnedObjectsList;

    [Tooltip("Count of spawned GameObject")]
    [SerializeField]
    private uint _spawnCount;

    [Tooltip("Delay between spawning in seconds")]
    [SerializeField]
    private float _spawnDelay;

    [Tooltip("Start position for first spawned GameObject")]
    [SerializeField]
    private Vector3 _spawnStartPosition;

    [Tooltip("Distance in x,y,z between Spawn Objects")]
    [SerializeField]
    private Vector3 _spawnDistanceBetweenObjects;

    [Tooltip("Deleted count of spawned objects")]
    private uint _deletedCount;

    //Scale, rotation...

	private void Start()
	{
		SpawnCall ();
	}

    /// <summary>
    /// Calls Spawn method
    /// </summary>
    public void SpawnCall()
    {
        if (_spawnObject != null)
        {
            StartCoroutine(SpawnCoroutine(_spawnObject, _spawnCount, _spawnDelay, /*_firstInCenter,*/ _spawnStartPosition, _spawnDistanceBetweenObjects));
        }
        else
        {
            Debug.LogWarning("Assign GameObject to SpawnObject in Inspector!");
        }
    }

    /// <summary>
    /// Removes all spawned GameObjects
    /// </summary>
    public void Clear()
    {
        var spawnedObjects = GameObject.FindObjectsOfType(_spawnObject.GetType());

        foreach (var spawnedObj in spawnedObjects)
        {
            if (spawnedObj.name == _spawnObject.name + "(Clone)")
            {
                Destroy(spawnedObj);
                _deletedCount += 1;
            }
        }

        Debug.Log("Removed " + _deletedCount + " " + _spawnObject.name + " objects.");
        _deletedCount = 0;
    }

    /// <summary>
    /// Spawns GameObjects (customizable)
    /// </summary>
    /// <param name="spawnObject"></param>
    /// <param name="spawnCount"></param>
    /// <param name="spawnDelay"></param>
    /// <param name="firstInCenter"></param>
    /// <param name="spawnStartPosition"></param>
    /// <param name="spawnDistanceBetweenObjects"></param>
    /// <returns></returns>
    public IEnumerator SpawnCoroutine(GameObject spawnObject, uint spawnCount, float spawnDelay, /*bool firstInCenter,*/
        Vector3 spawnStartPosition, Vector3 spawnDistanceBetweenObjects)
    {
        for (int i = 0; i < spawnCount; i++)
        {

            GameObject.Instantiate(spawnObject, new Vector3(spawnStartPosition.x + spawnDistanceBetweenObjects.x * i, spawnStartPosition.y + spawnDistanceBetweenObjects.y * i, spawnStartPosition.z + spawnDistanceBetweenObjects.z * i), spawnObject.transform.rotation);

            if (i == spawnCount - 1)
            {
                Debug.Log("GameObject: " + spawnObject.name + ". Count: " + spawnCount + ". SpawnDelay: " + spawnDelay + " seconds.");
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
