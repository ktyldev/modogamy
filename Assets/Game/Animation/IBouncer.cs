using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IBouncer
{
    UnityEvent StartBouncing { get; }
    UnityEvent StopBouncing { get; }
}
