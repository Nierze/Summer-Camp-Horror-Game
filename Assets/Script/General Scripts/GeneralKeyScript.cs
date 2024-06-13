using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeneralKeyScript : MonoBehaviour
{
    private Button _button;
    [SerializeField] private KeyCode _keyCode1;
    [SerializeField] private KeyCode _keyCode2;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyCode1) || Input.GetKeyDown(_keyCode2))
        {
            _button.onClick.Invoke();
        }
    }
}
