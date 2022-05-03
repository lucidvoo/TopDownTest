using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// класс, который прикрепляется к игроку и который хранит его статы.
// нужен чтобы считать статы игрока когда он вторгается в чей то коллайдер

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO stats;

    public PlayerStatsSO Stats => stats;


    // метод OnEnable на ScriptableObjects не вызывается при перезагрузке уровня, вызвать вручную
    private void Start()
    {
        stats.OnEnable();
    }
}
