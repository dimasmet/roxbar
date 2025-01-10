using System;
using System.Collections.Generic;
using UnityEngine;

public class MainDataClientsHandler : MonoBehaviour
{
    public static MainDataClientsHandler Instance;

    [SerializeField] private WrapDrinks _wrapDrinks;

    [SerializeField] private List<ClientData> clientDatas;
    [SerializeField] private WrapClientsDataChange wrapClientsDataChange;
    [SerializeField] private List<Sprite> clientCharacter;
    [SerializeField] private List<Sprite> clientCharacterSmall;

    [Header("BarData")]
    [SerializeField] private WrapBarDataUpgrade wrapBarDataUpgrade;
    [SerializeField] private BarLevelsData barLevelsData;
    [SerializeField] private Sprite[] _barHomeSprites;

    [SerializeField] private VisitsBarHandler visitsBarHandler;

    [Header("View handlers")]
    [SerializeField] private ViewMenu viewMenu;
    [SerializeField] private ViewNewBarPanel viewNewBar;
    [SerializeField] private ViewShopUpgrade viewShopUpgrade;

    private int _lastIndexClient;

    public TypeBar _currentTypeBar;
    private TypeBar _lastOpenBar;

    private bool isNewOpenBar = false;
    private int numberOpenBar = 0;

    private void Awake()
    {
        Application.targetFrameRate = 90;

        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        EventsGame.OnCheckUpgradeCurrentBar += CheckCountUpgradeCurrentBar;

        string jsonDrinks = PlayerPrefs.GetString("DrinksInfo");

        if (jsonDrinks != "")
        {
            _wrapDrinks = JsonUtility.FromJson<WrapDrinks>(jsonDrinks);
        }

        string jsonDataChangedClient = PlayerPrefs.GetString("ClientsData");

        if (jsonDataChangedClient != "")
        {
            wrapClientsDataChange = JsonUtility.FromJson<WrapClientsDataChange>(jsonDataChangedClient);
        }

        string jsonBarUpgrade = PlayerPrefs.GetString("BarDataUpgrade");

        if (jsonBarUpgrade != "")
        {
            wrapBarDataUpgrade = JsonUtility.FromJson<WrapBarDataUpgrade>(jsonBarUpgrade);
        }

        string jsonBarData = PlayerPrefs.GetString("BarDataLevels");

        if (jsonBarData != "")
        {
            barLevelsData = JsonUtility.FromJson<BarLevelsData>(jsonBarData);
        }

        int numLastOpenBar = PlayerPrefs.GetInt("NumberLastBarOpen");
        _lastOpenBar = (TypeBar)Enum.GetValues(typeof(TypeBar)).GetValue(numLastOpenBar);
        ChangeShopToBar(_lastOpenBar);
        BookHandler.Instance.InitBookClients(clientDatas, wrapClientsDataChange.clientDataToChangeds, clientCharacterSmall, clientCharacter);
        viewMenu.Init(barLevelsData);
    }

    private void OnDisable()
    {
        EventsGame.OnCheckUpgradeCurrentBar -= CheckCountUpgradeCurrentBar;
    }

    public void ChoiceBar(TypeBar typeBar)
    {
        _currentTypeBar = typeBar;

        HandlerScreensGame.Instance.ShowScreen(HandlerScreensGame.ScreenName.Game, true);

        visitsBarHandler.SetCurrentBarActive(typeBar);

        EventsGame.OnStartGame?.Invoke();
    }

    private void CheckCountUpgradeCurrentBar()
    {
        int numLvl = (int)_lastOpenBar;
        barLevelsData.barDatas[numLvl].countStar += 1;

        if (barLevelsData.barDatas[numLvl].countStar >= 5)
        {
            if (numLvl < 2)
            {
                if (barLevelsData.barDatas[numLvl + 1].stateBarLevel == StateBarLevel.Close)
                {
                    barLevelsData.barDatas[numLvl + 1].stateBarLevel = StateBarLevel.Open;

                    isNewOpenBar = true;
                    numberOpenBar = numLvl + 1;
                }
            }
        }

        SaveBarDataLevel();

        viewMenu.UpdateViewData();
    }

