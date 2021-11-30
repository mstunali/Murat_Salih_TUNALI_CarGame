using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarBehaviourBase : MonoBehaviour {
    public float speed = 3;
    public float rotateAngle = 1;
    public CarScriptableObject carScriptableObject;
    
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    private bool isPlayer = false;
    private int currentFrameCount = 0;
    private bool isControlButtonsPressing = false;
    private Vector3 rotateDirection;

    protected abstract void InitFields();

    private void Start() {
        InitFields();
    }

    void FixedUpdate() {
        if (GameManager.instance.isGameStarted) {
            if (isPlayer) {
                if (isControlButtonsPressing) {
                    transform.Rotate( rotateDirection * rotateAngle * Time.deltaTime);
                }
                
                transform.position += transform.forward * Time.deltaTime * speed;
                positions.Add(transform.position);
                rotations.Add(transform.rotation);
            }
            else {
                if (currentFrameCount < positions.Count) {
                    transform.position = positions[currentFrameCount];
                    transform.localRotation = rotations[currentFrameCount];
                    currentFrameCount++;
                }
            }
        }
    }

    public void RotateLeft() {
        if (!isControlButtonsPressing)
            isControlButtonsPressing = true;
        rotateDirection = Vector3.down;
    }

    public void RotateRight()
    {
        if (!isControlButtonsPressing)
            isControlButtonsPressing = true;
        rotateDirection = Vector3.up;
    }

    public void ControlButtonUp() {
        isControlButtonsPressing = false;
    }
    
    public void InitializeFieldsReplay(CarBehaviourBase carBehaviour) {
        positions = carBehaviour.positions;
        rotations = carBehaviour.rotations;
        isPlayer = false;
    }

    public void InitializeFieldsForPlayer() {
        isPlayer = true;
    }

    public Vector3 GetFirstPosition() {
        return positions[0];
    }

    private void OnTriggerEnter(Collider other) {
        if (isPlayer) {
            if (other.transform.CompareTag("target")) {
                GameManager.instance.WaveSuccess();
            }  
            if (other.transform.CompareTag("obstacle") || other.transform.CompareTag("car")) {
                GameManager.instance.WaveFail();
            }  
        }
    }
}
