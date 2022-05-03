﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Generic event system

// list of all events, events are created here.
public static class Events
{
    // Gameplay
    public static readonly Evt<InteractiveObjectBase> onInteractiveObjectClicked = new Evt<InteractiveObjectBase>();
    public static readonly Evt<InteractiveObjectBase> onInteractiveObjectDestroyed = new Evt<InteractiveObjectBase>();
    public static readonly Evt<InteractiveObjectBase> onObjectBecomeInteractive = new Evt<InteractiveObjectBase>();
    public static readonly Evt<InteractiveObjectBase> onObjectBecomeNotInteractive = new Evt<InteractiveObjectBase>();

    // Player stats
    public static readonly Evt<string> onPlayerDeath = new Evt<string>();

    // Event with parameter example
    //public static readonly Evt<Card> onCardClicked = new Evt<Card>();

    // Event without parameters example
    //public static readonly Evt onGameStarted = new Evt();
}

// event class with no parameters
public class Evt
{
    private event Action EventAction = delegate { };

    public void AddListener(Action listenerMethod)
    {
        EventAction += listenerMethod;
    }

    public void RemoveListener(Action listenerMethod)
    {
        EventAction -= listenerMethod;
    }

    public void Invoke()
    {
        EventAction.Invoke();
    }
}

// event with 1 parameter
public class Evt<T>
{
    private event Action<T> EventAction = delegate { };

    public void AddListener(Action<T> listenerMethod)
    {
        EventAction += listenerMethod;
    }

    public void RemoveListener(Action<T> listenerMethod)
    {
        EventAction -= listenerMethod;
    }

    public void Invoke(T param)
    {
        EventAction.Invoke(param);
    }
}

public class Evt<T, V>
{
    private event Action<T, V> EventAction = delegate { };

    public void AddListener(Action<T, V> listenerMethod)
    {
        EventAction += listenerMethod;
    }

    public void RemoveListener(Action<T, V> listenerMethod)
    {
        EventAction -= listenerMethod;
    }

    public void Invoke(T param1, V param2)
    {
        EventAction.Invoke(param1, param2);
    }
}
