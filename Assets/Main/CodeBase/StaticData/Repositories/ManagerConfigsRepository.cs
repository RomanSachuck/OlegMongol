using Main.CodeBase.StaticData.Configs;
using UnityEngine;

namespace Main.CodeBase.StaticData.Repositories
{
    [CreateAssetMenu(fileName = "ManagerConfigsRepository", menuName = "Repositories/ManagerConfigsRepository")]
    public class ManagerConfigsRepository : ScriptableObject
    {
        [field:SerializeField] public ManagerConfigs[] Configs { get; private set;}
    }
}