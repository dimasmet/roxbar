using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarLevelButton : MonoBehaviour
{
    [SerializeField] private StarsView starsView;
    [SerializeField] private Button _button;

    [Header("view bar")]
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private GameObject _closeIcon;

    private BarData bar;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            MainDataClientsHandler.Instance.ChoiceBar(bar.typeBar);
        });
    }

    public void InitLevelBar(BarData barData)
    {
        bar = barData;
        UpdateView();
    }

    public void UpdateView()
    {
        if (starsView != null)
        starsView.SetCountStar(bar.countStar);

        switch (bar.stateBarLevel)
        {
            case StateBarLevel.Close:
                starsView.gameObject.SetActive(false);
                _button.interactable = false;
                _img.sprite = _lockedSprite;
                _closeIcon.SetActive(true);
                break;
            case StateBarLevel.Open:
                starsView.gameObject.SetActive(true);
                _button.interactable = true;
                _img.sprite = _activeSprite;
                _closeIcon.SetActive(false);
                break;
        }
    }
}
