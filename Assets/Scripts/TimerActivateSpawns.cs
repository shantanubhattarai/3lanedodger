using UnityEngine;

public class TimerActivateSpawns : MonoBehaviour {

  public delegate void TimerCallDelegate();
  public TimerCallDelegate allTimerCalls;
  [SerializeField] private float timeBetweenActivations = 2f;

  private float timeSinceLastCall;

  private void Start()
  {
    timeSinceLastCall = Time.time;
  }

  private void FixedUpdate()
  {
    if(Time.time > timeSinceLastCall + timeBetweenActivations){
      timeSinceLastCall = Time.time;
      if(allTimerCalls != null) allTimerCalls();
    }
  }

}