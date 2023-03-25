using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ScrollViewHandler : MonoBehaviour, IPointerDownHandler {

    [SerializeField] ScrollRect scrollRect;
    
    [SerializeField] private List<Transform> scrollViewChilderen;

    public float sizeDiff = 0.5f;
    private float scrollRectPosition = 0f;

    public int currentIndex = 0;
    public float localDiff = 0f;
    public float percentProgress = 0f;
    public float percentProgressBetweenTwoConsecutiveElements = 0f;
    public Vector2 _previousPosition;


    void Start() {
        _previousPosition = scrollRect.content.anchoredPosition;
        
        scrollViewChilderen = new List<Transform>();

        foreach(Transform t in scrollRect.content) {
            scrollViewChilderen.Add(t);
        }

        localDiff = (float) 1 / (scrollViewChilderen.Count - 1);
    }
    
    public void HandleScroll() {
        
        percentProgress = ((scrollRect.horizontalScrollbar.value))/localDiff;
        percentProgressBetweenTwoConsecutiveElements  = percentProgress %15f;

        currentIndex = (int) percentProgress;

        if (scrollRect.content.anchoredPosition.x > _previousPosition.x)
        {
            Transform currentElement = scrollViewChilderen[currentIndex+1];

            if(currentElement.localScale.x > 0.5f) {
                float newScale = currentElement.localScale.x - (localDiff * percentProgressBetweenTwoConsecutiveElements);
                if(newScale < 0.5f) newScale = 0.5f; 
                currentElement.localScale = new Vector3(newScale, newScale, newScale);
            }

            if(currentIndex >= 0) {
                Transform previousElement = scrollViewChilderen[currentIndex];

                if(previousElement.localScale.x <= 1f) {
                    float newScale = previousElement.localScale.x + (localDiff * percentProgressBetweenTwoConsecutiveElements);
                    if(newScale > 1.0f) newScale = 1.0f; 
                    previousElement.localScale = new Vector3(newScale, newScale, newScale);;
                }

            }

            float epsilon = 0.01f;

            if (Mathf.Abs(scrollRect.horizontalScrollbar.value - ((currentIndex)*localDiff)) < epsilon) {
                scrollRect.horizontal = false;
            }
            
        }
        else if (scrollRect.content.anchoredPosition.x < _previousPosition.x)
        {
            
            Transform currentElement = scrollViewChilderen[currentIndex];

            if(currentElement.localScale.x > 0.5f) {
                float newScale = currentElement.localScale.x - (localDiff * percentProgressBetweenTwoConsecutiveElements);
                if(newScale < 0.5f) newScale = 0.5f; 
                currentElement.localScale = new Vector3(newScale, newScale, newScale);
            }

            if(currentIndex + 1 < scrollViewChilderen.Count) {
                Transform nextElement = scrollViewChilderen[currentIndex + 1];
                
                if(nextElement.localScale.x <= 1f) {
                    float newScale = nextElement.localScale.x + (localDiff * percentProgressBetweenTwoConsecutiveElements);
                    if(newScale > 1.0f) newScale = 1.0f; 
                    nextElement.localScale = new Vector3(newScale, newScale, newScale);;
                }

            }
            
            float epsilon = 0.01f;
            if (Mathf.Abs(scrollRect.horizontalScrollbar.value - ((currentIndex+1)*localDiff)) < epsilon) {
                scrollRect.horizontal = false;
            }
            
        }
        _previousPosition = scrollRect.content.anchoredPosition;
    }

    private IEnumerator DisableHorizontalScrollBarTemporarily()
    {
        scrollRect.horizontal = false;
        yield return new WaitForSeconds(0.2f);
        scrollRect.horizontal = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        scrollRect.horizontal = true;
    }
}
