using UnityEngine;

public class StoreMenus : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject personalPage;
    public GameObject weaponsPage;

    public HUD hud;

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
        hud.hideHud();
    }

}
