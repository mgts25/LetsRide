using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEventListener : MonoBehaviour
{
    public bool animCompleted;
    Transform skateBoard => RefManager.I.skateBoard;
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform leftHand;
    public Transform hips;

    private RootMotion.FinalIK.FullBodyBipedIK fullBodyBipedIK;
    private bool correctRotation;

    string state = "0";

    string oldState = "";


    private void Start()
    {
        fullBodyBipedIK = GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();
    }

    private void Update()
    {
        //if (skateBoard.localRotation != Quaternion.Euler(0, 0, 0))

        switch (state)
        {
            case "Foot":
                //skateBoard.localRotation = Quaternion.Euler(90f, 0, 0);
                //skateBoard.localPosition = Vector3.zero;
                skateBoard.position = (leftFoot.position + rightFoot.position) / 2f;
                //skateBoard.forward = leftFoot.up;

                //var projectedDot = Vector3.Project(rightFoot.position - leftFoot.position)
                skateBoard.right = (leftFoot.position - rightFoot.position).normalized;
                //skateBoard.rotation = Quaternion.LookRotation(leftFoot.up, (hips.position - skateBoard.position));
                //skateBoard.position -= skateBoard.up * 0.04f;
                Debug.Log(Vector3.Angle(Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right)));
                Debug.Log(Quaternion.FromToRotation(Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right)).eulerAngles);

                var upVector = Vector3.Cross(leftFoot.position - rightFoot.position, Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right));
                skateBoard.LookAt(skateBoard.position + Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right) * 4f, upVector);
                skateBoard.position -= skateBoard.up * 0.06f;

                //skateBoard.position = (leftFoot.position + rightFoot.position) / 2f;
                ////skateBoard.forward = leftFoot.up;
                //skateBoard.rotation = Quaternion.LookRotation(leftFoot.up, (hips.position - skateBoard.position));
                //skateBoard.position -= skateBoard.up * 0.04f;

                break;
            case "Completed":
                if (oldState == "Foot")
                {

                    skateBoard.position = (leftFoot.position + rightFoot.position) / 2f;
                    //skateBoard.forward = leftFoot.up;

                    //var projectedDot = Vector3.Project(rightFoot.position - leftFoot.position)
                    skateBoard.right = (leftFoot.position - rightFoot.position).normalized;
                    //skateBoard.rotation = Quaternion.LookRotation(leftFoot.up, (hips.position - skateBoard.position));
                    //skateBoard.position -= skateBoard.up * 0.04f;
                    Debug.Log(Vector3.Angle(Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right)));
                    Debug.Log(Quaternion.FromToRotation(Vector3.ProjectOnPlane(skateBoard.forward * -1, skateBoard.right), Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right)).eulerAngles);

                    var upVector1 = Vector3.Cross(leftFoot.position - rightFoot.position, Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right));
                    skateBoard.LookAt(skateBoard.position + Vector3.ProjectOnPlane(leftFoot.up, skateBoard.right) * 4f, upVector1);
                    skateBoard.position -= skateBoard.up * 0.06f;


                    //skateBoard.position = (leftFoot.position + rightFoot.position) / 2f;
                    //skateBoard.rotation = Quaternion.LookRotation(leftFoot.up, (hips.position - skateBoard.position));
                    //skateBoard.position -= skateBoard.up * 0.04f;
                }
                break;
            default:
                skateBoard.localRotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    public void ChangeSkateBoardParent(string parent = "")
    {
        state = parent;
        switch (parent)
        {
            case "Foot":
                PrintLog("Foot");
                animCompleted = false;
                skateBoard.parent = leftFoot;
                fullBodyBipedIK.enabled = false;
                correctRotation = true;
                oldState = "Foot";
                break;
            case "RightFoot":
                PrintLog("RightFoot");
                animCompleted = false;
                skateBoard.parent = rightFoot;
                fullBodyBipedIK.enabled = false;
                correctRotation = true;
                oldState = "RightFoot";
                break;
            case "Hand":
                PrintLog("Hand");
                skateBoard.parent = leftHand;
                oldState = "Hand";
                break;
            case "Completed":
                //Invoke("AnimationCompleted", 0.2f);
                AnimationCompleted();
                break;
            default:
                PrintLog("Normal");
                skateBoard.parent = transform;
                fullBodyBipedIK.enabled = true;
                skateBoard.localRotation = Quaternion.Euler(0, 0, 0);
                correctRotation = false;
                oldState = "Normal";
                break;
        }
    }

    public void StartAnimation(string name)
    {
        FindFirstObjectByType<QATestUI>().SetAnimationName(name);
    }

    public void AnimationCompleted()
    {
        animCompleted = true;
        //FindFirstObjectByType<QATestUI>().SetAnimationName("");
    }

    void PrintLog(string log)
    {
        Debug.Log("PlayerAnimEventListener: " + log);
    }
}
