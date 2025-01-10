using UnityEngine;
using UnityEngine.UI;

public class ViewSettings : ScreenGame
{
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Button _privacyButton;
    [SerializeField] private Button _termsButton;

    [Header("Reader")]
    [SerializeField] private ReaderScroll _reader;

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
        _privacyButton.onClick.AddListener(() =>
        {
            _reader.OpenReader(ReaderScroll.TypeText.PrivacyPolicy);

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });

        _termsButton.onClick.AddListener(() =>
        {
            _reader.OpenReader(ReaderScroll.TypeText.TermsOfUse);

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });

        _closeButton.onClick.AddListener(() =>
        {
            HandlerScreensGame.Instance.CloseLastActiveScreen();

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });

        _tutorialButton.onClick.AddListener(() =>
        {
            HandlerScreensGame.Instance.ShowScreen(HandlerScreensGame.ScreenName.Tutorial, false);

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
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

    public override void ShowScreen()
    {
        base.ShowScreen();

        if (SoundsGame.Instance.isSoundsGlobalActive)
            _soundsBtn.GetComponent<Image>().sprite = _acitveSpriteSounds;
        else _soundsBtn.GetComponent<Image>().sprite = _noAcitveSpriteSounds;

        if (SoundsGame.Instance.isMusicGlobalActive)
            _musicBtn.GetComponent<Image>().sprite = _acitveSpriteMusic;
        else _musicBtn.GetComponent<Image>().sprite = _noAcitveSpriteMusic;

        if (SoundsGame.Instance.isVibroGlobalActive)
            _vibroBtn.GetComponent<Image>().sprite = _acitveSpriteVibro;
        else _vibroBtn.GetComponent<Image>().sprite = _noAcitveSpriteVibro;
    }
}
