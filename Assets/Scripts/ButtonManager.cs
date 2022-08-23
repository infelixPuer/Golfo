using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour 
{
    [SerializeField] private int _levelCount;

    public int GetLevelCount() {
        return _levelCount;
    }

    public void Play() {
        SceneManager.LoadScene("Level 1");
    }

    public void Rules() {
        SceneManager.LoadScene("Rules");
    }

    public void Exit() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void NextLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex + 2;
        int nextScene = currentScene + 1;

        if (nextScene > _levelCount + 2) {
            SceneManager.LoadScene("EndScreen");
            return;
        }

        SceneManager.LoadScene(nextScene);
    }
}
