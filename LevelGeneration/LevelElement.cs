using UnityEngine;

public class LevelElement : MonoBehaviour
{
    [SerializeField]
    private Transform extremeBackPoint;
    [SerializeField]
    private Transform extremeForwardPoint;

    protected CommonGenerator generator;

    protected new Transform transform;

    protected Vector3 direction;

    public virtual void Init(CommonGenerator generator, int index)
    {
        transform = GetComponent<Transform>();
        this.generator = generator;
        direction = generator.GetDirection();
    }

    public Vector3 Position => transform.position;

    public float Length => Vector3.Project(extremeForwardPoint.position - extremeBackPoint.position, direction).magnitude;

    public virtual void Regen(Vector3 position, int index)
    {
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerModel player))
            generator.OnElementFinish(this);
    }

}
