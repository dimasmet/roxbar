using UnityEngine;

public class BalancePlayer : MonoBehaviour
{
    public static BalancePlayer Instance;

    [SerializeField] private ViewBalance _viewBalance;
    private int _balancePlayer;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        _balancePlayer = PlayerPrefs.GetInt("BalancePlayer");

        if (_balancePlayer == 0)
            _balancePlayer = 2050;

        _viewBalance.SetValueView(_balancePlayer, false);
    }

    public void AddToBalance(int value)
    {
        _balancePlayer += value;
        _viewBalance.SetValueView(_balancePlayer, true);

        SoundsGame.Instance.PlayShotSound(SoundName.BalanceChange);
        SaveBalance();
    }

    public void DiscreseBalance(int value)
    {
        _balancePlayer -= value;
        _viewBalance.SetValueView(_balancePlayer, true);
        SaveBalance();
    }

    public int GetCurrentValueBalance()
    {
        return _balancePlayer;
    }

    private void SaveBalance()
    {
        PlayerPrefs.SetInt("BalancePlayer", _balancePlayer);
    }
}
