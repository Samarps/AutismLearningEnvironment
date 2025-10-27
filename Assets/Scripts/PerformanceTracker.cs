using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceTracker : MonoBehaviour
{
    public int totalTasks = 0;
    private float taskStartTime;
    private List<float> times = new List<float>();
    private List<int> mistakes = new List<int>();
    private int currentMistakes = 0;

    void Start()
    {
        taskStartTime = Time.time;
    }

    public void StartTask()
    {
        currentMistakes = 0;
        taskStartTime = Time.time;
    }

    public void RegisterMistake()
    {
        currentMistakes++;
    }

    public void FinishTask()
    {
        float t = Time.time - taskStartTime;
        times.Add(t);
        mistakes.Add(currentMistakes);
        totalTasks = times.Count;
    }

    // For UI/report
    public float GetTotalTime()
    {
        float s = 0;
        foreach(var t in times) s += t;
        return s;
    }

    public string GetSummary()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine($"Tasks completed: {times.Count}");
        for (int i=0;i<times.Count;i++)
            sb.AppendLine($"Task {i+1}: time={times[i]:F1}s mistakes={mistakes[i]}");
        sb.AppendLine($"Total time: {GetTotalTime():F1}s");
        return sb.ToString();
    }
}
