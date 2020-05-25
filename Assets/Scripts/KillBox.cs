using UnityEngine;

public class KillBox : MonoBehaviour {

  private void OnCollisionEnter2D(Collision2D other)
  {
    if(other.gameObject.GetComponent<Enemy>() != null) other.gameObject.GetComponent<Enemy>().TakeDamage(1, "killbox");
    if(other.gameObject.GetComponent<EnemyBullet>() != null) Destroy(other.gameObject);
  }

}