using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _totalHits;
    [SerializeField] private TextMeshProUGUI _obstacleHits;
    private Growth _growth;
    private Ball _ball;

    private void Awake() {
        _growth = FindObjectOfType<Growth>();
        _ball = FindObjectOfType<Ball>();
    }

    private void Start() 
    {
      _totalHits.text = "Total hits: " + _ball.GetTotalHits();
      _obstacleHits.text = "Obstacle hits: " + _growth.GetGrowthTimes() + " of " + _growth.GetMaxGrowthTimes();  
    }

    private void Update() 
    {
        _totalHits.text = "Total hits: " + _ball.GetTotalHits();
        _obstacleHits.text = "Obstacle hits: " + _growth.GetGrowthTimes() + " of " + _growth.GetMaxGrowthTimes(); 
    }
}
