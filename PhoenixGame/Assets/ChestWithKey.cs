using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestWithKey : MonoBehaviour
{
    public ChestOpened chestOpened;
    public KeyHUD keyHUD;
    public TorchToAppear torchAppear1;
    public TorchToAppear torchAppear2;
    public TorchToAppear torchAppear3;
    public TorchToDestroy torchDestroy1;
    public TorchToDestroy torchDestroy2;
    public TorchToDestroy torchDestroy3;
    public void DestroyMyself()
    {
        chestOpened.gameObject.SetActive(true);
        keyHUD.gameObject.SetActive(true);

        torchAppear1.gameObject.SetActive(true);
        torchAppear2.gameObject.SetActive(true);
        torchAppear3.gameObject.SetActive(true);
        torchDestroy1.gameObject.SetActive(false);
        torchDestroy2.gameObject.SetActive(false);
        torchDestroy3.gameObject.SetActive(false);

        Destroy(this.gameObject);
    }
}
