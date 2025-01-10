using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrinkFinal
{
    public int strengthValue = 0;
    public List<SyrupType> syrupTypes = new List<SyrupType>();
    public List<Strength> strengths = new List<Strength>();
}

public class WaterGlassHandler : MonoBehaviour
{
    public enum TypeWater
    {
        None,
        Drink,
        Syrup
    }

    [SerializeField] private SpriteRenderer[] _spriteWaters;
    [SerializeField] private float _speedFill;
    private int stepNum = 0;

    private Transform _currentWater;
    private float _valueY;

    private bool isFillActive = false;

    [Header("Current Glass")]
    [SerializeField] private DrinkFinal drinkFinal;

    private TypeWater typeCurrent = TypeWater.None;

    private void Start()
    {
        ResetGlassWater();
    }

    public void ResetGlassWater()
    {
        stepNum = -1;

        drinkFinal = new DrinkFinal();

        for (int i = 0; i < _spriteWaters.Length; i++)
        {
            _spriteWaters[i].color = Color.white;
            _spriteWaters[i].transform.localScale = new Vector2(_spriteWaters[i].transform.localScale.x, 0);
        }
    }

    public bool AddDrink(Color color, Strength strength)
    {
        bool isAvailbleToAdd = true;

        if (stepNum < 2)
        {
            stepNum++;

            if (stepNum >= 3)
            {
                isAvailbleToAdd = false;
            }

            SoundsGame.Instance.PlayShotSound(SoundName.Pour);

            drinkFinal.strengthValue += MainDataClientsHandler.Instance.GetStrength(strength);

            typeCurrent = TypeWater.Drink;
            AddWaterView(color);
            drinkFinal.strengths.Add(strength);
        }
        else
        {
            switch (typeCurrent)
            {
                case TypeWater.Drink:
                    Debug.Log("End limit drink");
                    EventsGame.OnCurrentDrinkPourEnd?.Invoke();
                    break;
                case TypeWater.Syrup:
                    Debug.Log("End limit syrup");
                    EventsGame.OnSyrupPourEnd?.Invoke();
                    break;
            }

            isAvailbleToAdd = false;
        }

        return isAvailbleToAdd;
    }

    public void AddDrink(Color color, SyrupType syrup)
    {
        if (stepNum < 2)
        {
            stepNum++;

            drinkFinal.syrupTypes.Add(syrup);

            SoundsGame.Instance.PlayShotSound(SoundName.Pour);

            typeCurrent = TypeWater.Syrup;
            EventsGame.OnSyrupPourStart?.Invoke();
            AddWaterView(color);
        }
    }

    private void AddWaterView(Color color)
    {
        _spriteWaters[stepNum].color = color;
        _currentWater = _spriteWaters[stepNum].transform;
        _valueY = 0;

        isFillActive = true;      
    }

    public DrinkFinal GetDrinkFinal()
    {
        if (drinkFinal.syrupTypes.Count == 0)
        {
            drinkFinal.syrupTypes.Add(SyrupType.None);
        }

        return drinkFinal;
    }

    private void Update()
    {
        if (isFillActive)
        {
            _valueY = Mathf.MoveTowards(_valueY, 0.15f, Time.deltaTime * _speedFill);
            _currentWater.localScale = new Vector2(_currentWater.localScale.x, _valueY);

            if (_valueY >= 0.15f)
            {
                isFillActive = false;

                switch (typeCurrent)
                {
                    case TypeWater.Drink:
                        EventsGame.OnCurrentDrinkPourEnd?.Invoke();
                        break;
                    case TypeWater.Syrup:
                        EventsGame.OnSyrupPourEnd?.Invoke();
                        break;
                }

                //typeCurrent = TypeWater.None;
            }
        }
    }
}
