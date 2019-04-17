using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour {
    public Dropdown _dropdown;
    public GameObject LineObject;
    // Use this for initialization
    void Start () {
        _dropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate
        {
            DropdownValueChangedHandler(_dropdown);

        });
        
    }

    private void DropdownValueChangedHandler(Dropdown target)

    {
        if (target.value == 1)
        {
            LineObject.GetComponent<DrawLine2D>().enabled = false;
            LineObject.GetComponent<DrawJump2D>().enabled = true;
        }
        else
        {
            LineObject.GetComponent<DrawLine2D>().enabled = true;
            LineObject.GetComponent<DrawJump2D>().enabled = false;
        }
      //  Debug.Log("target : " + target.value);
    }

}
