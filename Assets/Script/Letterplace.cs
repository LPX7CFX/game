using UnityEngine;

public class Letterplace : MonoBehaviour
{
    [SerializeField] private Transform Container;
    [SerializeField] private GameObject letter;
    

    public void Addletter()
    {
        Instantiate(letter, Container);


    }
}
