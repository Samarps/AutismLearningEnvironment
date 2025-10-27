using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public LessonManager lessonManager;
    public SimpleInteractable[] interactables;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                var obj = hit.collider.GetComponent<SimpleInteractable>();
                if (obj != null)
                {
                    obj.Highlight(true);
                    lessonManager.NextTask();
                }
            }
        }
    }
}
