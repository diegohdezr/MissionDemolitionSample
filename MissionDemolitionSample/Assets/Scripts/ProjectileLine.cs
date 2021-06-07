using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine PLSingleton;

    [Header("set in inspector")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        PLSingleton= this;
        //get6 a reference to the line renderer
        line = GetComponent<LineRenderer>();
        //disable the line renderer until it is necesary
        line.enabled = false;
        //initialize the list of points
        points = new List<Vector3>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (poi == null) 
        {
            //if there is no poi then search for one
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }
                else
                {
                    return;
                }
            }
            else 
            {
                return;
            }
        }
        //if there is a poiu, its location is added every fixed update
        AddPoint();
        if (FollowCam.POI == null) 
        {
            //once followcampoi is null, make the local poi null too
            poi = null;
        }
    }

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();

    }

    public void AddPoint()
    {
        //this is called to add a point to the line
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist) 
        {
            //if the point is not far enough from the last point it returns
            return;
        }
        if (points.Count == 0)
        {
            //if this is the launchpoint
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            //sets the first two points
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            //enable the line renderer
            line.enabled = true;
        }
        else 
        {
            //normal behavior of adding a point
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true; 
        }
    }

    public GameObject poi 
    {
        get { return (_poi); }
        set 
        {
            _poi = value;
            if (_poi != null) 
            {
                //when poi is set to something new, it resets everything
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    
    }

    public Vector3 lastPoint 
    {
        get
        {
            if (points == null) return Vector3.zero;
            return (points[points.Count - 1]);
        }
    }
}
