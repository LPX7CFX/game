using UnityEngine;

public class Spawnitem : MonoBehaviour
{
    public Letterplace Letterplace;
    void Update(){
    if (Input.GetKeyDown(KeyCode.Keypad9)){
        Letterplace.Addletter();
    }



}

    


}
