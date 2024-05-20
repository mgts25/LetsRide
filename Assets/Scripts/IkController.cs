using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkController : MonoBehaviour
{
    public static IkController instance;

    [System.Serializable]
    public class point
    {
        public string name;
        public Transform IkPoint;
        public Vector3 defaultPosition, maxPosition, minPosition;

        public Vector3 initialPosition;

        public bool manipulatePosition;
        public bool manipulateRotation;

        [Space(10)]
        public Vector3 defaultRotation;
        public Vector3 maxRotation, minRotation;
        public float transitionSpeed, defaultTransitionSpeed;
    }

    public List<point> IkPoints;
    //public PlayerController controller;

    PlayerController controller => RefManager.I.playerController;

    public float delay = 1;
    private float maxDelay = 1;

    private bool resetting;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var point in IkPoints)
        {
            point.initialPosition = point.defaultPosition = point.IkPoint.localPosition;
            point.defaultRotation = point.IkPoint.localEulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var h = controller.tilt;
        //var v = ControlFreak2.CF2Input.GetAxis("Vertical");

        foreach (var point in IkPoints)
        {
            if (controller.isGrounded)
            {
                point.defaultPosition = point.initialPosition;
                delay = Mathf.Clamp(delay + Time.deltaTime, 0, maxDelay);

                if (delay == maxDelay)
                    resetting = false;
            }


            if (point.IkPoint)
            {
                if (h != 0 && !controller.midAirTrickSettings.isMidair)
                {
                    if (h > 0)
                    {
                        point.IkPoint.localPosition = Vector3.MoveTowards(point.IkPoint.localPosition, point.maxPosition, Time.deltaTime * point.transitionSpeed * controller.NormalizedThrust());//point.maxPosition * controller.thrust * h;//Vector3.Lerp(point.IkPoint.localPosition, point.maxPosition, Time.deltaTime * point.transitionSpeed);

                        if (point.manipulateRotation)
                            point.IkPoint.localEulerAngles = Vector3.MoveTowards(point.IkPoint.localEulerAngles, point.maxRotation, Time.deltaTime * point.transitionSpeed * controller.NormalizedThrust());//point.maxPosition * controller.thrust * h;//Vector3.Lerp(point.IkPoint.localEulerAngles, point.maxRotation, Time.deltaTime * point.transitionSpeed);
                    }

                    if (h < 0)
                    {
                        point.IkPoint.localPosition = Vector3.MoveTowards(point.IkPoint.localPosition, point.minPosition, Time.deltaTime * point.transitionSpeed * controller.NormalizedThrust());//point.maxPosition * controller.thrust * h;//Vector3.Lerp(point.IkPoint.localPosition, point.minPosition, Time.deltaTime * point.transitionSpeed);

                        if (point.manipulateRotation)
                            point.IkPoint.localEulerAngles = Vector3.MoveTowards(point.IkPoint.localEulerAngles, point.minRotation, Time.deltaTime * point.transitionSpeed * controller.NormalizedThrust());//point.maxPosition * controller.thrust * h;//Vector3.Lerp(point.IkPoint.localEulerAngles, point.minRotation, Time.deltaTime * point.transitionSpeed);
                    }
                }
                else
                {
                    point.IkPoint.localPosition = Vector3.Lerp(point.IkPoint.localPosition, point.defaultPosition, Time.deltaTime * point.defaultTransitionSpeed);

                    if (point.manipulateRotation)
                        point.IkPoint.localEulerAngles = Vector3.Lerp(point.IkPoint.localEulerAngles, point.defaultRotation, Time.deltaTime * point.defaultTransitionSpeed);
                }
            }
        }
    }

    public void updatePositions(string name, Vector3 pos)
    {
        foreach (var p in IkPoints)
        {
            if (p.name == name)
            {
                p.defaultPosition = pos;
                //Debug.Log("Positions updated.");
            }
        }
    }
}
