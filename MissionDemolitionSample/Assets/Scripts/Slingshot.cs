using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Set in inspector")]
    public GameObject           prefabProjectile;
    public float                velocityMult = 8f;

    [Header("set Dynamically")]
    public GameObject           launchPoint;
    public Vector3              launchPos;
    public GameObject           projectile;
    public bool                 aimingMode;
    private Rigidbody           projectileRigidBody;

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Slingshot:OnMouseEnter();");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("Slingshot: OnMouseExit();");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        //the player has pressed the mouse button while over the slingshot
        aimingMode = true;
        //instantiate a projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        //start it at the launchpoint
        projectile.transform.position = launchPos;
        //set it to isKinematic for now in order for the mouse to control it
        projectileRigidBody = projectile.GetComponent<Rigidbody>();
        projectileRigidBody.isKinematic = true;
    }

    private void Update()
    {
        //if slingshot is not in aiming mode then do not run this code
        if (!aimingMode) return;

        //get the current mouse postition 2D in screen coords
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //find the delta from the launch pos to the mousepos3d
        Vector3 mouseDelta = mousePos3D - launchPos;
        //limit mouseDelta to the radius of the slingshot Sphere colider
        float MaxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > MaxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= MaxMagnitude;
        }

        //move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        //when the mouse has been released
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRigidBody.isKinematic = false;
            projectileRigidBody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
        }

    }
}
