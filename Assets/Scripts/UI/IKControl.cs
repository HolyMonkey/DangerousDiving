using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IKControl : MonoBehaviour, IDragHandler
{
    private RectTransform _rectTransform;
    private Transform _effector;
    private Vector3 _offset;
    private float _xCoord;

    public Rect Rect => new Rect((Vector2) transform.position - (_rectTransform.rect.size * 0.5f), _rectTransform.rect.size);
    public Transform Effector => _effector;

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void SetEffector(Transform effector)
    {
        _effector = effector;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000, LayerMask.GetMask("Effectors")))
            {
                GameObject collideObj = hit.collider.gameObject;
                _xCoord = Camera.main.WorldToScreenPoint(collideObj.transform.position).z;
                collideObj.transform.position = GetMousePosition();
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _xCoord));
    }

    public void Activate()
    {
        GetComponent<Image>().color = Color.red;
    }

    public void Deactivate()
    {
        GetComponent<Image>().color = Color.white;
    }
}
