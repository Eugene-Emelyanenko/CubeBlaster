using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;

    public Image loading;
    public Sprite[] progress;

    public void LoadScene(int ind)
    {
        loadingScreen.SetActive(true);
        
        StartCoroutine(LoadAsync(ind));
    }

    private IEnumerator LoadAsync(int ind)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(ind);

        while (!operation.isDone)
        {
            loading.sprite = operation.progress == 0.25f ? progress[0] : operation.progress == 0.5f ? progress[1] : progress[2];

            if (operation.progress >= .9f && !operation.allowSceneActivation)
            {
                yield return new WaitForSeconds(2.2f);
                operation.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
}
