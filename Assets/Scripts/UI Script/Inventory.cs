using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    // ÇÊ¿äÇÑ ÄÄÆ÷³ÍÆ®
    [SerializeField]
    private GameObject go_InventroyBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    // ½½·Ôµé
    private Slot[] slots;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        GameManager.isOpenInventory = true;
        go_InventroyBase.SetActive(true);
    }

    private void CloseInventory()
    {
        GameManager.isOpenInventory = false;
        go_InventroyBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
