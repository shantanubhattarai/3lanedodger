using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 100;
  private Rigidbody2D rb2D;

  private void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    rb2D.velocity = new Vector2(0f, -1f *  moveSpeed * Time.fixedDeltaTime);
  }
}