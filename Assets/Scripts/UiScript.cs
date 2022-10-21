using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour
{
    GameObject Player;
    PlayerSwipe PlayerSwipe;

    public GameObject TutorialPanel;

    void Start()
    {
        Player = GameObject.Find("player");
        PlayerSwipe = Player.GetComponent<PlayerSwipe>();

        if(IdleManager.gtg == false) {
            TutorialPanel.SetActive(true);
        }
    }

    // graphics vs performance
    public static bool goodGraphics;

    public void Graphics(bool opt) {
        goodGraphics = opt;
    }

    // Powerups
    public void MagnetUp(float num) {
        if(PlayerSwipe.gold - 50 >= 0) {
            PlayerSwipe.magnetRadius += num;
            PlayerSwipe.gold -= 50;
        }
    }
    public void SpeedUp(float num) {
        if(PlayerSwipe.gold - 50 >= 0) {
            PlayerSwipe.timeSpeed += num;
            PlayerSwipe.gold -= 50;
        }
        PlayerSwipe.timeSpeed += num;
    }

    // Loads Game
    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //  Jumping
    public void playerJump() {
        for (int i = 0; i <= PlayerSwipe.jumpBoostCollection.ToArray().Length - 1; i++) {
            // Debug.Log(PlayerSwipe.jumpBoostCollection[i] != null);
            if (PlayerSwipe.jumpBoostCollection[i] != null) {
                // Debug.Log(PlayerSwipe.jumpBoostCollection[i] != null);
                PlayerSwipe.jumpBoostCollection[i] = null;
                PlayerSwipe.physicallyJump();  
                break;
            }
        }
    }

    // Pausing Game

    public static bool paused = false;

    public void PauseGame() {
        Spawn.playerSpeed = 0;
        foreach (GameObject blocks in GameObject.FindGameObjectsWithTag("allSpawns")) {
            Destroy(blocks);
        }

        paused = true;
    }

    public void ResumeGame() {
        foreach (GameObject blocks in GameObject.FindGameObjectsWithTag("allSpawns")) {
            Destroy(blocks);
        }
        Spawn.playerSpeed = 1000;

        paused = false;
    }

}