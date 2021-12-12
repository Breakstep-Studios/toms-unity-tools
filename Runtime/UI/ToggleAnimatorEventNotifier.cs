using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Outputs the state of the toggle as an animator trigger event.
/// <para>OnValueChangedOn when toggle turns on or OnValueChangedOff when toggle turns off.</para>
/// </summary>
public class ToggleAnimatorEventNotifier : MonoBehaviour
{
    /// <summary>
    /// The animator we will output our trigger to. Trigger events are 'OnValueChangedOn' and 'OnValueChangedOff'
    /// </summary>
    [Tooltip("The animator we will output our trigger to. Trigger events are 'OnValueChangedOn' and 'OnValueChangedOff'")]
    public Animator animator;

    private Toggle toggle;
    
    private static readonly int OnValueChangedOn = Animator.StringToHash("OnValueChangedOn");
    private static readonly int OnValueChangedOff = Animator.StringToHash("OnValueChangedOff");

    void OnEnable()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChangedHandler);
    }

    void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChangedHandler);

    }

    /// <summary>
    /// Handles the toggle onValueChanged event
    /// </summary>
    /// <param name="isOn"></param>
    private void OnToggleValueChangedHandler(bool isOn)
    {
        animator.SetTrigger(isOn ? OnValueChangedOn : OnValueChangedOff);
    }
}
