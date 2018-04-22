using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMalusManager : MonoBehaviour {

    [SerializeField] float _timeInterval=30f;
    [SerializeField] int _shapeCreationInterval=2;

    [Header("All bonus/malus gameobjects")]
    [SerializeField] List<GameObject> AllBonusMalus;

    bool mustUseMalusBonus = false;
    int currentCreatedShape = 1;

    private void Awake()
    {
        mustUseMalusBonus = true;
        NotificationManager.Instance.AttachNotif(EventNotification.ShapeCreation, this, (o) =>
        {
            currentCreatedShape++;
            mustUseMalusBonus = currentCreatedShape % _shapeCreationInterval == 0;
            ShapeMovementController newShape = Managers.Game.currentShape.movementController;

            if (mustUseMalusBonus)
            {
                var selected = newShape.GetComponentsInChildren<BonusMalusSlot>().Random();
                GameObject.Instantiate(AllBonusMalus.Random(), selected.transform, false);
            }

        });
    }
    






}
