using UnityEngine;

public class Pattern : MonoBehaviour {
  [SerializeField] private float weight;
  [SerializeField] private bool isLevelBoss;

  public bool pauseSpawns;
  public bool allEnemiesKilled;

  private void Update()
  {
    if(allEnemiesKilled && isLevelBoss) GameObject.Find("SpawnManager").GetComponent<SpawnManager>().IncreaseLevel();
  }

  public float GetWeight()
  {
    return weight;
  }

}