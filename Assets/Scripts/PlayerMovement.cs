using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float smoothTime;
  [SerializeField] private float distanceToMove;
  [SerializeField] private List<Transform> lanes;
  [SerializeField] private float movementSpeed;

  private int laneIndex = 1;
  private Vector3 currentVelocity = Vector3.zero;
  private bool isMoving = false;

  private void Update()
  {
    int moveToLane = (int) Input.GetAxisRaw("Horizontal");
    if(!isMoving){
      StartCoroutine(Move(moveToLane));
    }
  }

  public float GetNearestLaneX()
  {
    float minDistance = 1000f;
    float minDistanceLaneX = 0f;

    foreach(Transform lane in lanes)
    {
      float distanceToLane = Mathf.Abs(transform.position.x - lane.position.x);
      if(distanceToLane < minDistance)
      {
        minDistance = distanceToLane;
        minDistanceLaneX = lane.position.x;

      }
    }
    return minDistanceLaneX;
  }

  private IEnumerator Move(int moveToLane)
  {
    if(!(moveToLane > 0 && laneIndex > 1) && !(moveToLane < 0 && laneIndex < 1))
    {
      laneIndex += moveToLane;
      Vector3 toMove = new Vector3(lanes[laneIndex].position.x, transform.position.y, 0f);
      while(true)
      {
        if(Mathf.Abs(Vector3.Distance( transform.position,toMove)) > 0.1f)
        {
          isMoving = true;
          transform.position = Vector3.Lerp(transform.position, toMove, movementSpeed * smoothTime * Time.deltaTime);
        }else{
          isMoving = false;
          yield break;
        }
        yield return null;
      }
    }else{
      yield break;
    }
  }
}
