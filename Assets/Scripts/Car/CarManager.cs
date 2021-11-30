using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {
    public static CarManager instance;
    public GameObject[] carPrefabs;
    public CarScriptableObject carScriptableObject;

    private GameObject carPrefab;
    private List<CarBehaviourBase> carHistory = new List<CarBehaviourBase>();
    private List<GameObject> currentCarObjs = new List<GameObject>();
    private CarBehaviourBase currentCar;
    
    void Awake() {
        if (instance != null) {
            DestroyImmediate(gameObject);
        } else {
            // DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    private void Start() {
        switch (carScriptableObject.carType) {
            case CarType.GREEN:
                carPrefab = carPrefabs[0];
                break;
            case CarType.BLUE:
                carPrefab = carPrefabs[1];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void LeftButtonClick() {
        if (!GameManager.instance.isGameStarted) {
            GameManager.instance.isGameStarted = true;
        }
        currentCar.RotateLeft();
    }

    public void RightButtonClick() {
        if (!GameManager.instance.isGameStarted) {
            GameManager.instance.isGameStarted = true;
        }
        currentCar.RotateRight();
    }

    public void ControlButtonUp() {
        currentCar.ControlButtonUp();
    }
    
    public void CreateCars() {
        foreach (var car in carHistory) {
            var obj = Instantiate(carPrefab, car.GetFirstPosition(), Quaternion.identity);
            obj.GetComponent<CarBehaviourBase>().InitializeFieldsReplay(car);
            currentCarObjs.Add(obj);
        }
        currentCar = Instantiate(carPrefab, LevelManager.instance.GetCurrentStartPos(), Quaternion.identity).GetComponent<CarBehaviourBase>();
        currentCar.InitializeFieldsForPlayer();
        currentCarObjs.Add(currentCar.gameObject);
    }

    public void CreateCarsWithHistory(bool isRestart) {
        if (LevelManager.instance.currentWaveIndex == 0) {
            carHistory.Clear();
        }
        else {
            if (!isRestart) {
                carHistory.Add(currentCar);
            }
        }
        
        foreach (var car in currentCarObjs) {
            Destroy(car);
        }
        
        CreateCars();
    }
}
