using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScoreController : MonoBehaviour
{
    [SerializeField] Text _textPoint;

    private ScoreManager _scoreManager;
    [Inject]
    public void Construct(ScoreManager scoreService)
    {
        _scoreManager = scoreService;
    }
    void Start()
    {
        _textPoint.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        _textPoint.text = _scoreManager.GetScore().ToString();
    }
}
