using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CareerSelectSceneUI : MonoBehaviour
{
    [SerializeField] private float appearTime = 0.4f;
    [SerializeField] private Image careerButton;
    [SerializeField] private Image rankedButton;
    [SerializeField] private RectTransform directionTrans;
    [SerializeField] private Ease appearEase;

    private void Start()
    {
        var targetPos_pipe = careerButton.transform.position;
        var targetPos_grind = rankedButton.transform.position;
        careerButton.transform.position = careerButton.transform.position + directionTrans.right * (careerButton.rectTransform.anchorMax.x - careerButton.rectTransform.anchorMin.x) * Screen.width * 1.1f;
        rankedButton.transform.position = rankedButton.transform.position - directionTrans.right * (rankedButton.rectTransform.anchorMax.x - rankedButton.rectTransform.anchorMin.x) * Screen.width * 1.1f;

        careerButton.transform.DOMove(targetPos_pipe, appearTime).SetEase(appearEase);
        rankedButton.transform.DOMove(targetPos_grind, appearTime).SetEase(appearEase).SetDelay(appearTime);
    }
}
