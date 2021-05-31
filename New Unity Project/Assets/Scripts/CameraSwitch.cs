using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject cameraOne;

    public GameObject cameraTwo;

    public GameObject canvasOne;

    public GameObject canvasTwo;

    public GameObject light;

    private bool isMapActive = false;
    // Start is called before the first frame update
    void Start()
    {
        isMapActive = false;
        
        cameraTwo.SetActive(false);
        canvasTwo.SetActive(false);
        cameraOne.SetActive(true);
        canvasOne.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
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
