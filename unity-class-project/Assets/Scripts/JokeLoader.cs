using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class joke_class {
    public string Joke;

    public joke_class(string Joke) {
        this.Joke = Joke;
    }
    public string get_joke() {
        return this.Joke;
    }

    public void format_joke() {
        Joke.Trim('\\');
    }
}

public class JokeLoader : MonoBehaviour
{

    public Text joke;

    public IEnumerator get_random_joke()
    {
        //print("hi");
        string joke_url_template = "http://127.0.0.1:5000/jokes/{0}";
        int num = Random.Range(0, 97);
        string joke_url = string.Format(joke_url_template, num);
        //print(joke_url);
        UnityWebRequest www = UnityWebRequest.Get(joke_url);

        yield return www.SendWebRequest();
        //Debug.LogWarning(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            //Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            joke_class requested_joke = JsonUtility.FromJson<joke_class>(www.downloadHandler.text);
            print(www.downloadHandler.text);
            requested_joke.format_joke();
            joke.text = requested_joke.Joke;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(get_random_joke());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
