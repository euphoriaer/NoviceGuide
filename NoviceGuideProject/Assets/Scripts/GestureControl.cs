using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RectTransform))]
public class GestureControl : MonoBehaviour, IPointerClickHandler ,IPointerDownHandler,IPointerUpHandler
{
    private RectTransform tarRect;
    // Start is called before the first frame update
    void Start()
    {
        tarRect = this.GetComponent<RectTransform>();
        this.GetComponent<Button>().onClick.AddListener((() =>
        {
            Debug.Log("Mask被点击");
        }));
    }
    
    //点击渗透
    public void OnPointerClick(PointerEventData eventData)
    {
        PassEvent(eventData,ExecuteEvents.pointerClickHandler);
    }
    //按下渗透
    public void OnPointerDown(PointerEventData eventData)
    {
        PassEvent(eventData,ExecuteEvents.pointerDownHandler);
    }

    //抬起渗透
    public void OnPointerUp(PointerEventData eventData)
    {
        PassEvent(eventData,ExecuteEvents.pointerUpHandler);
    }
    public void  PassEvent<T>(PointerEventData data,ExecuteEvents.EventFunction<T> function)
        where T : IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results); 
        GameObject current = data.pointerCurrentRaycast.gameObject ;
        for(int i =0; i< results.Count&&i< 5;i++)//渗透不能太多，防卡死
        {
            if(current!= results[i].gameObject)
            {
                ExecuteEvents.Execute(results[i].gameObject, data,function);
                //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
            }
        }
    }
}