    public void ShopClosedCheckOpenedNewBar()
    {
        if (isNewOpenBar)
        {
            viewNewBar.ShowNewBarOpen(_barHomeSprites[numberOpenBar], 5000, 30);

            TypeBar typebar = (TypeBar)Enum.GetValues(typeof(TypeBar)).GetValue(numberOpenBar);
            ChangeShopToBar(typebar);

            _lastOpenBar = typebar;

            BalancePlayer.Instance.AddToBalance(5000);
            isNewOpenBar = false;
        }
    }

    private void ChangeShopToBar(TypeBar typeBar)
    {
        HandlerShopUpgrade.Instance.InitShopUpgrade(GetBarUpgrade(typeBar));

        viewShopUpgrade.SetImageBar(_barHomeSprites[(int)typeBar]);

        PlayerPrefs.SetInt("NumberLastBarOpen", (int)typeBar);
    }

    public void SaveInfoDrink()
    {
        PlayerPrefs.SetString("DrinksInfo", JsonUtility.ToJson(_wrapDrinks));
    }

    public void SaveDataChangedClients()
    {
        PlayerPrefs.SetString("ClientsData", JsonUtility.ToJson(wrapClientsDataChange));
    }

    public void SaveBarUpgradeData()
    {
        PlayerPrefs.SetString("BarDataUpgrade", JsonUtility.ToJson(wrapBarDataUpgrade));
    }

    public void SaveBarDataLevel()
    {
        PlayerPrefs.SetString("BarDataLevels", JsonUtility.ToJson(barLevelsData));
    }

    public ClientData GetRandomClient(TypeBar typeBar)
    {
        int numberClient = 0;

        switch(typeBar)
        {
            case TypeBar.Bar1:
                if (UnityEngine.Random.Range(0,100) > 20)
                    numberClient = UnityEngine.Random.Range(0, 2);
                else
                    numberClient = UnityEngine.Random.Range(3, 8);
                break;
            case TypeBar.Bar2:
                if (UnityEngine.Random.Range(0, 100) > 20)
                    numberClient = UnityEngine.Random.Range(3, 5);
                else
                    numberClient = UnityEngine.Random.Range(5, 8);
                break;
            case TypeBar.Bar3:
                    numberClient = UnityEngine.Random.Range(6, 8);
                break;
        }
        _lastIndexClient = numberClient;

        ClientData client = clientDatas[_lastIndexClient];
        return client;
    }

    public Sprite GetSpriteCurrentCharacter()
    {
        return clientCharacter[_lastIndexClient];
    }

    public int GetStrength(Strength strength)
    {
        int strengthValue = 0;
        switch (strength)
        {
            case Strength.percent10:
                strengthValue = 10;
                break;
            case Strength.percent30:
                strengthValue = 30;
                break;
            case Strength.percent60:
                strengthValue = 60;
                break;
        }

        return strengthValue;
    }

    public int GetCurrentPriceDrink(Strength strength)
    {
        int price = 0;
        for (int i = 0; i < _wrapDrinks.drinks.Count; i++)
        {
            if (_wrapDrinks.drinks[i].strength == strength)
            {
                ItemUpgrade itemDrink = wrapBarDataUpgrade.barUpgrades[(int)_currentTypeBar].itemUpgrades[_wrapDrinks.drinks[i].numberDrink];
                price = itemDrink.drinkDataUpgrades[itemDrink.currentLastUpgade].resultAwardUpgrade;
                break;
            }
        }

        return price;
    }

    public int GetCurrentPriceSyrup(SyrupType syrup)
    {
        int price = 0;
        for (int i = 0; i < _wrapDrinks.syrups.Count; i++)
        {
            if (_wrapDrinks.syrups[i].syrupType == syrup)
            {
                price = _wrapDrinks.syrups[i].priceAward;
                break;
            }
        }

        return price;
    }

    public Sprite GetSpriteCharacterSmall(int num)
    {
        return clientCharacterSmall[num];
    }

    public ClientDataToChanged GetClientDataToChanged(int index)
    {
        return wrapClientsDataChange.clientDataToChangeds[index];
    }

    public BarUpgrade GetBarUpgrade(TypeBar typeBar)
    {
        return wrapBarDataUpgrade.barUpgrades[(int)typeBar];
    }
}

[System.Serializable]
public class WrapDrinks
{
    public List<Drinks> drinks;
    public List<Syrup> syrups;
}
