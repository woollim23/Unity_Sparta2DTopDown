using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private string playerTag;
    public ObjectPool ObjectPool;

    public Transform Player { get; private set; }

    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this; // 이렇게 하면 주소가 할당 되나??

        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

        ObjectPool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
