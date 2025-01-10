using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenu : ScreenGame
{
    [SerializeField] private List<BarLevelButton> barLevelButtons;

    [Header("View")]
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingsButton;

    private void Awake()
    {
        _shopButton.onClick.AddListener(() =>
        {
            HandlerScreensGame.Instance.ShowScreen(HandlerScreensGame.ScreenName.Shop, false);

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });

        _settingsButton.onClick.AddListener(() =>
        {
            HandlerScreensGame.Instance.ShowScreen(HandlerScreensGame.ScreenName.Option, false);
            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });
    }

    public void Init(BarLevelsData barLevelsData)
    {
        for (int i = 0; i < barLevelButtons.Count; i++)
        {
            barLevelButtons[i].InitLevelBar(barLevelsData.barDatas[i]);
        }
    }

    public override void ShowScreen()
    {
        base.ShowScreen();

        UpdateViewData();
    }

    public override void HideScreen()
    {
        base.HideScreen();
    }

    public void UpdateViewData()
    {
        for (int i = 0; i < barLevelButtons.Count; i++)
        {
            barLevelButtons[i].UpdateView();
        }
    }
}
