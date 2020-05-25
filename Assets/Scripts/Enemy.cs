using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] private int scoreIncrement;
  [SerializeField] private ParticleSystem deathAnim;
  [SerializeField] private ParticleSystem damageAnim;
  [SerializeField] private GameObject toDestroy;
  [SerializeField] private float maxHP;
  [SerializeField] private Transform shield;
  private float currentHP;
  private float damageCounter;
  private void Start()
  {
    currentHP = maxHP;
  }

  public void TakeDamage(float damage, string damageMode)
  {
    if(damageMode == "player")
    {
      if(maxHP != 1) return;
    }


    if(shield != null && shield.gameObject.activeSelf){
      if(damageMode == "ability") shield.gameObject.SetActive(false);
      damageCounter = 0;
      return;
    }
    if(damageAnim != null) damageAnim.Play();
    currentHP -= damage;
    damageCounter ++;
    if(currentHP <= 0) KillEnemy();
    if(damageCounter >=3)
    {
      damageCounter = 0;
      shield.gameObject.SetActive(true);
    }

  }

  public void KillEnemy()
  {
    deathAnim.Play();
    GameManager.GetGameManager().IncreaseScore(scoreIncrement);
    if(transform.parent != null && transform.parent.GetComponent<Pattern>() != null) transform.parent.GetComponent<Pattern>().allEnemiesKilled = true;
    Destroy(GetComponent<BoxCollider2D>());
    Destroy(GetComponent<SpriteRenderer>());
    Destroy(toDestroy, 0.2f);
    GameObject.Find("IndividualSpawner").GetComponent<IndividualSpawner>().ReduceSpawnedObjectCount();
  }
}