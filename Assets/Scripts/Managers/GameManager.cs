using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum GameState { Menu, Transition, OOP, ECS };
    static public GameState State;
    public GameState _state;

    private void Start() {
        State = _state;
    }
}

static public class StatExtentions {
    static public int ArmorMitigation(this int Damage, in int Armor) {
        return Damage - (int)((float)Damage * ((float)Armor).Limit(50));
    }

    static public float Limit(this float value, float halfThreshold) {
        return math.atan(value / halfThreshold) / (math.PI / 2);
    }
}
