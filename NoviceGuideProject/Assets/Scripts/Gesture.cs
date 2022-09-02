using System;
using UnityEngine;
using UnityEngine.UI;

public class Gesture : MonoBehaviour
{
    private Texture _guideTexture; //todo 手势图标
    private RectTransform targetRect;
    private RectTransform gasterRect;
    private Action _finishClicked;

    public Gesture(string uiPath, Action finishClicked)
    {
        var canvas = GameObject.Find("Canvas");
        var btnTarget = canvas.transform.Find(uiPath);
        targetRect = btnTarget.GetComponent<RectTransform>();
        if (btnTarget == null)
        {
            Debug.Log("未找到对应手势目标");
        }
        else
        {
            Debug.Log("成功找到手势目标");
        }

        var gestureSource = Resources.Load<GameObject>("gesture");
        var gesture = GameObject.Instantiate(gestureSource, targetRect.position, targetRect.localRotation, targetRect);
        gasterRect = gesture.GetComponent<RectTransform>();
        gesture.GetComponent<Button>().onClick.AddListener((() =>
        {
            finishClicked();
        }));

        InitMask(targetRect);
        _finishClicked = finishClicked;
    }

    private void InitMask(RectTransform targetRect)
    {
        gasterRect.sizeDelta = targetRect.sizeDelta + new Vector2(25, 25);
    }
}