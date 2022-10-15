using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_ActiveSkills : MonoBehaviour
{
    // Input game objects
    [Header("Skill1 Change color")]
    [SerializeField] private GameObject button;
    [SerializeField] private Image skill1_mask;
    [SerializeField] private TextMeshProUGUI skill1_text;

    // Count the cooldown time
    private bool isS1Cooldown = false;
    public float s1CooldownTime = 3.0f;
    private float s1Timer = 0f;

    // Get next color
    private GameObject player = null;
    private Color playerColor;
    private Color[] colors = { new Color32(162,52,25,255), new Color32(244,187,15,255), new Color32(47,55,91,255)};


    // Start is called before the first frame update
    void Start()
    {
        // Get player gameObject
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        GetNextColor();
        
        // Player can use skill, mask = 0
        skill1_mask.fillAmount = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        Skill1();
    }

    // Skill1 = color changing
    void Skill1()
    {
        GetNextColor();
        if (Input.GetKey(KeyCode.Space) && isS1Cooldown == false)
        {
            // Skill1 used, need a cooldown 
            isS1Cooldown = true;
            skill1_mask.fillAmount = 1;
            skill1_text.text = s1CooldownTime.ToString();
            s1Timer = s1CooldownTime;   // Reset Timer 
        }

        if (isS1Cooldown)
        {
            // start to count down
            s1Timer -= Time.deltaTime;

            skill1_mask.fillAmount -= Time.deltaTime / s1CooldownTime;
            skill1_text.text = Mathf.RoundToInt(s1Timer).ToString();
            Debug.Log("text = " + skill1_text.text);

            if ( skill1_mask.fillAmount <= 0)
            {
                // Can use skill 1 again
                skill1_mask.fillAmount = 0;
                skill1_text.text = " ";
                isS1Cooldown = false; 
                s1Timer = s1CooldownTime;   // Reset Timer 
            } 
        }

    }

    void GetNextColor()
    {

        // detecting playerColor
        playerColor = player.GetComponent<SpriteRenderer>().color;
        
        // Player color format: Red = RGBA(0.635, 0.204, 0.098, 1.000)
        // colors = {Red, Yellow, Blue}
        
        Debug.Log("playerColor = " + playerColor );

        Color nextColor = playerColor; 
        if (playerColor.Equals(colors[0]))  // Red
        {
            nextColor = colors[1];
        }
        else if (playerColor.Equals(colors[1])) // Yellow
        {
            
            nextColor = colors[2];
        }
        else if (playerColor.Equals(colors[2])) // Blue
        {
            nextColor = colors[0];
        }
        else
        {
            nextColor = Color.black;
        } 
        button.GetComponent<Image>().color = nextColor;
    }
}
