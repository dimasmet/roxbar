using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Drinks
{
    public int numberDrink;
    public Strength strength;
    public int priceAward;
}

[System.Serializable]
public class Syrup
{
    public int numberSyrup;
    public SyrupType syrupType;
    public int priceAward;
}

[System.Serializable]
public class WrapBarDataUpgrade
{
    public List<BarUpgrade> barUpgrades;
}

[System.Serializable]
public class BarUpgrade
{
    public List<ItemUpgrade> itemUpgrades;
    public int countUpgradeItems = 0;
}

[System.Serializable]
public class ItemUpgrade
{
    public int currentLastUpgade = 0;
    public List<ItemDataUpgrade> drinkDataUpgrades;
}

[System.Serializable]
public class ItemDataUpgrade
{
    public int priceUpgade;
    public int resultAwardUpgrade;
}

public enum Strength
{
    percent0,
    percent10,
    percent30,
    percent60,
    percentGreater0,
    percentGreater10,
    percentGreater20,
    percentGreater30,
    percentGreater40,
    percentGreater50,
    percentGreater60,
}

public enum SyrupType
{
    None,
    Kiwi,
    Lemon,
    Raspberry,
    Pineapple
}
