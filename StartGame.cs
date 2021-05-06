using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour
{
    public GameObject fade;
    public GameObject progressBar;
    Slider slider;

    private void Start()
    {
        slider = progressBar.GetComponent<Slider>();
    }
    public void startGame()
    {

        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        AudioManager.instance.Clicking.Play();
        fade.SetActive(true); 
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            slider.value = operation.progress;
            yield return null;
            
        }
        

    }

   
}
