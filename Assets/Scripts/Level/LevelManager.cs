using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;
    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject obstaclePrefab;
    
    public Levels levels = new Levels();
    public int currentLevelIndex = 0;
    public int currentWaveIndex = 0;
    private GameObject currentStartObj;
    private GameObject currentEndObj;
    private List<GameObject> currentObstacleObjs = new List<GameObject>();
    private string filePath = "Assets/Resources/levelsData.json";
    // private string filePath = Application.persistentDataPath + "/levelsData.json";

    void Awake() {
        if (instance != null) {
            DestroyImmediate(gameObject);
        } else {
            instance = this;
            string json = System.IO.File.ReadAllText(filePath);
            levels = JsonUtility.FromJson<Levels>(json);
        }
    }

    public void CreateCurrentWave() {
        if (currentStartObj != null && currentEndObj != null) {
            Destroy(currentStartObj);
            Destroy(currentEndObj);
        }

        if (currentWaveIndex == 0) {
            foreach (var obj in currentObstacleObjs) {
                Destroy(obj);
            }
            currentObstacleObjs.Clear();
            
            foreach (var obstacle in levels.levels[currentLevelIndex].obstacles) {
                var obj = Instantiate(obstaclePrefab, obstacle.position, obstacle.rotation);
                currentObstacleObjs.Add(obj);
            }
        }
        var cw = levels.levels[currentLevelIndex].waves[currentWaveIndex];

        currentStartObj = Instantiate(startPrefab, cw.startPosition, startPrefab.transform.rotation);
        currentEndObj = Instantiate(endPrefab, cw.endPosition, endPrefab.transform.rotation);
    }
    
    public void CreateNextWave() {
        currentWaveIndex++;
        if (currentWaveIndex >= levels.levels[currentLevelIndex].waves.Count) {
            currentLevelIndex++;
            currentWaveIndex = 0;
        }

        if (currentLevelIndex >= levels.levels.Count) {
            Debug.LogError("All levels finished");
        }
        else {
            CreateCurrentWave();
        }
    }

    public Vector3 GetCurrentStartPos() {
        return levels.levels[currentLevelIndex].waves[currentWaveIndex].startPosition;

    }

    public void RestartLevel() {
        currentWaveIndex = 0;
        GameManager.instance.WaveFail();
    }
}
