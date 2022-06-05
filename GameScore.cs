using UnityEngine;
using UnityEngine.Events;

public class GameScore : MonoBehaviour
{
    private const string bestScoreKey = "BestScore";
    [SerializeField]
    private UnityEvent<string> scoreView;
    
    private float floatValue;

    public void Nullify()
    {
        FloatValue = 0;
    }

    private void UpdateViews(int value)
    {
        scoreView?.Invoke(value.ToString());
    }

    private void SetScore(float value)
    {
        floatValue = value;
        UpdateViews((int)floatValue);
    }

    public float FloatValue
    {
        get => floatValue;
        set => SetScore(value);
    }

    public int IntValue => (int)FloatValue;

    public void Save()
    {
        PlayerPrefs.SetInt(bestScoreKey, Mathf.Max(PlayerPrefs.GetInt(bestScoreKey), IntValue));
        try {
            GameSession.SendScore(IntValue);
        }
        catch (System.Exception) {
            Debug.LogWarning("Send score to server is falled");
        }
    }

    public int GetBest()
    {
        if (!PlayerPrefs.HasKey(bestScoreKey))
            PlayerPrefs.SetInt(bestScoreKey, 0);
        return PlayerPrefs.GetInt(bestScoreKey);
    }
}
