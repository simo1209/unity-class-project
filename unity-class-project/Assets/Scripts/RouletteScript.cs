using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteScript : MonoBehaviour
{
    public float rotateSpeed;
    private bool rotate = false;

    public void OnClick()
    {
        rotateSpeed = 600f;
        rotate = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            rotateSpeed -= 2;
            if(rotateSpeed < 0)
            {
                rotate = false;
            }
        }
    }
}
