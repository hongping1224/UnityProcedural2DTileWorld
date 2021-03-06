﻿using System;
using UnityEngine;
using System.Collections;

namespace PoolingSystem.GarbageCollectors
{
    [Serializable]
    public class ProportionGarbageCollector : IGarbageCollector
    {
        private GarbageCollectorParameters _garbageCollectorParameters;

        public void Setup(GarbageCollectorParameters garbageCollectorParameters)
        {
            _garbageCollectorParameters = garbageCollectorParameters;
        }

        public void Run()
        {
            foreach (var objectPool in PoolManager.Instance)
            {
                var ammount = CalcAmmount(objectPool)*_garbageCollectorParameters.Proportion;
                for (var i = 0; i < ammount; i++)
                {
                    if (!objectPool.Instantiate())
                        break;
                }
            }
        }

        private static int CalcAmmount(IObjectPool objectPool)
        {
            return objectPool.Count - Mathf.FloorToInt(objectPool.ActiveCount/objectPool.Ratio);
        }
    }
}