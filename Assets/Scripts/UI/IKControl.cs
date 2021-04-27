using UnityEngine;
using UnityEngine.EventSystems;

public class IKControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    private RectTransform _rectTransform;
    private Transform _effector;
    private Vector3 _offset;
    private float _xCoord;

    public Transform Effector => _effector;

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnMouseDown()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;

        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent.GetComponent<RectTransform>(), RectTransformToScreenSpace(_rectTransform, Camera.main).position, Camera.main, out pos);
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out pos);

        _effector.position = new Vector3(_effector.position.x, pos.y, pos.z);
        //_effector.position = GetMouseWorld(_effector) + _offset;
    }

    private Vector3 GetMouseWorld(Transform effector)
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.x = _xCoord;
        return mousePoint;
    }

    public static Rect RectTransformToScreenSpace(RectTransform transform, Camera cam, bool cutDecimals = false)
    {
        var worldCorners = new Vector3[4];
        var screenCorners = new Vector3[4];

        transform.GetWorldCorners(worldCorners);

        for (int i = 0; i < 4; i++)
        {
            screenCorners[i] = cam.WorldToScreenPoint(worldCorners[i]);
            if (cutDecimals)
            {
                screenCorners[i].x = (int)screenCorners[i].x;
                screenCorners[i].y = (int)screenCorners[i].y;
            }
        }

        return new Rect(screenCorners[0].x,
                        screenCorners[0].y,
                        screenCorners[2].x - screenCorners[0].x,
                        screenCorners[2].y - screenCorners[0].y);
    }

    public void SetEffector(Transform effector)
    {
        _effector = effector;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("down");
        _xCoord = Camera.main.WorldToScreenPoint(_effector.position).x;
        _offset = _effector.position - GetMouseWorld(_effector);
    }
}
