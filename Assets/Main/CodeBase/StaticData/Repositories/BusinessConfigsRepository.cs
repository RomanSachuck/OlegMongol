using Main.CodeBase.StaticData.Configs;
using UnityEngine;

namespace Main.CodeBase.StaticData.Repositories
{
    [CreateAssetMenu(fileName = "BusinessConfigsRepository", menuName = "Repositories/BusinessConfigsRepository")]
    public class BusinessConfigsRepository : ScriptableObject
    {
        [field:SerializeField] public BusinessConfigs[] Configs { get; private set; }
    }
}