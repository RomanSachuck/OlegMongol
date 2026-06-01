using System;
using Main.CodeBase.StaticData.Configs;
using UnityEngine;

namespace Main.CodeBase.Systems.WorkSystem
{
    public class Manager
    {
        public event Action<BusinessType> ManagerClicked;
        
        private float _timeCounter;

        public ManagerType ManagerType { get; private set; }
        public BusinessType BusinessType { get; private set; }
        public float TimeToClick { get; private set; }

        public Manager(ManagerType managerType, BusinessType businessType)
        {
            ManagerType = managerType;
            BusinessType = businessType;
        }
        
        public void SetTimeToClick(float timeToClick)
        {
            TimeToClick = timeToClick;
        }
        
        public void UpdateTimer()
        {
            _timeCounter += Time.deltaTime;

            if (_timeCounter >= TimeToClick)
            {
                _timeCounter = 0;
                ManagerClicked?.Invoke(BusinessType);
            }
        }
    }
}