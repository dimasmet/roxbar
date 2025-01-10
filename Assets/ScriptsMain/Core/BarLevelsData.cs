using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BarLevelsData
{
    public List<BarData> barDatas;
}

public enum StateBarLevel
{
    Close,
    Open
}

[System.Serializable]
public class BarData
{
    public TypeBar typeBar;
    public int countStar;
    public StateBarLevel stateBarLevel;
}
