using System;
using Cysharp.Threading.Tasks;

namespace Main.CodeBase.Infrastructure.Services.LocalizationService
{
    public enum Language
    {
        En = 0,
        Ru = 1,
    }
    
    public class Localization : ILocalization
    {
        public Language Language { get; set; }
        
        public UniTask<string> GetHoursShortWord()
        {
            switch (Language)
            {
                case Language.En:
                    return new UniTask<string>("h");
                case Language.Ru:
                    return new UniTask<string>("ч");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public UniTask<string> GetMinutesShortWord()
        {
            switch (Language)
            {
                case Language.En:
                    return new UniTask<string>("m");
                case Language.Ru:
                    return new UniTask<string>("м");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}