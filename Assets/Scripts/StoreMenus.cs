using UnityEngine;

public class StoreMenus : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject personalPage;
    public GameObject weaponsPage;




    public void OnPersonalUpgrades()
    {
        mainPage.SetActive(false);
        weaponsPage.SetActive(false);
        personalPage.SetActive(true);
    }

    public void OnWeaponsUpgrades()
    {
        mainPage.SetActive(false);
        personalPage.SetActive(false);
        weaponsPage.SetActive(true);
    }

    public void CloseAllMenus()
    {
        mainPage.SetActive(false);
        personalPage.SetActive(false);
        weaponsPage.SetActive(false);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
