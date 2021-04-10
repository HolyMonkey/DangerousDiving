using UnityEngine;

public class Stages : MonoBehaviour
{
    private Stage[] _stages;

    private void Start()
    {
        _stages = GetComponentsInChildren<Stage>();
    }

    public void Activate()
    {
        foreach (Stage stage in _stages)
        {
            stage.gameObject.SetActive(true);
        }
    }
}
