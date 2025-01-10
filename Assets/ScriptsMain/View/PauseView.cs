using UnityEngine;
using UnityEngine.UI;

public class PauseView : ScreenGame
{
    [SerializeField] private Button _leaveBtn;
    [SerializeField] private Button _continueBtn;

    [Header("Settings button")]
    [Header("Sounds view")]
    [SerializeField] private Button _soundsBtn;
    [SerializeField] private Sprite _acitveSpriteSounds;
    [SerializeField] private Sprite _noAcitveSpriteSounds;
    [Header("Music view")]
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Sprite _acitveSpriteMusic;
    [SerializeField] private Sprite _noAcitveSpriteMusic;
    [Header("Vibro view")]
    [SerializeField] private Button _vibroBtn;
    [SerializeField] private Sprite _acitveSpriteVibro;
    [SerializeField] private Sprite _noAcitveSpriteVibro;

    private void Awake()
    {
        _leaveBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;

            EventsGame.OnStopGame?.Invoke();

            HandlerScreensGame.Instance.ShowScreen(HandlerScreensGame.ScreenName.Menu, true);

            HideScreen();
        });

        _continueBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            HideScreen();

            EventsGame.OnTrakingTouchActive?.Invoke(true);
        });

        _soundsBtn.onClick.AddListener(() =>
        {
            if (SoundsGame.Instance.ChangeSoundsStatas())
                _soundsBtn.GetComponent<Image>().sprite = _acitveSpriteSounds;
            else _soundsBtn.GetComponent<Image>().sprite = _noAcitveSpriteSounds;
        });

        _musicBtn.onClick.AddListener(() =>
        {
            if (SoundsGame.Instance.ChangeMusicStatus())
                _musicBtn.GetComponent<Image>().sprite = _acitveSpriteMusic;
            else _musicBtn.GetComponent<Image>().sprite = _noAcitveSpriteMusic;
        });

        _vibroBtn.onClick.AddListener(() =>
        {
            if (SoundsGame.Instance.ChangeVibroHaptic())
                _vibroBtn.GetComponent<Image>().sprite = _acitveSpriteVibro;
            else _vibroBtn.GetComponent<Image>().sprite = _noAcitveSpriteVibro;
        });
    }

    public void RunPause()
    {
        if (SoundsGame.Instance.isSoundsGlobalActive)
            _soundsBtn.GetComponent<Image>().sprite = _acitveSpriteSounds;
        else _soundsBtn.GetComponent<Image>().sprite = _noAcitveSpriteSounds;

        if (SoundsGame.Instance.isMusicGlobalActive)
            _musicBtn.GetComponent<Image>().sprite = _acitveSpriteMusic;
        else _musicBtn.GetComponent<Image>().sprite = _noAcitveSpriteMusic;

        if (SoundsGame.Instance.isVibroGlobalActive)
            _vibroBtn.GetComponent<Image>().sprite = _acitveSpriteVibro;
        else _vibroBtn.GetComponent<Image>().sprite = _noAcitveSpriteVibro;

        EventsGame.OnTrakingTouchActive?.Invoke(false);
        ShowScreen();

        Time.timeScale = 0;
    }
}
