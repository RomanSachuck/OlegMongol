using System;
using Cysharp.Threading.Tasks;
using Main.CodeBase.StaticData.Configs;

namespace Main.CodeBase.Infrastructure.Services.LocalizationService
{
    public enum Language
    {
        En = 0,
        Ru = 1,
    }
    
    public class Localization : ILocalization
    {
        public Language Language { get; set; } = Language.Ru;
        
        public string GetHoursShortWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "h";
                case Language.Ru:
                    return "ч";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetMinutesShortWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "m";
                case Language.Ru:
                    return "м";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetLockedTitleForHouse(HouseType house)
        {
            switch (Language)
            {
                case Language.En:
                    return "m";
                case Language.Ru:
                    return $"Купите жилье \"{GetHouseName(house)}\"";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetLockedTitleForClothes(ClothesType clothes)
        {
            switch (Language)
            {
                case Language.En:
                    return "m";
                case Language.Ru:
                    return $"Купите костюм \"{GetClothesName(clothes)}\"";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetClothesName(ClothesType clothes)
        {
            switch (clothes)
            {
                case ClothesType.Rag:
                    return "Рванье";
                case ClothesType.PrisonRobes:
                    return "Тюремная роба";
                default:
                    throw new ArgumentOutOfRangeException(nameof(clothes), clothes, null);
            }
        }

        public string GetHouseName(HouseType house)
        {
            switch (house)
            {
                case HouseType.Street:
                    return "Берлога на помойке";
                case HouseType.Bando:
                    return "Заброшенная конура";
                default:
                    throw new ArgumentOutOfRangeException(nameof(house), house, null);
            }
        }

        public string GetClickShortWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "cl.";
                case Language.Ru:
                    return "кл.";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}