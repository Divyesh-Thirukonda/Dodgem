using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldscript : MonoBehaviour
{
    
    System.Random random = new System.Random();

    // How often a shield is generated (1 in 15 chance)
    void Start()
    {
        int x = random.Next(0,15);
        if (x == 4) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
