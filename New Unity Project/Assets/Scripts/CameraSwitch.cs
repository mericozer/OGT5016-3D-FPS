using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject cameraOne; //First person camera

    public GameObject cameraTwo; //Map camera

    public GameObject canvasOne; //Main Canvas

    public GameObject canvasTwo; //Map panel

    public GameObject light; //Directional light goes off when map is open

    private bool isMapActive = false;
    // Start is called before the first frame update
    void Start()
    {
        isMapActive = false;
        
        //always start with fps
        cameraTwo.SetActive(false);
        canvasTwo.SetActive(false);
        cameraOne.SetActive(true);
        canvasOne.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if battery time finished, player cannot open tablet
        if (TabletController.instance.isTimeFinished)
        {
            Activate(false);
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isMapActive)
            {
                if (!TabletController.instance.isTimeFinished)
                {
                    Activate(true);
                }
                //if battery is finished, warning shows up
                else
                {
                    StartCoroutine(CanvasController.instance.PeriodicText(2f, "Tablet needs charging"));
                }
                
            }
            else
            {
                Activate(false);
                
            }
            
        }
        
        
    }

    //opens and closes map
    //adjust canvas
    void Activate(bool map)
    {
        cameraTwo.SetActive(map);
        canvasTwo.SetActive(map);
        cameraOne.SetActive(!map);
        canvasOne.SetActive(!map);
        
        isMapActive = map;
        
        light.SetActive(!map);

        TabletController.instance.isMapActive = isMapActive;
        TabletController.instance.FocusPlayer();
        PlayerController.instance.isMapActive = isMapActive;
    }
}
