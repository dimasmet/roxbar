using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MovingObject
{
    [Header("Data drink")]
    [SerializeField] private Strength strength;
    [SerializeField] private Color _colorDrink;

    [Header("Visual")]
    [SerializeField] private VisualSelectionObject _visualSelectionObject;
    [SerializeField] private Animator _animDrink;
    [SerializeField] private ParticleSystem _particleSystemDrink;

    private Vector2 _positionSource;

    public bool isPour = false;
    public bool isPourReady = false;

    private void Start()
    {
        _positionSource = transform.position;
    }

    public Strength GetStrengthDrink()
    {
        return strength;
    }

    public Color GetColorDrink()
    {
        return _colorDrink;
    }

    public override void DownTap()
    {
        base.DownTap();
        if (isPour == false)
        {
            _visualSelectionObject.ChangeState(VisualSelectionObject.Select.Select);
        }
    }

    public override void UpTap()
    {
        base.UpTap();

        if (isPour == false)
        {
            _visualSelectionObject.ChangeState(VisualSelectionObject.Select.AnSelect);
        }
        else
        {
            if (isPourReady)
            {
                isPourReady = false;
                Pour();
            }
        }
    }

    public override void ResetPos()
    {
        transform.position = _positionSource;
        isPour = false;
        isPourReady = false;
        _visualSelectionObject.ChangeState(VisualSelectionObject.Select.AnSelect);
        //_animDrink.Play("Reset");
    }

    private void Pour()
    {
        _animDrink.Play("Pour");
        isReadyToMove = false;
        StartCoroutine(WaitToStartPourParticles());
    }

    private IEnumerator WaitToStartPourParticles()
    {
        yield return new WaitForSeconds(0.4f);
        _particleSystemDrink.Play();

        EventsGame.OnCurrentDrinkPourStart?.Invoke();

        StartCoroutine(WaitToResetPour());
    }

    private IEnumerator WaitToResetPour()
    {
        yield return new WaitForSeconds(1.5f);
        _animDrink.Play("Reset");
        StartCoroutine(WaitToNextPourReady());
    }

    private IEnumerator WaitToNextPourReady()
    {
        yield return new WaitForSeconds(0.5f);
        isReadyToMove = true;
        if (isPour)
            isPourReady = true;
    }

    public void PourReady()
    {
        isPour = true;
        isPourReady = true;
    }

    public override void StopMove()
    {
        base.StopMove();

        bool isConnect = EventsGame.OnCheckDrinkObjectStoppedMove.Invoke(this);

        if (isConnect == false)
        {
            DrinkExitTable();
            ResetPos();
        }
    }

    public void DrinkExitTable()
    {
        isPour = false;
        isPourReady = false;
    }

    public override void SetPosition(Vector2 pos)
    {
        base.SetPosition(pos);
        transform.position = pos;
    }
}
