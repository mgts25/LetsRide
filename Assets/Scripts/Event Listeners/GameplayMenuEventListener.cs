using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenuEventListener : MonoBehaviour
{
    public Text speedMPH;

    [Serializable]
    public class CameraSettings
    {
        public FollowTarget followCamera;

        [Header("Texts")]
        public Text xValue;
        public Text yValue;
        public Text zValue;

        [Header("Sliders")]
        public Slider xSlider;
        public Slider ySlider;
        public Slider zSlider;
    }

    public CameraSettings cameraSettings;

    public Slider JDSlider;
    public Text JDValue;

    public bool isTrick1Pressed = false;
    public bool isTrick2Pressed = false;
    public bool isTrick3Pressed = false;
    public bool isTrick4Pressed = false;

    private void Start()
    {
        cameraSettings.xSlider.value = 0;
        cameraSettings.ySlider.value = 15;
        cameraSettings.zSlider.value = -20;

        JDSlider.value = 0.5f;
    }

    public void OnPressedTrick1AirButton(bool isPressed)
    {
        isTrick1Pressed = isPressed;
    }

    public void OnPressedTrick2AirButton(bool isPressed)
    {
        isTrick2Pressed = isPressed;
    }

    public void OnPressedTrick3AirButton(bool isPressed)
    {
        isTrick3Pressed = isPressed;
    }

    public void OnPressedTrick4AirButton(bool isPressed)
    {
        isTrick4Pressed = isPressed;
    }

    public void OnClickFakieButton()
    {
        RefManager.I.playerController.PerFormFakie();
    }

    public void SetSpeedUI(float speed)
    {
        speedMPH.text = ((int)speed > 70 ? 70 : (int)speed).ToString();
    }

    public void OnXVlueChanged(Single value)
    {
        cameraSettings.followCamera.offset.x = value;
        cameraSettings.xValue.text = "" + value;
    }

    public void OnYVlueChanged(Single value)
    {
        cameraSettings.followCamera.offset.y = value;
        cameraSettings.yValue.text = "" + value;
    }

    public void OnZVlueChanged(Single value)
    {
        cameraSettings.followCamera.offset.z = value;
        cameraSettings.zValue.text = "" + value;
    }

    // JD = Jump Distance
    public void OnJDValueChange(Single value)
    {
        RefManager.I.playerController.jumpSettings.max_h_Tojump = 1 - value;
        JDValue.text = (value).ToString("#.00");
    }
}
