using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public bool isActive = false;
    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CanvasController.instance.CustomInteractiveText(false, "");
                
                CheckPointManager.instance.DeactivateCheckPoints();

                isActive = true;
                
                CheckPointManager.instance.GetActiveCheckPointIndex();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            if (other.CompareTag("Player"))
            {
                isTriggered = true;
                CanvasController.instance.CustomInteractiveText(true, "Press E to active checkpoint");
            }
        }
       
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
            CanvasController.instance.CustomInteractiveText(false, "");
        }
    }
}
