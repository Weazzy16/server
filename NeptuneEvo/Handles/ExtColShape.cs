using GTANetworkAPI;
using NeptuneEvo.Players.Models;
using System;

namespace NeptuneEvo.Handles
{
    public class ExtColShape : ColShape
    {
        public ExtColShape(NetHandle handle) : base(handle)
        {
        }
        public ExtColShapeData ColShapeData;
        public void SetColShapeData(ExtColShapeData сolShapeData)
        {
            ColShapeData = сolShapeData;
        }

        internal void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}