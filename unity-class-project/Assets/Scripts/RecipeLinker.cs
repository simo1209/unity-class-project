using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class TitleList {
    string[] titles;
}
public class RecipeClass {
    public string Recipe;
    public string Title;

    public RecipeClass(string Recipe, string Title) {
        this.Recipe = Recipe;
        this.Title = Title;
    }
}

public class RecipeLoader : MonoBehaviour
{
    public GameObject canvas;
    public GameObject button;
    public GameObject text;

    void Start()
    {
        RequestTitlles();
    }

    IEnumerator RequestTitlles() { 
    string titlesURL = "http://127.0.0.1:5000/titles";
    UnityWebRequest www = UnityWebRequest.Get(titlesURL);

    yield return www.SendWebRequest();
        //Debug.LogWarning(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            TitleList requestedTitles = JsonUtility.FromJson<TitleList>(www.downloadHandler.text);
            
        }
    }
    /*
    void List_recipes()
    {
        int i;
        GameObject newButton;
        for (i = 0; i < 10; i++)
        {
            newButton = Instantiate(button) as GameObject;
            newButton.name = "Title" + i;
            newButton.transform.SetParent(canvas.transform, true);
            newButton.transform.SetPositionAndRotation(newButton.transform.position + new Vector3(0, -i * 85, 0), newButton.transform.rotation);
            newButton.GetComponentInChildren<Text>().text = recipes[i].Split(';')[0];
            newButton.GetComponentInChildren<Button>().onClick.AddListener(TaskOnClick);
        }
    }

    void TaskOnClick()
    {
        DestroyAll("TitleButton");
        Create_Back_Button();
        int title = int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring(5));
        TextAsset recipe_asset = (TextAsset)Resources.Load("CocktailRecipes");
        string[] recipes = recipe_asset.text.Split('#');
        GameObject newText = Instantiate(text) as GameObject;
        newText.transform.SetParent(canvas.transform, true);
        newText.GetComponentInChildren<Text>().text= recipes[title].Split(';')[1];
    }


    void DestroyAll(string tag)
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == "Prefab") { continue; }
            Destroy(buttons[i]);
        }
    }

    void Back_On_click(){
        DestroyAll("TitleButton");
        List_recipes();
        DestroyAll("Recipe");
    }

    void Create_Back_Button() {
        GameObject newButton = Instantiate(button) as GameObject;
        newButton.name = "Back_button";
        newButton.transform.SetParent(canvas.transform, true);
        newButton.transform.SetPositionAndRotation(newButton.transform.position + new Vector3(200, 230, 0), newButton.transform.rotation);
        newButton.GetComponentInChildren<Text>().text = "Back";
        newButton.GetComponentInChildren<Button>().onClick.AddListener(Back_On_click);
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}

