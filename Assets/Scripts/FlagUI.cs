using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class FlagUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Vector3 move;
    Vector3 initialpos;
    Vector3 cubeinitialpos;
    Vector3 distance;
    float speed = 3.0f;
    public Transform flagCube;
    public Musician musician;

    void Start()
    {
        move = Vector3.zero;
    }

    #region IBeginDragHandler implementation
    public void OnBeginDrag (PointerEventData eventData)
    {
        initialpos = transform.position;
        move = Vector3.zero; 
        if (!flagCube.gameObject.activeSelf)
        {
            flagCube.gameObject.SetActive(true);
            flagCube.position = new Vector3(musician.transform.position.x + 2, musician.transform.position.y, musician.transform.position.z + 2);
        }
    }
    #endregion
 
    #region IDragHandler implementation
    public void OnDrag (PointerEventData eventData)
    {
        if (Input.mousePosition.x < Screen.width && Input.mousePosition.y < Screen.height) {
            distance = Input.mousePosition - initialpos;
            distance = Vector3.ClampMagnitude(distance, Screen.width / 14);
            transform.position = initialpos + distance;
            Vector3 distDirection = distance.normalized;
            move.x = distDirection.x * speed;
            move.z = distDirection.y * speed;
            flagCube.position = new Vector3(musician.transform.position.x + move.x, musician.transform.position.y, musician.transform.position.z + move.z);            
        }

        Debug.Log("FlagCube: (" + flagCube.position.x + ", " + flagCube.position.y + ", " + flagCube.position.z + ")");
        Debug.Log("Musician: (" + musician.transform.position.x + ", " + musician.transform.position.y + ", " + musician.transform.position.z + ")");
    }
    #endregion
 
     #region IEndDragHandler implementation
 
    public void OnEndDrag (PointerEventData eventData)
    {
        move = Vector3.zero;
        transform.position = initialpos;
    }

    #endregion
}
