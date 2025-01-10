using UnityEngine;
using UnityEngine.UI;

public class ViewShopUpgrade : ScreenGame
{
    [SerializeField] private Button _closeShopButton;
    [SerializeField] private Image _imageBar;

    [Header("View Choice Item")]
    [SerializeField] private GameObject _panelShowItem;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _currentPercentText;
    [SerializeField] private Text _upgradePercentText;
    [SerializeField] private Text _priceText;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _closeViewItemBtn;

    private void Awake()
    {
        _upgradeButton.onClick.AddListener(() =>
        {
            HandlerShopUpgrade.Instance.UpgradeChoicedItem();
            _panelShowItem.SetActive(false);

            SoundsGame.Instance.PlayShotSound(SoundName.Upgrade);
        });

        _closeViewItemBtn.onClick.AddListener(() =>
        {
            _panelShowItem.SetActive(false);

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });

        _closeShopButton.onClick.AddListener(() =>
        {
            HandlerScreensGame.Instance.CloseLastActiveScreen();

            MainDataClientsHandler.Instance.ShopClosedCheckOpenedNewBar();

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });
    }

    public override void ShowScreen()
    {
        base.ShowScreen();

        HandlerShopUpgrade.Instance.UpdateViewData();
    }

    public void ShowItem(ItemUpgrade item, Sprite imageItem)
    {
        _panelShowItem.SetActive(true);
        _itemImage.sprite = imageItem;
        _currentPercentText.text = "+" + GetPercentAdd(item.currentLastUpgade) + "%";
        _upgradePercentText.text = "+" + GetPercentAdd(item.currentLastUpgade + 1) + "%";

        _priceText.text = item.drinkDataUpgrades[item.currentLastUpgade].priceUpgade.ToString();
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

    public void SetImageBar(Sprite sprite)
    {
        _imageBar.sprite = sprite;
    }
}
