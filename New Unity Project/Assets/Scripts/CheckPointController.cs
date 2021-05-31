using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    
    public bool isActive = false; //checkpoint activated
    private bool isTriggered = false; //player is near to the checkpoint
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
                
                CheckPointManager.instance.DeactivateCheckPoints();  //if any other checkpoint is active close them

                isActive = true; //make this checkpoint active
                
                CheckPointManager.instance.GetActiveCheckPointIndex(); //save this checkpoint to playerpref as the active one
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
