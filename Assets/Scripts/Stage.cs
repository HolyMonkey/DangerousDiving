using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private string _stageAnimationName;
    [SerializeField] private string _dollyAnimationName;
    [SerializeField] private string _repeatTriggerName;
    [SerializeField] private Vector2 _joystickValues;
    [SerializeField] private int _index;

    public string StageAnimationName => _stageAnimationName;
    public string DollyAnimationName => _dollyAnimationName;
    public string RepeatTriggerName => _repeatTriggerName;
    public Vector2 JoyStickValues => _joystickValues;
    public int Index => _index;


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
