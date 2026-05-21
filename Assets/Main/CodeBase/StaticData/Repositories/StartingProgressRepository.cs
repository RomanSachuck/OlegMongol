using Main.CodeBase.SaveData;
using UnityEngine;

namespace Main.CodeBase.StaticData.Repositories
{
    [CreateAssetMenu(fileName = "StartingProgressRepository", menuName = "Repositories/StartingProgressRepository")]
    public class StartingProgressRepository : ScriptableObject
    {
        [field:SerializeField] public PlayerProgress PlayerProgress { get; private set; }
    }
}