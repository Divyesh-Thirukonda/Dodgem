using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public GameObject player;

    // Follows player with an offset
    void Update()
    {
        gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 3.5f, -5.5f);
    }
}
