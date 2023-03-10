using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private SceneSwitcher sceneSw;
    private PlayAudio playAudio;
    private Vector3 startPoint;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        sceneSw = GetComponent<SceneSwitcher>();
        playAudio = GetComponent<PlayAudio>();
        startPoint = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (transform.position.y <= -15)
        {
            transform.position = startPoint;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
            playAudio.DropAudio();
            Invoke("OpenSceneWithDelay", 5f);
        }
    }

    void OpenSceneWithDelay()
    {
        sceneSw.OpenScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            audioSource.Play();
        }

    }

}
