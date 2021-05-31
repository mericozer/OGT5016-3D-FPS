using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabletController : MonoBehaviour
{
    public static TabletController instance;
    
    [SerializeField] private Transform player;
    [SerializeField] private Transform tabletCamera;

    [SerializeField] private float maxDuration;
    [SerializeField] private float currentTimeLeft;

    private int minute;
    private int second;
    
    [SerializeField] private TMP_Text timeLeft;

    public bool isMapActive = false;
    public bool isLocated = false;
    public bool isTimeFinished = false;
    
    private float x;
    private float y;
    void Awake()
    {
        instance = this;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        //TO DO save and change duration
        //currentTimeLeft = maxDuration;
        isTimeFinished = false;
        EditTabletTime(maxDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMapActive)
        {
            
            EditTabletTime(-0.008f); //if map active make time decrease
            
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.F)) //finds the player on the map
            {
                FocusPlayer();
            }
        }
    }
    

    void FixedUpdate()
    {
        tabletCamera.position += new Vector3(x, 0, y);
    }

    void EditTabletTime(float value)
    {
        currentTimeLeft += value;

        if (currentTimeLeft <= 0)
        {
            isTimeFinished = true;
            
        }

        minute = (int) currentTimeLeft / 60; //takes the minutes from total time
        second = (int) currentTimeLeft - (60 * minute); //takes the seconds from the remains of minutes

        timeLeft.text = minute + ":" + second; //turns them into text for canvas

    }

    public void FocusPlayer() //makes camera look to player
    {
        tabletCamera.position = new Vector3(player.position.x, tabletCamera.position.y, player.position.z);
    }
}
