using System;


public class Tile_Key : Tile_Base
{

    // Implementations
    //=================

    protected override void Init()
    {
        base.Init();

        // Register OnVisit command 
        {
            Action<GameController> action;
            action = i => i.DecreaseStepCount();
            action += i => i.RegisterUndoCmd();
            m_cmd_onVisit = new Command_OnVisit<GameController>(GameController.Instance, action);
        }
    }


    // Interfaces
    //=================

    public override void Visit()
    {
        // Update self status 
        {
            m_sprite_visited.SetActive(true);
            m_tileState = TileState.Visited;
        }

        // Execute command
        {
            m_cmd_onVisit.Execute();
        }
    }
}
