using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasActions : MonoBehaviour {
    public void LeftControlButtonDown() {
        CarManager.instance.LeftButtonClick();
    }
    
    public void RightControlButtonDown() {
        CarManager.instance.RightButtonClick();
    }

    public void ControlButtonUp() {
        CarManager.instance.ControlButtonUp();
    }

    public void RestartLevel() {
        LevelManager.instance.RestartLevel();
    }
}
