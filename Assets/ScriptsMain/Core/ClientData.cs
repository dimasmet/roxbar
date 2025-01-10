using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ClientData
{
    public int indexClient;
    public string nameClient;
    public List<ClientMessage> clientMessages;
    public SplitText _splitSadMoodMessage;
    public SplitText _splitcheerfulMoodMessage;
}

[System.Serializable]
public class ClientDataToChanged
{
    public int valueMood;
    public StateClient stateClient;
}

[System.Serializable]
public class WrapClientsDataChange
{
    public List<ClientDataToChanged> clientDataToChangeds;
}

[System.Serializable]
public class SplitText
{
    public string firstPart;
    public string secondPart;
}

public enum MoodClient
{
    Normal,
    Happy,
    Sadly
}

public enum StateClient
{
    Close,
    Open
}

[System.Serializable]
public class ClientMessage
{
    public string message;
    public Strength strengthDrink;
    public List<PreferenceClient> preferences;
}

[System.Serializable]
public class PreferenceClient
{
    public SyrupType syrupType;
    public MoodClient moodClient;
}
