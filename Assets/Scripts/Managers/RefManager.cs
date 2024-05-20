using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefManager : MonoBehaviour
{
    public static RefManager I;

    private void Awake() {
        I = this;
    }
    
    [Header("Menus")]
    public GameplayMenuEventListener gameplayMenu;

    [Header("Controller")]
    public PlayerController playerController;

    [Header("General")]
    public PlayerAnimEventListener playerAnimEventListener;
    public Transform skateBoard;
}
