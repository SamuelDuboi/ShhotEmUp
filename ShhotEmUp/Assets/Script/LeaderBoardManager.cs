using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
public class LeaderBoardManager : MonoBehaviour
{
    List<string> nameListe;
    List<int> scoreListe;

    public List<Text> names;
    public List<Text> score;
    string currrentName;
    int currentScore;
    public InputField namePlayer;
  public  Text currentScoreGameOver;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
        Saving();
        player = GameObject.FindGameObjectWithTag("GameManager");
        currentScoreGameOver.text = "Game Over               SCORE :" + player.GetComponent<GameManager>().score.ToString();

    }
    
    private LeaderBoard CreateSave()
    {
        LeaderBoard save = new LeaderBoard();
        if (save.leaderboardName.Count != 0)
        {
            for (int i = 0; i < save.leaderboardName.Count; i++)
            {
                if (save.leaderboardName[i] == null)
                {
                    Debug.Log("yo");
                    save.leaderboardName.Add("NoName");
                    save.leaderboardScore.Add(0);
                }
                else
                {
                    save.leaderboardScore = scoreListe;
                    save.leaderboardName = nameListe;
                }

            }
            
        }
        else
        {
            for (int i =0; i<6; i++)
            {
                Debug.Log("yo3");
                save.leaderboardName.Add("NoName");
                save.leaderboardScore.Add(0);
            }
        }
        return save;
    }
    public void LoadSave()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.leaderboard"))
        {
            
            if (namePlayer.text != null)
            {
                currrentName = namePlayer.text;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.leaderboard", FileMode.Open);
                LeaderBoard save = (LeaderBoard)bf.Deserialize(file);
                file.Close();
                nameListe = save.leaderboardName;
                scoreListe = save.leaderboardScore;
                currentScore = (int)player.GetComponent<GameManager>().score;
               
                for (int i = 0; i < save.leaderboardName.Count - 1; i++)
                {
                    if (scoreListe[i] < currentScore)
                    {
                        Debug.Log("up");
                        int score = scoreListe[i];
                        scoreListe[i] = currentScore;
                        currentScore = score;
                        string name = nameListe[i];
                        nameListe[i] = currrentName;
                        currrentName = name;
                    }
                    names[i].text = nameListe[i];
                    score[i].text = scoreListe[i].ToString();
                }
            }
            

            Saving();
            
        }
    }
            public void Saving()
                
    {

        LeaderBoard save = CreateSave();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.leaderboard");
        bf.Serialize(file, save);
        file.Close();

    }
}
