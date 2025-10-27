using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LessonManager : MonoBehaviour
{
    public Text taskText;
    public string[] tasks;
    private int currentTask = 0;

    void Start()
    {
        if (tasks == null || tasks.Length == 0)
            tasks = new string[] {
                "Welcome! Click the red cube.",
                "Now click the blue sphere.",
                "Finally click the green cylinder."
            };

        UpdateText();
    }

    public void NextTask()
    {
        currentTask++;
        if (currentTask >= tasks.Length)
        {
            taskText.text = "All tasks complete! Great job!";
        }
        else
        {
            UpdateText();
        }
    }

    void UpdateText()
    {
        taskText.text = tasks[currentTask];
    }

    public int GetCurrentTask() => currentTask;
}
