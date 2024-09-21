using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableItem : MonoBehaviour
{
    static int _currentSelectableID = 0;

    public bool m_selected;

    public GameObject m_indicator = null;

    protected int _selectableItemID = 0;

    [Tooltip("This may be ignores for some classes (ThemeItem) if it is overriden on the start method")]
    [SerializeField]
    protected bool _deselectAfterUse = false;

    [SerializeField]
    protected Toggle _toggle;

    [SerializeField]
    Image _selectionImage;

    Sprite _originalSelectionSprite;

    public int SelectableItemID { get => _selectableItemID; }

    //this ID is for each button, all the items placed with the same button have the same selectable item ID
    //do not confuse with the item ID which is unique
    static int GetNewSelectableItemID()
    {
        _currentSelectableID++;
        return _currentSelectableID;
    }

    private void Awake()
    {
        _selectableItemID = GetNewSelectableItemID();

        if (_toggle == null)
        {
            TryGetComponent<Toggle>(out _toggle);
        }

        if (_selectionImage == null)
        {
            if (transform.childCount >= 1)
            {
                if (transform.GetChild(0).TryGetComponent<Image>(out _selectionImage))
                {
                    _originalSelectionSprite = _selectionImage.sprite;
                }
            }
        }
        else
        {
            _originalSelectionSprite = _selectionImage.sprite;
        }
    }

    public void ResetSelector()
    {
        if (_selectionImage == null)
        {
            return;
        }

        _selectionImage.sprite = _originalSelectionSprite;
        _selectionImage.color = Color.white;
    }

    public void ColorSelector(Color color)
    {
        if (_selectionImage == null)
        {
            return;
        }

        _selectionImage.color = color;
    }

    public void SetSelectorSprite(Sprite sprite)
    {
        if (_selectionImage == null)
        {
            return;
        }

        _selectionImage.sprite = sprite;
    }
    /*
    public void SetSelectedIcing()
    {
        if (m_selected)
        {
            CurrentItem.Instance.SetItemSelected(null);
            return;
        }

        CurrentItem.Instance.SetItemSelected(this);
    }*/

    public void SetSelected()
    {
        if (_toggle == null)
        {
            if (m_selected)
            {
                CurrentItem.Instance.SetItemSelected(null);
                return;
            }

            CurrentItem.Instance.SetItemSelected(this);
        }
        else
        {
            if (_toggle.isOn)
            {
                CurrentItem.Instance.SetItemSelected(this);
            }
            else
            {
                CurrentItem.Instance.SetItemSelected(null);
            }
        }
        /*
        if (TopSyrupController._instanceTool)
        {
            TopSyrupController._instanceTool.GetComponent<Animator>().SetTrigger("Return");
            Destroy(TopSyrupController._instanceTool, 1);
        }*/
    }

    public void ChangeToggleValue(bool isOn)
    {
        if (_toggle == null)
        {
            return;
        }

        _toggle.isOn = isOn;
    }
}
