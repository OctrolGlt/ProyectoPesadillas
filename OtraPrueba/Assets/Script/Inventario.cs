using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventario : MonoBehaviour
{
    public int NumeroDeFusibles = 0;
    public TMP_Text textoUI;

    public void AumentoDeFusibles()
    {
        NumeroDeFusibles++;
        textoUI.text = "Fusibles: " + NumeroDeFusibles.ToString() + " / 5";

    }

    public int Numerof()
    {
        return NumeroDeFusibles;
    }
}
