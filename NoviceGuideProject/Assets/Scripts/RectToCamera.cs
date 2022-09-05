using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {

        #region 横屏适配
        //横屏适配
        float whRate = 2338.0f / 1080.0f;
        //获取当前屏幕宽高，使当前分辨率等比达到最大
        float with = Screen.width; //2338
        float height = Screen.height; //1080
        float curRate = with / height;
        var rect = this.GetComponent<RectTransform>();

        if (curRate < whRate)
        {
            rect.sizeDelta = new Vector2(with, (with / whRate));
        }
        else
        {
            rect.sizeDelta = new Vector2(height * whRate, height);
            //屏幕长
        }
        #endregion 横屏适配
    }

    // Update is called once per frame
    private void Update()
    {
    }
}