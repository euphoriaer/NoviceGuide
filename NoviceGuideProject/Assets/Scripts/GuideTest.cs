using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideTest : MonoBehaviour
{
    // Start is called before the first frame update
    public string path;
    void Start()
    {
        Gesture testGesture = new Gesture(path,(() =>
        {
            Debug.Log("完成点击引导");
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
