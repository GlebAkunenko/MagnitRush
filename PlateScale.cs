using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlateScale : MonoBehaviour
{
    [SerializeField]
    private Transform[] frontBones;
    [SerializeField]
    private Transform[] backBones;
    [SerializeField]
    private Transform arm;
    [SerializeField]
    private Transform armTarget;
    [SerializeField]
    private Transform mainParent;

    private BoxCollider trigger;
    private Transform transform;

    public float GetLenght() => frontBones[0].position.x - backBones[0].position.x;

    public Vector3 GetEndPoint() => frontBones[0].position;

    public Transform MainParent => mainParent;

    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
        transform = GetComponent<Transform>();
    }

    /// <summary>
    /// WARING: work only with Ox game direction
    /// </summary>
    private void SetUpTrigger(Vector3 startPosition, Vector3 endPosition)
    {
        if (trigger == null)
            trigger = GetComponent<BoxCollider>();
        if (transform == null)
            transform = GetComponent<Transform>();

        startPosition = transform.InverseTransformPoint(startPosition);
        endPosition = transform.InverseTransformPoint(endPosition);
        float length = (endPosition - startPosition).x;

        Vector3 center = trigger.center;
        center.x = (startPosition.x + endPosition.x) / 2;
        trigger.center = center;
        Vector3 size = trigger.size;
        size.x = Mathf.Abs(length);
        trigger.size = size;
    }


    private void MoveObject(Transform obj, Vector3 position)
    {
        Vector3 pos = obj.position;
        pos.x = position.x;
        obj.position = pos;
    }

    private void MoveBones(Transform[] bones, Vector3 position)
    {
        if (bones.Length != 2)
            throw new System.Exception("Wrong bones massive");
        MoveObject(bones[0], position);
        MoveObject(bones[1], position);
    }

    public void SetUp(Vector3 startPosition, Vector3 endPosition)
    {
        SetUp(startPosition, startPosition, endPosition);
    }

    public void SetUp(Vector3 startTriggerPosition, Vector3 startModelPosition, Vector3 endPosition)
    {
        MoveObject(mainParent, (startModelPosition + endPosition) / 2);
        MoveBones(backBones, startModelPosition);
        MoveBones(frontBones, endPosition);
        SetUpTrigger(startTriggerPosition, endPosition);
    }
}
