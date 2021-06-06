using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject    POI; //the static point of interest
    /*POI is the point of interest that the camera should follow (e.g., a Projectile). 
     * As a static public field, the same value for POI is shared by all instances of the FollowCam class, 
     * and POI can be accessed anywhere in code as FollowCam.POI. This makes it easy 
     * for the Slingshot code to tell _MainCamera which Projectile to follow.
     */

    [Header("setInInspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("setDynamically")]
    public float                camZ; //the desired Z pos of the camera

    private void Awake()
    {
        camZ = this.transform.position.z;
    }


    /*
     * The Vector3.Lerp() method interpolates between two points, returning a weighted average of the two. 
     * If easing is 0, Lerp()returns the first point (transform.position); 
     * if easing is 1, Lerp()returns the second point (destination). 
     * If easing is any value in between 0 and 1, Lerp() returns a point between the two 
     * (with an easing of 0.5 returning the midpoint halfway between the two). 
     * Setting easing = 0.05f tells Unity to move the camera about 5% of the way from its current location 
     * to the location of the POI every FixedUpdate (i.e., each update of the physics engine, which occur 50 times per second). 
     * Because the POI is constantly moving, this gives you a nice smooth camera follow movement. 
     * Try playing with the value of easing to see how it affects the camera movement. 
     * This kind of use of Lerp() is a very simplistic form of linear interpolation.

        Gibson Bond, Jeremy. Introduction to Game Design, Prototyping, and Development (p. 515). Pearson Education. Kindle Edition. 
     */
    private void FixedUpdate()
    {
        if (POI == null) return;//return if there is no POI

        //get the position of the POI
        Vector3 destination = POI.transform.position;
        //Limit the X&Y to the minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current Camera position towards the destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        //set the camera to destination
        transform.position = destination;
        //set the orthographic size of the camera to keep ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
