using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPausable
{
    [Header("Player States")]
    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool inInventory;

    [Header("Crosshair")]
    [SerializeField]
    private CrosshairScript CrosshairComponent;
    public CrosshairScript Crosshair => CrosshairComponent;

    // Components
    public HealthComponent Health => healthComponent;
    private HealthComponent healthComponent;

    public WeaponHolder WeaponHolder => weaponHolder;
    private WeaponHolder weaponHolder;

    public InventoryComponent Inventory => inventory;
    private InventoryComponent inventory;

    private GameUIController UIController;
    private PlayerInput playerInput;

    private void Awake()
    {
        if (healthComponent == null)
            healthComponent = GetComponent<HealthComponent>();
        if (weaponHolder == null)
            weaponHolder = GetComponent<WeaponHolder>();
        if (inventory == null)
            inventory = GetComponent<InventoryComponent>();

        UIController = FindObjectOfType<GameUIController>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnPauseGame(InputValue value)
    {
        Debug.Log("Pause Game");
        PauseManager.instance.PauseGame();
    }

    public void OnUnpauseGame(InputValue value)
    {
        Debug.Log("Unpause Game");
        PauseManager.instance.UnpauseGame();
    }

    public void OnInventory(InputValue value)
    {
        if(inInventory)
        {
            inInventory = false;
            OpenInventory(false);
        }
        else
        {
            inInventory = true;
            OpenInventory(true);
        }
    }

    private void OpenInventory(bool open)
    {
        if(open)
        {
            PauseManager.instance.PauseGame();
            UIController.EnableInventoryMenu();
        }
        else
        {
            PauseManager.instance.UnpauseGame();
            UIController.EnableGameMenu();
        }
    }

    public void PauseMenu()
    {
        UIController.EnablePauseMenu();

        playerInput.SwitchCurrentActionMap("PauseActionMap");
    }

    public void UnpauseMenu()
    {
        UIController.EnableGameMenu();

        playerInput.SwitchCurrentActionMap("PlayerActionMap");
    }
}
