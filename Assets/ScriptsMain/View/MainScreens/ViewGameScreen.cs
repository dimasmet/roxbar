using UnityEngine;
using UnityEngine.UI;

public class ViewGameScreen : ScreenGame
{
    [SerializeField] private Button _bookButton;
    [SerializeField] private Button _pauseButton;

    [SerializeField] private PauseView pauseView;

    private void Awake()
    {
        _bookButton.onClick.AddListener(() =>
        {
            HandlerScreensGame.Instance.ShowScreen(HandlerScreensGame.ScreenName.Book, false);

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });

        _pauseButton.onClick.AddListener(() =>
        {
            pauseView.RunPause();

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });
    }

    public override void HideScreen()
    {
        //base.HideScreen();
    }
}
