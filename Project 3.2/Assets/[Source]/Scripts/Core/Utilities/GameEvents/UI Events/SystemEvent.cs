using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemEvent : MonoBehaviour {

    public void QuitGame() {
        Application.Quit();
    }

    public void ChangeScene(string newScene) {
        SceneManager.LoadScene(newScene);
    }

    public void ChangeScene(int newScene) {
        SceneManager.LoadScene(newScene);
    }

    public void LoadMain(){
        SceneManager.LoadScene("Main");
    }
}
