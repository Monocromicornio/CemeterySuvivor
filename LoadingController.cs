using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingController : MonoBehaviour {

    [SerializeField]
    private Text percent;
    [SerializeField]
    private Slider slider;

    string sceneName = "Game";

    AsyncOperation operation;

    private void Start()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        UpdateProgressUI(0);
        
        StartCoroutine("BeginLoad");
    }

    private IEnumerator BeginLoad()
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            UpdateProgressUI(operation.progress);
            yield return null;
        }

        UpdateProgressUI(operation.progress);
        operation = null;
        this.gameObject.SetActive(false);
    }

    private void UpdateProgressUI(float progress)
    {
        slider.value = progress;
        percent.text = (int)(progress * 100f) + "%";
    }
}
