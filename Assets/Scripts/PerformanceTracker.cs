using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PerformanceTracker : MonoBehaviour
{
    [System.Serializable]
    public class TaskResult
    {
        public string taskName;
        public float timeTaken;
        public int mistakes;
    }

    private List<TaskResult> results = new List<TaskResult>();
    private float taskStartTime;
    private int currentMistakes = 0;
    private bool taskActive = false;

    public void StartTask()
    {
        taskStartTime = Time.time;
        currentMistakes = 0;
        taskActive = true;
        Debug.Log("Task started");
    }

    public void RegisterMistake()
    {
        if (taskActive)
        {
            currentMistakes++;
            Debug.Log("Mistake recorded. Total mistakes: " + currentMistakes);
        }
    }

    public void FinishTask()
    {
        if (!taskActive) return;

        float timeTaken = Time.time - taskStartTime;
        results.Add(new TaskResult
        {
            taskName = "Task " + (results.Count + 1),
            timeTaken = timeTaken,
            mistakes = currentMistakes
        });

        Debug.Log($"Task finished. Time: {timeTaken:F2}s | Mistakes: {currentMistakes}");
        taskActive = false;
    }

    public void SaveResults()
    {
        // Save CSV inside the project folder (Assets/PerformanceData.csv)
        string filePath = Path.Combine(Application.dataPath, "PerformanceData.csv");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Task,TimeTaken,NumberOfMistakes");
            foreach (var r in results)
            {
                writer.WriteLine($"{r.taskName},{r.timeTaken:F2},{r.mistakes}");
            }
        }

        Debug.Log($"Results saved to: {filePath}");
    }

    private void OnApplicationQuit()
    {
        SaveResults();
    }
}
