using UnityEngine;

public class Player : MonoBehaviour
{
  private static Player instance;

  [SerializeField] private float maxHP = 3f;
  private float currentHP;

  private void Awake()
  {
    instance = this;
  }

  private void Start()
  {
    currentHP = maxHP;
  }

  public static Player GetPlayer()
  {
    return instance;
  }

  private void TakeDamage(float damage)
  {

    EZCameraShake.CameraShaker.Instance.ShakeOnce(1.3f, 5f, 0.5f, 0.2f);
    currentHP -= damage;

    if(currentHP <= 0)
    {
      GameManager.GetGameManager().KillPlayer();
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if(other.gameObject.GetComponent<Enemy>() != null)
    {
      other.gameObject.GetComponent<Enemy>().TakeDamage(1, "player");
      TakeDamage(1);
    }
    else if(other.gameObject.GetComponent<EnemyBullet>() != null)
    {
      Destroy(other.gameObject);
      TakeDamage(1f);
    }
  }
}