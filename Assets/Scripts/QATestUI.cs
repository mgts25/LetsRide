using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QATestUI : MonoBehaviour
{
    public PlayerController playerController;

    [SerializeField] Text trick1Text;
    [SerializeField] Text trick2Text;
    [SerializeField] Text trick3Text;
    [SerializeField] Text trick4Text;

    [SerializeField] Text trickName;

    private void Update()
    {
        trick1Text.text = "Play Trick1 Index : " + playerController.trick1Ind;
        trick2Text.text = "Play Trick2 Index : " + playerController.trick2Ind;
        trick3Text.text = "Play Trick3 Index : " + playerController.trick3Ind;
        trick4Text.text = "Play Trick4 Index : " + playerController.trick4Ind;
    }

    public void SetAnimationName(string name)
    {
        trickName.text = name;
    }
}
