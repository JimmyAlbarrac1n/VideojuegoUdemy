using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Ready, Playing, Ended};
public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Ready;
    public RawImage background, platform;
    public float parallaxSpeed = 0.02f;
    public GameObject uiReady;
    
    void Update()
    {
        bool action = Input.GetKeyDown("space") || Input.GetMouseButtonDown(0);
        HandleJump(action);
        HandleCollisions();
       UpdateParallax();
       UpdateGameState(action);
       HandleExit();
      
    }
    void HandleJump(bool action)
    {
        if(gameState == GameState.Playing && action)
        {
            PlayerManager.Instance.SetAnimation("PlayerJump");
        }
       
    }
    void HandleCollisions()
    {
        if (gameState == GameState.Playing && PlayerManager.Instance.enemyCollision)
        {
            gameState = GameState.Ended;
            PlayerManager.Instance.SetAnimation("PlayerDied");
            SpawnManager.Instance.StopSpawn();
            SpeedManager.Instance.ResetSpeed();
            
        }
    }
    void UpdateGameState(bool action)
    {
        if(gameState == GameState.Ready && action)
        {
           
            gameState = GameState.Playing;
            uiReady.SetActive(false);

            PlayerManager.Instance.SetAnimation("PlayerRun");
            SpawnManager.Instance.StarSpawn();
            SpeedManager.Instance.StartSpeedIncrease();
        }
        else if(gameState == GameState.Ended && action)
        {
            HandleRestart();
        }
         
    }

    void UpdateParallax()
    {
         if(gameState == GameState.Playing)
        {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 3, 0f, 1f, 1f);
        }
    }

    void HandleRestart()
    {
        SceneManager.LoadScene("Main");
    }
    void HandleExit()
    {
        if(Input.GetKeyDown("escape")) Application.Quit();
    }

}
