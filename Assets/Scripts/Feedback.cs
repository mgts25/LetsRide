using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public static Feedback instance;
    [System.Serializable]
    public class point
    {
        public string name;
        public Transform IKPoint;
        public Vector3 targetPosition;
    }

    [System.Serializable]
    public class reaction
    {
        public string name;
        public List<point> points;
    }

    [System.Serializable]
    public class trick : reaction
    {
        public KeyCode key;
    }

    public List<reaction> reactions;

    public List<trick> tricks;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        foreach(var t in tricks)
        {
            if(ControlFreak2.CF2Input.GetKey(t.key))
            {

            }
        }
    }

    public void React(string reaction)
    {
        foreach(var r in reactions)
        {
            if(r.name == reaction)
            {
                foreach(var p in r.points)
                {
                    //p.IKPoint.localPosition = p.targetPosition;
                    IkController.instance.updatePositions(p.name, p.targetPosition);
                }
            }
        }
    }
}
