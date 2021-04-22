using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private string _dollyAnimationName;
    [SerializeField] private Vector2 _joystickValues;

    public string DollyAnimationName => _dollyAnimationName;
    public Vector2 JoyStickValues => _joystickValues;
    public int Index => transform.GetSiblingIndex();


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
