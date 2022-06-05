using UnityEngine;

public class RotationRoot : MonoBehaviour
{
    [SerializeField]
    private Transform model;
    [SerializeField]
    private Transform downPoint;
    [SerializeField]
    private Magnit magnit;

    private new Transform transform;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    private void ReverseRotation(Transform obj)
    {
        obj.localEulerAngles += new Vector3(180, 0, 0);
    }

    public void ReverseModel() => ReverseRotation(model);

    public void ReverseDownPoint()
    {
        ReverseRotation(downPoint);
        magnit.ChangePolarity();
    }

}
