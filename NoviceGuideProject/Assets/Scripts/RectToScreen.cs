using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectToScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //获取当前屏幕宽高，使当前分辨率等比达到最大
        float with = Screen.width; //2338
        float height = Screen.height; //108   float curRate = with / height;
        var rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(with, height);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}