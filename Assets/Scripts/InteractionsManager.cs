using UnityEngine;
public class InteractionsManager : MonoBehaviour
{
    public GameObject benches;
    public GameObject pavers;
    public GameObject roof;
    public GameObject signs;



    public void Toggle_Benches()
    {
        benches.SetActive(!benches.activeInHierarchy);
    }

    public void Toggle_Pavers()
    {
        pavers.SetActive(!pavers.activeInHierarchy);
    }

    public void Toggle_Roof()
    {
        roof.SetActive(!roof.activeInHierarchy);
    }

    public void Toggle_Signs()
    {
        signs.SetActive(!signs.activeInHierarchy);
    }
}