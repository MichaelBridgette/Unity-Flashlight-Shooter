﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
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

    public void PlayGame()
   {
        SceneManager.LoadScene("Game");
   }
   public void Quit()
   {
       Application.Quit();
   }
}
