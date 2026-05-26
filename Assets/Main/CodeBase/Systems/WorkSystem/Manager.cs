using System;
using Main.CodeBase.StaticData.Configs;
using UnityEngine;

namespace Main.CodeBase.Systems.WorkSystem
{
    public class Manager
    {
        public event Action<BusinessType> ManagerClicked;
        
        private float _timeToClick;
        private float _timeCounter;

        public ManagerType ManagerType { get; private set; }
        public BusinessType BusinessType { get; private set; }

        public Manager(ManagerType managerType, BusinessType businessType)
        {
            ManagerType = managerType;
            BusinessType = businessType;
        }
        
        public void SetTimeToClick(float timeToClick)
        {
            _timeToClick = timeToClick;
        }
        
        public void UpdateTimer()
        {
            _timeCounter += Time.deltaTime;

            if (_timeCounter >= _timeToClick)
            {
                _timeCounter = 0;
                ManagerClicked?.Invoke(BusinessType);
            }
        }
    }
}