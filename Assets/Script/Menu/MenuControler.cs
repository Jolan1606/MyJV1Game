using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
 public void changeScene(string SceneTest)
 {
   UnityEngine.SceneManagement.SceneManager.LoadScene("SceneTest");

 }
    public void QuitGame()
    {
        Application.Quit();
    }
}
