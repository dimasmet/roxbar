using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsGame : MonoBehaviour
{
    public static Action OnClientHasLeft;
    public static Action<ClientData,ClientDataToChanged, Sprite> OnClientCome;
    public static Func<Drink, bool> OnCheckDrinkObjectStoppedMove;

    public static Action OnCurrentDrinkPourStart;
    public static Action OnCurrentDrinkPourEnd;

    public static Action<int> OnChangeMoodCurrentClient;

    public static Action OnSyrupPourStart;
    public static Action OnSyrupPourEnd;

    public static Action OnClientReceivedDrink;

    public static Action OnStartGame;
    public static Action OnStopGame;
    public static Action OnTimerBarEnd;

    public static Action OnWaitClientOver;
    public static Action<bool> OnTrakingTouchActive;

    public static Action OnCheckUpgradeCurrentBar;
}
