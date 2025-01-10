using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialView : ScreenGame
{
    [Header("Control tutorial")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _closeTutorialButton;

    [Header("Tutorial steps")]
    [SerializeField] private GameObject[] _stepsTutorial;
    [SerializeField] private Animator _animFrameTutorial;

    private int numberCurrentStep = 0;

    private void Awake()
    {
        _nextButton.onClick.AddListener(() =>
        {
            StartCoroutine(WaitNextStepTutorial());

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });

        _backButton.onClick.AddListener(() =>
        {
            StartCoroutine(WaitBackStepTutorial());
            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });

        _closeTutorialButton.onClick.AddListener(() =>
        {
            HandlerScreensGame.Instance.CloseLastActiveScreen();

            SoundsGame.Instance.PlayShotSound(SoundName.Click);
        });
    }

    public override void ShowScreen()
    {
        base.ShowScreen();

        ActiveStep(0);
    }

    private IEnumerator WaitNextStepTutorial()
    {
        _animFrameTutorial.Play("Close");
        _nextButton.interactable = false;
        _backButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        ActiveStep(numberCurrentStep + 1);
        _animFrameTutorial.Play("Open");
    }

    private IEnumerator WaitBackStepTutorial()
    {
        _animFrameTutorial.Play("Close");
        _nextButton.interactable = false;
        _backButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        ActiveStep(numberCurrentStep - 1);
        _animFrameTutorial.Play("Open");
    }

    private void ActiveStep(int number)
    {
        _stepsTutorial[numberCurrentStep].SetActive(false);
        numberCurrentStep = number;
        _stepsTutorial[numberCurrentStep].SetActive(true);

        _nextButton.interactable = true;
        _backButton.interactable = true;

        if (numberCurrentStep >= _stepsTutorial.Length - 1)
            _nextButton.gameObject.SetActive(false);
        else
            _nextButton.gameObject.SetActive(true);

        if (numberCurrentStep == 0)
            _backButton.gameObject.SetActive(false);
        else
            _backButton.gameObject.SetActive(true);
    }
}
