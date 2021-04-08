using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _event;

    public UnityEvent Response;

    private void OnEnable()
    {
        _event.RegisterListener(this);
    }

    private void OnDisable()
    {
        _event.UnRegisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
