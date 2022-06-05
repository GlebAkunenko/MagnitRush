using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    //const string fileName = "GameSettings";

    [SerializeField]
    private TravelDirection.Enum movementAxis;

    private static GlobalSettings instance;
    private Vector3 direction;
    private TravelDirection travelDirection;

    //static GlobalSettings()
    //{
    //    var o = Resources.Load<GlobalSettings>(fileName);
    //    if (o == null)
    //        throw new System.Exception("There is no " + fileName + " file is Resources folder");
    //    instance = o;
    //}

    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        travelDirection = new TravelDirection(movementAxis);
        direction = travelDirection.Direction;

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static Vector3 MovementDirection => instance.direction;
    public static TravelDirection TravelDirection => instance.travelDirection;
}
