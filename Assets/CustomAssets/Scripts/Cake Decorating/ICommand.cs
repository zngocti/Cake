using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo(bool undoButton = true);

    GameObject ItemToExecute();
    bool IsPlaced();
}
