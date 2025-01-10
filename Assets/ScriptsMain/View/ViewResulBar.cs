using UnityEngine;
using UnityEngine.UI;

public class ViewResulBar : ScreenGame
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Text _resultCoinText;

    private void Awake()
    {
        _closeButton.onClick.AddListener(() =>
        {
            HideScreen();
            HandlerScreensGame.Instance.ShowScreen(HandlerScreensGame.ScreenName.Menu, true);

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });
    }

    public void ShowResult(int resultScore)
    {
        _resultCoinText.text = resultScore.ToString();
        ShowScreen();

        SoundsGame.Instance.PlayShotSound(SoundName.Message);
    }
}
