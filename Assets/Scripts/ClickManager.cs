using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public LessonManager lessonManager;

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
                    // Instead of highlighting here,
                    // let the LessonManager decide if this was correct.
                    lessonManager.OnObjectClicked(obj.gameObject);
                }
            }
        }
    }
}
