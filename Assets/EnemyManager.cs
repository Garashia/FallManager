using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject target;
    [SerializeField]
    private float ScopeSizeRight;
    [SerializeField]
    private float ScopeSizeLeft;

    [SerializeField]
    private float speed;


    bool oikake;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        oikake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < target.transform.position.x + ScopeSizeRight)
        {
            if (transform.position.x > target.transform.position.x - ScopeSizeLeft)
            {
                oikake = true;
            }

        }
        if(oikake)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        }
    }
}
