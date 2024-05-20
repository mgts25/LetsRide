using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCareerUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> careerFlagSprites;
    [SerializeField] private List<Sprite> careerSprites;
    [SerializeField] private List<string> careerNames;
    [SerializeField] private List<Sprite> careerEnterButtonSprites;

    [SerializeField] private Image careerFlagImage;
    [SerializeField] private Image careerImage;
    [SerializeField] private Text careerName;
    [SerializeField] private Image careerEnterButton;

    private int currentCareerIndex = 0;
    public void OnNextCareer()
    {
        if (currentCareerIndex >= careerFlagSprites.Count - 1)
            return;

        currentCareerIndex++;

        SetCareerUI();
    }

    public void OnPrevCareer()
    {
        if (currentCareerIndex <= 0)
            return;

        currentCareerIndex--;

        SetCareerUI();
    }

    public void SetCareerUI()
    {
        careerFlagImage.sprite = careerFlagSprites[currentCareerIndex];
        careerImage.sprite = careerSprites[currentCareerIndex];
        careerEnterButton.sprite = careerEnterButtonSprites[currentCareerIndex];
        careerName.text = careerNames[currentCareerIndex];
    }
}
