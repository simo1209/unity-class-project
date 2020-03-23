using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecipeLinker : MonoBehaviour
{
    public GameObject canvas;
    public GameObject button;
    public GameObject text;

    void Start()
    {
        TextAsset recipe_asset = (TextAsset)Resources.Load("CocktailRecipes");
        string[] recipes = recipe_asset.text.Split('#');
        for (int i=0; i < 10; i++) {
            GameObject newButton = Instantiate(button) as GameObject;
            newButton.name="Title" + i;
            newButton.transform.SetParent(canvas.transform, true);
            newButton.transform.SetPositionAndRotation(newButton.transform.position + new Vector3(0, -i*85, 0), newButton.transform.rotation);
            newButton.GetComponentInChildren<Text>().text = recipes[i].Split(';')[0];
            newButton.GetComponentInChildren<Button>().onClick.AddListener(TaskOnClick);
        }
    }

    void TaskOnClick()
    {
        //string title = transform.parent.name;\
        int title = int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring(5));
        DestroyAll();
        TextAsset recipe_asset = (TextAsset)Resources.Load("CocktailRecipes");
        string[] recipes = recipe_asset.text.Split('#');
        GameObject newText = Instantiate(text) as GameObject;
        newText.transform.SetParent(canvas.transform, true);
        newText.GetComponentInChildren<Text>().text= recipes[title].Split(';')[1];
    }


    void DestroyAll()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("TitleButton");
        for (int i = 0; i < buttons.Length; i++)
        {
            Destroy(buttons[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
