using UnityEngine;

public class RunSyrup : MonoBehaviour
{
    private SyrupObj _syrupObj;

    private bool isActiveButton = true;

    private void Start()
    {
        EventsGame.OnTrakingTouchActive += ClickTraking;
        EventsGame.OnSyrupPourStart += TrakingClickStop;
        EventsGame.OnSyrupPourEnd += TrakingClickStart;
    }

    private void OnDisable()
    {
        EventsGame.OnTrakingTouchActive -= ClickTraking;
        EventsGame.OnSyrupPourStart -= TrakingClickStop;
        EventsGame.OnSyrupPourEnd -= TrakingClickStart;
    }

    private void ClickTraking(bool st)
    {
        if (st)
        {
            TrakingClickStart();
        }
        else
        {
            TrakingClickStop();
        }
    }

    private void TrakingClickStop()
    {
        isActiveButton = false;
    }

    private void TrakingClickStart()
    {
        isActiveButton = true;
    }

    public void Init(SyrupObj syrup)
    {
        _syrupObj = syrup;
    }

    private void OnMouseDown()
    {
        if (isActiveButton)
        _syrupObj.RunSyrup();
    }
}
