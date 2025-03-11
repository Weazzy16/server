using System;
using System.Collections.Generic;
using GTANetworkAPI;
using Localization;
using NeptuneEvo.Character;
using NeptuneEvo.Core;
using NeptuneEvo.Functions;
using NeptuneEvo.Handles;
using NeptuneEvo.Players;
using NeptuneEvo.Quests.Models;
using NeptuneEvo.VehicleData.LocalData.Models;
using Newtonsoft.Json;
using Redage.SDK;
using NeptuneEvo.Character.Models;

namespace NeptuneEvo.AleSystems
{
    public class VehicleSpawn : Script
    {
        public static readonly nLog Log = new nLog(nameof(VehicleSpawn));


        private static List<string> AirCarList = new List<string>
        {
            "akula",
            "annihilator",
            "annihilator2",
            "buzzard",
            "buzzard2",
            "cargobob",
            "cargobob2",
            "cargobob3",
            "cargobob4",
            "frogger",
            "frogger",
            "havok",
            "hunter",
            "maverick",
            "savage",
            "seasparrow",
            "seasparrow2",
            "seasparrow3",
            "skylift",
            "supervolito",
            "supervolito2",
            "swift",
            "swift2",
            "valkyrie",
            "valkyrie2",
            "volatus",
        };
        //
        public static Vector3 SpawnVehiclePos = new Vector3(103.07955, -1074.3184, 29.192348);
        //
        [ServerEvent(Event.ResourceStart)]
        public void Event_ResourceStart()
        {

            Main.CreateBlip(new Main.BlipData(402, "Спавн транспорта", SpawnVehiclePos, 4, true));
            CustomColShape.CreateCylinderColShape(SpawnVehiclePos, 1, 1, 0, ColShapeEnums.VehicleSpawn, 0);
            NAPI.Marker.CreateMarker(1, SpawnVehiclePos - new Vector3(0, 0, 0.87), new Vector3(), new Vector3(), 0.80f, new Color(255, 255, 255, 220));
        }

        public static bool isAirCar(string model)
        {
            model = model.ToLower();
            return AirCarList.Contains(model);
        }

        public static bool isAirCar(uint model)
        {
            foreach (var modelName in AirCarList)
                if (NAPI.Util.GetHashKey(modelName) == model)
                    return true;
            return false;
        }

        private static List<string> Airs = new List<string>
        {
            "Havok",
            "Seasparrow2",
            "Seasparrow",
            "Buzzard2",
        };

        private static int SelectVehiclePosition = 0;
        private static List<(Vector3, Vector3)> VehiclePositions = new List<(Vector3, Vector3)>
        {
            (new Vector3(105.65153, -1062.9272, 29.00295), new Vector3(-1.0429102, 0.14183156, -113.63608)),
            (new Vector3(107.96851, -1059.732, 28.984015), new Vector3(-0.48314232, -0.046658315, -115.022934)),
            (new Vector3(109.46743, -1056.453, 28.981308), new Vector3(-0.4545266, 0.03703493, -113.69676)),
            (new Vector3(111.00218, -1053.2532, 28.985123), new Vector3(-0.21205936, 0.16337319, -113.84691)),
            (new Vector3(119.196594, -1069.7085, 28.97589), new Vector3(-0.18845887, 0.000477726, 179.24657)),
            (new Vector3(122.49579, -1070.7654, 28.976112), new Vector3(-0.21502772, 0.0056975344, -178.91005)),
            (new Vector3(132.0809, -1070.4569, 28.975515), new Vector3(-0.1647644, 0.0040038214, -1.302954)),
            (new Vector3(132.291, -1081.1382, 28.977205), new Vector3(-0.2601968, -0.07454133, -1.5225098)),
            (new Vector3(128.26898, -1080.8688, 28.982338), new Vector3(-0.16314144, -0.50129944, 1.5271933)),
            (new Vector3(111.57704, -1080.2706, 28.97614), new Vector3(-0.17838907, 0.028348105, -19.421402)),
            (new Vector3(139.88145, -1081.7688, 28.976831), new Vector3(-0.22197355, 0.037288137, 0.5233857)),
        };

        public static (Vector3, Vector3) GetSpawnPosition()
        {
            var positionData = VehiclePositions[SelectVehiclePosition];

            SelectVehiclePosition++;
            if (SelectVehiclePosition >= VehiclePositions.Count)
                SelectVehiclePosition = 0;

            return positionData;
        }

        public static bool IsVehicleToSpawn(Vector3 position)
        {

            foreach (var data in VehiclePositions)
            {
                if (position.DistanceTo2D(data.Item1) < 10f)
                    return true;
            }

            return false;
        }

        [Interaction(ColShapeEnums.VehicleSpawn)]
        public static void OpenVehicleSpawn(ExtPlayer player, int index)
        {
            var sessionData = player.GetSessionData();
            if (sessionData == null) return;
            if (!player.IsCharacterData()) return;

            if (!FunctionsAccess.IsWorking("spawnveh"))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.FunctionOffByAdmins), 3000);
                return;
            }

            var carsData = new List<Dictionary<string, object>>();

            var vehiclesNumber = VehicleManager.GetVehiclesCarNumberToPlayer(player.Name);
            foreach (string number in vehiclesNumber)
            {
                var vehicleData = VehicleManager.GetVehicleToNumber(number);
                if (vehicleData == null) continue;
                var price = 0;

                if (BusinessManager.BusProductsData.ContainsKey(vehicleData.Model))
                    price = MoneySystem.Wallet.GetPriceToVip(player, BusinessManager.BusProductsData[vehicleData.Model].Price);

                var carData = new Dictionary<string, object>
{
    {"Model", vehicleData.Model},
    {"Number", number},
    {"IsSpawn", VehicleData.LocalData.Repository.IsVehicleToNumber(VehicleAccess.Personal, number)},
    {"Price", price},
};
                carsData.Add(carData);
            }

            Trigger.ClientEvent(player, "client.vehiclespawn.open", JsonConvert.SerializeObject(carsData));
        }
    }
}