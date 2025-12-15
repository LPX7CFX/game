using UnityEngine;

public class Letterplace : MonoBehaviour
{
    [SerializeField] private RectTransform Container;
    [SerializeField] private GameObject letter;
    

    public void Addletter()
    {
        Instantiate(letter, Container);


    }
}
