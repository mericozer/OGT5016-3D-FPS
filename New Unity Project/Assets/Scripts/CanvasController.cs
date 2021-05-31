using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;

   // public bool levelStarts = false; //makes battery start to drain
    
   // public float batteryCost = -1f; //each second battery drains 1 unit

   // private int batteryColorValue = 0;
    
    private bool isGameRunning = true; //checks if game is running
    private bool isShotGun = true;
    
    [SerializeField] private Slider healthBar; //slider for health
    [SerializeField] private Slider bulletBar;
    
     private float batteryPercantage; //holds battery value
     private float currenHealth; //holds current health value
     [SerializeField] private float maxBatteryValue = 100f; //max value for battery
     [SerializeField] private float maxHealthValue = 100f; //max health for player
     //private float maxBulletCount;
     //private float currentClip;
     
    [SerializeField] private TMP_Text interactableText; //Shows up when player gets near to the interactible object
    [SerializeField] private TMP_Text pausedText; //pause screen text
    [SerializeField] private Text curretBulletText;
    [SerializeField] private Text currentMaxBulletText;

     [SerializeField] private GameObject pausePanel; 
     [SerializeField] private GameObject winPanel;
     [SerializeField] private GameObject losePanel;
   
     
     [SerializeField] private Image gunImage;
     
     [SerializeField] private Sprite shotGun;
     [SerializeField] private Sprite autoGun;
     
     
     public float BatteryPercantage
     {
         get => batteryPercantage;
         set => batteryPercantage = value;
     }

     void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        healthBar.maxValue = maxHealthValue;
        currenHealth = maxHealthValue;
        healthBar.value = currenHealth;
        gunImage.sprite = shotGun;
        
        //TO DO
        //SAVE SYSTEM WILL BE ADDED 
        //batteryPercantage = PlayerPrefs.GetFloat("BatteryPercentage"); //assign the saved value for battery
        //battery.value = batteryPercantage;
        

        //if player talked with NPC, battery starts to drain
        /*if (PlayerPrefs.GetInt("Talk") == 1)
        {
            levelStarts = true;
        }*/
    }
    
    // Update is called once per frame
    void Update()
    {
        //makes battery start to drain
        /*if (levelStarts)
        {
            UpdateBatteryPercentage(batteryCost, false);
        }*/

        //pause the game. If press while game is paused, game continues
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameRunning)
            {
                Time.timeScale = 0;
                isGameRunning = false;
                pausePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pausedText.text = "PAUSED"; //pause text can be changed to "game saved", it reverts
                ContinueGame();
            }
        }
        
    }

    public void UpdateHealthBar(float value)
    {
        currenHealth += value;
        healthBar.value = currenHealth;

        if (healthBar.value <= 0)
        {
            LoseState();
            Time.timeScale = 0f;
        }
    }

    //edits ammo text when gun change
    public void EditBulletBar(float clipSize, float currentBullet, float maxBullet)
    {
        bulletBar.maxValue = clipSize;
        bulletBar.value = currentBullet;
        curretBulletText.text = currentBullet.ToString();
        currentMaxBulletText.text = maxBullet.ToString();
    }

    //decrease ammo number
    public void Shoot(float bulletCount)
    {
        curretBulletText.text = bulletCount.ToString();
        bulletBar.value = bulletCount;
    }
    
    //adjust ammo numbers when reload
    public void Reload(float clipSize, float maxBullet)
    {
        curretBulletText.text = clipSize.ToString();
        currentMaxBulletText.text = maxBullet.ToString();
        bulletBar.value = clipSize;
    }

    //change gun sprite
    public void ChangeGun()
    {
        if (isShotGun)
        {
            Debug.Log("here");
            gunImage.sprite = autoGun;
            isShotGun = false;
        }
        else
        {
            gunImage.sprite = shotGun;
            isShotGun = true;
        }

       
    }

    //updates battery percentage every second
    //if it is just draining the battery it calculates with time
    //if it is a damage it is an external update and it works seperately
   /* public void UpdateBatteryPercentage(float value, bool external)
    {
        if (!external)
        {
            batteryPercantage += (value * Time.deltaTime);
            battery.value = batteryPercantage;
        }
        else
        {
            batteryPercantage += value;
            battery.value = batteryPercantage;
        }

        if (battery.value <= 0)
        {
            LoseState();
        }
        
        //battery color change based on its percantage, it is a checker for that action
        ColorValueChecker();
        

    }*/

    //win panel opens, game world stops
    public void WinState()
    {
        Time.timeScale = 0;
        isGameRunning = false;
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        
    }
    
    //lose panel opens, game world stops
    public void LoseState()
    {
        Time.timeScale = 0;
        isGameRunning = false;
        losePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        
    }

    //makes unpause the game
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isGameRunning = true;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    
   
    
    //shows a text to player for direction when activated
    //it deletes text when it is deactivated
    public void CustomInteractiveText(bool enter, string s)
    {
        StopAllCoroutines();
        if (enter)
        {
            interactableText.text = s;
        }
        else
        {
            interactableText.text = "";
        }
    }
    
    //informative texts which shows up for a limited time
    public IEnumerator PeriodicText(float time, string s)
    {
        interactableText.text = s;
        yield return new WaitForSeconds(time);

        interactableText.text = "";

    }

}
