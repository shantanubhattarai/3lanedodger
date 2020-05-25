using UnityEngine;
using System.Collections;

public class Stomper : MonoBehaviour
{
  [SerializeField] private Transform child;

  private int direction = 1;

  public void Start()
  {
  }

  public void ShakeOnImpact()
  {
    EZCameraShake.CameraShaker.Instance.ShakeOnce(2f, 7f, 0.5f, 0.2f);
  }

  public IEnumerator ChangeLanes()
  {
    Vector3 toMoveTo = Vector3.zero;
    if((child.position.x > 1f && direction == 1) || (child.position.x < -1f && direction == -1)){
      toMoveTo = new Vector2(0f, child.position.y);
      direction = -1 * direction;
    }else{
      toMoveTo = new Vector2(child.position.x + direction * 3.75f, child.position.y);
    }
    while(true)
    {
      child.position = Vector3.Lerp(child.position, toMoveTo, 0.3f);
      if(Mathf.Abs(child.position.x - toMoveTo.x) <= 0.05f)
      {
        yield break;
      }
      yield return null;
    }

  }


}
