using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CircleAnimationCanvas : MonoBehaviour {

  private List<float> durations;
  private RectTransform canvasRect;
  private List<Transform> targets;
  private List<float> currentValues;
  private List<Transform> circlesToAnimate;

  private void Start()
  {
    targets = new List<Transform>();
    circlesToAnimate = new List<Transform>();
    currentValues = new List<float>();
    durations = new List<float>();
    canvasRect = GetComponent<RectTransform>();
  }

  public void SetAnimationParameters(float chargeDuration, Transform enemy, Transform indicator)
  {
    currentValues.Add(0f);
    circlesToAnimate.Add(indicator);
    targets.Add(enemy);
    durations.Add(chargeDuration);
  }

  private void Update()
  {
    //MoveToTargetPos();
    Vector2 canvasPos;
    for(int i = 0; i < targets.Count; i++)
    {
      if(targets[i] != null)
      {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(targets[i].position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);
        circlesToAnimate[i].localPosition = canvasPos;

        //AnimateCircle();
        if(currentValues[i] < 100)
        {
          currentValues[i] += (100/ durations[i]) * Time.deltaTime;
        }
        else
        {
          circlesToAnimate[i].gameObject.SetActive(false);
        }
        circlesToAnimate[i].GetComponent<Image>().fillAmount = currentValues[i]/ 100;
      } else {
        circlesToAnimate[i].gameObject.SetActive(false);
      }
    }
  }

}