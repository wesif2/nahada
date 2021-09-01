using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    AudioClip transformation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * 4 *Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Ta3weza"))
        {
            Destroy(other.gameObject);
            StartCoroutine(startTransformation());

           
        }
    }

    IEnumerator startTransformation()
    {
        AudioSource.PlayClipAtPoint(transformation, transform.position);
        yield return new WaitForSeconds(.3f);
        Instantiate(enemy, transform.position, Quaternion.identity);
       

        Destroy(this.gameObject);
    }
}
