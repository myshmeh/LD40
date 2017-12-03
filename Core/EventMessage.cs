using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// represents message <br> from <b>subject</b>
    /// </summary>
    public enum EventMessage
    {
        //formatting rule
        //[SUBJECT]_[EXPRESSION] (i.e. [sth] is [doing])
        //[EXPRESSION] patterns: VERB, VERB_OBJECT, ADJECTIVE

        //game state
        ROOM_MOVE,
        PLAYER_DEAD,
        PLAYER_CALLPC,
        PLAYER_OPENLAWBOOK,
        ROOMMANAGER_WAKECANVAS,
        CANVAS_DEACTIVATED,
        ROOMMANAGER_WAKEUPPLAYER,
        ENEMY_BANANAED,
        ENEMY_ATTACKED,
        ENEMY_KILLED,
        ENEMY_NEEDLE,
    } 
}