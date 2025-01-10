using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterClient : MonoBehaviour
{
    public enum ActionClient
    {
        Come,
        HasLeft
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private MessageClientView _messageClientView;
    private int _numberMessage;

    private ClientData _currentClientData;
    private ClientDataToChanged _currentClientDataToChanged;

    [SerializeField] private LineMoodClient lineMoodClient;

    [Header("Emoji view")]
    [SerializeField] private Material[] _materialsEmojiNormal;
    [SerializeField] private Material[] _materialsEmojiHappy;
    [SerializeField] private Material[] _materialsEmojiSadly;

    [SerializeField] private ParticleSystem _emojiParticles;

    [Header("Client Desire")]
    public Strength strength;
    public List<PreferenceClient> preferenceClients = new List<PreferenceClient>();

    private void Start()
    {
        EventsGame.OnClientCome += SetCharacter;
        EventsGame.OnClientHasLeft += HasLeftClient;
        EventsGame.OnChangeMoodCurrentClient += ChangeMoodValue;
        EventsGame.OnStopGame += StopGame;
    }

    private void OnDisable()
    {
        EventsGame.OnClientCome -= SetCharacter;
        EventsGame.OnClientHasLeft -= HasLeftClient;
        EventsGame.OnChangeMoodCurrentClient -= ChangeMoodValue;
        EventsGame.OnStopGame -= StopGame;
    }

    private void StopGame()
    {
        StopAllCoroutines();
    }

    public void SetCharacter(ClientData client, ClientDataToChanged clientDataToChanged, Sprite sprite)
    {
        _currentClientDataToChanged = clientDataToChanged;
        _currentClientData = client;
        _sprite.sprite = sprite;
        MoveClient(ActionClient.Come);

        StopAllCoroutines();
        StartCoroutine(WaitStartMessage());
    }

    private IEnumerator WaitStartMessage()
    {
        yield return new WaitForSeconds(3);

        lineMoodClient.StartMoodTime();

        _numberMessage = Random.Range(0, _currentClientData.clientMessages.Count);
        _messageClientView.SetMessage(_currentClientData.nameClient, _currentClientData.clientMessages[_numberMessage].message, false);

        strength = _currentClientData.clientMessages[_numberMessage].strengthDrink;

        preferenceClients = _currentClientData.clientMessages[_numberMessage].preferences;

        SoundsGame.Instance.PlayShotSound(SoundName.Message);

        yield return new WaitForSeconds(10);

        SplitText splitMessageMood;

        if (_currentClientDataToChanged.valueMood > 50)
        {
            splitMessageMood = _currentClientData._splitcheerfulMoodMessage;
        }
        else
        {
            splitMessageMood = _currentClientData._splitSadMoodMessage;
        }

        _messageClientView.SetLongMessage(splitMessageMood);
    }

    private void HasLeftClient()
    {
        _messageClientView.CloseMessageWindow();
        MoveClient(ActionClient.HasLeft);
    }

    public void MoveClient(ActionClient actionClient)
    {
        switch (actionClient)
        {
            case ActionClient.Come:
                _animator.Play("Come");
                break;
            case ActionClient.HasLeft:
                _animator.Play("HasLeft");
                break;
        }
    }

    public void TakeCocktail(DrinkFinal drink, int priceDrink)
    {
        bool isStrengthAlcoholTrue = CheckValueStrengthDrink(drink.strengthValue);

        int valueAddMood = 0;
        int tipsClient = 0;

        bool isMatch = false;
        int num = -1;

        if (isStrengthAlcoholTrue)
        {
            for (int i = 0; i < drink.syrupTypes.Count; i++)
            {
                isMatch = false;
                num = -1;
                for (int y = 0; y < preferenceClients.Count; y++)
                {
                    if (preferenceClients[y].syrupType == drink.syrupTypes[i])
                    {
                        isMatch = true;
                        num = y;
                        break;
                    }
                }
                if (isMatch)
                {
                    int mood = GetValueMood(preferenceClients[num].moodClient);
                    valueAddMood += mood;

                    if (mood == 10)
                        tipsClient = 30;
                    else
                    {
                        if (mood == 5)
                        {
                            tipsClient = 15;
                        }
                        else
                        {
                            tipsClient = 0;
                        }
                    }
                }
                else
                {
                    tipsClient = 0;
                    valueAddMood += GetValueMood(MoodClient.Sadly);
                }
            }
        }
        else
        {
            tipsClient = 0;
            valueAddMood += GetValueMood(MoodClient.Sadly);
        }

        int valueMood = _currentClientDataToChanged.valueMood;
        valueMood += valueAddMood;
        valueMood = Mathf.Clamp(valueMood, 0, 100);
        _currentClientDataToChanged.valueMood = valueMood;

        //Save
        MainDataClientsHandler.Instance.SaveDataChangedClients();

        ShowEmoji(valueAddMood);

        if (tipsClient > 0)
        {
            float percentWaitMood = lineMoodClient.GetPercentMoodWait();
            percentWaitMood *= 100;
            Debug.Log(percentWaitMood + " %  wait mood");

            if (percentWaitMood > 70)
            {
                tipsClient += 20;
            }
            else
            {
                if (percentWaitMood > 30)
                {
                    tipsClient += 5;
                }
                else
                {
                    tipsClient -= 10;
                }
            }

            float coefTips = _currentClientDataToChanged.valueMood * 0.01f;
            tipsClient = (int)(tipsClient + (tipsClient * coefTips));

            Debug.Log("Tips Client and valueMood" + tipsClient);
        }

        int payValue = priceDrink + tipsClient;
        Debug.Log("Sum pay " + payValue);
        WorkBar.Instance.ReceivePayment(priceDrink, tipsClient);

        EventsGame.OnClientReceivedDrink?.Invoke();

        StartCoroutine(WaitToHasLeft());
    }

    private void ShowEmoji(int resultValue)
    {
        if (resultValue >= 10)
        {
            _emojiParticles.GetComponent<ParticleSystemRenderer>().material = _materialsEmojiHappy[Random.Range(0, _materialsEmojiHappy.Length)];
            SoundsGame.Instance.PlayShotSound(SoundName.FunMood);
        }
        else
        {
            if (resultValue > 0)
            {
                _emojiParticles.GetComponent<ParticleSystemRenderer>().material = _materialsEmojiNormal[Random.Range(0, _materialsEmojiNormal.Length)];
                SoundsGame.Instance.PlayShotSound(SoundName.GoodMood);
            }
            else
            {
                _emojiParticles.GetComponent<ParticleSystemRenderer>().material = _materialsEmojiSadly[Random.Range(0, _materialsEmojiSadly.Length)];
                SoundsGame.Instance.PlayShotSound(SoundName.NoFunMood);
                SoundsGame.Instance.RunVibroHaptic(VibroName.VibroType3);
            }
        }

        _emojiParticles.Play();
    }

    private IEnumerator WaitToHasLeft()
    {
        yield return new WaitForSeconds(1f);
        EventsGame.OnClientHasLeft?.Invoke();
        lineMoodClient.HideLine();
    }

    private int GetValueMood(MoodClient moodClient) 
    {
        int valueMood = 0;
        switch (moodClient)
        {
            case MoodClient.Normal:
                valueMood = 5;
                break;
            case MoodClient.Happy:
                valueMood = 10;
                break;
            case MoodClient.Sadly:
                valueMood = -10;
                break;
        }

        return valueMood;
    }

    private bool CheckValueStrengthDrink(int strengthDrink)
    {
        bool isStrengthTrue = false;

        switch (strength)
        {
            case Strength.percent0:
                if (strengthDrink == 0)
                    isStrengthTrue = true;
                break;
            case Strength.percent10:
                if (strengthDrink == 10)
                    isStrengthTrue = true;
                break;
            case Strength.percent30:
                if (strengthDrink == 30)
                    isStrengthTrue = true;
                break;
            case Strength.percent60:
                if (strengthDrink == 60)
                    isStrengthTrue = true;
                break;
            case Strength.percentGreater0:
                if (strengthDrink > 0)
                    isStrengthTrue = true;
                break;
            case Strength.percentGreater10:
                if (strengthDrink >= 10)
                    isStrengthTrue = true;
                break;
            case Strength.percentGreater20:
                if (strengthDrink >= 20)
                    isStrengthTrue = true;
                break;
            case Strength.percentGreater30:
                if (strengthDrink >= 30)
                    isStrengthTrue = true;
                break;
            case Strength.percentGreater40:
                if (strengthDrink >= 40)
                    isStrengthTrue = true;
                break;
            case Strength.percentGreater50:
                if (strengthDrink >= 50)
                    isStrengthTrue = true;
                break;
            case Strength.percentGreater60:
                if (strengthDrink >= 60)
                    isStrengthTrue = true;
                break;
        }

        return isStrengthTrue;
    }

    private void ChangeMoodValue(int changeValue)
    {
        _currentClientDataToChanged.valueMood += changeValue;
        _currentClientDataToChanged.valueMood = Mathf.Clamp(_currentClientDataToChanged.valueMood, 0, 100);
        ShowEmoji(changeValue);
        MainDataClientsHandler.Instance.SaveDataChangedClients();
    }
}
