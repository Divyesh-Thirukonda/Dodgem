using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // This script stores variables and also spawns the buildings
    
    public GameObject[] blocksToSpawn; // blocks to place
    private GameObject nextBlock;
    public GameObject transformOfLastPlacementReference;

    System.Random random = new System.Random();

    public static float timeForNextBlock = 0;
    public static float playerSpeed = 1000f;
    public static float numOfBlocksSpawned;
    public static float points = 0;
    public static float scoreMultiplier = 1;

    // Update is called once per frame
    void Update()
    {
        timeForNextBlock += 1 * Time.deltaTime;

        if (UiScript.paused == false) {
            points += 5 * Time.deltaTime * scoreMultiplier;
        } else if (UiScript.paused == true) {
            points += 0 * Time.deltaTime;
        } else {
            Debug.Log("huh");
        }


        if (timeForNextBlock >= .8) {
            nextBlock = blocksToSpawn[random.Next(0,6)];
            nextBlock = Instantiate(nextBlock, transformOfLastPlacementReference.transform);
            numOfBlocksSpawned++;
            nextBlock.transform.parent = null;
            
            timeForNextBlock = 0;
        }
        nextBlock.GetComponent<Rigidbody>().AddForce(0, 0, -playerSpeed*Time.deltaTime);
        
    }
}
