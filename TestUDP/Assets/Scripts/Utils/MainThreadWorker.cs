using System;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadWorker : MonoBehaviour
{
    public static MainThreadWorker current;
    Queue<Action> jobs = new Queue<Action>();

    public void AddJob(Action newJob)
    {
        jobs.Enqueue(newJob);
    }

    void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        while (jobs.Count > 0)
            jobs.Dequeue().Invoke();
    }
}