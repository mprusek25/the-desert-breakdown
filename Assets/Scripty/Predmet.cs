using UnityEngine;

[CreateAssetMenu(fileName = "NovyPredmet", menuName = "Inventar/Predmet")]
public class Predmet : ScriptableObject
{
    public string nazev;
    public Sprite ikona;
    public float obnovaHP, obnovaJidla, obnovaVody;
    public bool jeSpotrebitelny = true;
}