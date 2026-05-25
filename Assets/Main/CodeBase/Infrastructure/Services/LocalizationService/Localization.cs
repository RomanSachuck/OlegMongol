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
    
    public class Localization
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

        public string GetBusinessName(BusinessType businessType)
        {
            switch (businessType)
            {
                case BusinessType.Bottle:
                    return "Сбор бутылок";
                case BusinessType.Scrap:
                    return "Сбор металлолома";
                default:
                    throw new ArgumentOutOfRangeException(nameof(businessType), businessType, null);
            }
        }

        public string GetBusinessWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "Business";
                case Language.Ru:
                    return "Бизнес";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetProductPriceText()
        {
            switch (Language)
            {
                case Language.En:
                    return "Product price";
                case Language.Ru:
                    return "Цена товара";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetSecondsShortWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "sec.";
                case Language.Ru:
                    return "сек.";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetPiecesShortWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "pi.";
                case Language.Ru:
                    return "шт.";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetCycleWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "Cycle";
                case Language.Ru:
                    return "Цикл";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetBuyWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "Buy";
                case Language.Ru:
                    return "Купить";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetExpandWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "Expand";
                case Language.Ru:
                    return "Развернуть";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetCollapseWord()
        {
            switch (Language)
            {
                case Language.En:
                    return "Collapse";
                case Language.Ru:
                    return "Свернуть";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}