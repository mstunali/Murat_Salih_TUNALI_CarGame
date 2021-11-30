using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorBehavior : MonoBehaviour {
    public GameObject obstaclePrefab;
    public GameObject startPrefab;
    [HideInInspector]
    public GameObject startObj;

    public GameObject endPrefab;
    [HideInInspector]
    public GameObject endObj;

    [HideInInspector] public int levelIndex = 0;
    [HideInInspector] public int waveIndex = 0;
    [HideInInspector] public List<GameObject> obtacleObjs = new List<GameObject>();
    [HideInInspector] public List<Vector3> obtaclePositions = new List<Vector3>();
    public Levels levels = new Levels(){levels = new List<LevelData>()};

    public void CreateStartObject(bool isRecreate, Vector3 pos) {
        if (startObj != null)
            return;

        startObj = isRecreate ? Instantiate(startPrefab, pos, startPrefab.transform.rotation, transform) : Instantiate(startPrefab, transform);
    }
    
    public void CreateEndObject(bool isRecreate, Vector3 pos) {
        if (endObj != null)
            return;
        endObj = isRecreate ? Instantiate(endPrefab, pos, endPrefab.transform.rotation, transform) : Instantiate(endPrefab, transform);
    }
    
    public void DestroyStartObject() {
        if (startObj == null)
            return;
        DestroyImmediate(startObj);
        startObj = null;
    }
    
    public void DestroyEndObject() {
        if (endObj == null)
            return;
        DestroyImmediate(endObj);
        endObj = null;
    }
    
    public void CreateObstacleObject(bool isRecreate, Vector3 pos, Quaternion rot) {
        var obj = isRecreate ? Instantiate(obstaclePrefab, pos, rot, transform) : Instantiate(obstaclePrefab, transform);
        obtacleObjs.Add(obj);
        obtaclePositions.Add(obj.transform.position);
    }

    public void ClearAllObstacles() {
        if (obtacleObjs.Count > 0) {
            foreach (var obj in obtacleObjs) {
                DestroyImmediate(obj);
            }
        }
        obtacleObjs.Clear();
        obtaclePositions.Clear();
    }

    public void SaveObstaclesForLevel() {
        bool isError = false;
        if (levelIndex == levels.levels.Count) {
            var data = new LevelData();
            data.waves = new List<WaveData>(){new WaveData()};
            data.obstacles = new List<ObstacleData>();
            for (int i = 0; i < obtacleObjs.Count; i++) {
                data.obstacles.Add(new ObstacleData() {
                    position = obtaclePositions[i],
                    rotation = Quaternion.identity
                });
            }
            levels.levels.Add(data);
        }
        else if (levelIndex < levels.levels.Count) {
            levels.levels[levelIndex].obstacles.Clear();
            for (int i = 0; i < obtacleObjs.Count; i++) {
                levels.levels[levelIndex].obstacles.Add(new ObstacleData() {
                    position = obtaclePositions[i],
                    rotation = Quaternion.identity
                });
            }
        }
        else {
            isError = true;
            Debug.LogError("Before this level index, set level index to " + levels.levels.Count);
        }

        if (!isError) {
            string json = JsonUtility.ToJson(levels);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/levelsData.json", json);
            Debug.Log(json);
        }
    }

    public void SaveWave() {
        bool isError = false;
        if (levelIndex == levels.levels.Count) {
            if (waveIndex == 0) {
                var data = new LevelData();
                data.waves = new List<WaveData>(){new WaveData() {startPosition = startObj.transform.position, endPosition = endObj.transform.position}};
                levels.levels.Add(data);
            }
            else {
                isError = true;
                Debug.LogError("Please set wave index 0");
            }
        }
        else if (levelIndex < levels.levels.Count) {
            if (waveIndex < levels.levels[levelIndex].waves.Count) {
                levels.levels[levelIndex].waves[waveIndex] = new WaveData(){startPosition = startObj.transform.position, endPosition = endObj.transform.position};
            }else if (waveIndex == levels.levels[levelIndex].waves.Count) {
                levels.levels[levelIndex].waves.Add(new WaveData(){startPosition = startObj.transform.position, endPosition = endObj.transform.position});
            }
            else {
                isError = true;
                Debug.LogError("Before this wave index, set wave index to " + levels.levels[levelIndex].waves.Count);
            }
        }
        else {
            isError = true;
            Debug.LogError("Before this level index, set level index to " + levels.levels.Count);
        }

        if (!isError) {
            string json = JsonUtility.ToJson(levels);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/levelsData.json", json);
            Debug.Log("Wave saved");
        }
    }

    public void LoadLevels() {
        string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/levelsData.json");
        levels = JsonUtility.FromJson<Levels>(json);
        Debug.Log(levels.levels.Count);

    }

    public void OpenCurrentWave() {
        if (levelIndex < levels.levels.Count) {
            if (waveIndex < levels.levels[levelIndex].waves.Count) {
                Debug.Log("Wave loaded");
                DestroyStartObject();
                DestroyEndObject();
                ClearAllObstacles();
                CreateStartObject(true,levels.levels[levelIndex].waves[waveIndex].startPosition);
                CreateEndObject(true,levels.levels[levelIndex].waves[waveIndex].endPosition);
                foreach (var obs in levels.levels[levelIndex].obstacles) {
                    CreateObstacleObject(true, obs.position, obs.rotation);
                }
            }
            else {
                Debug.LogError("This wave did not saved before. Arrange and save");
            }
        }
        else {
            Debug.LogError("This level did not saved before. Arrange and save");
        }
    }

    public void DeleteSave() {
        levels.levels.Clear();
        System.IO.File.WriteAllText(Application.persistentDataPath + "/levelsData.json", "{}");
    }
}



