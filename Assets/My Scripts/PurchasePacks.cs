using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasePacks : MonoBehaviour
{
    public void PurchaseAllPacks()
    {
        IAPManager.instance.purchaseAllPacks();
    }
}
