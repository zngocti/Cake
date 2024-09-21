using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIImageConverter : MonoBehaviour
{
    private void OnEnable()
    {
        var sprite = GetComponent<SpriteRenderer>().sprite;
        GameObject.Destroy(GetComponent<SpriteRenderer>().GetComponent<SpriteRenderer>());
        GetComponent<SpriteRenderer>().gameObject.AddComponent<Image>();
        GetComponent<SpriteRenderer>().gameObject.AddComponent<CanvasRenderer>();
        GetComponent<SpriteRenderer>().gameObject.GetComponent<Image>().sprite = sprite;
    }      
}
