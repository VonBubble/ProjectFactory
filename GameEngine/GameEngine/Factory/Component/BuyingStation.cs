using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace GameEngine.Factory.Component
{
    public class BuyingStation : IFactoryComponent
    {
        private FactoryEntity parent;
        private int delayBetweenSales;
        private int timeSinceLastSale;

        public FactoryEntity Parent { get { return parent; } set { parent = value; } }

        public BuyingStation()
        {
            delayBetweenSales = 5;
            timeSinceLastSale = 0;
        }

        public BuyingStation(int delay)
        {
            delayBetweenSales = delay;
            timeSinceLastSale = 0;
        }

        public void Update()
        {
            timeSinceLastSale++;
            if(timeSinceLastSale >= delayBetweenSales)
            {
                timeSinceLastSale = 0;
                var gain = 0;
                foreach (var faction in World.Instance.FactionList.Factions)
                {
                    for (int i = faction.Units.Count - 1; i >= 0; i--)
                    {
                        if(faction.Units[i].Position == parent.Position)
                        {
                            gain += faction.Units[i].Value;
                            faction.RemoveUnit(faction.Units[i]);
                        }
                    }
                    faction.Wallet.Balance += gain;
                }
            }
        }

        #region IXmlSerializer Methods
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Type", "BuyingStation");
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }
        #endregion
    }
}
