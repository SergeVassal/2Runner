using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject[] SystemPrefabs;
    private List<GameObject> instancedSystemPrefabs;

    private int score;

    private string currentLevelName = string.Empty;

    private AsyncOperation ao;
    private List<AsyncOperation> loadOperations;



    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOperations = new List<AsyncOperation>();
        instancedSystemPrefabs = new List<GameObject>();
        InstantiateSystemPrefabs();
        StartGame();
    }


    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;

        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }


    public void StartGame()
    {
        LoadLevel("001Level");
    }


    public void LoadLevel(string levelName)
    {
        ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to loal level" + levelName);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
        currentLevelName = levelName;
    }


    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to unloal level" + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }


    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            if (loadOperations.Count == 0)
            {
                SceneManager.SetActiveScene(SceneManager.GetActiveScene());  
            }
        }
    }


    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Boot"));        
    }   


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Load();
        }       
    }


    void FixedUpdate()
    {
        score += 10;
        UIManager.Instance.UpdateScore(score);
    }
    

    public void IncreaseScore(int amount)
    {
        score += amount;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < instancedSystemPrefabs.Count; ++i)
        {
            Destroy(instancedSystemPrefabs[i]);
        }
        instancedSystemPrefabs.Clear();
    }


    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath +
            "/playerInfo.dat");
        PlayerData playerData = new PlayerData();
        playerData.playerDataPoints = score;
        bf.Serialize(file, playerData);
        file.Close();
    }


    void Load()
    {
        if (File.Exists(Application.persistentDataPath +
           "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath +
                            "/playerInfo.dat", FileMode.Open);
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            score = playerData.playerDataPoints;
            file.Close();
        }
    }
}



[Serializable]
class PlayerData
{
    public int playerDataPoints;
}





