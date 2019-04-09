using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject[] SystemPrefabs;
    [SerializeField] private string initialSceneName;
    [SerializeField] private string bootSceneName;
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

        for (int i= 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    private void StartGame()
    {
        LoadLevel(initialSceneName);
    }

    private void LoadLevel(string levelName)
    {
        ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            throw new Exception("[GameManager] unable to load level " + levelName);          
        }
        ao.completed += OnLoadOperationComplete;
    }

    public void UnloadLevel(string levelName)
    {
        ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            throw new Exception("[GameManager] unable to unload level " + levelName);
        }
        ao.completed += OnUnloadOperationComplete;
    }   

    private void OnLoadOperationComplete(AsyncOperation ao)
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

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(bootSceneName));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Load();
        }
    }    

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath +
            "/playerInfo.dat");
        PlayerData playerData = new PlayerData();
        playerData.playerDataPoints = score;
        bf.Serialize(file, playerData);
        file.Close();
    }

    private void Load()
    {
        if(File.Exists(Application.persistentDataPath+
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

    private void FixedUpdate()
    {
        score += 10;
        UIManager.Instance.UpdateScore(score);
    }

    public void IncreateScore(int amount)
    {
        score += amount;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for(int i = 0; i < instancedSystemPrefabs.Count; ++i)
        {
            Destroy(instancedSystemPrefabs[i]);
        }
        instancedSystemPrefabs.Clear();
    }  
}



[Serializable]
public class PlayerData
{
    public int playerDataPoints;
}
