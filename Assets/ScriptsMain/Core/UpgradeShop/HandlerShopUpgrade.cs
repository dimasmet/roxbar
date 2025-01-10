using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerShopUpgrade : MonoBehaviour
{
    public static HandlerShopUpgrade Instance;

    [SerializeField] private List<ButtonShop> buttonShops;
    [SerializeField] private ViewShopUpgrade viewShopUpgrade;

    private BarUpgrade barUpgrade;

    private ItemUpgrade currentChoicedItem;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void InitShopUpgrade(BarUpgrade barUpgrade)
    {
        this.barUpgrade = barUpgrade;

        for (int i = 0; i < buttonShops.Count; i++)
        {
            buttonShops[i].InitShopItem(barUpgrade.itemUpgrades[i]);
        }
    }

    public void UpdateViewData()
    {
        for (int i = 0; i < buttonShops.Count; i++)
        {
            buttonShops[i].UpdateDataView();
        }
    }

    public void ChoiceItem(ItemUpgrade itemUpgrade, Sprite img)
    {
        SoundsGame.Instance.PlayShotSound(SoundName.Click);
        currentChoicedItem = itemUpgrade;
        viewShopUpgrade.ShowItem(currentChoicedItem, img);
    }

    public void UpgradeChoicedItem()
    {
        int numCurrentActive = currentChoicedItem.currentLastUpgade;
        int price = currentChoicedItem.drinkDataUpgrades[numCurrentActive].priceUpgade;
        if (BalancePlayer.Instance.GetCurrentValueBalance() >= price)
        {
            BalancePlayer.Instance.DiscreseBalance(price);
            currentChoicedItem.currentLastUpgade += 1;

            if (currentChoicedItem.currentLastUpgade >= currentChoicedItem.drinkDataUpgrades.Count - 1)
            {
                barUpgrade.countUpgradeItems += 1;
                EventsGame.OnCheckUpgradeCurrentBar?.Invoke();
            }

            MainDataClientsHandler.Instance.SaveBarUpgradeData();
            UpdateViewData();
        }
    }
}
