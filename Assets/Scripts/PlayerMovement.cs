using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{


    public float speed = 5f;

    Player player = new Player(100, 0, "Player");

    public TextMeshProUGUI m_HpText = null;
    public TextMeshProUGUI m_TimeText = null;
    public int m_Hp = 1;

    public Color[] colors = new Color[3];
    public float timeToChange = 5f;
    private float timeSinceChange = 0f;

    // UI show collectables (Collect 3 types of bullets)
    public TextMeshProUGUI UI_Collectable1 = null;
    public TextMeshProUGUI UI_Collectable2 = null;
    public TextMeshProUGUI UI_Collectable3 = null;
    private int[] collectables = new int[3]; // Record values for collectables. 

    // Prefab to show damage/collectable text
    public GameObject floatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        RefreshHpText();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // key: A, D, left, right
        float v = Input.GetAxis("Vertical"); // key: W, S, up, down

        Vector2 pos = transform.position;

        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;

        transform.position = pos;

        m_TimeText.text = "Time: " + Time.time.ToString("0.0");

        ChangeColor();
    }

    private void ChangeColor()
    {
        timeSinceChange += Time.deltaTime;

        if (timeSinceChange >= timeToChange)
        {
            Color newColor = colors[Random.Range(0, colors.Length)];

            while (newColor == gameObject.GetComponent<Image>().color)
            {
                newColor = colors[Random.Range(0, colors.Length)];
            }
            gameObject.GetComponent<Image>().color = newColor;
            timeSinceChange = 0f;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogError("OnTriggerEnter2D");
        Color bulletColor = collision.transform.GetChild(0).GetComponent<Image>().color;
        Color playerColor = gameObject.GetComponent<Image>().color;
        Debug.Log("Collision obj color: " + bulletColor); 
        Debug.Log("Collision player color: " + playerColor);
        
        int damage = -1; 
        // Different color, player take damage
        if ( playerColor != bulletColor)
        {
            m_Hp += damage;

            // Show damage text
            FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
            printer.SetFloatingValue(damage);   // damage = negative value
        }
        // Same color: player can collect the bullet as resources
        else
        {
            Sprite bulletType = collision.transform.GetChild(0).GetComponent<Image>().sprite;
            if (bulletType.name == "Knob") 
            {
                collectables[0] += 1;
            }
            else if (bulletType.name == "Triangle")  
            {
                collectables[1] += 1;
            }
            else if (bulletType.name == "UISprite") 
            {
                collectables[2] += 1;
            }

            // Show gain text
            FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
            printer.SetFloatingValue(+1);   // gain = positive value
        }
        
        RefreshHpText();
    }

    private void RefreshHpText()
    {
        m_HpText.text = "HP: " + m_Hp;
        UI_Collectable1.text = collectables[0].ToString();  // Circle	
        UI_Collectable2.text = collectables[1].ToString();  // Triangle	
        UI_Collectable3.text = collectables[2].ToString();  // Square	

    }
} // class
