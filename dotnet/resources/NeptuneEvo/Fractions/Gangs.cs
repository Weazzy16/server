﻿using System.Collections.Generic;
using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Core;
using Redage.SDK;
using System;
using Localization;
using NeptuneEvo.Chars;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.VehicleModel;
using NeptuneEvo.Functions;
using NeptuneEvo.Accounts;
using NeptuneEvo.Players.Models;
using NeptuneEvo.Players;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using NeptuneEvo.Fractions.Models;
using NeptuneEvo.Fractions.Player;
using NeptuneEvo.Table.Models;
using NeptuneEvo.VehicleData.LocalData;
using NeptuneEvo.Quests.Models;
using NeptuneEvo.World.War.Models;

namespace NeptuneEvo.Fractions
{
    class Gangs : Script
    {
        private static readonly nLog Log = new nLog("Fractions.Gangs");
        
        public static Vector3[] DrugPoints = new Vector3[2]
        {
            new Vector3(590.6146, 2795.5425, 42.045673),
            new Vector3(715.9515, 4174.404, 40.70009),
        };

        public static ExtTextLabel[] BuyDruygsLab = new ExtTextLabel[2] { null, null };

        [ServerEvent(Event.ResourceStart)]
        public void Event_OnResourceStart()
        {
            try
            {
                NAPI.TextLabel.CreateTextLabel("~g~Джексон Грин", new Vector3(-222.5464, -1617.449, 35.86932), 5f, 0.3f, 0, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~g~Люк Миллер", new Vector3(112.52232, -1961.4207, 19.94), 5f, 0.3f, 0, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~g~Виатт Адамсон", new Vector3(486.2756, -1528.2778, 30.28829), 5f, 0.3f, 0, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~g~Дэмиан Моралез", new Vector3(1435.6263, -1491.5796, 63.62193), 5f, 0.3f, 0, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~g~Саймон Родригез", new Vector3(944.5905, -2161.819, 31.188383), 5f, 0.3f, 0, new Color(255, 255, 255), true, 0);


                PedSystem.Repository.CreateQuest("a_m_m_afriamer_01", new Vector3(-210.7262, -1607.3243, 34.869274), 51.675472f, 0, "npc_gang_task", ColShapeEnums.GangNpcTask, "~y~NPC~w~ Полли\n Банда", false);
                PedSystem.Repository.CreateQuest("a_m_m_afriamer_01", new Vector3(475.27454, -1529.4019, 29.305523), -40.943142f, 0, "npc_gang_task", ColShapeEnums.GangNpcTask, "~y~NPC~w~ Полли\n Банда", false);
                PedSystem.Repository.CreateQuest("a_m_m_afriamer_01", new Vector3(1401.6206, -1487.9203, 59.352737), -5.179127f, 0, "npc_gang_task", ColShapeEnums.GangNpcTask, "~y~NPC~w~ Полли\n Банда", false);
                PedSystem.Repository.CreateQuest("a_m_m_afriamer_01", new Vector3(945.0789, -2170.6855, 30.551558), 142.86172f, 0, "npc_gang_task", ColShapeEnums.GangNpcTask, "~y~NPC~w~ Полли\n Банда", false);
                PedSystem.Repository.CreateQuest("a_m_m_afriamer_01", new Vector3(116.1713, -1953.8184, 20.751316), 46.621994f, 0, "npc_gang_task", ColShapeEnums.GangNpcTask, "~y~NPC~w~ Полли\n Банда", false);

                BuyDruygsLab[0] = (ExtTextLabel) NAPI.TextLabel.CreateTextLabel(LangFunc.GetText(LangType.Ru, DataName.TextLabelDrugs, Main.DrugsPrice), DrugPoints[0] + new Vector3(0, 0, 0.7), 5f, 0.3f, 0, new Color(255, 255, 255), true, 0);
                BuyDruygsLab[1] = (ExtTextLabel) NAPI.TextLabel.CreateTextLabel(LangFunc.GetText(LangType.Ru, DataName.TextLabelDrugs, Main.DrugsPrice), DrugPoints[1] + new Vector3(0, 0, 0.7), 5f, 0.3f, 0, new Color(255, 255, 255), true, 0);

                foreach (Vector3 pos in DrugPoints)
                {
                    NAPI.Marker.CreateMarker(1, pos - new Vector3(0, 0, 1.12), new Vector3(), new Vector3(), 4, new Color(255, 0, 0), false, 0);
                    //NAPI.Blip.CreateBlip(140, pos, 1f, 4, "Drugs", 255, 0, true, 0, 0);
                    Main.CreateBlip(new Main.BlipData(496, "Трава", pos, 25, true));

                    CustomColShape.CreateCylinderColShape(pos - new Vector3(0, 0, 1.12), 4, 5, 0, ColShapeEnums.DrugPoints);
                }
                
            }
            catch (Exception e)
            {
                Log.Write($"Event_OnResourceStart Exception: {e.ToString()}");
            }
        }

