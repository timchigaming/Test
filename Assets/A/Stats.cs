using System;
using System.ComponentModel;
using UnityEngine;

public class Stats : MonoBehaviour
{ 
    // ----------------------------------- //
    [Header ("Главное")]
    [SerializeField] public float MaxHealth;
    [SerializeField] public float CurrentHealth;
    public string TypeOfEntity;
    public Rigidbody RB;
    public Collider COL;
    [Space]
    [Tooltip("Иногда предметам или окружению мира требуется замедлять время.\nК примеру во время парирования значение будет 0.25")]
    [Range(0,1)] public float TimeSpeed = 1;
    [SerializeField][Range(0,5)] private float Speed = 3;
    // ----------------------------------- //
    [Header ("Вторичное")]
    public bool CanPary;
    
    [SerializeField][ReadOnly(true)]
    private bool ItsAGhost;
    GameObject original;
    [Space]
    [SerializeField] private AnimationCurve ColorCurve1;
   [SerializeField] private AnimationCurve ColorCurve2;
   [SerializeField] private AnimationCurve ColorCurve3;
    private float currentTime;
    private float totalTime;
    // ----------------------------------- //
    
    // Start is called before the first frame update
    void Start()
    {
        if (RB == null)
        {
            RB = GetComponent<Rigidbody>();
            if (RB == null)
            {
                RB = gameObject.AddComponent<Rigidbody>();
            }
        }
        if (COL == null)
        {
            COL = GetComponent<Collider>();
            if (COL == null)
            {
                COL = gameObject.AddComponent<BoxCollider>();
            }
        }
        switch(TypeOfEntity)
        {
            case "Player":
            {
                MaxHealth = 100;
                break;
            }
            case "Enemy":
            {
                MaxHealth = 80;
                break;
            }
            case "Box":
            {
                MaxHealth = 10;
                break;
            }
            case "Wall":
            {
                MaxHealth = 400;
                break;
            }
            case "Window":
            {
                MaxHealth = 40;
                break;
            }
            default:
            {
                MaxHealth = 100;
                break;
            }
        }
        CurrentHealth = MaxHealth;
        totalTime = ColorCurve1.keys[ColorCurve1.length - 1].time;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.color = new Color(ColorCurve1.Evaluate(currentTime), ColorCurve2.Evaluate(currentTime), ColorCurve3.Evaluate(currentTime));
        currentTime += Time.fixedDeltaTime;
        if (currentTime >= totalTime)
        {
            currentTime = 0;
        }
    }
    
    public void TakeDamage(float value)
    {
        CurrentHealth -= value;
        if (CurrentHealth <= 0)
        {
            ItsAGhost = true;
        }
    }
    public void Heal(float value)
    {
        if(CurrentHealth + value > MaxHealth)
            CurrentHealth = 100;
        else
            CurrentHealth += value;
    }

    [ContextMenu("ToggleGhostMode")]
    public void ToggleGhostMode()
    {
        ItsAGhost = !ItsAGhost;
        RB.useGravity = !ItsAGhost;
        RB.isKinematic = ItsAGhost;
        GetComponent<Collider>().enabled = !ItsAGhost;
        Material mat = GetComponent<MeshRenderer>().material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, ItsAGhost ? 0.01f : 1f);
        if (ItsAGhost)
        {
            original = Instantiate(gameObject, transform.position, Quaternion.identity);
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            mat = original.GetComponent<MeshRenderer>().material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b,1f);
        }
        else
        {
            transform.position = original.transform.position;
            transform.rotation = original.transform.rotation;
            transform.localScale = original.transform.localScale;
            Destroy(original);
        }

    }
}
