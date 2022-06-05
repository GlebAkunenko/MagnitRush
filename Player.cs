using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player GetInstance() => instance;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private KeyboardInput keyboardInput;
    [SerializeField] private TouchInput touchInput;
    [SerializeField] private PlayerParticles particles;
    [SerializeField] private RotationRoot rotationRoot;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Magnit head;
    [SerializeField] private PlayerModel model;
    [SerializeField] private PlayerSounds bumpSounds;
    [SerializeField] private Cinemachine.CinemachineBrain cinemachine;
    [SerializeField] private GameScore score;

    [Space(15)]
    [SerializeField] private Plate firstPlate;

    private Animator stateMachine;
    private PlayerInput input;
    private Plate nextPlate;
    private Plate currentPlate;

    private PlayerAction lastInput;

    public RotationRoot RotationRoot => rotationRoot;

    public PlayerMovingState CurrentMovingState { get; set; }

    public Plate CurrentPlate
    {
        get => currentPlate;
        set
        {
            currentPlate = value;
            ChangedCurrentPlate?.Invoke(value);
        }
    }


    public bool IsFly => (CurrentMovingState == null) ? false : CurrentMovingState.IsFlying;

    public Plate NextPlate
    {
        get => nextPlate;
        set
        {
            nextPlate = value;
            if (nextPlate != null)
                stateMachine.SetInteger("Orientation", (int)nextPlate.GetOrientation());
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        SetUpComponents();
        ChooseInputController();
        yield return new WaitForSeconds(1);
        StartGame();
    }

    private void SetUpComponents()
    {
        stateMachine = GetComponent<Animator>();
        model.Player = this;
        score.Nullify();

        movement.DistanceTraveled += (float distance) => score.FloatValue += distance;
        movement.SpeedChanged += (float up) => {
            stateMachine.speed = up;
            bumpSounds.Speed = up;
        };
    }

    private void ChooseInputController()
    {
        if (inputManager == InputManager.Auto) {
            if (Application.platform == RuntimePlatform.Android)
                input = touchInput;
            else
                input = keyboardInput;
        }
        else if (inputManager == InputManager.Keybourd)
            input = keyboardInput;
        else if (inputManager == InputManager.Touch)
            input = touchInput;
        else
            throw new System.Exception("Wrong input manager");
    }

    private void StartGame()
    {
        CurrentPlate = firstPlate;
        input.PlayerSwaped += OnPlayerSwap;
        movement.StartMove();
    }

    private void OnPlayerSwap(PlayerAction action)
    {
        if (NextPlate == null)
            return;
        if (IsFly) {
            //stateMachine.SetInteger("NextInput", (int)action);
            return;
        }

        lastInput = action;
        particles.OnPlayerJump();
        stateMachine.SetInteger("Input", (int)action);
    }

    public void OnPlayerLandOn()
    {
        UpdateCurrentPlate();
        particles.OnPlayerFall();
        bumpSounds.Play();
    }

    private void UpdateCurrentPlate()
    {
        CurrentPlate = NextPlate;
        NextPlate = null;
    }

    public bool IsAttractToNextPlate()
    {
        if (NextPlate == null)
            return false;
        return NextPlate.IsAttract(head, lastInput);
    }

    public void Lose()
    {
        stateMachine.SetBool("Losed", true);
    }

    public void OnLose(bool fall)
    {
        score.Save();
        movement.Stop();
        cinemachine.enabled = false;
        if (fall) {
            stateMachine.enabled = false;
            model.Fall(movement.Speed);
        }
    }

    public event System.Action<Plate> ChangedCurrentPlate;

    public enum InputManager
    {
        Auto,
        Keybourd,
        Touch,
    }
}
