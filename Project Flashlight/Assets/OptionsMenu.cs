using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    public Button btn;

    private void OnEnable()
    {

        StartCoroutine(SelectButtonLater());
        //es.SetSelectedGameObject(btn.gameObject);
    }
    IEnumerator SelectButtonLater()
    {
        yield return null;
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        btn.Select();
    }
}
