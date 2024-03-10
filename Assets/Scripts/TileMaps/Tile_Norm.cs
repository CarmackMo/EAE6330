

using System;

public class Tile_Norm : Tile_Base
{


    protected override void Init()
    {
        base.Init();

        // Register OnVisit command 
        {
            Action<GameController> action;
            action = i => i.DecreaseStepCount();
            action += i => i.RegisterUndoCmd();
            Command_OnVisit<GameController> onVisitCmd = new Command_OnVisit<GameController>(GameController.Instance, action);
        }
    }

}
