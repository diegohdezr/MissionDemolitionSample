using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //a static field accessible by code anywhere in the game
    static public bool goalMet = false;
    private void OnTriggerEnter(Collider other)
    {
        //when the trigger is hit by something
        //check if it is a projectile
        if (other.gameObject.tag == "Projectile") 
        {
            //if so, set the goalMet field to true
            Goal.goalMet = true;
            //also change the alpha of the color to higher opacity
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
