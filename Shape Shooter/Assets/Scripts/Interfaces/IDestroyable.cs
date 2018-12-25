using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    public interface IDestroyable
    {
        event Action OnDestroy;
        void Destroy();
    } 
}
