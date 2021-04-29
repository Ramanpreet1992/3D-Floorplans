using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public GameObject uploadingScreen;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void LoadingScene()
    {

        uploadingScreen.SetActive(false);
        loadingScreen.SetActive(true);
        // SceneManager.LoadScene("Scene1");
        // SceneManager.LoadScene("Scene2");
        StartCoroutine(LoadingScenes(sceneName));

    }
    IEnumerator LoadingScenes(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scene1");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress/0.9f);
            slider.value = progress;
            progressText.text = progress * 100f +"%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                   //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;

        }
    }
}

