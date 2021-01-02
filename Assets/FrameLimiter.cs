using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class FrameLimiter : MonoBehaviour
{
    private FrameLimiter m_Instance;
    public FrameLimiter Instance { get { return m_Instance; } }

    public double FrameTimeLimit = 4.0f;

    private long lastTime = DateTime.UtcNow.Ticks;

    void Awake()
    {
        m_Instance = this;
    }

    void OnDestroy()
    {
        m_Instance = null;
    }

    void Update()
    {
        if (FrameTimeLimit == 0.0) return;

        lastTime += TimeSpan.FromMilliseconds(FrameTimeLimit).Ticks;

        var now = DateTime.UtcNow.Ticks;

        if (now >= lastTime)
        {
            lastTime = now;
            return;
        }
        else
        {
            SpinWait.SpinUntil(() => { return (DateTime.UtcNow.Ticks >= lastTime); });
        }
    }
}