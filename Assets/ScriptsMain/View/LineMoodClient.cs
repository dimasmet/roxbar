using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LineMoodClient : MonoBehaviour
{
    [SerializeField] private Animator _animBlockLine;
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _speed;

    private void Start()
    {
        EventsGame.OnClientReceivedDrink += StopWait;
        EventsGame.OnTimerBarEnd += StopWait;
        EventsGame.OnStopGame += StopWait;
    }

    private void OnDisable()
    {
        EventsGame.OnClientReceivedDrink -= StopWait;
        EventsGame.OnTimerBarEnd -= StopWait;
        EventsGame.OnStopGame -= StopWait;
    }

    public void StartMoodTime()
    {
        _fillImage.fillAmount = 1;
        _animBlockLine.Play("Show");

        StartCoroutine(WaitStartMoodWaiting());
    }

    public void HideLine()
    {
        _animBlockLine.Play("Hide");
    }

    public IEnumerator WaitStartMoodWaiting()
    {
        while (_fillImage.fillAmount > 0)
        {
            _fillImage.fillAmount -= Time.deltaTime * _speed;
            yield return null;
        }

        EventsGame.OnChangeMoodCurrentClient?.Invoke(-10);
        EventsGame.OnClientHasLeft?.Invoke();
    }

    private void StopWait()
    {
        StopAllCoroutines();
        HideLine();
    }

    public float GetPercentMoodWait()
    {
        return _fillImage.fillAmount;
    }
}