        [Interaction(ColShapeEnums.GangNpcTask)]
        public static void Open(ExtPlayer player, int index)
        {
            var sessionData = player.GetSessionData();
            if (sessionData == null) return;
            var characterData = player.GetCharacterData();
            if (characterData == null) return;

            var fractionData = player.GetFractionId();
            if (fractionData >= (int)Models.Fractions.FAMILY && fractionData <= (int)Models.Fractions.BLOOD)
            {
                if (Main.IHaveDemorgan(player, true)) return;

                player.SelectQuest(new PlayerQuestModel("npc_gang_task", 0, 0, false, DateTime.Now));
                Trigger.ClientEvent(player, "client.quest.open", index, "npc_gang_task", 0, 0, 0);
            }
       
        }

        [Interaction(ColShapeEnums.DrugPoints)]
        public static void OnDrugPoints(ExtPlayer player)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                if (!player.IsInVehicle)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouMustInNarcoCar), 3000);
                    return;
                }
                var vehicle = (ExtVehicle) player.Vehicle;
                var vehicleLocalData = vehicle.GetVehicleLocalData();
                if (vehicleLocalData != null)
                {
                    if (!vehicleLocalData.CanDrugs)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouMustInNarcoCar), 3000);
                        return;
                    }
                    var fracId = player.GetFractionId();
                    if (Manager.FractionTypes[fracId] != FractionsType.Gangs && Manager.FractionTypes[fracId] != FractionsType.Bikers)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouCantBuyNarco), 3000);
                        return;
                    }
                    if (!player.IsFractionAccess(RankToAccess.BuyDrugs)) return;
                    Trigger.ClientEvent(player, "openInput", LangFunc.GetText(LangType.Ru, DataName.BuyDrugs), LangFunc.GetText(LangType.Ru, DataName.HowMuch), 4, "buy_drugs");
                }
            }
            catch (Exception e)
            {
                Log.Write($"OnDrugPoints Exception: {e.ToString()}");
            }
        }

        public static void BuyDrugs(ExtPlayer player, int amount)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                if (!player.IsInVehicle)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouMustInNarcoCar), 3000);
                    return;
                }
                var vehicle = (ExtVehicle) player.Vehicle;
                var vehicleLocalData = vehicle.GetVehicleLocalData();
                if (vehicleLocalData != null)
                {
                    if (!vehicleLocalData.CanDrugs)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouMustInNarcoCar), 3000);
                        return;
                    }
                    var fractionData = player.GetFractionData();
                    if (fractionData == null || (Manager.FractionTypes[fractionData.Id] != FractionsType.Gangs && Manager.FractionTypes[fractionData.Id] != FractionsType.Bikers))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouCantBuyNarco), 3000);
                        return;
                    }
                    if (!player.IsFractionAccess(RankToAccess.BuyDrugs)) return;
                    if (fractionData.Money < amount * Main.DrugsPrice)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NotEnoughWarehouseGangMoney), 3000);
                        return;
                    }
                    int count = Chars.Repository.getCountItem(VehicleManager.GetVehicleToInventory(vehicle.NumberPlate), ItemId.Drugs);
                    if (count >= (vMain.GetMaxSlots(vehicle.Model) * Chars.Repository.ItemsInfo[ItemId.Drugs].Stack))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.CarMaxDrugs), 3000);
                        return;
                    }
                    else if ((count + amount) > (vMain.GetMaxSlots(vehicle.Model) * Chars.Repository.ItemsInfo[ItemId.Drugs].Stack))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.CarDrugsHowMuchCan, vMain.GetMaxSlots(vehicle.Model) * Chars.Repository.ItemsInfo[ItemId.Drugs].Stack), 8000);
                        return;
                    }
                    Chars.Repository.AddNewItem(null, VehicleManager.GetVehicleToInventory(vehicle.NumberPlate), "vehicle", ItemId.Drugs, amount, MaxSlots: vMain.GetMaxSlots(vehicle.Model));
                    fractionData.Money -= amount * Main.DrugsPrice;
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouBoughtNarcoGrams, amount), 5000);
                    Fractions.Table.Logs.Repository.AddLogs(player, FractionLogsType.TakeMoney, LangFunc.GetText(LangType.Ru, DataName.BuyNarco, amount));
                }
            }
            catch (Exception e)
            {
                Log.Write($"BuyDrugs Exception: {e.ToString()}");
            }
        }
    }
}
