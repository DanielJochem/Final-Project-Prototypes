using UnityEngine;
using System.Collections;

public class KeyPressedEvent : GameEvent {
    public KeyCode PressedKeyCode {
        get; private set;
    }

    public KeyPressedEvent(KeyCode a_keyCodePressed) {
        PressedKeyCode = a_keyCodePressed;
    }
}    