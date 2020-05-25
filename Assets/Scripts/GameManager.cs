using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI wallCDTimerText;
  [SerializeField] private TextMeshProUGUI scoreMultiplierText;
  [SerializeField] private float multiplierFactor = 1f;

  private static GameManager instance;
  private static float score;
  private float scoreMultiplier = 1f;

  public static GameManager GetGameManager()
  {
    return instance;
  }

  private void Awake()
  {
    if(instance != null && instance != this)
    {
      Destroy(gameObject);
      return;
    }
    else instance = this;
    DontDestroyOnLoad(gameObject);
  }

  private void Start()
  {
    SetScoreText();
  }

  private void Update()
  {
    if(Input.GetKeyDown(KeyCode.R)) ReloadLevel();
    if(Input.GetKeyDown(KeyCode.Escape)) TogglePauseLevel();
  }

  private void TogglePauseLevel()
  {
    Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
  }

  public void ReloadLevel()
  {
    SetScore(0f);
    ResetMultiplier();
    SetScoreText();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void SetScoreText()
  {
    scoreText.text = Mathf.Round(score).ToString();
    scoreMultiplierText.text = "x " + scoreMultiplier.ToString();
  }

  public float GetScore(){
    return score;
  }

  public void SetScore(float newScore){
    score = newScore;
    SetMultiplier();
    SetScoreText();
  }

  public void SetMultiplier()
  {
    float multiplier = 1 + score / 10000 * multiplierFactor;
    scoreMultiplier = (float) Math.Round(multiplier, 2);
  }

  public float GetMultiplier()
  {
    return scoreMultiplier;
  }

  public void ResetMultiplier()
  {
    scoreMultiplier = 1f;
  }

  public void IncreaseScore(float scoreIncrement){
    score += scoreIncrement * scoreMultiplier;
    SetMultiplier();
    SetScoreText();
  }

  public void KillPlayer()
  {
    wallCDTimerText.text = "";
    Destroy(Player.GetPlayer().gameObject);
    ReloadLevel();
  }

  public TextMeshProUGUI GetCDUI()
  {
    return wallCDTimerText;
  }

}