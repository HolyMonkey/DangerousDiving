using RootMotion.FinalIK;
using UnityEngine;
using System.Collections.Generic;

public class IKTest : MonoBehaviour
{
    [SerializeField] private FullBodyBipedIK _ik;
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _effectorsUIPanel;
    [SerializeField] private RectTransform _canvas;

    [SerializeField] private RectTransform _effectorUITemplate;

    private bool _isStage;
    private Vector3 _targetPosition;
    private List<RectTransform> _effectors = new List<RectTransform>();

    private void Start()
    {
        CreateEfectorsUI(GetEffectors());
        
    }

    private void LateUpdate()
    {
        MoveEffector();
        
    }

    private void CreateEfectorsUI(IKEffector[] effectors)
    {
        /*
        foreach ()

        RectTransform effectorUI = Instantiate(_effectorUITemplate, _effectorsUIPanel);
        */
    }

    private void MoveEffector()
    {
        foreach (RectTransform effector in _effectors) 
        {
            

            Vector3 screenPosition = _camera.WorldToScreenPoint(effector.position);

            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, screenPosition, _camera, out anchoredPos);

            effector.anchoredPosition = anchoredPos;
        }
    }

    private IKEffector[] GetEffectors()
    {
        //here must be logic of choice efectors!!!
        IKEffector[] effectors = new IKEffector[] { _ik.solver.bodyEffector, _ik.solver.leftHandEffector };
        return effectors;
    }
}
