using UnityEngine;
using UnityEngine.SceneManagement;

public class Growth : MonoBehaviour 
{
    [SerializeField] private int _maxGrowthTimes = 4;
    private int _growthTimes;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            transform.localScale += transform.localScale * .25f;
            _growthTimes++;

            if (_growthTimes > _maxGrowthTimes) {
                SceneManager.LoadScene("LoseScreen");
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Fall")) { return; }

        SceneManager.LoadScene("LoseScreen");
    }

    public int GetGrowthTimes() {
        return _growthTimes;
    }

    public int GetMaxGrowthTimes() {
        return _maxGrowthTimes;
    }
}
