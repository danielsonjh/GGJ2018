﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Resource : MonoBehaviour
{
    public static Dictionary<ResourceColor, Resource> Map = new Dictionary<ResourceColor, Resource>();

    private const float MaxValue = 100;
    private const float IncrementValue = 10;
    private const float DecrementValue = 5;

    [SerializeField] private ResourceColor _color;
    private float _value = MaxValue;
    private RectTransform _rectTransform;

    void Awake()
    {
        Map.Add(_color, this);
    }

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        _rectTransform.offsetMax = new Vector2(_rectTransform.offsetMax.x, -Screen.height * (MaxValue - _value) / MaxValue);
    }

    public void Increase()
    {
        _value = Mathf.Clamp(_value + IncrementValue, 0, MaxValue);
    }

    public void Decrease()
    {
        _value -= DecrementValue;

        if (_value < 0)
        {
            Debug.LogError("GAME OVER");
            Time.timeScale = 0;
        }
    }
}

public enum ResourceColor
{
    Red, Green, Blue
}

public static class ResourceColorExtensions
{
    public static ResourceColor GetRandom()
    {
        var values = Enum.GetValues(typeof(ResourceColor));
        var random = new Random();
        return (ResourceColor)values.GetValue(random.Next(values.Length));
    }

    public static Color ToUnityColor(this ResourceColor resourceColor)
    {
        switch (resourceColor)
        {
            case ResourceColor.Red:
                return Color.red;
            case ResourceColor.Green:
                return Color.green;
            case ResourceColor.Blue:
                return Color.blue;
            default:
                return Color.white;
        }
    }
}