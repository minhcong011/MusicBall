using System;

[Serializable]
public class SongSheet
{
    public string songName;
    public Key[] keyArray;
    [Serializable]
    public class Key
    {
        public float length;
        public bool permanentSide;
        public int side;
        public bool isDoubleNote;
    }   
}
