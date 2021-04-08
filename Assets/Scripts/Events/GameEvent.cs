using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "SO/GameEvent", order = 52)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener listener)
    {
        _listeners.Remove(listener);
    }
}
