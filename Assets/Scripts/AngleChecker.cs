using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleChecker : MonoBehaviour
{
    [Range(-1, 1)]
    public float value;
    [Range(0, 1)]
    public float multiplier;

    [Space(10)]
    public float angle = 30;
    private float maxAngle;

    public Transform body;
    public Animator anim;

    [Header("IK Points")]
    public Vector3 pelvis;
    public Vector3 handLeft;
    public Vector3 handRight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        value = Vector3.Dot(body.up, Vector3.up);
        var h = Mathf.Abs(ControlFreak2.CF2Input.GetAxis("Horizontal"));
        var v = Mathf.Abs(ControlFreak2.CF2Input.GetAxis("Vertical"));
        multiplier = Mathf.Clamp(1 - (value < 0.5f ? value - 0.25f : value), 0, 1);
        anim.SetFloat("Vertical", multiplier);
    }
}
