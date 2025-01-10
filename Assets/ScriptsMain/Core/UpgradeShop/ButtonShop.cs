using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] private Image _imgItem;
    [SerializeField] private Button _button;
    [SerializeField] private Text _priceText;
    [SerializeField] private GameObject _upgradeMaxInfo;

    [Header("View current percent price")]
    [SerializeField] private Text _currentPercentText;

    private ItemUpgrade itemUpgrade;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            HandlerShopUpgrade.Instance.ChoiceItem(itemUpgrade, _imgItem.sprite);
        });
    }

    public void InitShopItem(ItemUpgrade itemUpgrade)
    {
        this.itemUpgrade = itemUpgrade;
        UpdateDataView();
    }

    public void UpdateDataView()
    {
        int priceUpgrade = itemUpgrade.drinkDataUpgrades[itemUpgrade.currentLastUpgade].priceUpgade;
        _priceText.text = priceUpgrade.ToString();

        _currentPercentText.text = "+" + GetPercentAdd(itemUpgrade.currentLastUpgade) + "%";

        if (itemUpgrade.currentLastUpgade >= itemUpgrade.drinkDataUpgrades.Count - 1)
        {
            _upgradeMaxInfo.gameObject.SetActive(true);
            _priceText.transform.parent.gameObject.SetActive(false);
            _button.interactable = false;
        }
        else
        {
            _upgradeMaxInfo.gameObject.SetActive(false);
            _priceText.transform.parent.gameObject.SetActive(true);

            if (priceUpgrade <= BalancePlayer.Instance.GetCurrentValueBalance())
            {
                _button.interactable = true;
            }
            else
            {
                _button.interactable = false;
            }
        }
    }

    private int GetPercentAdd(int currentUpgade)
    {
        int percentInt = 0;
        switch (currentUpgade)
        {
            case 0:
                percentInt = 5;
                break;
            case 1:
                percentInt = 10;
                break;
            case 2:
                percentInt = 15;
                break;
        }

        return percentInt;
    }
}
