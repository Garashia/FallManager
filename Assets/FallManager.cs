using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    GameObject playerObj;                   // プレイヤーオブジェクト
    Rigidbody2D fallRigid;                  // オブジェクトのリジッド
    [SerializeField]
    [Range(0, 1)]
    private float fallScopeSize = 0.0f;     // オブジェクトの落下判定を広げてくれる

    [SerializeField]
    private bool IsUp = true;               // 上がるかをInspectorで設定

    [SerializeField]
    private bool IsDown = true;             // 落ちるかをInspectorで設定

    private bool onFloorCollider = false;   // 上がり判定
    private float fallObjectY = 0.0f;       // 上がる上限
    private bool fallFlag = true;           // 落ちるべきかどうかを確認
    private bool IsFallFlag = false;        

    private Vector2 objSize; // オブジェクトのサイズ

    private WaitForSeconds cachedWait0;
    private WaitForSeconds cachedWait1;
    private WaitForSeconds cachedWait2;

    private void Start()
    {
        objSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        // プレイヤータグ
        playerObj = GameObject.FindWithTag("Player");
        fallRigid = gameObject.GetComponent<Rigidbody2D>();
        fallObjectY = fallRigid.transform.position.y;
        fallFlag = true;
        IsFallFlag = false;

        cachedWait0 = new WaitForSeconds(0.9f);
        cachedWait1 = new WaitForSeconds(0.3f);
        cachedWait2 = new WaitForSeconds(0.8f);
    }

    // Update is called once per frame
    private void Update()
    {
        if(IsDown)
        if (fallRigid.transform.position.x + objSize.x + fallScopeSize > playerObj.transform.position.x)
        {
            if (fallRigid.transform.position.x - objSize.x - fallScopeSize < playerObj.transform.position.x) 
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

    // 別のスクリプト上でも呼べる
    public void MoveFall()
    {
        if(fallFlag)
        {
            fallRigid.simulated = true;
            IsFallFlag = true;
            fallFlag = false;
        }
    }

    // 落下中のみtrue、攻撃判定などを付けたいとき用
    public bool GetFallFlag()
    {
        return IsFallFlag;
    }

    // 非同期処理、たまにWaitForSecondsが働いてない時があるので注意
    private IEnumerator MoveDown()
    {
        //var cachedWait0 = new WaitForSeconds(0.9f);
            // キャッシュしてるWaitForSecondsを使う  
        yield return cachedWait0;

        float speed = 0.001f;
        while (fallRigid.transform.position.y < fallObjectY/* - nowFallPos; i += 0.01f*/)
        {
            fallRigid.transform.position += new Vector3(0.0f, speed, 0.0f);
            //var cachedWait1 = new WaitForSeconds(0.3f);
            yield return cachedWait1;
            speed += 0.001f;
        }
        //var cachedWait2 = new WaitForSeconds(0.8f);
        yield return cachedWait2;

        fallFlag = true;
        onFloorCollider = false;
    }

    // 床に"Floor"タグを付けた場合は、関数内のコメント解除するのが好ましい
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Floor"))
        //{
        IsFallFlag = false;
            fallRigid.simulated = false;
            onFloorCollider = true;
        //}
    }
}
