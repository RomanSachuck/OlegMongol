using System.Collections.Generic;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.PersistentProgressService
{
    public interface IHousePersistent
    {
        IEnumerable<HouseType> GetOpenedHouses();
        HouseType SelectedHouse { get; set; }
        void AddOpenedHouse(HouseType house);
    }
}