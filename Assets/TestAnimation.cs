using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    //Transform skateBoard => RefManager.I.skateBoard;
    public Transform skateBoard;
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform leftHand;
    public Transform hips;
    public AnimationClip clip;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    skateBoard.position = (leftFoot.position + rightFoot.position) / 2f;
    //    //skateBoard.forward = leftFoot.up;

    //    //var projectedDot = Vector3.Project(rightFoot.position - leftFoot.position)
    //    skateBoard.right = (leftFoot.position - rightFoot.position).normalized;
    //    //skateBoard.rotation = Quaternion.LookRotation(leftFoot.up, (hips.position - skateBoard.position));
    //    skateBoard.position -= skateBoard.up * 0.04f;
    //    Debug.Log(Vector3.Angle(Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right)));
    //    Debug.Log(Quaternion.FromToRotation(Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right)).eulerAngles);

    //    var upVector = Vector3.Cross(leftFoot.position - rightFoot.position, Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right));
    //    skateBoard.LookAt(skateBoard.position + Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right) * 4f, upVector);
    //    //skateBoard.rotation *= Quaternion.FromToRotation(Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right));
    //    //skateBoard.Rotate(skateBoard.right, Vector3.Angle(Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right)));
    //    //skateBoard.rotation *= Quaternion.AngleAxis(Vector3.Angle(Vector3.ProjectOnPlane(skateBoard.forward, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right)), skateBoard.right);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftFoot.position, rightFoot.position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(skateBoard.position, skateBoard.position + skateBoard.right * 10f);

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(skateBoard.position, skateBoard.position + Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right) * 4f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(skateBoard.position, skateBoard.position + Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right) * 4f);

    }
}
