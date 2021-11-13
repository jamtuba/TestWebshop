using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop
{
    public class ShoppingBasket
    {
        /// <summary>
        /// Tilføjer en vare til indkøbskurven
        /// </summary>
        /// <param name="brugernavn">brugerens id</param>
        /// <param name="varenummer">varenummer på den vare der skal tilføjet</param>
        /// <param name="antal">antallet af varer</param>
        /// <returns>Det samlede antal varer i kurven</returns>
        public int AddToBasket(string brugernavn, int varenummer, int antal)
        {
            Database db = new();
            if (antal > 0)
            {
                db.AddToBasket(brugernavn, varenummer, antal);
            } else if (antal == 0)
            {
                db.RemoveFromBasket(brugernavn, varenummer);
            }
            
            BasketEntry[] varer = db.GetBasket(brugernavn);
            int totalAntal = 0;
            foreach (BasketEntry vare in varer) {
                totalAntal = totalAntal + vare.Antal;
            }
            return totalAntal;
        }

        /// <summary>
        /// Returnerer den samlede pris for alle varer i indkøbskurven
        /// 
        /// </summary>
        /// <param name="brugernavn">brugerens id</param>
        /// <returns>Den samlede pris som et beløb i kr.</returns>
        public double GetTotalPrice(string brugernavn)
        {
            Database db = new();
            BasketEntry[] varer = db.GetBasket(brugernavn);
            int totalPris = 0;
            foreach (BasketEntry entry in varer)
            {
                Article vare = db.GetArticle(entry.Varenummer);
                totalPris = totalPris + entry.Antal * (int)vare.Price;
            }
            return totalPris;

        }
    }
}
