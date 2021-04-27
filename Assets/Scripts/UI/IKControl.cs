using UnityEngine;
using UnityEngine.EventSystems;

public class IKControl : MonoBehaviour, IDragHandler
{
    private RectTransform _rectTransform;
    private Transform _effector;
    private Vector3 _offset;
    private float _xCoord;

    public Transform Effector => _effector;

    
    /***/

	private Vector3 screenPoint;
	private Vector3 offset;
		
	void OnMouseDown()
    {
        Debug.Log("click");
		screenPoint = Camera.main.WorldToScreenPoint(_effector.transform.position);
		offset = _effector.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
		
	void OnMouseDrag()
    {
        Debug.Log("drag");
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        _effector.transform.position = cursorPosition;
	}

    /*****/


    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;

        /*
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent.GetComponent<RectTransform>(), RectTransformToScreenSpace(_rectTransform, Camera.main).position, Camera.main, out pos);
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out pos);

        _effector.position = new Vector3(_effector.position.x, pos.y, pos.z);
        */
        
        //_effector.position = GetMouseWorld(_effector) + _offset;
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
                Debug.Log("drag");
                GameObject collideObj = hit.collider.gameObject;
                _xCoord = Camera.main.WorldToScreenPoint(collideObj.transform.position).z;
                collideObj.transform.position = GetMousePosition();
            }

        }
    }

    private Vector3 GetMousePosition()
    {
        //return Camera.main.ScreenToWorldPoint(new Vector3(_effector.position.x, Input.mousePosition.y, Input.mousePosition.x));
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _xCoord));
    }
}
