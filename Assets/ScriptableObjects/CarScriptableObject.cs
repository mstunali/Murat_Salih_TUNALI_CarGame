using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarType {GREEN, BLUE}

[CreateAssetMenu(fileName = "CarData", menuName = "ScriptableObjects/CarScriptableObject", order = 1)]
public class CarScriptableObject : ScriptableObject {
    public CarType carType = CarType.GREEN;
    
    public float greenCarSpeed = 2;
    public float greenCarRotationAngle = 60;
    
    public float blueCarSpeed = 1;
    public float blueCarRotationAngle = 30;
}
