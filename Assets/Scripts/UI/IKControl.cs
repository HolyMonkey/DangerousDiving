using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IKControl : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Transform _effector;
    private float _zCoord;
    private bool _isActive = true;

    public bool IsActive => _isActive;

    public Rect Rect => new Rect((Vector2) transform.position - (_rectTransform.rect.size * 0.5f), _rectTransform.rect.size);
    public Transform Effector => _effector;

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    
    public void SetEffector(Transform effector)
    {
        _effector = effector;
        _effector.GetComponent<Effector>().TargetMatch += OnEffectorDeactive;
    }

    private void OnEffectorDeactive()
    {
        Hide();
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zCoord));
    }

    public void Activate()
    {
        GetComponent<Image>().color = Color.red;
    }

    public void Deactivate()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void Hide()
    {
        GetComponent<Image>().color = new Color(1,1,1,0);
        _isActive = false; 
    }

    public void Move(Vector2 mousePosition)
    {
        transform.position = mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, LayerMask.GetMask("Effectors")))
        {
            GameObject effector = hit.collider.gameObject;
            _zCoord = Camera.main.WorldToScreenPoint(effector.transform.position).z;
            effector.transform.position = GetMousePosition();
            effector.GetComponent<Effector>().DoMatching();
        }
    }

    public void ResetControl()
    {
        Deactivate();
        _isActive = true;
    }
}
