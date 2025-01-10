using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar
{
    private Text timerText;

    private float _timeLeft = 0f;

    public TimerBar(Text text)
    {
        timerText = text;
    }

    public IEnumerator WaitStartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }

    public void SetTimer(float time)
    {
        _timeLeft = time;
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
        {
            _timeLeft = 0;
            EventsGame.OnTimerBarEnd?.Invoke();
        }

        timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(_timeLeft / 60), Mathf.FloorToInt(_timeLeft % 60));
    }
}
