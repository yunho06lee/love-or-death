using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // ReSharper disable InconsistentNaming

    private Transform transform;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    private float time = 0f;
    
    private GameManager gameManager;
    
    bool moveOn = false;
    
    private int dir;
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        gameManager = FindObjectOfType<GameManager>();
        
        //from here is only for video
        spriteRenderer.color = new Color(0f, 0f, 0f, 255f);
        transform.position = new Vector3(-12.9f, -1.8f, -1f);
    }
    
    void Update()
    {
        if (gameManager.tSceneName == 8)
        {
            spriteRenderer.color = new Color(255f, 255f, 255f, 255f);
        }

        time += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Q))
        {
            EnterWalking("left");
        }
    }

    void EnterWalking(string direction)
    {
        if (direction == "left")
        {
            transform.position = new Vector3(-12.9f, -1.8f, -1f);
        }
        else
        {
            //right side movement
        }
        Debug.Log("enter walking");
        MovementWalk(direction);
    }

    void MovementWalk(string direction)
    {
        Debug.Log("method called");

        if(direction == "left")
            dir = 1;
        else
            dir = -1;
        
        Invoke("SetVal", 1f);
        Invoke("SetVal", 2f);
        Invoke("SetVal", 3f);
        Invoke("SetVal", 4f);
    }

    void SetVal()
    {
        rigidbody.velocity = new Vector2(5 * dir, 0);
    }
    
}
