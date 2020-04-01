using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    private Dictionary<string, int> items;

    // Use this for initialization
	void Start ()
    {
        items = new Dictionary<string, int>();
	}

    public bool HasItem(string name)
    {
        if (items.ContainsKey(name))
        {
            return true;
        }

        return false;
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";

        foreach (KeyValuePair<string, int> item in items)
        {
            itemDisplay += item.Key + "(" + item.Value + ") ";
        }

        Debug.Log(itemDisplay);
    }

    public void AddItem(string name)
    {

        Messenger.Broadcast(GameEvent.PICKED_UP_KEY, name);

        if (items.ContainsKey(name))
        {
            items[name] += 1;
        }
        else
        {
            items[name] = 1;
        }

        DisplayItems();
    }

    public List<string> GetItemList()
    {
        List<string> list = new List<string>(items.Keys);

        return list;
    }

    public int GetItemCount(string name)
    {
        if (items.ContainsKey(name))
        {
            return items[name];
        }

        return 0;
    }

    public bool ConsumeItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name]--;

            if (items[name] == 0)
            {
                items.Remove(name);
            }

            DisplayItems();
            return true;
        }
        else
        {
            Debug.Log("cannot consume " + name);
            return false;
        }
    }
}
