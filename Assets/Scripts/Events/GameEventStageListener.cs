using UnityEngine;
using UnityEngine.Events;

public class GameEventStageListener : MonoBehaviour
{
    [SerializeField] private GameEventStage _event;

    public StageUnityEvent Response;

    private void OnEnable()
    {
        _event.RegisterListener(this);
    }

    private void OnDisable()
    {
        _event.UnRegisterListener(this);
    }

    public void OnEventRaised(Stage stage)
    {
        Response.Invoke(stage);
    }
}

[System.Serializable]
public class StageUnityEvent : UnityEvent<Stage>
{ }
