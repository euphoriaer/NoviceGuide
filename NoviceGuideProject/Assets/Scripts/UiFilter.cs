using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RectTransform))]
public class UiFilter : MonoBehaviour, ICanvasRaycastFilter, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool NatureDestory = false;
    public bool Osmosis = true;
    public bool Strong = false;
    public bool isTransOsmosis = true;//是转发型渗透

    private bool _onceEnter = true;

    private Camera _uiCamera;

    private RectTransform tarRect;

    public event Action ClickEvent;

    public event Action InterruptEvent;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        //是否强制引导指定位置
        if (isTransOsmosis)
        {
            return true;
        }
        if (_uiCamera == null)
        {
            return true;
        }

        if (tarRect == null)
        {
            return true;
        }

        bool isContain = RectTransformUtility.RectangleContainsScreenPoint(tarRect, sp, _uiCamera);
        if (isContain)
        {
            if (Input.GetMouseButtonDown(0) && _onceEnter)
            {
                _onceEnter = false;
                NatureDestory = true;

                //按下事件，销毁遮罩自身，需要避免多次销毁
                GameObject.Destroy(this.gameObject, 0.1f);
            }
            return false;//渗透
        }

        return true;//不渗透
    }

    private void OnDestroy()
    {
        if (ClickEvent != null)
        {
            ClickEvent();
        }
        if (!NatureDestory)
        {
            //非点击自然销毁，调用中断
            Debug.Log("非自然中断");
            InterruptEvent();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        tarRect = this.GetComponent<RectTransform>();
        this.GetComponent<ParticleSystem>().Play();

        var cameras = GameObject.FindObjectsOfType<Camera>();
        _uiCamera = cameras.Where(p => p.name == "UICamera").First();
        //是否强制引导

        if (isTransOsmosis)
        {
            var button= this.GetComponent<Button>();
            button.onClick.AddListener(ButtonClick);
        }
    }

    private void ButtonClick()
    {
        if (_onceEnter)
        {
            _onceEnter = false;
            NatureDestory = true;

            //按下事件，销毁遮罩自身，需要避免多次销毁
            GameObject.Destroy(this.gameObject, 0.1f);
        }
    }

    //点击渗透
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Osmosis)
        {
            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
        }
    }

    //按下渗透
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Osmosis)
        {
            PassEvent(eventData, ExecuteEvents.pointerDownHandler);
        }
    }

    //抬起渗透
    public void OnPointerUp(PointerEventData eventData)
    {
        if (Osmosis)
        {
            PassEvent(eventData, ExecuteEvents.pointerUpHandler);
        }
    }

    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
        where T : IEventSystemHandler
    {
        var parent = transform.parent.gameObject;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count && i < 5; i++)//渗透不能太多，防卡死
        {
            if (current != results[i].gameObject && parent != results[i].gameObject)
            {
                ExecuteEvents.Execute(results[i].gameObject, data, function);
                //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
                break;
            }
        }

        ExecuteEvents.Execute(parent, data, function);
    }
}