using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private string _stageAnimationName;
    [SerializeField] private string _dollyAnimationName;
    [SerializeField] private string _repeatTriggerName;

    public string StageAnimationName => _stageAnimationName;
    public string DollyAnimationName => _dollyAnimationName;

    public string RepeatTriggerName => _repeatTriggerName;

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
