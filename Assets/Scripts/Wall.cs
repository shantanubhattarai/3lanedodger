using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
  [SerializeField] private float moveSpeed;

  private Rigidbody2D rb2D;

  private void Awake()
  {
    rb2D = GetComponent<Rigidbody2D>();
  }

  private void OnEnable()
  {
    StartCoroutine(WallMovement());

    Invoke("DeactivateWall", 3f);
  }

  private IEnumerator WallMovement()
  {
    float destinationY = transform.position.y + 10f;

    while(true)
    {
      if(transform.position.y < destinationY) rb2D.velocity = new Vector2(0f, moveSpeed * Time.deltaTime);
      else
      {
        rb2D.velocity = Vector2.zero;
        yield break;
      }

      yield return null;
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if(other.gameObject.GetComponent<Enemy>() != null) other.gameObject.GetComponent<Enemy>().TakeDamage(1, "ability");
    if(other.gameObject.GetComponent<EnemyBullet>() != null) Destroy(other.gameObject);
  }

  private void DeactivateWall()
  {
    gameObject.SetActive(false);
  }

}