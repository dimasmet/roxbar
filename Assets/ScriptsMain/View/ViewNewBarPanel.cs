using UnityEngine;
using UnityEngine.UI;

public class ViewNewBarPanel : ScreenGame
{
    [Header("View new bar")]
    [SerializeField] private Image _imageBar;
    [SerializeField] private Text _awardCoinText;
    [SerializeField] private Text _awardCrystalText;

    [SerializeField] private Button _okButton;

    private void Awake()
    {
        _okButton.onClick.AddListener(() =>
        {
            HideScreen();

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });
    }

    public void ShowNewBarOpen(Sprite imageBar, int awardCoin, int awardCrystal)
    {
        SoundsGame.Instance.PlayShotSound(SoundName.NewBarOpen);

        _imageBar.sprite = imageBar;
        _awardCoinText.text = awardCoin.ToString();
        _awardCrystalText.text = awardCrystal.ToString();

        ShowScreen();
    }
}
