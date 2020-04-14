using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JokeLoader : MonoBehaviour
{

    public Text joke;
    public Text pucnhline;

    // Start is called before the first frame update
    public void Start()
    {

        TextAsset jokes_asset = (TextAsset)Resources.Load("dad_jokes"); // Loads a text file with jokes
        string[] jokes = jokes_asset.text.Split('\n'); // Splits the asset to jokes

        string[] random_joke = jokes[Random.Range(0,jokes.Length-1)].Split(';'); // Every joke is split in content and punchline with ';'
        joke.text = random_joke[0];
        pucnhline.text = random_joke[1];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
