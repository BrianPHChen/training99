using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_Reminder : MonoBehaviour
{
    public GameObject reminderPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            reminderPrefab.SetActive(true);
            StartCoroutine(TimeDelay());
        }
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(5);
        reminderPrefab.SetActive(false);
    }
}