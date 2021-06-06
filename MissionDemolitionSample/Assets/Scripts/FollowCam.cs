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

    [Header("setDynamically")]
    public float                camZ; //the desired Z pos of the camera

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        if (POI == null) return;//return if there is no POI

        //get the position of the POI
        Vector3 destination = POI.transform.position;
        //force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        //set the camera to destination
        transform.position = destination;
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
