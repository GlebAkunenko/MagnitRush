using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GameScore))]
public class GameRecordView : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<string> bestScoreFields;

    private void Start()
    {
        bestScoreFields?.Invoke(GetComponent<GameScore>().GetBest().ToString());
    }
}
