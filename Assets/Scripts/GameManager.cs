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
    public int level = 1; 
    const string FILE_DIR = "/DATA/";
    const string DATA_FILE = "hl.txt";
    string FILE_FULL_PATH;
    public int Level
        
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            
            if (isHighLevel(level))
            {
                int highLevelSlot = -1;

                for (int i = 0; i < HighLevels.Count; i++)
                {
                    if (level > highLevels[i])
                    {
                        highLevelSlot = i;
                        break;
                    }
                }
                highLevels.Insert(highLevelSlot, level);

                highLevels = highLevels.GetRange(0, 5);

                string levelBoardText = "";

                foreach (var highLevel in highLevels)
                {
                    levelBoardText += highLevel + "\n";
                }

                highLevelsString = levelBoardText;
                
                File.WriteAllText(FILE_FULL_PATH, highLevelsString);;
            }
        }
    }
    string highLevelsString = "";
    
    List<int> highLevels;
    
    public List<int> HighLevels
    {
        get
        {
            if (highLevels == null)
            {
                highLevels = new List<int>();

                highLevelsString = File.ReadAllText(FILE_FULL_PATH);

                highLevelsString = highLevelsString.Trim();
                
                string[] highLevelArray = highLevelsString.Split("\n");

                for (int i = 0; i < highLevelArray.Length; i++)
                {
                    int currentScore = Int32.Parse(highLevelArray[i]);
                    highLevels.Add(currentScore);
                }
            }

            return highLevels;
        }
    }
    float timer = 0;

    public int maxTime = 10;

    bool isInGame = true;
    
    
    public TextMeshProUGUI levelText;
    
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
        FILE_FULL_PATH = Application.dataPath + FILE_DIR + DATA_FILE;
    }


    // Update is called once per frame
    void Update()
    {
        if (isInGame)
        {
            levelText.text = "Level: " + level + "\nTime:" + (maxTime - (int)timer);
        }
        else
        {
            levelText.text = "GAME OVER\nFINAL LEVEL: " + level +
                             "\nHigh Levels:\n" + highLevelsString;
        }

        //add the fraction of a second between frames to timer
        timer += Time.deltaTime;
        
        //if timer is >= maxTime
        if (timer >= maxTime && isInGame)
        {
            isInGame = false;
            SceneManager.LoadScene("EndScene");
        }
        if(Game.win)
        {
            //level++;
            Level++;
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    bool isHighLevel(int level)
    {

        for (int i = 0; i < HighLevels.Count; i++)
        {
            if (highLevels[i] < level)
            {
                return true;
            }
        }

        return false;
    }
}