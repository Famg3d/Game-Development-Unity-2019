using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    public float _speed = 4.0f;
     [SerializeField]
    public GameObject player;
    // Start is called before the first frame update

    Player golpe;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5f )
        {
            float randomX = Random.Range(-8f , 8f);
            transform.position = new Vector3(randomX, 7 , 0 );
        }

    }   

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hit : " + other.transform.name);

        if (other.tag == "Player")
        {
            Player jugador = other.transform.GetComponent<Player>();

            if (jugador != null)
            {
                jugador.Damage();
            }

            Destroy(this.gameObject);

        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }


    }
}
