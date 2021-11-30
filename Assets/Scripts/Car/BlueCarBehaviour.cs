using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCarBehaviour : CarBehaviourBase {
    protected override void InitFields() {
        speed = carScriptableObject.blueCarSpeed;
        rotateAngle = carScriptableObject.blueCarRotationAngle;
    }
}
