using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Pawn> characterList = new List<Pawn>();

    private void Start()
    {
        TurnManager.Instance.SetPlayerPawns(characterList);

    }
}
