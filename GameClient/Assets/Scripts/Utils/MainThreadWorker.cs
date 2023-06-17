using System;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadWorker : MonoBehaviour
{
    private static MainThreadWorker _current;
    private Queue<Action> _jobs = new Queue<Action>();

    public static MainThreadWorker Current => _current;

    public void AddJob(Action newJob) {
        _jobs.Enqueue(newJob);
    }

    void Awake() {
        if (_current == null) {
            _current = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    void Update() {
        while (_jobs.Count > 0)
            _jobs.Dequeue().Invoke();
    }
}