using UnityEngine;
using System.Collections;

public class KeyListener : MonoBehaviour
{
    [SerializeField]
    private KeyCode[] m_keys;
    
    // Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < m_keys.Length; i++)
        {
            DetectKeyDown(m_keys[i]);
        }
    }

    void DetectKeyDown(KeyCode a_keyCode)
    {
        if (Input.GetKeyDown(a_keyCode))
        {
            EventManager.Raise(new KeyPressedEvent(a_keyCode));
        }
    }
}