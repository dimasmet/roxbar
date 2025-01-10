using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBar : MonoBehaviour
{
    [SerializeField] private Transform _posToGlass;
    [SerializeField] private Transform _posToDrink;

    public GlassObj _currentGlass;
    public Drink _currentDrink;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Drink drink))
        {
            if (_currentDrink == null)
            {
                if (_currentGlass != null)
                {
                    _currentDrink = drink;
                    _currentDrink.PourReady();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Drink drink))
        {
            if (_currentDrink != null & _currentDrink == drink)
            {
                _currentDrink.DrinkExitTable();
                _currentDrink = null;
            }
        }
    }

    public void SetGlass(GlassObj glass)
    {
        if (_currentGlass == null)
        {
            _currentGlass = glass;
        }
    }

    public void ExitGlass(GlassObj glass)
    {
        if (_currentGlass != null & _currentGlass == glass)
        {
            _currentGlass = null;
            if (_currentDrink != null)
                _currentDrink.ResetPos();
        }
    }

    public Vector2 GetPosToGlass()
    {
        return _posToGlass.position;
    }

    private void Start()
    {
        EventsGame.OnCheckDrinkObjectStoppedMove += CheckDrinkOnTable;

        EventsGame.OnCurrentDrinkPourStart += DrinkPour;
        EventsGame.OnCurrentDrinkPourEnd += GlassFreeMoveActive;

        EventsGame.OnStartGame += ClearScene;
    }

    private void OnDisable()
    {
        EventsGame.OnCheckDrinkObjectStoppedMove -= CheckDrinkOnTable;

        EventsGame.OnCurrentDrinkPourStart -= DrinkPour;
        EventsGame.OnCurrentDrinkPourEnd -= GlassFreeMoveActive;

        EventsGame.OnStartGame -= ClearScene;
    }

    private void ClearScene()
    {
        if (_currentDrink != null)
        {
            _currentDrink.ResetPos();
        }
        if (_currentGlass != null)
        {
            _currentGlass.ResetPos();
        }
    }

    private void DrinkPour()
    {
        Color colorDrink = _currentDrink.GetColorDrink();
        Strength strengthValue = _currentDrink.GetStrengthDrink();
        bool isAvailbleToAdd = _currentGlass.AddWater(colorDrink, strengthValue);

        if (isAvailbleToAdd == true)
            GlassFreezyToMove();
    }

    private bool CheckDrinkOnTable(Drink drink)
    {
        if (_currentDrink != null && _currentDrink == drink && _currentGlass != null)
        {
            _currentDrink.SetPosition(_posToDrink.position);
            return true;
        }
        else return false;
    }

    private void GlassFreezyToMove()
    {
        if (_currentGlass != null)
        {
            _currentGlass.isReadyToMove = false;
        }
    }

    private void GlassFreeMoveActive()
    {
        if (_currentGlass != null)
        {
            _currentGlass.isReadyToMove = true;
        }
    }
}
