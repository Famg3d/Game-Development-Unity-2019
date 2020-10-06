using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float _speed = 10f;
    private float _speedMultiplies = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 10;
    private SpawnManager _spawnManager;

    private bool _isTripleShotActivate = false;
    private bool _isSpeedBoostActive = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
         //personaje se mueve 1 metro
        //transform.Translate(new Vector3(1, 0, 0));
        //personaje se mueve 1 metro por segundo
        //transform.Translate(Vector3.right * Time.deltaTime);
        //personaje se mueve por parametro fijo en script
        //transform.Translate(Vector3.right * speed * Time.deltaTime);  

        //funcion que da al movimiento del personaje.
        movimiento(_speed);
        // funcion que limita el mapa a 9x-9x6x-4 con teleport
        limitadorDeMapa();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    public void Damage(){
        _lives--;
            if (_lives < 1)
            {
                Destroy(this.gameObject);
            }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if(_isTripleShotActivate == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
        else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
        /* //METODO 1
            Vector3 offset = new Vector3(0, 0.8f, 0);
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);

        //METODO 2
            //Instantiate(_laserPrefab, Disparo.transform.position, Quaternion.identity);
        */
    }
    void limitadorDeMapa()
    {
        if (transform.position.y >= 4.8f)
            transform.position = new Vector3(transform.position.x, 4.87f, 0);
        else if (transform.position.y <= -3f)
            transform.position = new Vector3(transform.position.x, -3f, 0);
        else if (transform.position.x >= 9f)
            transform.position = new Vector3(-9f, transform.position.y, 0);
        else if (transform.position.x <= -9f)
            transform.position = new Vector3(9f, transform.position.y, 0);

        if (transform.position.x <= -9f && transform.position.y <= -4f)
            transform.position = new Vector3(9f, -4f, 0); 
        else if (transform.position.x >= 9f && transform.position.y <= -4f)
            transform.position = new Vector3(-9f, -4f, 0); 

        if (transform.position.x <= -9f && transform.position.y >= 6f)
            transform.position = new Vector3(9f, 6f, 0); 
        else if (transform.position.x >= 9f && transform.position.y >= 6f)
            transform.position = new Vector3(-9f, 6f, 0);
    }
    public void movimiento(float _speed)
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
    }
    public void TripleShotActivate()
    {
        _isTripleShotActivate = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActivate = false;
    }
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplies;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplies;
    }
}
