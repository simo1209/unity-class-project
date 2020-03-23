using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneSwitch : MonoBehaviour
{

    public string SceneName;
    public void NextScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}