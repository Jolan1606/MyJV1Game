using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
 public void changeScene(string SceneTest)
 {
   UnityEngine.SceneManagement.SceneManager.LoadScene("MainLevelR");

 }
    public void QuitGame()
    {
        Application.Quit();
    }
}
