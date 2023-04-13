using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{

    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    public Ray ray;
    public RaycastHit hit;
    public int toque;

    private void Awake()
    {
        toque = 1;
    }


    private void Update()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

            ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, touchInputMask))
            {
                GameObject recipient = hit.transform.gameObject;
                touchList.Add(recipient);

                if (Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButton(0))
                {
                    recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                }

            }

            foreach (GameObject g in touchesOld)
            {
                if (!touchList.Contains(g))
                {
                    g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }

        }
#endif
        
        if (toque >= 2)
        {
            toque = 2;
        }

        

        if (Input.touchCount == toque)
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {

                ray = GetComponent<Camera>().ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }

                foreach (GameObject g in touchesOld)
                {
                    if (!touchList.Contains(g))
                    {
                        g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }

                

            }

            /*if (mapa)
            {
                if (Input.touchCount == 1)
                {
                    Touch currentTouch = Input.GetTouch(0);

                    if (currentTouch.phase == TouchPhase.Began)
                    {
                        this.worldStartPoint = this.getWorldPoint(currentTouch.position);
                    }

                    if (currentTouch.phase == TouchPhase.Moved)
                    {
                        Vector2 worldDelta = this.getWorldPoint(currentTouch.position) - this.worldStartPoint;

                        Camera.main.transform.Translate(
                            -worldDelta.x,
                            -worldDelta.y,
                            0
                        );
                    }
                }
                if (gameObject.transform.position.x < -69f)
                {
                    iTween.MoveTo(gameObject, iTween.Hash("x", -69, "time", 0.5f));
                }
                if (gameObject.transform.position.x > 56f)
                {
                    iTween.MoveTo(gameObject, iTween.Hash("x", 56, "time", 0.5f));
                }
                if (gameObject.transform.position.y < -14f)
                {
                    iTween.MoveTo(gameObject, iTween.Hash("y", -14, "time", 0.5f));
                }
                if (gameObject.transform.position.y > 81f)
                {
                    iTween.MoveTo(gameObject, iTween.Hash("y", 81, "time", 0.5f));
                }
            }*/
        }
    }

    private Vector2 getWorldPoint(Vector2 screenPoint)
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit);
        return hit.point;
    }

}
