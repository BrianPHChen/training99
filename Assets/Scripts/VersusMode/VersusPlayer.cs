using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VersusPlayer : MonoBehaviour
{

    public float speed = 5f;
    private int m_Hp = 5;
    private int m_Energy = 5;
    public HealthBar healthBar;
    public HealthBar energyBar;
    public string controlSet = null;  // Valid values: Player1, Player2
    public GameObject floatingTextPrefab; // Prefab to show damage/collectable text
    public CircleCollider2D m_collider;
    private PropPrototype prop;
    public bool isInvincible { get; set; } = false;
    [SerializeField] private VersusGameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(m_Hp);
        energyBar.SetMaxHealth(m_Energy);
        m_Energy = 0;
        energyBar.SetHealth(m_Energy);
        m_collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis(controlSet + " Horizontal"); // player1: A, D; player2: left, right
        float v = Input.GetAxis(controlSet + " Vertical");   // player1: W, S; player2: up, down

        Vector2 pos = transform.position;

        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;

        transform.position = pos;

        if (prop)
        {
            if (name == "Player1" && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.G)))
            {
                prop.enabled = true;
            }
            else if (name == "Player2" && (Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.Period)))
            {
                prop.enabled = true;
            }
        }
    }
    
    public void ReceiveProp(PropPrototype prop)
    {
        this.prop = prop;
        prop.owner = this;
    }
    
    public void RemoveProp()
    {
        this.prop = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // No damage when colliding with walls.
        if (collision.gameObject.CompareTag("Walls") || isInvincible)
        {
            return;
        }

        Color bulletColor = collision.transform.GetChild(0).GetComponent<Image>().color;
        Color playerColor = gameObject.GetComponent<SpriteRenderer>().color;

        FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
        if (bulletColor == playerColor) {
            m_Energy += 1;
            energyBar.SetHealth(m_Energy);
            printer.SetFloatingValue(+1);   // gain = positive value
        } else {
            m_Hp -= 1;
            healthBar.SetHealth(m_Hp);
            printer.SetFloatingValue(-1);
        }

        // Game over condition
        if (m_Hp <= 0)
        {
            string winner = "Player1";
            if (name == "Player1")
            {
                winner = "Player2";
            }
            VersusGameManager.winner = winner;
            SceneManager.LoadScene("VersusGameOver");
        }
        
        if (m_Energy >= 5)
        {
            if (name == "Player1")
            {
                manager.propPanel1.gameObject.SetActive(true);
            }
            else
            {
                manager.propPanel2.gameObject.SetActive(true);
            }
        }

    }
} // class
