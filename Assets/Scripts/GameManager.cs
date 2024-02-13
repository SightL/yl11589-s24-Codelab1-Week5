using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int level = 1; 
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            Debug.Log("level changed!");

            if (level > HighLevel)
            {
                HighLevel = level;
            }
        }
    }
    int highLevel = 1;

    const string KEY_HIGH_LEVEL = "High Level";
    
    int HighLevel
    {
        get
        {
            //highLevel = PlayerPrefs.GetInt(KEY_HIGH_LEVEL);

            if (File.Exists(DATA_FULL_HL_FILE_PATH))
            {
                string fileContents = File.ReadAllText(DATA_FULL_HL_FILE_PATH);
                highLevel = Int32.Parse(fileContents);
            }

            return highLevel;
        }
        
        set
        {
            highLevel = value;
            Debug.Log("New High Level!!!");
            //PlayerPrefs.SetInt(KEY_HIGH_LEVEL, highLevel);
            string fileContent = "" + highLevel;

            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }

            File.WriteAllText(DATA_FULL_HL_FILE_PATH, fileContent);
        }
    }
    
    
    public TextMeshProUGUI levelText;
    

    const string DATA_DIR = "/Data/";
    const string DATA_HL_FILE = "hl.txt";

    string DATA_FULL_HL_FILE_PATH;

    
    void Awake()
    {
        if (instance == null) //if the instance var has not been set
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //if there is already a singleton of this type
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DATA_FULL_HL_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HL_FILE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteKey(KEY_HIGH_LEVEL);
        }
        levelText.text = "Level: " + level +  "\nHighest Level: " + HighLevel;
        
        //go to the next level
        if(Game.win)
        {
            //level++;
            Level++;
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}