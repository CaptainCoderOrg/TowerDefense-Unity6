using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    public void LoadScene(string sceneName) => StartCoroutine(LoadSceneAscyn(sceneName));

    private IEnumerator LoadSceneAscyn(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while(!operation.isDone)
        {
            yield return null;
        }
    }
}
