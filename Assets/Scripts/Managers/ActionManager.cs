using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {
    public List<UnitFigure> Units = new();

    private void Update() {
        foreach (UnitFigure unit in Units) {
            unit.Ordered(Time.deltaTime);
        }
    }
}
