using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private string _stageAnimationName;
    [SerializeField] private string _dollyAnimationName;

    public string StageAnimationName => _stageAnimationName;
    public string DollyAnimationName => _dollyAnimationName;

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
