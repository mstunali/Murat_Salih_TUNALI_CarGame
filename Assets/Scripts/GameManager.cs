using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    
    public bool isGameStarted = false;

    void Awake() {
        if (instance != null) {
            DestroyImmediate(gameObject);
        } else {
            // DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
    
    private void Start() {
        LevelManager.instance.CreateCurrentWave();
        CarManager.instance.CreateCars();
    }
    
    public void WaveSuccess() {
        isGameStarted = false;
        LevelManager.instance.CreateNextWave();
        CarManager.instance.CreateCarsWithHistory(false);
    }

    public void WaveFail() {
        isGameStarted = false;
        LevelManager.instance.CreateCurrentWave();
        CarManager.instance.CreateCarsWithHistory(true);
    }
}
