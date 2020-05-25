using UnityEngine;
using System.Collections;

public class ChargerEnemy : MonoBehaviour
{
  [SerializeField] private float moveSpeed;
  [SerializeField] private float chargeForce;
  [SerializeField] private GameObject chargingIndicatorPrefab;
  [SerializeField] private float chargeDuration = 1f;
  [SerializeField] private Transform canvasObject;
  [SerializeField] private float gracePeriod;

  private Enemy enemy;
  private Rigidbody2D rb2D;
  private Transform target;
  private bool isLaunching = false;
  private Vector2 moveDirection;

  private void Start()
  {
    enemy = GetComponent<Enemy>();
    target = Player.GetPlayer().transform;
    rb2D = GetComponent<Rigidbody2D>();

    if(canvasObject == null) canvasObject = GameObject.Find("Canvas").transform;
  }

  private void FixedUpdate()
  {
    float randomYPercentage = Random.Range(0.65f, 0.9f);
    if(!isLaunching && transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0f, randomYPercentage, 0f)).y)
    {
      Transform chargingIndicator = Instantiate(chargingIndicatorPrefab, transform.position, Quaternion.identity).transform;
      chargingIndicator.SetParent(canvasObject);
      canvasObject.GetComponent<CircleAnimationCanvas>().SetAnimationParameters(chargeDuration, transform, chargingIndicator);

      StartCoroutine(LaunchTowardsTarget());
    }
    else if (!isLaunching)
    {
      rb2D.velocity = new Vector2(0f, -1f *  moveSpeed * Time.fixedDeltaTime);
    }
  }

  private IEnumerator ChargeAttack()
  {
    isLaunching = true;
    rb2D.velocity = Vector2.zero;
    float timeElapsed = 0f;

    while(timeElapsed < chargeDuration - gracePeriod)
    {
      if(target != null)
      {
        if(transform.position.x != target.position.x) transform.up = Vector3.Lerp(transform.up, (target.position - transform.position), 1f * Time.deltaTime);
        moveDirection = target.position - transform.position;
      }
      timeElapsed += Time.deltaTime;
      yield return null;
    }
  }

  private IEnumerator LaunchTowardsTarget()
  {
    StartCoroutine(ChargeAttack());

    yield return new WaitForSeconds(chargeDuration);
    Vector3 currentVelocity = Vector3.zero;
    Vector2 moveDirectionNormalized = moveDirection / moveDirection.magnitude;

    while(true)
    {
      if(target != null && Mathf.Abs(Vector3.Distance(transform.position,target.position)) > 0.5f)
      {
        rb2D.velocity = moveDirectionNormalized * chargeForce * Time.deltaTime;
      }
      else
      {
        enemy.KillEnemy();
        yield break;
      }
      yield return null;
    }
  }
}