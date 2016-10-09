using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseTile : MonoBehaviour {

	void Awake () {
        //m_keyText = GetComponent<Text>();
        EventManager.AddListener<KeyPressedEvent>(OnKeyPressed);
	}
    
    void OnDestroy() {
        EventManager.RemoveListener<KeyPressedEvent>(OnKeyPressed);
    }

    void OnKeyPressed(KeyPressedEvent a_event) {

    }
}
