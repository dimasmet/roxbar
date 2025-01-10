using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerScreensGame : MonoBehaviour
{
    public static HandlerScreensGame Instance;

    [SerializeField] private ScreenGame _bookScreen;
    [SerializeField] private ScreenGame _menuScreen;
    [SerializeField] private ScreenGame _shopScreen;
    [SerializeField] private ScreenGame _gameScreen;
    [SerializeField] private ScreenGame _tutorialScreen;
    [SerializeField] private ScreenGame _preview;
    [SerializeField] private ScreenGame _settingsScreen;

    [SerializeField] private List<ScreenGame> _activeScreens;

    public enum ScreenName
    {
        Menu,
        Shop,
        Game,
        Book,
        Tutorial,
        Option,
        Preview
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        ShowScreen(ScreenName.Menu, false);
        ShowScreen(ScreenName.Preview, false);

        StartCoroutine(WaitToHidePreview());
    }

    private IEnumerator WaitToHidePreview()
    {
        yield return new WaitForSeconds(1.5f);

        CloseLastActiveScreen();

        if (PlayerPrefs.GetInt("FirstRunToShowTutorial") != 1)
        {
            yield return new WaitForSeconds(1);
            ShowScreen(ScreenName.Tutorial, false);
            PlayerPrefs.SetInt("FirstRunToShowTutorial", 1);
        }
    }

    public void ShowScreen(ScreenName name, bool isCloseCurrent)
    {
        if (isCloseCurrent)
        {
            CloseAllActiveScreens();
        }

        switch (name)
        {
            case ScreenName.Book:
                _bookScreen.ShowScreen();
                _activeScreens.Add(_bookScreen);
                break;
            case ScreenName.Shop:
                _shopScreen.ShowScreen();
                _activeScreens.Add(_shopScreen);
                break;
            case ScreenName.Menu:
                _menuScreen.ShowScreen();
                _activeScreens.Add(_menuScreen);

                SoundsGame.Instance.PlayMusic(MusicName.MenuMusic);
                break;
            case ScreenName.Game:
                _gameScreen.ShowScreen();
                _activeScreens.Add(_gameScreen);
                break;
            case ScreenName.Tutorial:
                _tutorialScreen.ShowScreen();
                _activeScreens.Add(_tutorialScreen);
                break;
            case ScreenName.Preview:
                _preview.ShowScreen();
                _activeScreens.Add(_preview);
                break;
            case ScreenName.Option:
                _settingsScreen.ShowScreen();
                _activeScreens.Add(_settingsScreen);
                break;
        }
    }

    private void CloseAllActiveScreens()
    {
        for (int i = 0; i < _activeScreens.Count; i++)
        {
            _activeScreens[i].HideScreen();
        }

        _activeScreens.Clear();
    }

    public void CloseActiveScreen(ScreenGame screen)
    {
        if (_activeScreens.Contains(screen))
        {
            screen.HideScreen();
            _activeScreens.Remove(screen);
        }
    }

    public void CloseLastActiveScreen()
    {
        int indexLast = _activeScreens.Count - 1;
        if (indexLast < 0) indexLast = 0;

        _activeScreens[indexLast].HideScreen();
        _activeScreens.RemoveAt(indexLast);
    }
}
