using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_ActiveSkill0 : MonoBehaviour
{
    // -----------------------------------------------------------------------------
    // Skill0
    [Header("Skill0 Change color")]
    [SerializeField] private GameObject button;
    [SerializeField] private Image skill0_mask;
    [SerializeField] private TextMeshProUGUI skill0_text;

    private bool enableColorChange = true;

    // Count the cooldown time
    private bool isS0Cooldown = false;
    public float s0CooldownTime = 3.0f;
    private float s0Timer = 0f;


    // Get next color
    private GameObject player = null;
    private Color playerColor;
    private Color[] colors = { new Color32(153,0,0,255), new Color32(244,187,15,255), new Color32(39,116,174,255)};   // Red, Yellow, Blue


    void GetNextColor()
    {
        // detecting playerColor
        playerColor = player.GetComponent<SpriteRenderer>().color;

        // Debug.Log("colors = " + colors);
        
        // Player color format: Red = RGBA(0.635, 0.204, 0.098, 1.000)
        // colors = {Red, Yellow, Blue}
        
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
            nextColor = Color.white;
        } 
        button.GetComponent<Image>().color = nextColor;
    }


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
        skill0_mask.fillAmount = 0; 

        // Get cooldown time
        // s0CooldownTiime = player.GetComponent<LV_PlayerMovement>().Get();

    }

    // Update is called once per frame
    void Update()
    {
        Skill0();
    }

    // Skill0: color changing
    void Skill0()
    {
        // Chech whether color-changing ability is enable/disable
        enableColorChange = player.GetComponent<LV_PlayerMovement>().GetColorChanging();
        Debug.Log("enableColorChange =" + enableColorChange);
        if (enableColorChange == false)
        {
            return;
        }
        
        GetNextColor();
        if (Input.GetKey(KeyCode.Space) && isS0Cooldown == false)
        {
            // Skill0 used, need a cooldown 
            isS0Cooldown = true;
            skill0_mask.fillAmount = 1;
            skill0_text.text = s0CooldownTime.ToString();
            s0Timer = s0CooldownTime;   // Reset Timer 
        }

        if (isS0Cooldown)
        {
            // start to count down
            s0Timer -= Time.deltaTime;

            skill0_mask.fillAmount -= Time.deltaTime / s0CooldownTime;

            skill0_text.text = s0Timer.ToString("F1");  // show 1 Decimal Point 
            // skill0_text.text = Mathf.RoundToInt(s0Timer).ToString(); // show integer only

            if ( skill0_mask.fillAmount <= 0)
            {
                // Can use skill 0 again
                skill0_mask.fillAmount = 0;
                skill0_text.text = " ";
                isS0Cooldown = false; 
                s0Timer = s0CooldownTime;   // Reset Timer 
            } 
        }

    }

}
