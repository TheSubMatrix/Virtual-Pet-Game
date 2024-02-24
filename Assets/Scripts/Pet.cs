using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pet
{
    const int _maxFullness = 5;
    const int _minFullness = 0;
    const int _maxHappiness = 5;
    const int _minHappiness = 0;
    const int _maxEnergyLevel = 5;
    const int _minEnergyLevel = 0;

    string _name;
    public string Name 
    {  
        get
        { 
            return _name; 
        } 
    }

    int _currentFullness;
    public int CurrentFullness
    {
        get
        {
            return _currentFullness;
        }
        private set
        {
            _currentFullness = Mathf.Clamp(value, _minFullness, _maxFullness);
        }
    }
    public int MaxFullness { get { return _maxFullness; } }

    int _currentHappiness;
    public int CurrentHappiness 
    {
        get
        {
            return _currentHappiness;
        }
        private set
        {
            _currentHappiness = Mathf.Clamp(value, _minHappiness, _maxHappiness);
        }
    }
    public int MaxHappiness { get { return _maxHappiness; } }

    int _currentEnergyLevel;
    public int CurrentEnergyLevel
    {
        get
        {
            return _currentEnergyLevel;
        }
        private set
        {
            _currentEnergyLevel = Mathf.Clamp(value, _minEnergyLevel, _maxEnergyLevel);
        }
    }
    public int MaxEnergyLevel {  get { return _maxEnergyLevel; } }

    public Pet(string name)
    {
        _name = name;
        CurrentFullness = _maxFullness;
        CurrentHappiness = _maxHappiness;
        CurrentEnergyLevel = _maxEnergyLevel;
    }

    public void Eat()
    {
        CurrentFullness++;
    }
    public void Sleep() 
    {
        CurrentEnergyLevel++;
    }
    public void Play()
    {
        CurrentHappiness++;
    }

    public bool LoseFullness()
    {
        CurrentFullness--;
        if(CurrentFullness <= 0)
        {
            return true;
        }
        return false;
    }
    public bool LoseEnergy()
    {
        CurrentEnergyLevel--;
        if (CurrentEnergyLevel <= 0)
        {
            return true;
        }
        return false;
    }
    public bool LoseHappiness()
    {
        CurrentHappiness--;
        if (CurrentHappiness <= 0)
        {
            return true;
        }
        return false;
    }
}
