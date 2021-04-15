using UnityEngine;

[CreateAssetMenu(fileName = "New Pose", menuName = "SO/Pose", order = 52)]
public class PoseObject : ScriptableObject
{
    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;
}
