using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    
    public void Quit() {
        Application.Quit();
    }

    public void Restart() {
        SceneManager.LoadScene(1);
    }
}
