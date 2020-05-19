using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class HeadsUp : MonoBehaviour
{
    public Camera cam;
    public Text word;
    public Text timer;   
    public Image GuessImage;
    private int score;
    private Gyroscope gyro;
    private bool loadWord = false;
    private int index = -1;

    private string link = "http://localhost:5000/game_images/";

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gyro = Input.gyro;
        gyro.enabled = true;
        StartCoroutine(StartCountdown(5));
        enabled = false;
    }

    IEnumerator StartCountdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {

            // display something...
            word.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        StartCoroutine(Timer(59));
        enabled = true;
        loadWord = true;
        string wrd = LoadWord();
        StartCoroutine(setImage(link+wrd+".jpg"));
    
    }

    IEnumerator Timer(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {

            // display something...
            timer.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        EndGame();
    }

    private void EndGame()
    {
        word.text = string.Format("Final Score: {0}", score);
        enabled = false;
        timer.text = "";
    }

    string LoadWord()
    {
        // do something...

        string[] words = { "dog", "cat", "pikachu"};
        if(index == -1)
        {
            index = Random.Range(0, words.Length - 1);
        }
        else
        {
            int newIndex;
            do
            {
                newIndex = Random.Range(0, words.Length - 1);
            } while (newIndex == index);
            index = newIndex;
        }
        string w = words[index];
        word.text = w;
        return w;
    }

    IEnumerator setImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        GuessImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));

    }

    IEnumerator ReloadWord()
    {
        
        yield return new WaitForSeconds(1f);
        cam.backgroundColor = Color.white;
        loadWord = true;
        string word = LoadWord();
        StartCoroutine(setImage(link + word + ".jpg"));

        
    }

    // Update is called once per frame
    void Update()
    {
        if(loadWord && gyro.rotationRate.x < -3)
        {
            score++;
            cam.backgroundColor = Color.green;
            loadWord = false;
            StartCoroutine(ReloadWord());

        }

        if (loadWord && gyro.rotationRate.x > 3)
        {
            cam.backgroundColor = Color.red;
            loadWord = false;
            StartCoroutine(ReloadWord());
        }

    }


}
