using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Levels {
    public List<LevelData> levels;
    
}

[System.Serializable]
public class LevelData {
    public List<WaveData> waves;
    public List<ObstacleData> obstacles;
    
}

[System.Serializable]
public class WaveData {
    public Vector3 startPosition;
    public Vector3 endPosition;
}

[System.Serializable]
public class ObstacleData {
    public Vector3 position;
    public Quaternion rotation;
}
