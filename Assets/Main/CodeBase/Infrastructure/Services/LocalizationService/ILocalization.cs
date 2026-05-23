using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.LocalizationService
{
    public interface ILocalization
    {
        Language Language { get; }
        string GetHoursShortWord();
        string GetMinutesShortWord();
        string GetLockedTitleForHouse(HouseType house);
        string GetLockedTitleForClothes(ClothesType clothes);
        string GetHouseName(HouseType house);
        string GetClothesName(ClothesType clothes);
        string GetClickShortWord();
    }
}