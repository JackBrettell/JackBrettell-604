using UnityEngine;

public class StoreMenus : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject personalPage;
    public GameObject weaponsPage;

    public HUD hud;
    public static bool IsShopOpen { get; private set; } = false; // Track whether the shop is open

    public enum shopState
    {
        MainPage,
        PersonalPage,
        WeaponsPage,
        None
    }


    private void SetMenu(shopState state)
    {
        mainPage.SetActive(state == shopState.MainPage);
        weaponsPage.SetActive(state == shopState.WeaponsPage);
        personalPage.SetActive(state == shopState.PersonalPage);

        IsShopOpen = (state != shopState.None);
    } 

    public void OnPersonalUpgrades()
    {
        SetMenu(shopState.PersonalPage);


    }

    public void OnWeaponsUpgrades()
    {
        SetMenu(shopState.WeaponsPage);

    }


    public void CloseAllMenus()
    {
        SetMenu(shopState.None);
        hud.hideCrosshair();
    }

}
