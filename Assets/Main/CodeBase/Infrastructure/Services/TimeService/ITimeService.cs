using System;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Zenject;

namespace Main.CodeBase.Infrastructure.Services.TimeService
{
    public interface ITimeService : ITickable
    {
        event Action NewDayHasArrived;
        event Action OneSecondHasPassed;
        long OfflineTime { get; }
        void Initialize(long currentTime, ITimePersistent timePersistent);
        long GetCurrentTimeInUnixSeconds();
        int GetCurrentDayNumber();
        int GetRemainingTimeOfTheDay();
    }
}