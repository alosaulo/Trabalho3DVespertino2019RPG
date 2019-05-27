using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public GameObject directionaLight;
    public float lightSpeed;

    public static GameManager _instance;
    public Player Player;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        directionaLight.transform.Rotate(Vector3.right * lightSpeed * Time.deltaTime);
    }
}
