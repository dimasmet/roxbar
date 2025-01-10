using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassObj : MovingObject
{
    [SerializeField] private WaterGlassHandler _waterGlassHandler;

    private Vector2 _positionSource;
    private Vector2 _targetPosition;

    private bool _isTrashcan = false;
    private bool _isClient = false;

    private CharacterClient _client;

    private void Start()
    {
        _positionSource = transform.position;
        _targetPosition = _positionSource;
    }

    public override void ResetPos()
    {
        transform.position = _positionSource;
        _isClient = false;
        _isTrashcan = false;

        _waterGlassHandler.ResetGlassWater();
    }

    public override void StopMove()
    {
        base.StopMove();

        if (_isTrashcan)
        {
            _waterGlassHandler.ResetGlassWater();
        }
        else
        {
            if (_isClient)
            {
                DrinkFinal drinkFinal = _waterGlassHandler.GetDrinkFinal();

                int priceDrink = WorkBar.Instance.CalculatingCost(drinkFinal);

                _client.TakeCocktail(drinkFinal, priceDrink);

                ResetPos();
            }
        }

        SoundsGame.Instance.PlayShotSound(SoundName.GlassOnTable);

        SetPosition(_targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TableBar table))
        {
            table.SetGlass(this);
            _targetPosition = table.GetPosToGlass();
        }

        if (collision.TryGetComponent(out SyrupObj syrup))
        {
            syrup.SetGlass(this);
            _targetPosition = syrup.GetPosToGlass();
        }

        if (collision.TryGetComponent(out Trashcan trashcan))
        {
            trashcan.SelectState(VisualSelectionObject.Select.Select);
            _targetPosition = _positionSource;
            _isTrashcan = true;
        }

        if (collision.TryGetComponent(out CharacterClient characterClient))
        {
            if (_client == null) _client = characterClient;

            _isClient = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TableBar table))
        {
            table.ExitGlass(this);
            _targetPosition = _positionSource;
        }

        if (collision.TryGetComponent(out SyrupObj syrup))
        {
            syrup.ExitGlass(this);
            _targetPosition = _positionSource;
        }

        if (collision.TryGetComponent(out Trashcan trashcan))
        {
            trashcan.SelectState(VisualSelectionObject.Select.AnSelect);
            _isTrashcan = false;
        }

        if (collision.TryGetComponent(out CharacterClient characterClient))
        {
            _isClient = false;
        }
    }

    public override void SetPosition(Vector2 pos)
    {
        base.SetPosition(pos);
        transform.position = pos;
    }

    public bool AddWater(Color color, Strength strength)
    {
        bool isAvailbelToAdd = _waterGlassHandler.AddDrink(color, strength);

        return isAvailbelToAdd;
    }

    public void AddWater(Color color, SyrupType syrup)
    {
        _waterGlassHandler.AddDrink(color, syrup);
    }
}
