using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction;

    private List<Transform> segments = new List<Transform>();

    public Transform segmentPrefab;
    public int initialSize = 4;

    private void Start()
    {
        ResetState();                                
                                                      
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }

    }

    private void FixedUpdate()
    {
        for (int i = segments.Count -1; i > 0 ; i--) 
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x + direction.x), Mathf.Round(transform.position.y + direction.y), 0.0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;  
        segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < segments.Count; i++) 
        {  
            Destroy(segments[i].gameObject);
        }

        segments.Clear();                     
        segments.Add(this.transform); 
        
        for (int i = 1; i < initialSize; i++)  
        {
            segments.Add(Instantiate(segmentPrefab));
        }
        transform.position = Vector3.zero;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }
        else if (collision.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
    
