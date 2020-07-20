using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EndScene : MonoBehaviour
{
    public GameObject minuteHand;
    public GameObject playerDial;
    public GameObject minuteHandPrefab;
    public PlayerController playerController;
    public GameObject gameManager;
    public GameObject playerSecondHand;
    public AudioSource tickSound;
    public AudioSource specialTickSound;

    public Image blackPanel;
    public Image whitePanel;
    public GameObject endText;

    private new Camera camera;
    void Start()
    {
        camera = Camera.main;
        StartCoroutine(MinuteHandGlitch(minuteHand.transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PickUp()
    {
        yield return FadeInImage(whitePanel, 0.1f);
        
        FindObjectOfType<Soundtrack>().GetComponent<AudioSource>().Stop();
        
        StartCoroutine(FadeOutImage(whitePanel, 10f));
        
        var minuteHandNew = Instantiate(minuteHandPrefab, playerDial.transform);
        playerController.transform.position = minuteHand.transform.position;
        minuteHand.SetActive(false);
        playerController.enabled = false;
        gameManager.SetActive(false);
        //Camera.main.orthographicSize /= 3;
        StartCoroutine(ZoomIn());


        tickSound.pitch = 1f;
        tickSound.volume = 0.7f;
        
        while (Math.Abs(playerSecondHand.transform.rotation.z) > 0.05f)
        {
            playerSecondHand.transform.Rotate(Vector3.back, 6);
            tickSound.Play();
            yield return new WaitForSeconds(1f);
        }
        playerSecondHand.transform.Rotate(Vector3.back, 6);
        specialTickSound.Play();
        minuteHandNew.transform.Rotate(Vector3.back, 6);
        yield return new WaitForSeconds(0.93f);
        yield return FadeInImage(blackPanel, 0.07f);

        for (var i = 0; i < 10; i++)
        {
            tickSound.volume /= 2;
            tickSound.Play();
            yield return new WaitForSeconds(1f);
        }
        
        endText.SetActive(true);

    }
    
    IEnumerator MinuteHandGlitch(Transform transform)
    {
        tickSound.volume = 0.4f;
        while (minuteHand.activeSelf)
        {
            tickSound.pitch = Random.Range(0.3f, 0.7f);
            tickSound.Play();
            
            transform.Rotate(Vector3.back, 10f);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
            transform.Rotate(Vector3.back, -10f);
            
            tickSound.pitch = Random.Range(1.25f, 2f);
            tickSound.Play();
            
            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(PickUp());
    }
    
    IEnumerator FadeInImage(Graphic image, float duration, float targetAlpha = 0.999f)
    {
        if (duration <= 0 || image == null) throw new ArgumentException();

        while (image.color.a <= targetAlpha)
        {
            var color = image.color;
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / duration);
            image.color = color;
            yield return null;
        }
    }
    
    
    
    IEnumerator FadeOutImage(Graphic image, float duration, float targetAlpha = 0.01f)
    {
        if (duration <= 0 || image == null) throw new ArgumentException();

        while (image.color.a >= targetAlpha)
        {
            var color = image.color;
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / duration);
            image.color = color;
            yield return null;
        }
    }

    private IEnumerator ZoomIn()
    {
        while (camera.orthographicSize >= 5/3f)
        {
            camera.orthographicSize -= 0.1f * Time.deltaTime;
            yield return null;
        }
    }
}
