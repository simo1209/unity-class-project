using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HeadsUp : MonoBehaviour
{
    public Camera cam;
    public Text word;
    public Text timer;
    private int score;
    private Gyroscope gyro;
    private bool loadWord = false;
    private int index = -1;

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
        LoadWord();
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

    void LoadWord()
    {
        // do something...

        string[] words = { "dog", "cat", "pikachu", "WoW", "Unity"};
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
    }

    IEnumerator ReloadWord()
    {
        
        yield return new WaitForSeconds(1f);
        cam.backgroundColor = Color.white;
        loadWord = true;
        LoadWord();
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
