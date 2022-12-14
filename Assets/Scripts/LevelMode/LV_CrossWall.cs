using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_CrossWall : MonoBehaviour
{
    // Wall color before & after
    //public Color originalColor = new Color32(0,24,100,255);
    //public Color newColor = new Color32(0, 24, 100, 100);
    //public Color[] wallColors;

    
    [Header("To PassThroughWallButton")]
    public float timer = 5;
    // private float timeRemaining = 5;
    // private bool timerIsRunning = false;
    // private bool isUsingSkill = false;
    
    public LV_ActiveSkill1 skill1;
    // public CrossPowerBar crossPowerBar;
    // public TextMeshProUGUI timerText = null;

    private GameObject[] crossWalls;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crossWalls = GameObject.FindGameObjectsWithTag("CrossWall");
        //foreach (GameObject crossWall in crossWalls)
        //{
        //    crossWall.GetComponent<SpriteRenderer>().color = wallColors[Random.Range(0, wallColors.Length)];
        //}
        // Debug.Log("walls: " + crossWalls.Length);

    }

    // Update is called once per frame
    void Update()
    {
        // Original with crossPowerBar 
        // if (!timerIsRunning && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && crossPowerBar.GetPowerAmount() > 0)

        // New with PassThroughWallButton 
        //if (!isUsingSkill 
        //    && (skill1.GetPowerAmount() > 0) 
        //    && (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)))
        //{
        //    Debug.Log("1 key was pressed.");
        //    isUsingSkill = true;
        //}

        //if (isUsingSkill)
        //{
        //    if (timeRemaining >= 0)
        //    {
        //        timeRemaining -= Time.deltaTime;
        //        // DisplayTime(timeRemaining);
        //        EnableCrossWalls();
        //    }
        //    else
        //    {
        //        isUsingSkill = false;
        //        timeRemaining = timer;
        //        // DisplayTime(0);
        //        DisableCrossWalls();
        //    }
        //}
        EnableCrossWalls();
    }

    void DisplayTime(float timeToDisplay)
    {
        //timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        // timerText.text = string.Format("Cross Wall Remaining: {0:00}:{1:00}", seconds, milliSeconds);
    }

    void EnableCrossWalls()
    {
        foreach(GameObject crossWall in crossWalls)
        {
            // Debug.Log("Player: " + player.GetComponent<SpriteRenderer>().color);


            //if (isSameColor(player, crossWall))
            //{
            //    //Debug.Log("can wall: " + crossWall.GetComponent<SpriteRenderer>().color);
            //    crossWall.GetComponent<Collider2D>().enabled = false;
            //    Color tmp = crossWall.GetComponent<SpriteRenderer>().color;
            //    tmp.a = 0.42f;
            //    crossWall.GetComponent<SpriteRenderer>().color = tmp;
            //}
            //else
            //{
            //    //Debug.Log("cannot wall: " + crossWall.GetComponent<SpriteRenderer>().color);
            //    crossWall.GetComponent<Collider2D>().enabled = true;
            //    Color tmp = crossWall.GetComponent<SpriteRenderer>().color;
            //    tmp.a = 1f;
            //    crossWall.GetComponent<SpriteRenderer>().color = tmp;
            //}
            Transform wallFill = crossWall.transform.Find("fill");
            if (isSameShape(player, crossWall))
            {
                crossWall.GetComponent<Collider2D>().enabled = false;
                //Color tmp = crossWall.GetComponent<SpriteRenderer>().color;
                //tmp.a = 0.42f;
                crossWall.GetComponent<SpriteRenderer>().color = getTransparentColor(crossWall);
                wallFill.GetComponent<SpriteRenderer>().color = getTransparentColor(wallFill.gameObject);
            }
            else
            {
                crossWall.GetComponent<Collider2D>().enabled = true;
                //Color tmp = crossWall.GetComponent<SpriteRenderer>().color;
                //tmp.a = 1f;
                //crossWall.GetComponent<SpriteRenderer>().color = tmp;
                crossWall.GetComponent<SpriteRenderer>().color = getNormalColor(crossWall);
                wallFill.GetComponent<SpriteRenderer>().color = getNormalColor(wallFill.gameObject);
            }

        }
    }

    void DisableCrossWalls()
    {
        foreach (GameObject crossWall in crossWalls)
        {
            crossWall.GetComponent<Collider2D>().enabled = true;
            Transform wallFill = crossWall.transform.Find("fill");
            crossWall.GetComponent<SpriteRenderer>().color = getNormalColor(crossWall);
            wallFill.GetComponent<SpriteRenderer>().color = getNormalColor(wallFill.gameObject);
            //crossWall.GetComponent<SpriteRenderer>().color = originalColor;
            //Color tmp = crossWall.GetComponent<SpriteRenderer>().color;
            //tmp.a = 1f;
            //crossWall.GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    bool isSameColor(GameObject a, GameObject b)
    {
        return (a.GetComponent<SpriteRenderer>().color.r == b.GetComponent<SpriteRenderer>().color.r)
            && (a.GetComponent<SpriteRenderer>().color.g == b.GetComponent<SpriteRenderer>().color.g)
            && (a.GetComponent<SpriteRenderer>().color.b == b.GetComponent<SpriteRenderer>().color.b);
    }

    Color getTransparentColor(GameObject gameObject)
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0.42f;
        return color;
    }

    Color getNormalColor(GameObject gameObject)
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        return color;
    }

    bool isSameShape(GameObject player, GameObject wall)
    {
        Transform wallFill = wall.transform.Find("fill");
        // Debug.Log("print wall sprite: " + wallFill.GetComponent<SpriteRenderer>().sprite);
        // Debug.Log("print player sprite: " + player.GetComponent<SpriteRenderer>().sprite);
        if (player.GetComponent<SpriteRenderer>().sprite == Resources.Load<Sprite>("Sprites/Square") && wallFill.GetComponent<SpriteRenderer>().sprite == Resources.Load<Sprite>("Sprites/Square_small"))
        {
            return true;
        }
        else if (player.GetComponent<SpriteRenderer>().sprite == wallFill.GetComponent<SpriteRenderer>().sprite)
        {
            return true;
        }
        return false;
    }

}