using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ModeSelectSceneUI : MonoBehaviour
{
    [SerializeField] private float appearTime = 0.5f;
    [SerializeField] private Image pipeButton;
    [SerializeField] private Image grindButton;
    [SerializeField] private RectTransform directionTrans;
    [SerializeField] private Ease appearEase;

    private void Start()
    {
        var targetPos_pipe = pipeButton.transform.position;
        var targetPos_grind = grindButton.transform.position;
        Debug.Log((pipeButton.rectTransform.anchorMax.x - pipeButton.rectTransform.anchorMin.x) * Screen.width * 1.1f);
        Debug.Log(Screen.width);
        Debug.Log(pipeButton.rectTransform.sizeDelta.x * Screen.width * 1.1f);
        pipeButton.transform.position = pipeButton.transform.position + directionTrans.right * (pipeButton.rectTransform.anchorMax.x - pipeButton.rectTransform.anchorMin.x) * Screen.width * 1.1f;
        grindButton.transform.position = grindButton.transform.position - directionTrans.right * (grindButton.rectTransform.anchorMax.x - grindButton.rectTransform.anchorMin.x) * Screen.width * 1.1f;

        pipeButton.transform.DOMove(targetPos_pipe, appearTime).SetEase(appearEase);
        grindButton.transform.DOMove(targetPos_grind, appearTime).SetEase(appearEase).SetDelay(appearTime);
    }
}
