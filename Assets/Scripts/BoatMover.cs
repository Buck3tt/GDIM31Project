using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMover : MonoBehaviour
{
    private Vector3 startposition;
    private Vector3 endposition;
    private float speed;
    private Obstacle ob;

    private float startTime;
    private float distance;

    [SerializeField]
    private float dropdelay = 2f;

    bool atPos = false;

    private GameObject parrent;
    public void SetValues(Vector3 endpos, float speed, Obstacle ob, GameObject parrent)
    {
        startposition = transform.position;
        endposition = endpos;
        this.speed = speed * GameStateManager.msMult;
        this.ob = ob;
        startTime = Time.time;
        distance = Vector3.Distance(transform.position, endpos);
        this.parrent = parrent;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != endposition) { 
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfDistance = distCovered / distance;

            transform.position = Vector3.Lerp(startposition, endposition, fractionOfDistance);
        }
        else if (!atPos)
        {
            Vector3 dropoffset = new Vector3(transform.position.x-transform.localScale.x/2f, transform.position.y);
            Instantiate(ob, dropoffset, Quaternion.identity, parrent.transform);
            StartCoroutine(delayContinue());
        }
    }

    IEnumerator delayContinue()
    {
        atPos = true;
        yield return new WaitForSeconds(dropdelay);
        startposition = transform.position;
        endposition = new Vector3(13.5f, transform.position.y);
        distance = Vector3.Distance(transform.position, endposition);
        startTime = Time.time;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Despawn"))
        {
            Destroy(gameObject);
        }
    }
}
