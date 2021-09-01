using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyItSelf());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator DestroyItSelf()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Tile Map"))
        {
            Destroy(this.gameObject);
        }
    }
}
