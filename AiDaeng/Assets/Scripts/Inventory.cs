using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class Inventory : MonoBehaviour, IObserver
{
    public static Inventory instance;
    public List<Items> items = new List<Items>();
    //public List<int> itemNumbers = new List<int>();
    public GameObject[] slots;

    [SerializeField] private Image _inventorySprite;
    [SerializeField] private float _shakeStrength;
    private WeatherManager _weatherManager;
    Vector3 defaultPos;

    private void Awake()
    {
        instance = this;
        _weatherManager = FindObjectOfType<WeatherManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayItems();
        defaultPos = transform.TransformPoint(transform.position);
    }    

    public void DisplayItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;            
            //slots[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1);
            //slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = itemNumbers[i].ToString();
            //slots[i].transform.GetChild(1).gameObject.SetActive(true);
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;

                slots[i].GetComponent<InventorySlot>().OnChanged(items[i].itemSprite);
                slots[i].GetComponent<InventorySlot>().SetItem(items[i]);
                //slots[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1);
                //slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = itemNumbers[i].ToString();
                //slots[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;

                slots[i].GetComponent<InventorySlot>().OnChanged(null);
                slots[i].GetComponent<InventorySlot>().SetItem(null);
                //slots[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0);
                //slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = null;
                //slots[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }    

    public void AddItem(Items _item)
    {
        //inform player if slots are all full
        if(items.Count >= slots.Length)
        {            
            _inventorySprite.transform.DOShakePosition(1f, _shakeStrength).OnComplete(() =>
            _inventorySprite.transform.DOMove(defaultPos, 0.2f));
            
            NotificationManager.instance.AddNoti("ของเต็มแล้ว!");
            return;
        }

        items.Add(_item);
        //itemNumbers.Add(1);

        //if (!items.Contains(_item))
        //{
        //    items.Add(_item);
        //    itemNumbers.Add(1);
        //}
        //else
        //{
        //    //Debug.Log("You already have this one");
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        if (items[i] == _item)
        //        {
        //            itemNumbers[i]++;
        //        }
        //    }
        //}
        DisplayItems();        
    }

    public void RemoveItem(Items _item)
    {
        if (items.Contains(_item))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (_item == items[i])
                {
                    items.Remove(_item);
                    //itemNumbers[i]--;
                    //if (itemNumbers[i] == 0)
                    //{
                    //    items.Remove(_item);
                    //    itemNumbers.Remove(itemNumbers[i]);
                    //    //selectedItem = null;
                    //}
                }
            }
        }
        else
        {            
            print("You got nothing in this slot");
        }
        DisplayItems();
    }

    //public bool HasItem(Items _item)
    //{
    //    return items.Any(item => item == _item);
    //}

    public void UpdateItem(Items requiredItem, Items goalItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == requiredItem)
            {
                items[i] = goalItem;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;
                return;
            }
        }
    }


    //public void RemoveItem(Items _item)
    //{
    //    if (items.Contains(_item))
    //    {
    //        for (int i = 0; i < items.Count; i++)
    //        {
    //            if (_item == items[i])
    //            {
    //                itemNumbers[i]--;
    //                if (itemNumbers[i] == 0)
    //                {
    //                    items.Remove(_item);
    //                    itemNumbers.Remove(itemNumbers[i]);
    //                    selectedItem = null;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        selectedItem = null;
    //        print("You got nothing in this slot");
    //    }
    //    DisplayItems();
    //}

    public bool HasItem(Items requiredItem)
    {
        return items.Contains(requiredItem);
    }

    public void OnNotify(WeatherType weather)
    {
        switch (weather)
        {
            case (WeatherType.Rainy):
                for (int i = 0; i < slots.Length; i++)
                {
                    if (i < items.Count)
                    {                        
                        slots[i].GetComponent<InventorySlot>().CountdownLifeBar();                        
                    }                    
                }
                return;

            default:                
                return;
        }
    }

    void OnEnable()
    {
        _weatherManager.AddObserver(this);
    }

    void OnDisable()
    {
        _weatherManager.RemoveObserver(this);
    }
}
