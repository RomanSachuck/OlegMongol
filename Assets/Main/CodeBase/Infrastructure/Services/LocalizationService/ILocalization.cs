using Cysharp.Threading.Tasks;

namespace Main.CodeBase.Infrastructure.Services.LocalizationService
{
    public interface ILocalization
    {
        Language Language { get; }
        UniTask<string> GetHoursShortWord();
        UniTask<string> GetMinutesShortWord();
    }
}