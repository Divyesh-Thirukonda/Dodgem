using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerSwipe : MonoBehaviour
{
    // Touch variables
    Vector2 startTouchPosition;
    Vector2 currentTouchPosition;
    Vector2 endTouchPosition;
    bool stopTouch = false;
    public float swipeRange = 2;
    public float tapRange;

    // amount of gold collected
    float gold = 0;

    // UI
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreMultiplierText;
    public TextMeshProUGUI scoreText;

    // Audio
    public AudioSource coinAudio;
    public AudioSource powerupAudio;

    // The jump-boost LIST
    public GameObject[] jumpBoostCollection;

    public TextMeshProUGUI finalScore;

    
    void Update()
    {
        Swipee();

        finalScore.text = scoreText.text;

        // display Spawn.points and gold in UI
        coinText.text = Mathf.Floor(gold).ToString();
        scoreMultiplierText.text = "X " + UIupdate(Spawn.scoreMultiplier);
        scoreText.text = UIupdate(Spawn.points);

        if(transform.position.y < -0.25f) {
            transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);
        }
    }

    string UIupdate(float numToConvert) {

        string[] suffix = {"K", "M", "B", "T"};
        for(int i = 3; i >= 0; i--) {
            if (numToConvert < 1000) {
                return Mathf.Floor(numToConvert).ToString();
                break;
            }
            if(numToConvert / (1000 * Mathf.Pow(10, i*3)) > 1) {
                return Mathf.Floor(numToConvert / (1000 * Mathf.Pow(10, i*3))) + suffix[i];
                break;
            }
        }
        return "";

        // BELOW IS AN ALTERNATE WAY TO WRITE THE CODE ABOVE
        // if (numToConvert > 1000f && numToConvert < 1000000f) {
        //     return Mathf.Floor(numToConvert/1000f).ToString() + "K";
        // } else if (numToConvert > 1000000f && numToConvert < 1000000000f) {
        //     return Mathf.Floor(numToConvert/1000000f).ToString() + "M";
        // } else if (numToConvert > 1000000000f && numToConvert < 1000000000000f) {
        //     return Mathf.Floor(numToConvert/1000000000f).ToString() + "B";
        // } else {
        //     return Mathf.Floor(numToConvert).ToString();
        // }
    }

    void OnTriggerEnter(Collider collision) {

        // if you collide with a gameobject and its tag is 'X' then do its corrosponding action
        if (collision.gameObject.tag == "destroyPlayer")
        {
            deathScreen.SetActive(true);
            UiScript.paused = true;
            Spawn.playerSpeed = 0;
            foreach (GameObject blocks in GameObject.FindGameObjectsWithTag("allSpawns")) {
                Destroy(blocks);
            }
            // intead add death screen and play again button links to ResetGame()
        }
        if (collision.gameObject.tag == "gold")
        {
            IncreaseGold();
            coinAudio.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "multiplierIncrease")
        {
            powerupAudio.Play();
            IncreaseMultiplier();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "shield")
        {
            nodie();
            Destroy(collision.gameObject);
        }
    }

    public void nodie() {
        StartCoroutine("deleteem");
    }

    IEnumerator deleteem()
    {
        float timePassed = 0;
        while (timePassed < 3)
        {
            foreach (GameObject blocks in GameObject.FindGameObjectsWithTag("destroyPlayer")) {
                Destroy(blocks);
            }
            timePassed += Time.deltaTime;
    
            yield return null;
        }
    }

    public AudioSource boom;
    public GameObject deathScreen;

    public void ResetGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        deathScreen.SetActive(false);
        UiScript.paused = false;
        Spawn.points = 0;
        Spawn.scoreMultiplier = 1;
        Spawn.timeForNextBlock = 0;
        Spawn.playerSpeed = 1000f;
        Spawn.numOfBlocksSpawned = 0;
        updateJumps();
        boom.Play();
        Spawn.playerSpeed = 1000;
        foreach (GameObject blocks in GameObject.FindGameObjectsWithTag("allSpawns")) {
            Destroy(blocks);
        }
        // GetComponent<FirebaseCoins>().SaveButton(); // ALSO DO THIS WHEN YOU PURCHASE SMTH
    }


    void IncreaseGold() {
        gold++;
    }
    void IncreaseMultiplier() {
        Spawn.scoreMultiplier *= (float)2;
    }

    // private Vector2 startTouchPosition;
    // private Vector2 endTouchPosition;
    float dragDistance = Screen.height * 2 / 100;
    void Swipee() {
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            if(Mathf.Abs(endTouchPosition.x - startTouchPosition.x) > dragDistance || Mathf.Abs(endTouchPosition.y - startTouchPosition.y) > dragDistance) {
                if(Mathf.Abs(endTouchPosition.x - startTouchPosition.x) > Mathf.Abs(endTouchPosition.y - startTouchPosition.y)){
                    if(endTouchPosition.x < startTouchPosition.x && transform.position.x != (float)-1.4){
                        transform.position += new Vector3((float)-1.4, 0, 0);
                        
                    }
                    if(endTouchPosition.x>startTouchPosition.x && transform.position.x != (float)1.4){
                        transform.position += new Vector3((float)1.4, 0, 0);
                    }
                } else {
                    if(endTouchPosition.y < startTouchPosition.y){
                        if (isJump) {
                            transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);
                            isJump = false;
                        }
                    }
                    if(endTouchPosition.y>startTouchPosition.y){
                        try {
                            GameObject.Find("UI Script").GetComponent<UiScript>().playerJump();
                            updateJumps();
                        } catch {
                            // oh well
                        }
                    }
                }
            }
        }
    }

    void Swipe()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPosition - startTouchPosition;

            if(!stopTouch) {
                // if swipe up more than 5 on Y, and the fingers stay between 3 and -3 on the X, it's up swipe. Try jumping. If player doesn't have the jump-boost, then it swipes left or right
                if(Distance.y > swipeRange && ((Distance.x < 2f) | (Distance.x > -2f))) {
                    Debug.Log("up");
                    // up
                    try {
                        GameObject.Find("UI Script").GetComponent<UiScript>().playerJump();
                        updateJumps();
                    } catch {
                        // oh well
                    }
                    stopTouch = true;
                } else if(Distance.y < -swipeRange && ((Distance.x < 2f) | (Distance.x > -2f))) {
                    Debug.Log("down");
                    if (isJump) {
                        transform.position += new Vector3(0, -1.25f, 0);
                    }
                } else if(Distance.x < -.2f && ((Distance.y < 2) | (Distance.y > -2)) && transform.position.x != (float)-1.4) {
                    // left
                    Debug.Log("left");
                    transform.position += new Vector3((float)-1.4, 0, 0);
                    stopTouch = true;
                    return;
                } else if(Distance.x > .2f && ((Distance.y < 2) | (Distance.y > -2)) && transform.position.x != (float)1.4) {
                    // right
                    Debug.Log("right");
                    transform.position += new Vector3((float)1.4, 0, 0);
                    stopTouch = true;
                    return;
                }
                
            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;
            if(Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange) {
                //tap
            }
        }
    }


    bool isJump;

    IEnumerator Jumpy()
    {
        // move it up, wait 1.15 seconds, then move it back down (creating the illusion of a jump)
        transform.position += new Vector3(0, 1, 0);
        isJump = true;

        yield return new WaitForSeconds(1.15f);

        if(isJump == false) {
            // do nothing
        } else {
            transform.position += new Vector3(0, -1, 0);
            isJump = false;
        }
    }
    
    public void physicallyJump() {
        // Jump
        StartCoroutine(Jumpy());
    }


    void Start() {
        updateJumps();
    }

    public TextMeshProUGUI textOfJumps;
    public void updateJumps() {
        textOfJumps.text = "";
        foreach(GameObject jump in jumpBoostCollection) {
            if(jump != null) {
                textOfJumps.text = textOfJumps.text + "D";
            } else {
                textOfJumps.text = textOfJumps.text + "□";
            }
        }
    }

}
