using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetectCheckPoint()
    {
       
    }

    public void DeactivateCheckPoints()
    {
        Debug.Log("i delete the check");
        foreach(Transform checkpoint in transform)
        {
            checkpoint.GetComponent<CheckPointController>().isActive = false;
        }
    }

    public void GetActiveCheckPointIndex()
    {
        Debug.Log("i get the check");
        int counter = 0;
        foreach(Transform checkpoint in transform)
        {
            if (checkpoint.GetComponent<CheckPointController>().isActive)
            {
                PlayerPrefs.SetInt("ActiveCheckPoint", counter);
                break;
            }
            
            counter++;
        }
    }

    public void ActivateCurrentCheckPoint()
    {
        Transform obj = gameObject.transform.GetChild(PlayerPrefs.GetInt("ActiveCheckPoint"));

        Debug.Log(PlayerPrefs.GetInt("ActiveCheckPoint"));
        
        obj.gameObject.GetComponent<CheckPointController>().isActive = true;

        PlayerController.instance.gameObject.transform.position = obj.position;
    }
    
    
}
