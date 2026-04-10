using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Predmet dataPredmetu;
    public void Sebrat() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Inventar>().Pridej(dataPredmetu);
        Destroy(gameObject);
    }
}