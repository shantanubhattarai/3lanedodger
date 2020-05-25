using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
  public string name;
  public List<Transform> patterns;
  public float delay;
  public float budget;
  public float numberToSpawn;
  public Transform levelBoss;
  public int difficultyMultiplier;
}

public class SpawnManager : MonoBehaviour
{
  [SerializeField] private List<Wave> waves;
  private Dictionary<int, Wave> wavesPerLevel;

  private List<Transform> patternsToSpawn;
  private int currentLevel = 0;
  private List<Transform> spawnedObjects;

  private void Start()
  {
    wavesPerLevel = new Dictionary<int, Wave>();
    patternsToSpawn = new List<Transform>();
    spawnedObjects = new List<Transform>();

    for(int i = 0; i < waves.Count; i++){
      wavesPerLevel.Add(i, waves[i]);
    }

    StartLevel();
  }

  private void StartLevel()
  {
    foreach(Transform spawnedObject in spawnedObjects)
    {
      Destroy(spawnedObject.gameObject);
    }

    patternsToSpawn.Clear();
    CalculatePatternsForLevel();
    StartCoroutine(PatternSpawner());
  }

  public void IncreaseLevel()
  {
    print("new Level");
    currentLevel++;
    StartLevel();
  }

  private IEnumerator PatternSpawner()
  {
    foreach(Transform patternToSpawn in patternsToSpawn)
      {
        if(wavesPerLevel[currentLevel].delay > 0)
          yield return new WaitForSeconds(wavesPerLevel[currentLevel].delay);

        Transform newObject = Instantiate(patternToSpawn, transform.position, Quaternion.identity).transform;
        spawnedObjects.Add(newObject);

        if(patternToSpawn.GetComponent<Pattern>().pauseSpawns == true)
          yield return new WaitUntil(() => patternToSpawn.GetComponent<Pattern>().allEnemiesKilled == true);

        yield return null;
      }
  }

  private void CalculatePatternsForLevel()
  {
    float weightSum = 0;

    for(int i = 0; i < wavesPerLevel[currentLevel].numberToSpawn; i++)
    {
      int randomPatternIndex = Random.Range(0, wavesPerLevel[currentLevel].patterns.Count) * wavesPerLevel[currentLevel].difficultyMultiplier;
      weightSum += wavesPerLevel[currentLevel].patterns[randomPatternIndex].GetComponent<Pattern>().GetWeight();

      if(weightSum > wavesPerLevel[currentLevel].budget)
      {
        weightSum -= wavesPerLevel[currentLevel].patterns[randomPatternIndex].GetComponent<Pattern>().GetWeight();
      }
      else
      {
        patternsToSpawn.Add(wavesPerLevel[currentLevel].patterns[randomPatternIndex]);
      }
    }

    if(wavesPerLevel[currentLevel].levelBoss != null) patternsToSpawn.Add(wavesPerLevel[currentLevel].levelBoss);
  }

}