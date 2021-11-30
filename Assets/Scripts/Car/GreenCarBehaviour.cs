using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCarBehaviour : CarBehaviourBase {
    protected override void InitFields() {
        base.speed = carScriptableObject.greenCarSpeed;
        base.rotateAngle = carScriptableObject.greenCarRotationAngle;
    }
}
