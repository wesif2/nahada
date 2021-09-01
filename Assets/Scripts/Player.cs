using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float _horizontalInput;
    float _verticalInput;

    [SerializeField]
    float _speed;

    int taawezaCount ;

    [SerializeField]
    GameObject eye;

    [SerializeField]
    AudioClip heartSound;

    [SerializeField]
    AudioClip taawezaSound;

    [SerializeField]
    AudioClip blackMagicSound;

    //[SerializeField]
    //AudioClip eye;
    UiManager uiManager;

    public static bool gameOver;

    float maxplayerHealth ;
    float currentplayerHealth ;

   
    [SerializeField]
    Image healthImage;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        gameOver = false;
        maxplayerHealth = 100;
        currentplayerHealth = 100 ;
        healthImage = GameObject.FindGameObjectWithTag("Health Image").GetComponent<Image>();
        taawezaCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
        InstantiateEyes();

        healthImage.fillAmount = currentplayerHealth / maxplayerHealth;
        if (currentplayerHealth == 0 || transform.position.x < -21 || transform.position.x > 21.5)
        {
            Die();
        }

    }
    void Move()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _speed * _horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * _verticalInput * Time.deltaTime);
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == ("Ta3weza"))
        {
          
            Destroy(other.gameObject);

            if (taawezaCount < 2)
            {
                taawezaCount++;
            }

            uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(taawezaSound, transform.position);
        }

        if (other.gameObject.tag == ("Black Magic"))
        {

            Destroy(other.gameObject);

            currentplayerHealth -= 20;

            Debug.Log(currentplayerHealth);

            AudioSource.PlayClipAtPoint(blackMagicSound, transform.position);
        }

        if (other.gameObject.tag == ("Enemy"))
        {

            Die();



        }

        if (other.gameObject.tag == ("Heart"))
        {
            Destroy(other.gameObject);
            currentplayerHealth += 20;
            if (currentplayerHealth >= 100)
            {
                 currentplayerHealth = 100;
            }
            AudioSource.PlayClipAtPoint(heartSound, transform.position);

        }
    }

    void InstantiateEyes ()
    {
        if (taawezaCount == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(eye, this.transform.position, Quaternion.identity);
            taawezaCount -= 2;
        }
    }

    void Die()
    {
        
            
            gameOver = true;
            uiManager.ShowGameOverPanel();


    }
}
