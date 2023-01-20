using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    GameObject playerObj;
    Rigidbody2D fallRigid;
    [SerializeField]
    [Range(0, 1)]
    private float fallScopeSize = 0.0f;
    [SerializeField]
    private bool IsUp = true;
    [SerializeField]
    private bool IsDown = true;
    bool onFloorCollider = false;
    float fallObjectY = 0.0f;
    bool fallFlag = true;
    bool IsFallFlag = false;
    Vector2 objSize;
    private void Start()
    {
        objSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        // プレイヤータグ
        playerObj = GameObject.FindWithTag("Player");
        fallRigid = gameObject.GetComponent<Rigidbody2D>();
        fallObjectY = gameObject.transform.position.y;
        fallFlag = true;
        IsFallFlag = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(IsDown)
        if (gameObject.transform.position.x + objSize.x + fallScopeSize > playerObj.transform.position.x)
        {
            if (gameObject.transform.position.x - objSize.x - fallScopeSize < playerObj.transform.position.x) 
            {
                MoveFall();
            }
        }

        if(IsUp)
        if(onFloorCollider)
        {
            StartCoroutine(MoveDown());
        }

    }

    public void MoveFall()
    {
        if(fallFlag)
        {
            fallRigid.simulated = true;
            IsFallFlag = true;
            fallFlag = false;
        }
    }

    public bool GetFallFlag()
    {
        return IsFallFlag;
    }
    private IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(0.9f);

        float speed = 0.001f;
        while (gameObject.transform.position.y < fallObjectY/* - nowFallPos; i += 0.01f*/)
        {
            fallRigid.transform.position += new Vector3(0.0f, speed, 0.0f);
            yield return new WaitForSeconds(0.3f);
            speed += 0.001f;
        }

        yield return new WaitForSeconds(0.8f);

        fallFlag = true;
        onFloorCollider = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Floor")
        //{
            IsFallFlag = false;
            fallRigid.simulated = false;
            onFloorCollider = true;
        //}
    }
}
