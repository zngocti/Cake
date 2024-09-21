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

    [Min(1)]
    [SerializeField]
    float _sizeToShow = 4;

    [SerializeField]
    Transform _childContainer;

    private void Start()
    {
        TryGetComponent<Button>(out _myButton);

        int child = 1;

        if (_childContainer == null)
        {
            child = transform.parent.GetChild(0).GetChild(0).childCount;
        }
        else
        {
            child = _childContainer.childCount;
        }

        if (child <= _sizeToShow)
        {
            Step = 0f;

            if (_myButton != null)
            {
                _myButton.interactable = false;
            }

            if (TheOtherButton != null)
            {
                TheOtherButton.interactable = false;
            }
        }
        else
        {
            Step = 1.0f / (child - _sizeToShow);
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