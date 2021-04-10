using UnityEngine;

public class WaterEnter : MonoBehaviour
{
    private ParticleSystem[] _effects;

    private void OnEnable()
    {
        _effects = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem effect in _effects)
        {
            effect.Play();
        }
    }
}
