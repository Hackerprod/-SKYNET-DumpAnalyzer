using System;

namespace SKYNET
{
    internal class SpaceManager
    {
        public string BlackSpace { get; set; }
        string DefaultSpace = "    ";
        
        public SpaceManager()
        {

        }

        internal void SetFirstSpace()
        {
            BlackSpace = DefaultSpace;
        }
        public void AddSpace()
        {
            BlackSpace = BlackSpace + DefaultSpace;
        }
        public void RemoveSpace()
        {
            if (BlackSpace.Length > 4)
            {
                BlackSpace = BlackSpace.Remove(0, 4);
            }
            
        }
    }
}