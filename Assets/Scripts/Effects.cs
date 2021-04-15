using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _stars;
    [SerializeField] private ParticleSystem _ten;
    [SerializeField] private ParticleSystem _fifty;
    [SerializeField] private ParticleSystem _hundred;

    public bool IsPlaying => _stars.isPlaying;

    public void PoseSetup(int stageNumber)
    {
        PlayEnvironment();

        switch (stageNumber)
        {
            case 1:
                _ten.Play();
                break;
            case 2:
                _fifty.Play();
                break;
            case 3:
                _hundred.Play();
                break;
        }
    }

    private void PlayEnvironment()
    {
        _stars.Play();
    }
}
