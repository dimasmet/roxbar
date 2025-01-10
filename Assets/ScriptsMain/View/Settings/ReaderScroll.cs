using UnityEngine;
using UnityEngine.UI;

public class ReaderScroll : ScreenGame
{
    public enum TypeText
    {
        PrivacyPolicy,
        TermsOfUse
    }

    [SerializeField] private Text _titleReader;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _contanerText;
    [SerializeField] private GameObject _privacyApp;
    [SerializeField] private GameObject _termsApp;

    private void Awake()
    {
        _closeButton.onClick.AddListener(() =>
        {
            HideScreen();
            WorkBar.Instance.ActiveMaskObjects(true);
        });
    }

    public void OpenReader(TypeText typeText)
    {
        switch(typeText)
        {
            case TypeText.PrivacyPolicy:
                _privacyApp.SetActive(true);
                _termsApp.SetActive(false);
                _titleReader.text = "Privacy Policy";
                break;
            case TypeText.TermsOfUse:
                _privacyApp.SetActive(false);
                _termsApp.SetActive(true);
                _titleReader.text = "Terms of Use";
                break;
        }

        //Vector3 pos = _contanerText.position;
        //pos.y = 0;
        //_contanerText.position = pos;

        WorkBar.Instance.ActiveMaskObjects(false);

        ShowScreen();
    }
}
