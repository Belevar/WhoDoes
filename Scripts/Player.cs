using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System;

[Serializable]
public class Player  {

    public string playerName;
    public string sex;


}

[Serializable]
public class PlayersHanlder
{
    public List<Player> players;

    public PlayersHanlder()
    {
        players = new List<Player>();
    }
}
