using System;


public class Tile_Lock : Tile_Base
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
            action += i => i.DecreaseKeyCount();
            action += i => i.RegisterUndoCmd();
            m_cmd_onVisit = new Command_OnVisit<GameController>(GameController.Instance, action);
        }
    }


    // Interfaces
    //=================

    public override void Visit()
    {
        if (GameController.Instance.KeyCount>0)
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
}
