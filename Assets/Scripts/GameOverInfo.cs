using UnityEngine;
using UnityEngine.UI;

public class GameOverInfo : MonoBehaviour
{
    [SerializeField] private Text _currUIScoreText;
    [SerializeField] private Text _bestUIScoreText;
    [SerializeField] private Text _currScore;

    void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        _bestUIScoreText.text = bestScore.ToString();
        _currUIScoreText.text = _currScore.text;
    }

    private void Update()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");

        if (int.Parse(_currUIScoreText.text) > bestScore)
        {
            _bestUIScoreText.text = _currUIScoreText.text;
            PlayerPrefs.SetInt("BestScore", int.Parse(_currUIScoreText.text));
        }
        else
        {
            _currUIScoreText.text = _currScore.text;
            _bestUIScoreText.text = bestScore.ToString();
        }
    }
}
