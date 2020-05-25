using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
  [SerializeField] private float movementSpeed;
  private Rigidbody2D rb2D;

  private void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate()
  {
    rb2D.velocity = new Vector2(0f, -1f * movementSpeed * Time.fixedDeltaTime);
  }
}