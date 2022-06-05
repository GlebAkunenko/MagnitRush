using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private int tailSize;
    [SerializeField]
    private float space;
    [SerializeField]
    private bool isLevelElement;
    [SerializeField]
    private Transform startPosition;
    [SerializeField]
    [Tooltip("In ealers")]
    private Vector3 startRotation;
    [SerializeField]
    private Transform parent;

    private Quaternion spawnRotation;
    private Vector3 direction;

    private int index;
    private int GetNextIndex() => ++index;

    private List<LevelElement> pool = new List<LevelElement>();
    private LevelElement element;
    private LevelElement last => pool[pool.Count - 1];
    private LevelElement first => pool[0];

    public Vector3 GetDirection() => direction;

    private void Start()
    {
        direction = GlobalSettings.MovementDirection;
        spawnRotation = Quaternion.Euler(startRotation);

        element = prefab.GetComponent<LevelElement>();
        if (element == null)
            throw new System.Exception("Generate object must have ILevelElement component");
        if (tailSize >= poolSize)
            throw new System.Exception("Tail size must be lesser than pull size");

        if (isLevelElement) {
            LevelElement self = GetComponent<LevelElement>();
            if (self == null)
                throw new System.Exception("If generator if LevelElement it must have ILevelElement component");
            pool.Add(self);
            self.Init(this, GetNextIndex());
        }

        CreatePool();
    }

    public void OnElementFinish(LevelElement element)
    {
        if (pool.IndexOf(element) > tailSize - 1)
            GenerateNewElement();
    }

    private void CreatePool()
    {
        Vector3 offset = Vector3.zero;
        if (pool.Count == 1)
            offset = direction * (pool[0].Length + space);
        for (int i = 0; i < poolSize; i++) {
            GameObject o = Instantiate(prefab, startPosition.position + offset, spawnRotation, parent);
            LevelElement e = o.GetComponent<LevelElement>();
            e.Init(this, GetNextIndex());
            offset += direction * (e.Length + space);
            pool.Add(e);
        }
    }

    private void GenerateNewElement()
    {
        Vector3 newPos = last.Position + direction * (last.Length + space);
        first.Regen(newPos, GetNextIndex());
        pool.Add(first);
        pool.RemoveAt(0);
    }

}
