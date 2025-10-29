using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TaskDef
{
    public string instruction;   // e.g. "Click the red cube."
    public string objectName;    // e.g. "RedCube"
}

public class LessonManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text taskText;

    [Header("Task List")]
    public TaskDef[] tasks;

    [Header("External References")]
    public PerformanceTracker tracker;
    public RewardEffect rewardEffect;

    private int currentTask = 0;
    private bool allDone = false;

    private SimpleInteractable lastHighlighted = null; // Tracks last correctly highlighted object

    void Start()
    {
        // Default tasks if none assigned
        if (tasks == null || tasks.Length == 0)
        {
            tasks = new TaskDef[]
            {
                new TaskDef { instruction = "Welcome! Click the red cube.", objectName = "RedCube" },
                new TaskDef { instruction = "Now click the blue sphere.", objectName = "BlueSphere" },
                new TaskDef { instruction = "Finally click the green cylinder.", objectName = "GreenCylinder" }
            };
        }

        if (tracker == null)
            tracker = FindObjectOfType<PerformanceTracker>();

        if (tracker != null)
            tracker.StartTask();

        UpdateText();
    }

    public void OnObjectClicked(GameObject clicked)
    {
        if (allDone || currentTask >= tasks.Length)
            return;

        string expectedName = tasks[currentTask].objectName;
        var interactable = clicked.GetComponent<SimpleInteractable>();

        // Reset previous highlight
        if (lastHighlighted != null)
        {
            lastHighlighted.Highlight(false);
            lastHighlighted = null;
        }

        // Correct object clicked
        if (clicked.name == expectedName)
        {
            if (tracker != null)
                tracker.FinishTask();

            if (interactable != null)
            {
                interactable.Highlight(true);
                lastHighlighted = interactable;
            }

            currentTask++;

            if (currentTask < tasks.Length)
            {
                if (tracker != null)
                    tracker.StartTask();

                UpdateText();
            }
            else
            {
                allDone = true;
                taskText.text = "All tasks complete! Great job!";

                if (rewardEffect != null)
                    rewardEffect.TriggerReward();

                if (tracker != null)
                    tracker.SaveResults(); // 💾 Save data when done
            }
        }
        else
        {
            // Wrong object clicked
            if (tracker != null)
                tracker.RegisterMistake();

            taskText.text = "Oops! Try again.\n" + tasks[currentTask].instruction;
        }
    }

    void UpdateText()
    {
        if (taskText != null && currentTask < tasks.Length)
            taskText.text = tasks[currentTask].instruction;
    }

    public int GetCurrentTask() => currentTask;
}
