using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentItem : MonoBehaviour
{
    static CurrentItem _instance;
    static public CurrentItem Instance { get => _instance; }

    SelectableItem _itemSelected;

    public SelectableItem ItemSelected { get => _itemSelected; }

    bool _settingNewItem = false;

    [SerializeField]
    Sprite _selectorSprite;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ColorItemSelected(Color color)
    {
        if (_itemSelected == null)
        {
            return;
        }

        _itemSelected.ColorSelector(color);
    }

    public void SetItemSelected(SelectableItem newItem)
    {
        if (_settingNewItem)
        {
            return;
        }

        _settingNewItem = true;

        if (_itemSelected != null)
        {
            _itemSelected.m_selected = false;
            _itemSelected.ChangeToggleValue(false);
            _itemSelected.ResetSelector();
        }

        _itemSelected = newItem;

        if (_itemSelected != null)
        {
            _itemSelected.m_selected = true;
            _itemSelected.ChangeToggleValue(true);
            _itemSelected.ColorSelector(SyrupSelector.Instance.m_selected);
            _itemSelected.SetSelectorSprite(_selectorSprite);
        }

        _settingNewItem = false;
    }
}
