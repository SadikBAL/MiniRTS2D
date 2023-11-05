using Assets.Scripts;
using Assets.Scripts.CommonEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GridManager GridManager;
    public AnimationManager AnimationManager;
    public Fabrica ObjectFabrica;
    [HideInInspector]
    public Object Build = null;
    [HideInInspector]
    public Object Select = null;
    public PreviewPopup PreviewPopup;
    GameState CurrentState = GameState.WaitInput;
    public event Action OnMoveEvent;
    public CommonDatas SpriteDatas;
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            // If not, set instance to this
            Instance = this;
        }
        // If instance already exists and it's not this:
        else if (Instance != this)
        {
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        GridManager = new GridManager(20,10);
        OnMoveEvent += OnMoveCompleted;
    }

    private void OnMoveCompleted()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D Hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (Hit.collider != null )
            {
                Object Temp = Hit.collider.gameObject.GetComponent<Object>();
                if(Temp)
                {
                    SelectItem(Temp);
                }
            }
            else
            {
                bool visible = false;
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    position = Input.mousePosition
                };

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                if (results.Count > 0)
                {
                  ;
                    foreach (RaycastResult result in results)
                    {
                        if(result.gameObject.CompareTag("Preview"))
                        {
                            visible = true;
                        }
                    }
                }
                if(!visible)
                    SelectItem(null);
            }
        }
        if(Build)
        {
            Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Build.gameObject.transform.position = new Vector3((int)Pos.x, (int)Pos.y, 0);
            if (GridManager.CheckBuildable(Build))
            {
                Build.SpriteRenderer.color = Color.green;
            }
            else
            {
                Build.SpriteRenderer.color = Color.red;
            }
        }
        if(Build && Input.GetKeyUp(KeyCode.Space))
        {
            if (!GridManager.AddObject(Build))
            {
                Build.GetComponent<PooledPrefab>().Pool.ReturnObject(Build.gameObject);
            }
            Build = null;
        }
        if (Build && Input.GetKeyUp(KeyCode.Escape))
        {
            Build.GetComponent<PooledPrefab>().Pool.ReturnObject(Build.gameObject);
            Build = null;
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Select) 
            {
                Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 Target = new Vector3((int)Pos.x, (int)Pos.y, 0);
                if(GridManager.CheckLocationMovebility(Target))
                {
                    Select.MovementComponent.Move(Select, Target);
                }
                else
                {
                    if(GridManager.InGridControl(Target))
                    {
                        Object Selectable = GridManager.GetObjectOnLocation(Target);
                        if(Selectable != Select)
                        {
                            Select.MovementComponent.Atack(Select, Selectable);
                        }
                    }
                }

            }
        }
    }
    public void BuildItem(Object Item)
    {
        if (Select != null) 
        {
            Select = null;
        }
        if (Build)
        {
            Build.GetComponent<PooledPrefab>().Pool.ReturnObject(Build.gameObject);
            Build = null;
        }
        Build = ObjectFabrica.GetObject(Item.Type);
    }
    public void SelectItem(Object Item)
    {
        if (Item != null) 
        {
            Select = Item;
            PreviewPopup.Show(Item.GetPreview());
        }
        else
        {
            Select = null;
            PreviewPopup.Hide();
        }
        
    }
    public Sprite GetSprite(ObjectType Type)
    {
        for (int i = 0; i< SpriteDatas.Types.Count; i ++)
        {
            if (SpriteDatas.Types[i] == Type)
            {
                if(i < SpriteDatas.Icons.Count)
                    return SpriteDatas.Icons[i];
            }
        }
        return null;
    }




}
