using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Load() {
        SceneManager.LoadScene("TemplateScene", LoadSceneMode.Additive);
    }

    public void Remove() {
        //SceneManager.("TemplateScene", LoadSceneMode.Additive);
    }
}
