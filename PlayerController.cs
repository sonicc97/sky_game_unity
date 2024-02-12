using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private float speed = 3.0f;
    private GameObject focalPoint;
    private float powerStrength = 15.0f;
    public bool powerUp = false;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        //A method that adds force to RB... ( focalPoint.tran.... It represents the direction in which the force will be applied)
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (playerRB.transform.position.y <= -10.0f)
        {
            SceneManager.LoadScene("Start Game");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
            Destroy(other.gameObject);
        powerUp = true;
        StartCoroutine(PowerUpCountDownRoutine());
        powerupIndicator.gameObject.SetActive(true);
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    //Used to check if a collision has occurred with an object that has the tag "Enemy" and if the powerUp variable is set to true
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Enemy") && powerUp)
        {
            //Used for access to apply force or change speed of movement
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            
            Debug.Log("Collider with:" + collision.gameObject.name + "With set up:" + powerUp);
            enemyRigidbody.AddForce(awayFromPlayer * powerStrength, ForceMode.Impulse);
        }
    }
}
