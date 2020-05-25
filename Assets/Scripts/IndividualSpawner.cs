using UnityEngine;
using System.Collections.Generic;

public class IndividualSpawner : MonoBehaviour
{
  [SerializeField] private List<Transform> objectsToSpawn;
  [SerializeField] private int numberOfObjects;
  [SerializeField] private float timeBetweenSpawns;
  [SerializeField] private List<Transform> lanes;

  private float timer = 0;
  private int laneIndex = 0;
  private int currentObjectIndex;
  private float lastSpawnTime = 0f;
  private int numberOfObjectsSpawned = 0;

  private void Start()
  {
    lastSpawnTime = Time.time;
  }

  private void Update()
  {
    if(Time.time > lastSpawnTime + timeBetweenSpawns && numberOfObjectsSpawned < numberOfObjects)
    {
      numberOfObjectsSpawned++;

      Vector3 positionToSpawn = new Vector3(lanes[laneIndex].position.x, transform.position.y, 0f);
      Transform currentObject = Instantiate(objectsToSpawn[currentObjectIndex], positionToSpawn, objectsToSpawn[currentObjectIndex].transform.rotation);

      lastSpawnTime = Time.time;
      laneIndex = Random.Range(0, lanes.Count);

      if(currentObjectIndex < objectsToSpawn.Count - 1) currentObjectIndex ++;
      else currentObjectIndex = 0;
    }
  }

  public void ReduceSpawnedObjectCount()
  {
    numberOfObjectsSpawned--;
  }

}