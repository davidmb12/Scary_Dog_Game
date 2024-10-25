using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager<TEventArgs>
{
    private static Dictionary<string, Action<TEventArgs>> eventDictionary = new Dictionary<string, Action<TEventArgs>>();

    public static void RegisterEvent(string eventType,  Action<TEventArgs> eventHandler)
    {
        if(!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = eventHandler;
        }
        else
        {
            eventDictionary[eventType] += eventHandler;
        }
    }

    public static void UnregisterEvent(string eventType,Action<TEventArgs> eventHandler) 
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= eventHandler;
        }
    }

    public static void TriggerEvent(string eventType,TEventArgs eventArgs)
    {
        if(eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType]?.Invoke(eventArgs);
        }
    }
    public static class EventKey
    {
        //TODO: Add event keys/name for the event Dictionary
    }
}
