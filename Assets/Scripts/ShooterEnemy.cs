using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
  [SerializeField] private float movementSpeed;
  [SerializeField] private Transform bullet;
  [SerializeField] private Transform animationTillShoot;
  [SerializeField] private float recoilForce = 10f;

  private Enemy enemy;
  private Rigidbody2D rb2D;
  private bool isShooting = false;
  private float distanceMoved;
  private Vector3 initPos;
  private float timeToReachMaxDistance;
  private float animationSpeed;
  private float maxDistanceToMove;

  private void Start(){
    maxDistanceToMove = transform.position.y - Camera.main.ViewportToWorldPoint(new Vector2(0f, 0.7f)).y;

    enemy = GetComponent<Enemy>();
    rb2D = GetComponent<Rigidbody2D>();
    initPos = transform.position;
    timeToReachMaxDistance = maxDistanceToMove/ (movementSpeed * Time.fixedDeltaTime);
    animationSpeed = 1f /timeToReachMaxDistance; //1 is the size of the shooter
  }

  private void FixedUpdate()
  {
    if(!isShooting && distanceMoved < maxDistanceToMove)
    {
      animationTillShoot.localScale = new Vector2(animationTillShoot.localScale.x - animationSpeed * Time.fixedDeltaTime, animationTillShoot.localScale.y);
      rb2D.velocity = new Vector2(0f, -1f * movementSpeed * Time.fixedDeltaTime);
    }
    else if (!isShooting && distanceMoved >= maxDistanceToMove)
    {
      rb2D.velocity = Vector2.zero;
      isShooting = true;
      ShootBullet();
    }
    else if (isShooting && distanceMoved >= maxDistanceToMove)
    {
      if(transform.position.y > Camera.main.ViewportToWorldPoint(Vector2.one).y) enemy.KillEnemy();
      rb2D.AddForce(new Vector2(0f, recoilForce), ForceMode2D.Impulse);
    }

    distanceMoved = Mathf.Abs(Vector3.Distance(transform.position, initPos));
  }

  private void ShootBullet()
  {
    animationTillShoot.gameObject.SetActive(false);
    bullet.transform.SetParent(null);
    bullet.gameObject.SetActive(true);
    bullet.position = transform.position + Vector3.down;
  }

}