using UnityEngine;
using UnityEngine.UI;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GameEvent _levelStart;

    public void OnStartButtonClick()
    {
        _startButton.gameObject.SetActive(false);
        _levelStart.Raise();
    }
}


