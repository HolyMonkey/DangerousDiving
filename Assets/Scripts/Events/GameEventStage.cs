using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "SO/GameEventStage", order = 53)]
public class GameEventStage : ScriptableObject
{
    private List<GameEventStageListener> _listeners = new List<GameEventStageListener>();

    public void Raise(Stage stage)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised(stage);
    }

    public void RegisterListener(GameEventStageListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventStageListener listener)
    {
        _listeners.Remove(listener);
    }
}
