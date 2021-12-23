using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {

    public SO_PlayerInventory inventory;
    public Weapon currentWeapon;

    public PlayerMovement movement;

    private ItemData arrowItem;

    private bool DebugMenu = false;

    private bool InfiniteArrows = false;

    [HideInInspector] public HCamera playerCamera;

    public void Init(HCamera cam) {
        Debug.Log("Init player");

        playerCamera = cam;

        SwitchCursorLock();

        arrowItem = inventory.GetItem(ItemType.Arrow);

        movement.Init(cam);

        currentWeapon.Equip(this);
    }

    public void InternalUpdate() {
        movement.InternalUpdate();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SwitchCursorLock();
        }

        SomeCursorCheck();

        // Debug Menu Start
        // will allow shooting with 0 Arrows
        if (Input.GetKeyDown("0") && DebugMenu == false)
        {
            GameUI.inst.EnableDebugMenu(true);
            Debug.Log("DeBug Menu Activated");
            DebugMenu = true;
        }

        else if (Input.GetKeyDown("/") && InfiniteArrows == false && DebugMenu)
        {
            Debug.Log("Infin. Arrow Activated");
            InfiniteArrows = true;
        }
        else if (Input.GetKeyDown("/") && InfiniteArrows && DebugMenu)
        {
            Debug.Log("Infin. Arrow Deactivated");

            InfiniteArrows = false;
        }

        else if (Input.GetKeyDown("0") && DebugMenu)
        {
            Debug.Log("DeBug Menu Deactivated");
            GameUI.inst.EnableDebugMenu(false);
            DebugMenu = false;
        }
        // Debug Menu End

        if (currentWeapon != null) {

            
            if (arrowItem.count > 0 || InfiniteArrows)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentWeapon.UseStart();
                    GameUI.inst.EnableCrosshair(true);
                }
            }

            if (Input.GetMouseButton(0))
            {
                currentWeapon.UseHold();
                playerCamera.SetZoomPercent(currentWeapon.charge / currentWeapon.maxChargeTime);
                if (arrowItem.count <= 0 && !InfiniteArrows)
                {
                    GameUI.inst.EnableCantShoot(true);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentWeapon.UseRelease();
                playerCamera.SetZoomPercent(0f);
                GameUI.inst.EnableCrosshair(false);
                GameUI.inst.EnableCantShoot(false);
                inventory.TakeItem(arrowItem, 1);
            }
        }
    }

    private void SomeCursorCheck()
    {
        // Gets the cursor position relative to the game window
        Vector2 cursorPosition = playerCamera.cameraComponent.ScreenToViewportPoint(Input.mousePosition);
        // Scales the position properly
        cursorPosition.x *= playerCamera.cameraComponent.scaledPixelWidth;
        cursorPosition.y *= playerCamera.cameraComponent.scaledPixelHeight;

        // Makes cursor invisible only when it's within the game window
        //if (movement.isInWindow(cursorPosition))
        //{
        //    Cursor.visible = false;
        //} else
        //{
        //    Cursor.visible = true;
        //} //This dose not help during building up the game it just keeps unlocking the cursor fromt he game.
    }

    public void InternalFixedUpdate()
    {
        movement.InternalFixedUpdate();
    }

    private void SwitchCursorLock() {
        if (Cursor.lockState == CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}