using UnityEngine;
using TMPro;
using System;

public class PlayerFireAbilities : MonoBehaviour
{
  [SerializeField] private GameObject wallPrefab;
  [SerializeField] private float wallCD;
  [SerializeField] private Transform cdCover;
  [SerializeField] private Transform wallIcon;
  private TextMeshProUGUI wallCDTimerText;
  private Player player;
  private GameObject wall;

  private float timeTillWallEnable = 0f;
  private float wallCDTimer = 0f;
  private bool wallOnCooldown = false;

  private void Start()
  {
    cdCover.localScale = Vector2.right;
    if(wallCDTimerText == null) wallCDTimerText = GameManager.GetGameManager().GetCDUI();
    player = GetComponent<Player>();
    timeTillWallEnable = Time.time;
  }

  private void Update()
  {
    if(wallCDTimer > 0) DecreaseWallCD();

    if(Input.GetButtonDown("Ability1") && !wallOnCooldown) PlaceWall();
  }

  private void PlaceWall()
  {
    wallCDTimer = wallCD;

    if(wall == null) wall = Instantiate(wallPrefab, transform.position, Quaternion.identity);

    if(wall != null && !wallOnCooldown)
    {
      wall.SetActive(false);
      wall.transform.position = new Vector2(GetComponent<PlayerMovement>().GetNearestLaneX(), transform.position.y);
      wall.SetActive(true);
    }

    wallOnCooldown = true;
  }

  private void DecreaseWallCD()
  {
    if(wallCDTimerText != null) wallCDTimerText.text = String.Format("{0:F1}", wallCDTimer);
    wallIcon.GetComponent<SpriteRenderer>().color = new Color32(85,85,85, 255);
    if(wallCDTimer > 0)
    {
      wallCDTimer -= Time.deltaTime;
    }

    cdCover.localScale = new Vector2(1f, wallCDTimer * 1f/5f);

    if(wallCDTimer <= 0)
    {
      wallCDTimerText.text = "";
      wallIcon.GetComponent<SpriteRenderer>().color = Color.white;
      wallOnCooldown = false;
      wallCDTimer = 0f;
    }
  }

}