using UnityEngine;
using UnityEngine.UI;

public class ShowPayClient : MonoBehaviour
{
    [SerializeField] private Text _textPay;
    [SerializeField] private Text _textTips;
    [SerializeField] private Animator _anim;

    public void ShowPay(int payValueDrink, int tips)
    {
        _textPay.text = "+" + payValueDrink.ToString();
        _textTips.text = "tips: +" + tips;
        _anim.Play("Show");
    }
}
