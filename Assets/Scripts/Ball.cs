using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_renderer;
    //private Movement m_player1;
    //private Movement m_player2;

    [SerializeField] private float m_beginForce = 30;
    [SerializeField] private Logic m_logic;
    [SerializeField] private Bonus m_bonus;
    [HideInInspector] public float force;
    [HideInInspector] public Vector2 velocity;
    private void Start()
    {
        //var objs = FindObjectsOfType<Movement>();
        //m_player1 = objs[0];
        //m_player2 = objs[1];
        m_renderer = GetComponent<SpriteRenderer>();
        //m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        gameObject.transform.Translate(velocity*Time.deltaTime);
    }

    private float Direction()
    {
        if(Random.value > 0.5)
        {
            return 1;
        }
        return -1;
    }
    public void ResetBall()
    {
        m_renderer.enabled = true;
        force = m_beginForce;
        gameObject.transform.position = new Vector3(0, 0);
    }
   
    public void RandMoveBall()
    {
        velocity = new Vector2(force * Direction(), force * Direction());
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Top")
        {
            velocity = new Vector2(velocity.x, -force);
        }
        else if (collision.gameObject.name == "Bottom")
        {
            velocity = new Vector2(velocity.x, force);
        }
        else if (collision.gameObject.name == "Player1")
        {
            if(collision.gameObject.transform.position.x < gameObject.transform.position.x)
            {
                velocity = new Vector2(force, velocity.y);
            }
        }
        else if (collision.gameObject.name == "Player2")
        {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                velocity = new Vector2(-force, velocity.y);
            }
        }
        else if (collision.gameObject.name == "Left")
        {
            velocity = new Vector2();
            m_logic.UpScoreSecond();
            m_logic.ResetGame();

        }
        else if (collision.gameObject.name == "Right")
        {
            velocity = new Vector2();
            m_logic.UpScoreFirst();
           
            m_logic.ResetGame();
        }
        m_bonus.TrySpawnSomething();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpeedUp")
        {
            force += m_beginForce / 2;
            var dir = velocity.normalized;
            velocity.Set(force * dir.x, force * dir.y);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.name == "Random")
        {
            RandMoveBall();
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.name == "Cut")
        {
            var objs = FindObjectsOfType<Movement>();

            var transform1 = objs[0].GetComponent<Transform>();
            transform1.localScale -= new Vector3(0, 0.4f * transform.localScale.y);

            var transform2 = objs[1].GetComponent<Transform>();
            transform2.localScale -= new Vector3(0, 0.4f * transform.localScale.y);
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.name == "Clock")
        {
            var objs = FindObjectsOfType<Movement>();
            StartCoroutine(SlowDown(objs));
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.name == "Portal")
        {
            m_bonus.Spawn(gameObject.transform);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.name == "Inviz")
        {
            StartCoroutine(Inviz());
            collision.gameObject.SetActive(false);
        }
    }
    private IEnumerator SlowDown(Movement[] objs)
    {
        var power = 3;
        objs[0].speed /= power;
        objs[1].speed /= power;
        yield return new WaitForSeconds(1f);
        objs[0].speed *= power;
        objs[1].speed *= power;
        yield return null;
    }
    private IEnumerator Inviz()
    {
        m_renderer.enabled = false;
        yield return new WaitForSeconds(1f);
        m_renderer.enabled = true; 
        yield return null;
    }

}

