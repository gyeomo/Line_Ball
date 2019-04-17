using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DrawLine2D : MonoBehaviour
{
    
    public GameObject LineP;
    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = false;
    [SerializeField]
    protected EdgeCollider2D m_EdgeCollider2D;
    [SerializeField]
    protected Camera m_Camera;
    protected List<Vector2> m_Points;
    private Vector3 startPos;    // Start position of line
    private Vector3 endPos;    // End position of line
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

    public virtual bool addCollider
    {
        get
        {
            return m_AddCollider;
        }
    }

    public virtual EdgeCollider2D edgeCollider2D
    {
        get
        {
            return m_EdgeCollider2D;
        }
    }

    public virtual List<Vector2> points
    {
        get
        {
            return m_Points;
        }
    }

    protected virtual void Awake()
    {
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        m_Points = new List<Vector2>();
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
                if (isCanDraw) {
                    startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (startPos.y > -4)
                    {
                        if (m_LineRenderer == null)
                        {
                            CreateDefaultLineRenderer();
                            CreateDefaultEdgeCollider2D();
                            Reset();
                        }
                    }
                }
            }
            if (Input.GetMouseButton(0) && startPos.y > -4 )//&& Input.touchCount == 1)
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
                    Vector2 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
                    if (!m_Points.Contains(mousePosition))
                    {
                        m_Points.Add(mousePosition);
                        if (m_LineRenderer != null)
                        {
                            m_LineRenderer.positionCount = m_Points.Count;
                            m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, mousePosition);
                        }
                        if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1)
                        {
                            m_EdgeCollider2D.points = m_Points.ToArray();
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && startPos.y > -4)// && Input.touchCount == 1)
            {
                if (m_EdgeCollider2D != null && m_LineRenderer != null)
                {
                    GameObject lineP = Instantiate(LineP, NewCenterOfMass(), Quaternion.identity);
                    lineP.tag = "LineP";
                    
                    m_LineRenderer.transform.SetParent(lineP.transform);
                    endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (Vector3.Distance(startPos, endPos) < 0.1)
                    {
                        Destroy(lineP.gameObject);
                        Destroy(m_LineRenderer.gameObject);
                        Destroy(m_EdgeCollider2D.gameObject);

                        //UnityEngine.Debug.Log("Destroy");
                    }
                    m_LineRenderer = null;
                }
            }
        }
    }

    protected virtual void Reset()
    {
        if (m_Points != null)
        {
            m_Points.Clear();
        }
        if (m_EdgeCollider2D != null && m_AddCollider)
        {            
            m_EdgeCollider2D.Reset();
            
        }
    }

    protected virtual void CreateDefaultLineRenderer()
    {
        m_LineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        m_LineRenderer.positionCount = 1;
        m_LineRenderer.material = lineMaterial;
        m_LineRenderer.startColor = Color.white;
        m_LineRenderer.endColor = Color.white;
        m_LineRenderer.startWidth = 0.1f;
        m_LineRenderer.endWidth = 0.1f;
        m_LineRenderer.useWorldSpace = false;
        m_LineRenderer.tag = "Line";
        
    }

    protected virtual void CreateDefaultEdgeCollider2D()
    {
        m_EdgeCollider2D = new GameObject("Collider").AddComponent<EdgeCollider2D>();
        m_EdgeCollider2D.transform.parent = m_LineRenderer.transform;
        m_EdgeCollider2D.tag = "Collider";
        m_EdgeCollider2D.gameObject.layer = LayerMask.NameToLayer("Line");
        m_EdgeCollider2D.edgeRadius = 0.001f;
    }
    public Vector2 NewCenterOfMass()
    {
        float totalx = 0;
        float totaly = 0;
        foreach (Vector2 point in m_Points)
        {
            totalx += point.x;
            totaly += point.y; 
        }
        Vector2 centerPoint = new Vector2(totalx / m_Points.Count, totaly / m_Points.Count);
        return centerPoint;
    }

}
