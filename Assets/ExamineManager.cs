using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExamineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject message;

    [SerializeField]
    private float size;

    bool examineFlag;
    bool examinedFlag;

    [SerializeField]
    private bool x_axis;

    [SerializeField]
    private bool y_axis;
    [SerializeField]
    private Vector2 pos;
    GameObject playerObject;

    [SerializeField]
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        message = Instantiate(message);
        message.transform.position = new Vector3(gameObject.transform.position.x + pos.x, gameObject.transform.position.y + pos.y, 0.0f);

        playerObject = GameObject.FindWithTag("Player");
        examineFlag = false;
        examinedFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        message.transform.position = new Vector3(gameObject.transform.position.x + pos.x, gameObject.transform.position.y + pos.y, 0.0f);
        if (x_axis && y_axis)
        {
            if (playerObject.transform.position.x - size < transform.position.x &&
                playerObject.transform.position.x + size > transform.position.x &&
                playerObject.transform.position.y - size < transform.position.y &&
                playerObject.transform.position.y + size > transform.position.y)
            {
                message.SetActive(true);
                examineFlag = true;
            }
            else
            {
                message.SetActive(false);
                examineFlag = false;
            }

        }
        else if(x_axis)
        {
            if (playerObject.transform.position.x - size < transform.position.x &&
                playerObject.transform.position.x + size > transform.position.x)
            {
                message.SetActive(true);
                examineFlag = true;
            }
            else
            {
                message.SetActive(false);
                examineFlag = false;
            }

        }
        else if(y_axis)
        {
            if (playerObject.transform.position.y - size < transform.position.y &&
                playerObject.transform.position.y + size > transform.position.y)
            {
                message.SetActive(true);
                examineFlag = true;
            }
            else
            {
                message.SetActive(false);
                examineFlag = false;
            }

        }
        else
        {
            message.SetActive(false);
            examineFlag = false;
        }
        if(examineFlag)
        if(Input.GetKeyUp(KeyCode.Space))
        {
             examinedFlag = true;
                if (anim.GetBool("item"))
                {
                        anim.SetBool("item", false);
                }
                else
                anim.SetBool("item", true);

            }
        Debug.Log(examinedFlag);

    }
    public bool GetExamineFlag()
    {
       
        return examineFlag;
    }
    public bool GetExaminedFlag()
    {
        return examinedFlag;
    }
}
