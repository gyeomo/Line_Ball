using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DrawJump2D : MonoBehaviour {

    public GameObject LineJ;
    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = false;
    [SerializeField]
    protected BoxCollider2D col;
    [SerializeField]
    protected Camera m_Camera;
    private Vector3 startPos;    // Start position of line
    private Vector3 endPos;    // End position of line
    private Vector3 mousePos;
    protected GameObject Circle;
    public Material lineMaterial;
    public bool isMenuActive = false;
    private ButtonScript script1;
    protected RaycastHit2D hit;
    protected bool isCanDraw = false;
    protected bool isDummyFlag = true;
    public virtual LineRenderer lineRenderer
    {
        get
        {
            return m_LineRenderer;
        }
    }
    public virtual BoxCollider2D BoxCollider2D
    {
        get
        {
            return col;
        }
    }
    public virtual bool addCollider
    {
        get
        {
            return m_AddCollider;
        }
    }

    protected virtual void Awake()
    {
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }

        script1 = GameObject.Find("ButtonManager").GetComponent<ButtonScript>();
    }

    protected virtual void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false && !isMenuActive && script1.startFlag)
        {

            if (Input.GetMouseButtonDown(0))// && Input.touchCount == 1)
            {
                //////////////////////////////////////

                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                isCanDraw = true;
                isDummyFlag = true;
                if (hit)
                {
                    if (!hit.transform.tag.Equals("Collider") && !hit.transform.tag.Equals("LineJ") && !hit.transform.tag.Equals("uniqueBlock"))
                    {
                        // Debug.Log("hit" + hit.transform.name);
                        isCanDraw = false;
                        isDummyFlag = false;
                    }
                }

                /////////////////////////////////////
                if (isCanDraw)
                {
                    startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (startPos.y > -4)
                    {
                        if (m_LineRenderer == null)
                        {
                            CreateDefaultLineRenderer();
                            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            mousePos.z = 0;
                            m_LineRenderer.SetPosition(0, mousePos);
                            startPos = mousePos;
                        }
                    }
                }
            }
            if (Input.GetMouseButton(0) && startPos.y > -4)//&& Input.touchCount == 1)
            {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit)
                {
                    if (!hit.transform.tag.Equals("Collider") && !hit.transform.tag.Equals("LineJ") && !hit.transform.tag.Equals("uniqueBlock"))
                    {
                        isDummyFlag = false;
                    }
                }

                isCanDraw = isDummyFlag;

                if (isCanDraw)
                {
                    if (m_LineRenderer)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        m_LineRenderer.SetPosition(1, mousePos);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && startPos.y > -4)// && Input.touchCount == 1)
            {
                if (m_LineRenderer != null)
                {
                    endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    GameObject lineJ = Instantiate(LineJ, NewCenterOfMass(), Quaternion.identity);
                    lineJ.tag = "LineJ";
                    m_LineRenderer.transform.SetParent(lineJ.transform);
                    
                    if (Vector3.Distance(startPos, endPos) < 0.1)
                    {
                        Destroy(lineJ.gameObject);
                        Destroy(m_LineRenderer.gameObject);
                     //   Destroy(col.gameObject);

                        //UnityEngine.Debug.Log("Destroy");
                    }
                    else
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        m_LineRenderer.SetPosition(1, mousePos);
                        endPos = mousePos;
                        addColliderToLine();
                    }
                    m_LineRenderer = null;
                }
            }
        }
    }


    protected virtual void CreateDefaultLineRenderer()
    {
        m_LineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        m_LineRenderer.positionCount = 2;
        m_LineRenderer.material = lineMaterial;
        m_LineRenderer.startColor = Color.white;
        m_LineRenderer.endColor = Color.white;
        m_LineRenderer.startWidth = 0.1f;
        m_LineRenderer.endWidth = 0.1f;
        m_LineRenderer.useWorldSpace = false;
        m_LineRenderer.tag = "Line";
        m_LineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
    }

    private void addColliderToLine()
    {
        col = new GameObject("ColliderJ").AddComponent<BoxCollider2D>();
        col.tag = "Collider";
        col.gameObject.layer = LayerMask.NameToLayer("Line");
        col.transform.parent = m_LineRenderer.transform; // Collider is added as child object of line
        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (startPos + endPos) / 2;
        col.transform.position = midPoint; // setting position of collider object
        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle);

    }

    public Vector2 NewCenterOfMass()
    {

        Vector2 centerPoint = (startPos + endPos) / 2;
        return centerPoint;
    }

}