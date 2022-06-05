using LootLocker.Requests;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    const int leaderboardID = 2865;
    const int topSize = 50;

    private static LootLockerGuestSessionResponse player = new LootLockerGuestSessionResponse();

    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) => {
            if (!response.success) {
                Debug.Log("error starting LootLocker session");
                return;
            }
            player = response;
            Debug.Log("successfully started LootLocker session");
        });

        DontDestroyOnLoad(gameObject);
    }

    public static void SendScore(int score)
    {
        if (player == null) 
        {
            Debug.Log("ERROR GameSession.SendScore: var player=null");
            return;
        }
        LootLockerSDKManager.SubmitScore(player.player_identifier, score, leaderboardID, (response) =>
        {
            if (response.statusCode == 200) {
                Debug.Log("Successful");
            }
            else {
                Debug.Log("failed: " + response.Error);
            }
        });
    }

    public static void GetTop()
    {
        LootLockerSDKManager.GetScoreList(leaderboardID, topSize, (response) =>
        {
            if (response.statusCode == 200) {
                Debug.Log("Successful");
            }
            else {
                Debug.Log("failed: " + response.Error);
            }
        });
    }


}
