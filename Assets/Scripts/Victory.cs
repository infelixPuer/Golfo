using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour 
{
    [SerializeField] private float _timeToLoadNextScene = 1.5f;
    private int _levelIndex;
    private int _levelCount;
    private bool _isWin;

    private void Start() {
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        _levelCount = SceneManager.sceneCountInBuildSettings - 4;
        Debug.Log(_levelCount);
        Debug.Log(_levelIndex);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Ground")) { return; }

        if (!_isWin && _levelIndex == 6) {
            Debug.Log("End!");
            _isWin = true;
            Invoke("LoadEndScreen", _timeToLoadNextScene);
        } else if (!_isWin) {
            Debug.Log("Victory!");
            _isWin = true;
            Invoke("LoadNextLevel", _timeToLoadNextScene);
        }
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadEndScreen() {
        SceneManager.LoadScene("EndScreen");
    }
}
