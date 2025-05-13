using UnityEngine;

public class StoreMenus : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject personalPage;
    public GameObject weaponsPage;
    public GameObject WeaponSelectionPage;

    public HUD hud;
    public static bool IsShopOpen { get; private set; } = false; // Track whether the shop is open

    public enum shopState
    {
        MainPage,
        PersonalPage,
        WeaponSelectionPage,
        WeaponsPage,
        None
    }


    public void SetMenu(shopState state)
    {

        mainPage.SetActive(state == shopState.MainPage);
        WeaponSelectionPage.SetActive(state == shopState.WeaponSelectionPage);
        weaponsPage.SetActive(state == shopState.WeaponsPage);
        personalPage.SetActive(state == shopState.PersonalPage);

        IsShopOpen = (state != shopState.None);

        hud.ShopToggled(IsShopOpen);
        //Debug.Log(IsShopOpen);
        //Debug.Log(state);

    }

    public void OnPersonalUpgrades()
    {
        SetMenu(shopState.PersonalPage);


    }

    public void OnWeaponSelectionPage()
    {
        SetMenu(shopState.WeaponSelectionPage);
    }

    public void OnWeaponsUpgrades()
    {
        SetMenu(shopState.WeaponsPage);

    }


    public void CloseAllMenus()
    {
        SetMenu(shopState.None);
    }

}
