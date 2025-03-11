using System;
namespace Redage.SDK.Models
{
    public class PricesSettings
    {
        /// <summary>
        /// цены фейерверка
        /// </summary>
        public int[] FireworkPrices = new int[]
        {
            150,
            500,
            1000,
            2000
        };
        /// <summary>
        /// цены топора и кирки
        /// </summary>
        public int[] InstrumentPrices = new int[]
        {
            1250, // Топор
            300,
            1250,
            2250
        };
        public int[] Instrument2Prices = new int[]
       {
            1250, // Топор
            3000
       };

        public int[] FurtinurePrices = new int[]
        {
            1500,
            1500,
            1500,
            10000,
            1000,
            1000,
            1000,
            1000,
            1000,
            10000,
            10000,
            10000,
            500,
            500,
            500,
            500,
            500,
            500,
            750,
            750,
            750,
            750,
            1000,
            750,
            2500,
            10000,
            1000,
            10000,
            25000,
            50000,
            2500,
            100000,
            15000,
            250,
            1500,
            500,
            500,
            500,
            500,
            500
        };
        
        public int[] GaragesPrice = new int[]
        {
            800000,
            1200000,
            2000000,
            3500000,
            5000000,
            17500000,
            10000,
            25000,
            50000
        };
        
        public int[] DalnoboyMoney = new int[]
        {
            620000,
            120000
        };
        
        public int CreateOrgPrice = 2500000; // 2.5kk $
        public int FirstOrgPrice = 1000000; // 1kk $
        public int SecondOrgPrice = 5000; // 5000 RB
        public int CustomsPrice = 75000; // 750k $
        public int StockPrice = 50000; // 500k $
        public int CrimeOptionsPrice = 1999; // 600k $
        
        public int UpdateTypeOrganization = 1999; // 1999 RB
        public int UpdateOrganizationLeader = 250; // 1999 RB
        
        public int AirdropInfoPrice = 2500000;
        public int AirdropOrderPrice = 10000000;
        
        public int ZapravkaMinPrice = 200;
        public int ZapravkaMaxPrice = 3000;
        
        public int ClothesMinPrice = 4800;
        public int ClothesMaxPrice = 8800;
        
        public int TattooBarberMasksLscMinPrice = 95000;
        public int TattooBarberMasksLscMaxPrice = 175000;
        
        public int PistolAmmoPrice = 40;
        public int ShotgunAmmoPrice = 80;
        public int SMGAmmoPrice = 80;
        public int RiflesAmmoPrice = 150;
        public int SnniperAmmoPrice = 1100;
            
        public int PosobieNew = 25000;
        public int PosobieOld = 50000;
        
        public int MaxOgrableine = 3000000;
    }
}