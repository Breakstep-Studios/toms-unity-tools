using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Switches the gameobject on or off depending on the isOn value of the specified toggle
/// </summary>
public class ToggleStateReflector : MonoBehaviour {
    
    [Tooltip("The toggle that the reflector will update alongside.")]
    public Toggle associatedToggle;

    void Start() {
        associatedToggle.onValueChanged.AddListener((isOn) => {
            gameObject.SetActive(isOn);
        });
    }
}
