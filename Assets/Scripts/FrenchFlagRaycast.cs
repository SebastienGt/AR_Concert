using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 using UnityEngine.EventSystems;
 using UnityEngine.UI;
 
public class FrenchFlagRaycast : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Vector3 move;
    Vector3 initialpos;
    Vector3 distance;
    float speed=0.2f;
    void Start()
    {
        move = Vector3.zero;
    
    }
    #region IBeginDragHandler implementation
     public void OnBeginDrag (PointerEventData eventData)
     {
         initialpos = transform.position;
         move = Vector3.zero;
 
     }
     #endregion
 
     #region IDragHandler implementation
 
     public void OnDrag (PointerEventData eventData)
     {
         if (Input.mousePosition.x < Screen.width && Input.mousePosition.y < Screen.height) {
             distance = Input.mousePosition - initialpos;
             //distance = Vector3.ClampMagnitude (distance, 45 * Screen.width / 708);
             transform.position = initialpos + distance;
             Vector3 move1 = distance.normalized;
             move.x = move1.x * speed;
             move.z = move1.y * speed;
             int layerMask = 1 << 7;
            // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed at
            RaycastHit hit;

            var direction = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y)).direction;
            if (Physics.Raycast(Input.mousePosition, direction, out hit, layerMask)) {
                Debug.DrawRay(Input.mousePosition, direction * hit.distance, Color.yellow);
                Debug.Log("Hit = " + hit);
            } else {
                Debug.DrawRay(Input.mousePosition, direction * 1000, Color.white);
                Debug.Log("Did not hit :(");
            }
         }
                 
 
     }
 
     #endregion
 
     #region IEndDragHandler implementation
 
     public void OnEndDrag (PointerEventData eventData)
     {
         move = Vector3.zero;
         transform.position = initialpos;
     }

     #endregion

    void FixedUpdate()
    {

        
    }
}
