using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBar
{
    Bar1,
    Bar2,
    Bar3
}

[System.Serializable] public class DecorBar
{
    public Sprite backgroundBar;
    public Sprite tableBar;
}

public class VisitsBarHandler : MonoBehaviour
{
    private TypeBar _currentBar;

    [SerializeField] private DecorBar[] decorBars;
    [Header("Decor bar view")]
    [SerializeField] private SpriteRenderer _backgroundBar;
    [SerializeField] private SpriteRenderer _tableBar;

    private bool isActiveGame = false;

    private void Start()
    {
        EventsGame.OnClientHasLeft += NextClient;
        EventsGame.OnStartGame += StartGameBar;
        EventsGame.OnStopGame += StopClients;
    }

    private void OnDisable()
    {
        EventsGame.OnClientHasLeft -= NextClient;
        EventsGame.OnStartGame -= StartGameBar;
        EventsGame.OnStopGame -= StopClients;
    }

    public void SetCurrentBarActive(TypeBar typeBar)
    {
        _currentBar = typeBar;

        MusicName musicName = (MusicName)Enum.GetValues(typeof(MusicName)).GetValue((int)typeBar + 1);

        SoundsGame.Instance.PlayMusic(musicName);

        _backgroundBar.sprite = decorBars[(int)typeBar].backgroundBar;
        _tableBar.sprite = decorBars[(int)typeBar].tableBar;
    }

    private void StartGameBar()
    {
        isActiveGame = true;
        NextClient();
    }

    private void NextClient()
    {
        if (isActiveGame)
        StartCoroutine(WaitToNextClient());
    }

    private void StopClients()
    {
        isActiveGame = false;
        EventsGame.OnClientHasLeft?.Invoke();
        StopAllCoroutines();
    }

    private IEnumerator WaitToNextClient()
    {
        yield return new WaitForSeconds(2);
        ClientData client = MainDataClientsHandler.Instance.GetRandomClient(_currentBar);
        ClientDataToChanged clientDataToChanged = MainDataClientsHandler.Instance.GetClientDataToChanged(client.indexClient);
        Sprite spriteCharacter = MainDataClientsHandler.Instance.GetSpriteCurrentCharacter();
        EventsGame.OnClientCome?.Invoke(client, clientDataToChanged, spriteCharacter);
    }
}
