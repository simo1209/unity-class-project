using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C : MonoBehaviour
{
    public Text recepe;


    // Start is called before the first frame update
    void Start()
    {
        TextAsset recipe_asset = (TextAsset)Resources.Load("CocktailRecipes"); //Loads the recipes
        string[] recipes = recipe_asset.text.Split('#'); // Separates new lines


    }

    // Update is called once per frame
    void Update()
    {

    }
}
