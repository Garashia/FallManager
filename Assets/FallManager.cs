using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    GameObject playerObj;
    Rigidbody2D fallRigid;
    [SerializeField]
    private float fallObjectSize = 0.0f;
    bool onFloorCollider = false;
    float fallObjectY = 0.0f;
    bool fallFlag = true;
    Vector2 objSize;
    private void Start()
    {
        objSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        // 高さ

        // プレイヤータグをつけて
        playerObj = GameObject.FindWithTag("Player");
        fallRigid = gameObject.GetComponent<Rigidbody2D>();
        fallObjectY = gameObject.transform.position.y;
        fallFlag = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (fallFlag) 
        { 
            if (gameObject.transform.position.x + objSize.x + fallObjectSize > playerObj.transform.position.x)
            {
                if (gameObject.transform.position.x - objSize.x - fallObjectSize < playerObj.transform.position.x) 
                {
                    fallFlag = false;
                    fallRigid.simulated = true;
                }
            }
        }

        if(onFloorCollider)
        {
            StartCoroutine(MoveDown());
        }

    }
    private IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(0.9f);

        float speed = 0.001f;
        while (gameObject.transform.position.y < fallObjectY/* - nowFallPos; i += 0.01f*/)
        {
            gameObject.transform.position += new Vector3(0.0f, speed, 0.0f);
            yield return new WaitForSeconds(0.3f);
            speed += 0.001f;
        }

        yield return new WaitForSeconds(0.8f);

        fallFlag = true;
        onFloorCollider = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.name == "Square") { 
            fallRigid.simulated = false;
            onFloorCollider = true;
        // }
    }
}
