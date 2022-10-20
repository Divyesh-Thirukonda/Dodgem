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

    public GameObject panel1;
    public GameObject fullInventoryUI;


    PlayerSwipe PlayerSwipe;

    void Start()
    {
        Player = GameObject.Find("player");
        PlayerSwipe = Player.GetComponent<PlayerSwipe>();

        fullInventoryUI.SetActive(false);

    }

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /* This is a visual way to keep track of how many jump boosts you have
    void Update()
    {
        foreach(Transform sloti in panel1.transform) {
            Transform tmpi = sloti.transform.GetChild(0);
            int oooi = Int32.Parse(tmpi.gameObject.name);
            
            if(PlayerSwipe.jumpBoostCollection[oooi] == null) {
                tmpi.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
            } else {
                tmpi.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerSwipe.jumpBoostCollection[oooi].name;
            }
        }
    }
    */

    public void playerJump() {
        for (int i = 0; i <= GameObject.Find("Player").GetComponent<PlayerSwipe>().jumpBoostCollection.ToArray().Length - 1; i++) {
            // Debug.Log(GameObject.Find("Player").GetComponent<PlayerSwipe>().jumpBoostCollection[i] != null);
            if (GameObject.Find("Player").GetComponent<PlayerSwipe>().jumpBoostCollection[i] != null) {
                // Debug.Log(GameObject.Find("Player").GetComponent<PlayerSwipe>().jumpBoostCollection[i] != null);
                GameObject.Find("Player").GetComponent<PlayerSwipe>().jumpBoostCollection[i] = null;
                GameObject.Find("Player").GetComponent<PlayerSwipe>().physicallyJump();  
                break;
            }
        }
    }



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