using System.Collections.Generic;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public interface IClothesPersistent
    {
        IEnumerable<ClothesType> GetOpenedClothes();
        ClothesType SelectedClothes{ get; set; }
        void AddOpenedClothes(ClothesType clothes);
    }
}