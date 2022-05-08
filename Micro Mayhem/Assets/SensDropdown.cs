using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensDropdown : MonoBehaviour
{
    Dropdown sensDropdown;
    // Start is called before the first frame update
    void Start()
    {
        sensDropdown = GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sensDropdown.value == 0)
        {
            InputMgr.inst.mouseSensitivity = 500;
        }

        if (sensDropdown.value == 1)
        {
            InputMgr.inst.mouseSensitivity = 750;
        }

        if (sensDropdown.value == 2)
        {
            InputMgr.inst.mouseSensitivity = 250;
        }
    }
}
