using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;

    [SerializeField]
    GameObject blackMagic;

    CapsuleCollider2D cc;

    float maxhealth;
    float currenthealth;
    SpriteRenderer sr;

    Rigidbody2D rb;

    Vector3 pos;

    float velocity;

    UiManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
        //player = GameObject.Find("Player");
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        StartCoroutine(SpawningBlackMagic());

        currenthealth = maxhealth = 100;
        sr = GetComponentInChildren<SpriteRenderer>();

        pos = transform.position;

        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);


        if (transform.rotation.x >0)
        {
            transform.Rotate(new Vector3(0,transform.rotation.y,transform.rotation.z));
        }
        if (transform.position.y < -7.73f )
        {
            transform.position = new Vector3(transform.position.x, -7.73f, transform.position.z);
        }

        transform.Find("Enemy Canvas").GetChild(1).GetComponent<Image>().fillAmount = currenthealth / maxhealth;
        Die();
        FlipSprite();
        uiManager.IncreaseScore();
    }

    IEnumerator SpawningBlackMagic()
    {
        while (true)
        {
            yield return new WaitForSeconds(8);
            Instantiate(blackMagic, this.transform.position, Quaternion.identity);

        }

    }

    IEnumerator DelayComponents()
    {
        cc.enabled = false;
        agent.enabled = false;
        yield return new WaitForSeconds(5);
        agent.enabled = true;
        cc.enabled = true;



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Eye"))
        {
            currenthealth -= 50;
            Destroy(other.gameObject);
            StartCoroutine(DelayComponents());

            Debug.Log(currenthealth);
        }
    }

    void Die ()
    {
        if (currenthealth == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FlipSprite()
    {
        velocity = (transform.position.x - pos.x) / Time.deltaTime;
        pos = transform.position;
        if (velocity > 0)
        {
           
            sr.flipX = true;
        }
        else if (velocity < 0)
        {
           
            sr.flipX = false;
        }
    }
}
