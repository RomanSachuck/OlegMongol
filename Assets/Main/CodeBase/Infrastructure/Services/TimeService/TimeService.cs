using System;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using UnityEngine;

namespace Main.CodeBase.Infrastructure.Services.TimeService
{
    public class TimeService : ITimeService
    {
        public event Action NewDayHasArrived;
        public event Action OneSecondHasPassed;

        public long OfflineTime { get; private set; }

        private long _currentTime;
        private int _currentDay;

        private float _tickCounter;
        
        private ITimePersistent _timePersistent;

        public void Initialize(long currentTime, ITimePersistent timePersistent)
        {
            _timePersistent = timePersistent;
            _currentTime = currentTime;
            _currentDay = GetCurrentDayNumber();
            OfflineTime = CalculateOfflineTime(timePersistent.TimeExitGame);
        }

        public void Tick()
        {
            float divider = Time.timeScale != 0 ? Time.timeScale : 1;
            
            _tickCounter += Time.deltaTime / divider;

            if (_tickCounter >= 1)
            {
                UpdateTimer();
                _tickCounter = 0;
            }
        }

        public long GetCurrentTimeInUnixSeconds() =>
            _currentTime;

        public int GetCurrentDayNumber() =>
            (int)(GetCurrentTimeInUnixSeconds() / Constants.NumberOfSecondsInDay);

        public int GetRemainingTimeOfTheDay() =>
            Constants.NumberOfSecondsInDay - (int)(GetCurrentTimeInUnixSeconds() % Constants.NumberOfSecondsInDay);

        private long CalculateOfflineTime(long timeExitGame)
        {
            return timeExitGame == 0 ? 0 : GetCurrentTimeInUnixSeconds() - timeExitGame;
        }

        private void UpdateTimer()
        {
            _currentTime++;
            OneSecondHasPassed?.Invoke();
            
            if (GetCurrentDayNumber() > _currentDay)
            {
                _currentDay++;
                NewDayHasArrived?.Invoke();
            }
            
            _timePersistent.TimeExitGame = _currentTime;
        }
    }
}