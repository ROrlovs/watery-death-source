using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject loading_screen_object;


    public void ChangeLevel(string level){
        loading_screen_object.SetActive(true);
        SceneManager.LoadScene(level);
        StartCoroutine(LoadLevelASync(level));
    }

    IEnumerator LoadLevelASync(string level){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(level);

        while(!loadOperation.isDone){
            yield return null;
        }
    }

}
