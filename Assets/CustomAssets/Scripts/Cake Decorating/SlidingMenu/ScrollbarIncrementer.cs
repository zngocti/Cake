using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScrollbarIncrementer : MonoBehaviour
{
    public Scrollbar Target;
    public Button TheOtherButton;
    public float Step;
    public bool _increment;

    Button _myButton;

    private void Start()
    {
        _myButton = GetComponent<Button>();

        int child = transform.parent.GetChild(0).GetChild(0).childCount;

        if (child <= 4)
        {
            Step = 0f;
            _myButton.interactable = false;
            TheOtherButton.interactable = false;
        }
        else
        {
            Step = 1.0f / (child - 4);
        }

        Target.onValueChanged.AddListener(Adjust);
    }
    public void Increment()
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp01(Mathf.Clamp(Target.value, 0, 1) + Step);
        //_myButton.interactable = Target.value != 1;
        //TheOtherButton.interactable = true;
    }

    public void Decrement()
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp01(Mathf.Clamp(Target.value, 0, 1) - Step);
        //_myButton.interactable = Target.value != 0; ;
        //TheOtherButton.interactable = true;
    }

    public void Adjust(float newValue)
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");

        if (Step == 0f)
        {
            return;
        }

        if (_increment)
        {
            if (Target.value >= 1)
            {
                _myButton.interactable = false;
                TheOtherButton.interactable = true;
            }
            else if (Target.value <= 0)
            {
                _myButton.interactable = true;
                TheOtherButton.interactable = false;
            }
            else
            {
                _myButton.interactable = true;
                TheOtherButton.interactable = true;
            }
        }
        else
        {
            if (Target.value >= 1)
            {
                _myButton.interactable = true;
                TheOtherButton.interactable = false;
            }
            else if (Target.value <= 0)
            {
                _myButton.interactable = false;
                TheOtherButton.interactable = true;
            }
            else
            {
                _myButton.interactable = true;
                TheOtherButton.interactable = true;
            }
        }
    }
}   