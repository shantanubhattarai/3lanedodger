using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
  [SerializeField] private GameObject objectToSpawn;

  private void Start()
  {
    //transform.parent.GetComponent<TimerActivateSpawns>().allTimerCalls += SpawnObject;
    SpawnObject();
  }

  private void SpawnObject()
  {
    Instantiate(objectToSpawn, transform.position, Quaternion.Euler(0f, 0f, 180f));
  }

}