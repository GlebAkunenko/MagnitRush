using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Magnit))]
public class Plate : MonoBehaviour
{
    [SerializeField]
    private Orientation position;

    public Orientation GetOrientation() => position;

    public PlateScale Scale => plateScale;

    public Magnit magnit { private set; get; }

    [SerializeField]
    private PlateScale plateScale;
    [SerializeField]
    private GameObject north;
    [SerializeField]
    private GameObject south;
    [SerializeField]
    private UnityEvent onPlayerFall;

    private BoxCollider trigger;
    private Player player;

    private void Start()
    {
        magnit = GetComponent<Magnit>();
        trigger = GetComponent<BoxCollider>();
        player = Player.GetInstance();
        player.ChangedCurrentPlate += OnCurrentPlateChange;
    }

    private void OnCurrentPlateChange(Plate current)
    {
        if (current == null)
            return;
        trigger.enabled = (this != current);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerModel playerModel)) {
            playerModel.Player.NextPlate = this;
            onPlayerFall?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerModel playerModel)) {
            if (!playerModel.Player.IsFly) {
                if (playerModel.Player.NextPlate == this)
                    playerModel.Player.NextPlate = null;
            }
        }
    }

    public bool IsAttract(Magnit playerHead, PlayerAction input)
    {
        int sign = input == PlayerAction.Left ? 1 : -1;
        sign *= -(int)playerHead.Polarity * (int)magnit.Polarity;
        sign *= (int)position - (int)player.CurrentPlate.position;
        return sign == 1;
    }

    public void SetUpSize(Vector3 startPoint, float lenght)
    {
        plateScale.SetUp(startPoint, startPoint + GlobalSettings.MovementDirection * lenght);
    }

    public void SetUpSize(Vector3 startPoint, float lenght, float triggerBuffer)
    {
        plateScale.SetUp(
            startTriggerPosition: startPoint - GlobalSettings.MovementDirection * triggerBuffer,
            startModelPosition: startPoint,
            endPosition: startPoint + GlobalSettings.MovementDirection * lenght
            );
    }

    public void SetUpPolarity(Polarity polarity)
    {
        if (magnit == null)
            magnit = GetComponent<Magnit>();

        magnit.Polarity = polarity;
        switch (polarity) {
            case Polarity.North:
                north.SetActive(true);
                south.SetActive(false);
                break;
            case Polarity.South:
                north.SetActive(false);
                south.SetActive(true);
                break;
            default:
                throw new System.Exception("Wrong polarity");
        }
    }


    private void OnDestroy()
    {
        player.ChangedCurrentPlate -= OnCurrentPlateChange;
    }

}

public enum Orientation { Mid = 0, Left = 1, Right = -1 }
