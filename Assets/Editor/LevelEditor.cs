using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


[CustomEditor(typeof(LevelEditorBehavior))]
public class PlayerEditor : Editor {
    private void OnEnable() {
        LevelEditorBehavior m_target = (LevelEditorBehavior) target;
        m_target.LoadLevels();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        LevelEditorBehavior m_target = (LevelEditorBehavior) target;

        GUILayout.Space(16);

        m_target.levelIndex = EditorGUILayout.IntField("Level Index", m_target.levelIndex);
        m_target.waveIndex = EditorGUILayout.IntField("Wave Index", m_target.waveIndex);
        GUILayout.Space(16);

        if (GUILayout.Button("Open Current Wave")) {
            m_target.OpenCurrentWave();
        }
        
        GUILayout.Space(8);
        if (GUILayout.Button("Save Start-End For Wave")) {
            m_target.SaveWave();
        }
        
        if (GUILayout.Button("Save Obstacles For Level")) {
            m_target.SaveObstaclesForLevel();
        }
        
        GUILayout.Space(16);
        if (GUILayout.Button("Destroy Start")) {
            m_target.DestroyStartObject();
        }
        if (GUILayout.Button("Create Start")) {
            m_target.CreateStartObject(false,Vector3.zero);
        }

        if (m_target.startObj != null) {
            m_target.startObj.transform.position = EditorGUILayout.Vector3Field("Start Position", m_target.startObj.transform.position);
        }
        
        GUILayout.Space(16);
        if (GUILayout.Button("Destroy End")) {
            m_target.DestroyEndObject();
        }
        if (GUILayout.Button("Create End")) {
            m_target.CreateEndObject(false,Vector3.zero);
        }

        if (m_target.endObj != null) {
            m_target.endObj.transform.position = EditorGUILayout.Vector3Field("End Position", m_target.endObj.transform.position);
        }
        
        GUILayout.Space(16);
        
        if (GUILayout.Button("Clear All Obstacle")) {
            m_target.ClearAllObstacles();
        }
        
        GUILayout.Space(16);
        
        if (GUILayout.Button("Create Obstacle")) {
            m_target.CreateObstacleObject(false, Vector3.zero, Quaternion.identity);
        }

        if (m_target.obtacleObjs.Count > 0) {
            for (int i = 0; i < m_target.obtacleObjs.Count; i++) {
                m_target.obtacleObjs[i].transform.position = EditorGUILayout.Vector3Field(m_target.obtacleObjs[i].transform.name, m_target.obtacleObjs[i].transform.position);
                m_target.obtaclePositions[i] = m_target.obtacleObjs[i].transform.position;
            }
        }


        GUILayout.Space(32);
        
        if (GUILayout.Button("Load Data From File")) {
            m_target.LoadLevels();
        }
        
        if (GUILayout.Button("Remove Data From File")) {
            m_target.DeleteSave();
        }
    }
}
