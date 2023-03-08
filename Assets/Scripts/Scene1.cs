using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Scene1 : MonoBehaviour
{
    public static Scene1 Instance;
    [SerializeField] public TextMeshProUGUI BestScoreName;
    [SerializeField] public int BestScorePoints;
    [SerializeField] public string playerName;
    [SerializeField] public string BestScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string BestScoreName;
        public int BestScorePoints;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.BestScoreName = BestScoreText;

        data.BestScorePoints = BestScorePoints;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScoreText = "Best Score: " + data.BestScoreName + ": " + data.BestScorePoints;
            if(BestScoreName != null)
            {
                BestScoreName.text = "Best Score: " + data.BestScoreName + ": " + data.BestScorePoints;
            }

            BestScorePoints = data.BestScorePoints;


        }
    }
}
