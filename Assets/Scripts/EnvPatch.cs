using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvPatch : MonoBehaviour
{
    public Transform nextPatch;
    public Vector3 Offset;

    public Vector3 initialPosition;
    public bool reset;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !reset)
        {
            nextPatch.localPosition = transform.localPosition + Offset;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        reset = false;
    }

    public void ResetPosition()
    {
        reset = true;
        transform.localPosition = initialPosition;
        //nextPatch.localPosition = transform.localPosition + Offset;
    }
}
