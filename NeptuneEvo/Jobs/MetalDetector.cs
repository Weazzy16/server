using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Character;
using NeptuneEvo.Chars;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Core;
using NeptuneEvo.Fractions;
using NeptuneEvo.Functions;
using NeptuneEvo.MoneySystem;
using NeptuneEvo.Players;
using NeptuneEvo.Quests;
using NeptuneEvo.Quests.Models;
using Newtonsoft.Json;
using Redage.SDK;
using System;
using System.Collections.Generic;
using Localization;

namespace NeptuneEvo.Jobs
{
    class TreasuresData
    {
        public int TreasureIndex = 0;
        public Vector3 TreasurePosition;

        public ExtColShape TreasureShape;
        public GTANetworkAPI.Object TreasureObject;
        public byte TreasureStage = 0;
        public short TreasureHP = 100;
        public string RebirthTimer = null;
        public ExtPlayer StartedTreasureHitPlayerInfo { set; get; } = null;
        public ExtPlayer LastTreasureHitPlayer { set; get; } = null;
        public DateTime LastTreasureHitTime { set; get; } = DateTime.MinValue;

        public TreasuresData(int index, Vector3 TreasurePosition)
        {
            this.TreasureIndex = index;
            this.TreasurePosition = TreasurePosition;
        }
    }

    class Metaldetector : Script
    {
        private static readonly nLog Log = new nLog("Jobs.Metaldetector");

        private static Dictionary<int, TreasuresData> TreasuresData = new Dictionary<int, TreasuresData>();

        private static List<(string, Vector3)> TreasuresPositions = new List<(string, Vector3)>()
        {

            ("prop_snow_tree_04_f", new Vector3(-481.63535, -1160.133, 21.63349)),
            ("prop_snow_tree_04_f", new Vector3(-464.7431, -1172.6416, 22.41592)),
            ("prop_snow_tree_04_f", new Vector3(-468.1925, -1151.3473, 23.663685)),
            ("prop_snow_tree_04_f", new Vector3(-476.75443, -1127.6227, 23.67057)),
            ("prop_snow_tree_04_f", new Vector3(-490.60272, -1124.4965, 23.138412)),
            ("prop_snow_tree_04_f", new Vector3(-458.86343, -1188.6072, 21.689891)),
            ("prop_snow_tree_04_f", new Vector3(-447.7554, -1182.7928, 22.635908)),
            ("prop_snow_tree_04_f", new Vector3(-442.88788, -1196.6096, 22.664852)),
            ("prop_snow_tree_04_f", new Vector3(-445.90454, -1151.6956, 23.298677)),
            ("prop_snow_tree_04_f", new Vector3(-486.94104, -1143.0538, 23.219059)),
            ("prop_snow_tree_04_f", new Vector3(-474.45114, -1135.6971, 25.190186)),
            ("prop_snow_tree_04_f", new Vector3(-456.11792, -1151.8817, 24.563269)),
            ("prop_snow_tree_04_f", new Vector3(-458.828, -1175.5568, 22.576423)),
            ("prop_snow_tree_04_f", new Vector3(-450.9154, -1175.1796, 22.51234)),
            ("prop_snow_tree_04_f", new Vector3(-446.41922, -1166.6053, 23.078728)),
            ("prop_snow_tree_04_f", new Vector3(-468.37744, -1124.925, 26.565672)),
            ("prop_snow_tree_04_f", new Vector3(-484.61398, -1153.391, 21.5925)),
            ("prop_snow_tree_04_f", new Vector3(-460.06134, -1199.1066, 21.20713)),
            ("prop_snow_tree_04_f", new Vector3(-472.6164, -1182.0721, 20.361935)),
            ("prop_snow_tree_04_f", new Vector3(-448.71597, -1136.526, 25.326338))
        };

        private static List<int> TreasureStock = new List<int>()
        {
            0, // Дуб
            0, // Клен
            0  // Сосна
        };

        public static ExtTextLabel TreasureStockLabel { get; set; } = null;

        [ServerEvent(Event.ResourceStart)]
        public void onResourceStartHandler()
        {
            try
            {
                for (int i = 0; i < TreasuresPositions.Count; i++)
                {
                    TreasuresData.Add(i, new TreasuresData(i, new Vector3(TreasuresPositions[i].Item2.X, TreasuresPositions[i].Item2.Y, TreasuresPositions[i].Item2.Z + 1.25)));
                    TreasuresData[i].TreasureShape = CustomColShape.CreateSphereColShape(new Vector3(TreasuresPositions[i].Item2.X, TreasuresPositions[i].Item2.Y, TreasuresPositions[i].Item2.Z + 1.25), 1.75f, 0, ColShapeEnums.MetaldetectorTreasure, i);
                    TreasuresData[i].TreasureObject = NAPI.Object.CreateObject(NAPI.Util.GetHashKey(TreasuresPositions[i].Item1), new Vector3(TreasuresPositions[i].Item2.X, TreasuresPositions[i].Item2.Y, TreasuresPositions[i].Item2.Z - 0.45), new Vector3(0, 0, 0), 255, 0);
                }

                Main.CreateBlip(new Main.BlipData(468, LangFunc.GetText(LangType.Ru, DataName.LesniyeResi), new Vector3(-490.60272, -1124.4965, 25.138412), 2, true));
                Main.CreateBlip(new Main.BlipData(468, LangFunc.GetText(LangType.Ru, DataName.LesniyeResi), new Vector3(3370.0408, 4945.8154, 33.202995), 2, true));
                Main.CreateBlip(new Main.BlipData(468, LangFunc.GetText(LangType.Ru, DataName.LesniyeResi), new Vector3(-1319.7822, 4444.8164, 23.27308), 2, true));
                Main.CreateBlip(new Main.BlipData(468, LangFunc.GetText(LangType.Ru, DataName.LesniyeResi), new Vector3(-1988.7891, 2584.667, 3.311179), 2, true));
                Main.CreateBlip(new Main.BlipData(468, LangFunc.GetText(LangType.Ru, DataName.LesniyeResi), new Vector3(160.4275, 6895.9033, 20.979313), 2, true));

                Main.CreateBlip(new Main.BlipData(569, LangFunc.GetText(LangType.Ru, DataName.SkladLesa), new Vector3(-540.13434, 5380.038, 70.48429), 4, true));
                Main.CreateBlip(new Main.BlipData(480, LangFunc.GetText(LangType.Ru, DataName.SkladDereva), new Vector3(-575.54004, 5350.516, 70.214424), 70, true));

                Main.CreateBlip(new Main.BlipData(119, LangFunc.GetText(LangType.Ru, DataName.MetalMagaz), new Vector3(-752.59265, -623.9094, 30.237242), 23, true));

                TreasureStockLabel = (ExtTextLabel)NAPI.TextLabel.CreateTextLabel(Main.StringToU16(LangFunc.GetText(LangType.Ru, DataName.Hranilishe, TreasureStock[0], TreasureStock[1], TreasureStock[2])), new Vector3(-540.13434, 5380.038, 70.48429), 5F, 0.5F, 0, new Color(255, 255, 255), true, 0);

                PedSystem.Repository.CreateQuest("s_m_y_construct_01", new Vector3(-752.59265, -623.9094, 30.237242), -90.1f, title: LangFunc.GetText(LangType.Ru, DataName.Bear), colShapeEnums: ColShapeEnums.MetaldetectorShop);
                PedSystem.Repository.CreateQuest("s_m_y_construct_01", new Vector3(-752.145, -613.2575, 30.231133), 58.71f, title: LangFunc.GetText(LangType.Ru, DataName.DmitrySkupshik), colShapeEnums: ColShapeEnums.TreasuresSell);
            }
            catch (Exception e)
            {
                Log.Write($"onResourceStartHandler Exception: {e.ToString()}");
            }
        }
        [Interaction(ColShapeEnums.MetaldetectorShop, In: true)]
        public void OpenDialogIn(ExtPlayer player, int index)
        {
            BattlePass.Repository.UpdateReward(player, 38);
        }
        [Interaction(ColShapeEnums.MetaldetectorShop)]
        public void OpenDialog(ExtPlayer player, int index)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;

                if (sessionData.CuffedData.Cuffed)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsCuffed), 3000);
                    return;
                }
                else if (sessionData.DeathData.InDeath)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsDying), 3000);
                    return;
                }
                else if (Main.IHaveDemorgan(player, true)) return;

                player.SelectQuest(new PlayerQuestModel("npc_huntingshop", 0, 0, false, DateTime.Now));
                Trigger.ClientEvent(player, "client.quest.open", index, "npc_huntingshop", 0, 0, 0);
            }
            catch (Exception e)
            {
                Log.Write($"OpenDialog Exception: {e.ToString()}");
            }
        }

        [Interaction(ColShapeEnums.TreasuresSell)]
        public void OpenTreasuresSellDialog(ExtPlayer player, int index)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;

                if (sessionData.CuffedData.Cuffed)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsCuffed), 3000);
                    return;
                }
                else if (sessionData.DeathData.InDeath)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsDying), 3000);
                    return;
                }
                else if (Main.IHaveDemorgan(player, true)) return;

                player.SelectQuest(new PlayerQuestModel("npc_treessell", 0, 0, false, DateTime.Now));
                Trigger.ClientEvent(player, "client.quest.open", index, "npc_treessell", 0, 0, 0);
            }
            catch (Exception e)
            {
                Log.Write($"OpenDialog Exception: {e.ToString()}");
            }
        }

        public static void Perform(ExtPlayer player, byte index)
        {
            try
            {
                if (!player.IsCharacterData()) return;

                if (index == 1)
                {
                    List<Manager.FracMatsData> jsonData = new List<Manager.FracMatsData>();

                    if (Manager.FractionDataMats.ContainsKey(100)) jsonData.Add(Manager.FractionDataMats[100]);
                    if (Manager.FractionDataMats.ContainsKey(101)) jsonData.Add(Manager.FractionDataMats[101]);

                    Trigger.ClientEvent(player, "client.sm.openMetaldetectorShop", JsonConvert.SerializeObject(jsonData));
                }
                else if (index == 2)
                {
                    Trigger.ClientEvent(player, "resourceSell_openMenu", 1);
                }
            }
            catch (Exception e)
            {
                Log.Write($"Perform Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("server.sm.metaldetectorShop")]
        public static void callback_MetaldetectorShop(ExtPlayer player, int index)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                switch (index)
                {
                    case 100:
                        if (UpdateData.CanIChange(player, Main.PricesSettings.Instrument2Prices[0], true) != 255) return;
                        else if (Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", ItemId.Metaldetector, 1, "1250") == -1)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NoSpaceInventory), 3000);
                            return;
                        }
                        Wallet.Change(player, -Main.PricesSettings.Instrument2Prices[0]);
                        GameLog.Money($"player({characterData.UUID})", $"server", Main.PricesSettings.InstrumentPrices[0], $"buyMetaldetectorShop(100)");
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.BoughtMetaldetector), 3000);
                        BattlePass.Repository.UpdateReward(player, 57);
                        return;
                    
                    case 101:
                        if (UpdateData.CanIChange(player, Main.PricesSettings.Instrument2Prices[1], true) != 255) return;
                        else if (Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", ItemId.Shovel, 1, "3000") == -1)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NoSpaceInventory), 3000);
                            return;
                        }
                        Wallet.Change(player, -Main.PricesSettings.Instrument2Prices[1]);
                        GameLog.Money($"player({characterData.UUID})", $"server", Main.PricesSettings.InstrumentPrices[3], $"buyMetaldetectorShop(101)");
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.BoughtShovel), 3000);
                        BattlePass.Repository.UpdateReward(player, 57);
                        return;
                }
            }
            catch (Exception e)
            {
                Log.Write($"callback_MetaldetectorShop Exception: {e.ToString()}");
            }
        }

        [Interaction(ColShapeEnums.MetaldetectorTreasure, In: true)]
        public static void OnEnterTreasureShape(ExtPlayer player, int TreasureIndex)
        {
            try
            {
                if (!player.IsCharacterData()) return;
                if (TreasuresData.ContainsKey(TreasureIndex))
                {
                    var treesData = TreasuresData[TreasureIndex];
                    if (treesData.TreasureStage > 1) return;
                    Trigger.ClientEvent(player, "metaldetectorJob_setTreasureInfo", TreasureIndex, treesData.TreasureStage, treesData.TreasurePosition, treesData.TreasureHP);
                }
            }
            catch (Exception e)
            {
                Log.Write($"OnEnterTreasureShape Exception: {e.ToString()}");
            }
        }

        [Interaction(ColShapeEnums.MetaldetectorTreasure, Out: true)]
        public static void OnExitTreasureShape(ExtPlayer player, int TreasureIndex)
        {
            try
            {
                if (!player.IsCharacterData()) return;
                Trigger.ClientEvent(player, "metaldetectorJob_clearInfo");
            }
            catch (Exception e)
            {
                Log.Write($"OnExitTreasureShape Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("Metaldetector_HitTreasure")]
        public static void Metaldetector_HitTreasure(ExtPlayer player, int TreasureIndex)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;

                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                if (!FunctionsAccess.IsWorking("Metaldetector_HitTreasure"))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.FunctionOffByAdmins), 3000);
                    return;
                }

                if (sessionData.CuffedData.Cuffed)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsCuffed), 3000);
                    return;
                }
                else if (sessionData.DeathData.InDeath)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsDying), 3000);
                    return;
                }
                else if (sessionData.AnimationUse != null || Main.IHaveDemorgan(player, true)) return;

                /*if (DateTime.Now.Hour > 9 && DateTime.Now.Hour != 22 && DateTime.Now.Hour != 23)
                { // todo translate
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.OgranVremyaMiner), 3000);
                    return;
                }*/

                if (TreasuresData.ContainsKey(TreasureIndex))
                {
                    var treesData = TreasuresData[TreasureIndex];
                    if (treesData.TreasureStage != 0) return;

                    ItemStruct Metaldetector = Chars.Repository.isItem(player, "inventory", ItemId.Metaldetector);
                    if (Metaldetector == null)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouHaveNoAxe), 3000);
                        return;
                    }

                    if (treesData.StartedTreasureHitPlayerInfo != null)
                    {
                        if (!treesData.StartedTreasureHitPlayerInfo.IsCharacterData()) treesData.StartedTreasureHitPlayerInfo = null;
                        else if (treesData.StartedTreasureHitPlayerInfo.Position.DistanceTo(treesData.TreasurePosition) >= 3) treesData.StartedTreasureHitPlayerInfo = null;
                        else if (treesData.StartedTreasureHitPlayerInfo == player) return;
                        else
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.SomebodyCutting), 3000);
                            return;
                        }
                    }

                    if (treesData.LastTreasureHitPlayer != null && treesData.LastTreasureHitPlayer != player)
                    {
                        if (!treesData.LastTreasureHitPlayer.IsCharacterData()) treesData.LastTreasureHitPlayer = null;
                        else if (treesData.LastTreasureHitPlayer.Position.DistanceTo(treesData.TreasurePosition) >= 3) treesData.LastTreasureHitPlayer = null;
                        else if (treesData.LastTreasureHitTime > DateTime.Now)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.SomebodyCutting), 3000);
                            return;
                        }
                    }

                    //Main.OnAntiAnim(player);
                    //Trigger.PlayAnimation(player, "melee@large_wpn@streamed_core", "car_side_attack_a", 1);
                    //Attachments.AddAttachment(player, Attachments.AttachmentsName.MetaldetectorProp);
                    Metaldetector_StartAnimCutTreasure(player);

                    if (Metaldetector.Item.Data == "" || Metaldetector.Item.Data.Length < 1)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.AxeBroken), 3000);
                        Chars.Repository.Remove(player, $"char_{characterData.UUID}", "inventory", Metaldetector.Item.ItemId, 1);
                    }
                    else if ((Convert.ToInt32(Metaldetector.Item.Data) - 6) <= 0)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.AxeBroken), 3000);
                        Chars.Repository.Remove(player, $"char_{characterData.UUID}", "inventory", Metaldetector.Item.ItemId, 1);
                    }
                    else
                    {
                        Metaldetector.Item.Data = $"{Convert.ToInt32(Metaldetector.Item.Data) - 6}";
                        Chars.Repository.SetItemData(player, Metaldetector.Location, Metaldetector.Index, Metaldetector.Item, true);
                    }

                    treesData.StartedTreasureHitPlayerInfo = player;
                    treesData.LastTreasureHitPlayer = player;
                    treesData.LastTreasureHitTime = DateTime.Now.AddSeconds(30);

                    Trigger.ClientEvent(player, "metaldetectorJob_startProcess");
                }
            }
            catch (Exception e)
            {
                Log.Write($"Metaldetector_HitTreasure Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("Metaldetector_CutTreasure")]
        public static void Metaldetector_CutTreasure(ExtPlayer player, int TreasureIndex)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;

                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                if (!FunctionsAccess.IsWorking("Metaldetector_CutTreasure"))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.FunctionOffByAdmins), 3000);
                    return;
                }

                if (sessionData.CuffedData.Cuffed)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsCuffed), 3000);
                    return;
                }
                else if (sessionData.DeathData.InDeath)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsDying), 3000);
                    return;
                }
                else if (Main.IHaveDemorgan(player, true)) return;

                Main.OffAntiAnim(player);
                Trigger.StopAnimation(player);
                Attachments.RemoveAttachment(player, Attachments.AttachmentsName.MetaldetectorProp);

                if (TreasuresData.ContainsKey(TreasureIndex))
                {
                    var treesData = TreasuresData[TreasureIndex];
                    if (treesData.TreasureStage != 0) return;

                    treesData.TreasureHP = 0;
                    treesData.TreasureStage = 1;
                    treesData.LastTreasureHitPlayer = null;

                    if (treesData.TreasureObject != null && treesData.TreasureObject.Exists)
                        treesData.TreasureObject.Delete();

                    treesData.TreasureObject = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_tree_stump_01"), new Vector3(TreasuresPositions[TreasureIndex].Item2.X, TreasuresPositions[TreasureIndex].Item2.Y, TreasuresPositions[TreasureIndex].Item2.Z - 0.3), new Vector3(0, 0, 0), 255, 0);

                    var item_chance = new Random().Next(1, 100);
                    var item_type = ItemId.WoodOak;

                    if (item_chance <= 33)
                        item_type = ItemId.WoodOak;
                    else if (item_chance <= 66)
                        item_type = ItemId.WoodMaple;
                    else
                        item_type = ItemId.WoodPine;

                    if (Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", item_type, 1) == -1)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NoMoreLogs), 3000);
                    }
                    else
                    {
                        treesData.TreasureStage = 2;
                    }

                    NAPI.ClientEvent.TriggerClientEventInRange(treesData.TreasurePosition, 2.5f, "metaldetectorJob_updateTreasureInfo", TreasureIndex, treesData.TreasureStage, treesData.TreasureHP);

                    if (treesData.RebirthTimer != null)
                    {
                        Timers.Stop(treesData.RebirthTimer);
                    }

                    treesData.RebirthTimer = Timers.StartOnce($"TreasureRebirthTimer{TreasureIndex}", 15000, () => TreasureRebirthTimerFunction(TreasureIndex), true);

                    //BattlePass.Repository.UpdateReward(player, 54);
                }
            }
            catch (Exception e)
            {
                Log.Write($"Metaldetector_CutTreasure Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("Metaldetector_StartAnimCutTreasure")]
        public static void Metaldetector_StartAnimCutTreasure(ExtPlayer player)
        {
            try
            {
                if (!player.IsCharacterData()) return;

                /*if (DateTime.Now.Hour > 9 && DateTime.Now.Hour != 22 && DateTime.Now.Hour != 23)
                { // todo translate
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.OgranVremyaMiner), 3000);
                    Trigger.ClientEvent(player, "metaldetectorJob_stopProcess");
                    return;
                }*/

                Main.OnAntiAnim(player);
                Trigger.PlayAnimation(player, "melee@large_wpn@streamed_core", "car_side_attack_a", 1);
                // Trigger.ClientEventInRange(player.Position, 250f, "PlayAnimToKey", player, false, "metaldetector");
                Attachments.AddAttachment(player, Attachments.AttachmentsName.MetaldetectorProp);
            }
            catch (Exception e)
            {
                Log.Write($"Metaldetector_StartAnimCutTreasure Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("Metaldetector_StopCutTreasure")]
        public static void Metaldetector_StopCutTreasure(ExtPlayer player, int TreasureIndex)
        {
            try
            {
                if (!player.IsCharacterData()) return;

                Main.OffAntiAnim(player);
                Trigger.StopAnimation(player);
                Attachments.RemoveAttachment(player, Attachments.AttachmentsName.MetaldetectorProp);

                if (TreasuresData.ContainsKey(TreasureIndex))
                    TreasuresData[TreasureIndex].StartedTreasureHitPlayerInfo = null;
            }
            catch (Exception e)
            {
                Log.Write($"Metaldetector_StopCutTreasure Exception: {e.ToString()}");
            }
        }

        private static List<ItemId> GetItems()
        {
            return new List<ItemId>()
            {
                ItemId.WoodOak,
                ItemId.WoodMaple,
                ItemId.WoodPine,
            };
        }
        [RemoteEvent("Metaldetector_TakeTimber")]
        public static void Metaldetector_TakeTimber(ExtPlayer player, int TreasureIndex)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;

                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                if (!player.IsCharacterData()) return;

                if (sessionData.CuffedData.Cuffed)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsCuffed), 3000);
                    return;
                }
                else if (sessionData.DeathData.InDeath)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsDying), 3000);
                    return;
                }
                else if (sessionData.AnimationUse != null || Main.IHaveDemorgan(player, true)) return;

                if (TreasuresData.ContainsKey(TreasureIndex))
                {
                    var treesData = TreasuresData[TreasureIndex];
                    if (treesData.TreasureStage != 1) return;

                    int item_chance = new Random().Next(1, 100);
                    ItemId item_type = ItemId.WoodOak;

                    if (item_chance <= 33) item_type = ItemId.WoodOak;
                    else if (item_chance > 33 && item_chance <= 66) item_type = ItemId.WoodMaple;
                    else if (item_chance > 66) item_type = ItemId.WoodPine;

                    if (Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", item_type, 1) == -1)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NoMoreLogs), 3000);
                        return;
                    }

                    Trigger.TaskPlayAnim(player, "random@domestic", "pickup_low", 39);

                    treesData.TreasureStage = 2;

                    NAPI.ClientEvent.TriggerClientEventInRange(treesData.TreasurePosition, 2.5f, "metaldetectorJob_updateTreasureInfo", TreasureIndex, treesData.TreasureStage, treesData.TreasureHP);
                }
            }
            catch (Exception e)
            {
                Log.Write($"Metaldetector_TakeTimber Exception: {e.ToString()}");
            }
        }

        private static void TreasureRebirthTimerFunction(int TreasureIndex)
        {
            try
            {
                if (TreasuresData.ContainsKey(TreasureIndex))
                {
                    var treesData = TreasuresData[TreasureIndex];
                    if (treesData.TreasureStage == 0) return;

                    treesData.TreasureHP = 100;
                    treesData.TreasureStage = 0;
                    treesData.StartedTreasureHitPlayerInfo = null;

                    if (treesData.TreasureObject != null && treesData.TreasureObject.Exists)
                        treesData.TreasureObject.Delete();

                    treesData.TreasureObject = NAPI.Object.CreateObject(NAPI.Util.GetHashKey(TreasuresPositions[TreasureIndex].Item1), new Vector3(TreasuresPositions[TreasureIndex].Item2.X, TreasuresPositions[TreasureIndex].Item2.Y, TreasuresPositions[TreasureIndex].Item2.Z - 0.45), new Vector3(0, 0, 0), 255, 0);

                    NAPI.ClientEvent.TriggerClientEventInRange(treesData.TreasurePosition, 2.5f, "metaldetectorJob_updateTreasureInfo", TreasureIndex, treesData.TreasureStage, treesData.TreasureHP);
                }
            }
            catch (Exception e)
            {
                Log.Write($"TreasureRebirthTimerFunction Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("PlayerSellTreasures")]
        public static void PlayerSellTreasures(ExtPlayer player, int stock_item_index, int amount)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;

                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                if (!FunctionsAccess.IsWorking("PlayerSellTreasures"))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.FunctionOffByAdmins), 3000);
                    return;
                }
                else if (TreasureStock.Count < stock_item_index) return;

                ItemId item_type = ItemId.WoodOak;
                int price = Main.MetaldetectorPrice.OakPrice;

                if (stock_item_index == 1)
                {
                    item_type = ItemId.WoodMaple;
                    price = Main.MetaldetectorPrice.MaplePrice;
                }
                else if (stock_item_index == 2)
                {
                    item_type = ItemId.WoodPine;
                    price = Main.MetaldetectorPrice.PinePrice;
                }

                ItemStruct woodamount = Chars.Repository.isItem(player, "inventory", item_type);
                int count = (woodamount == null) ? 0 : woodamount.Item.Count;
                bool success = false;

                if (count < amount)
                {
                    count = Chars.Repository.getCountToLacationItem($"char_{characterData.UUID}", "inventory", item_type);
                    if (count >= amount)
                    {
                        success = true;
                        for (int i = 0; i < amount; i++) Chars.Repository.Remove(player, $"char_{characterData.UUID}", "inventory", item_type, 1);
                    }
                }
                else
                {
                    success = true;
                    Chars.Repository.Remove(player, $"char_{characterData.UUID}", "inventory", item_type, amount);
                }

                if (success == true)
                {
                    int totalmoney = Convert.ToInt32(price * amount);
                    Wallet.Change(player, totalmoney);
                    GameLog.Money($"server", $"player({characterData.UUID})", totalmoney, $"sellTreasures({stock_item_index},{amount})");

                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.SellResources, Wallet.Format(totalmoney)), 3000);
                    //BattlePass.Repository.UpdateReward(player, 59);

                    TreasureStock[stock_item_index] += amount;
                    TreasureStockLabel.Text = Main.StringToU16(LangFunc.GetText(LangType.Ru, DataName.Hranilishe, TreasureStock[0], TreasureStock[1], TreasureStock[2]));

                    Perform(player, 1);

                    if (qMain.GetQuestsLine(player, Zdobich.QuestName) == (int)zdobich_quests.Stage11)
                    {
                        sessionData.WorkData.PointsCount += totalmoney;
                        if (sessionData.WorkData.PointsCount < qMain.GetQuestsData(player, Zdobich.QuestName, (int)zdobich_quests.Stage11))
                            sessionData.WorkData.PointsCount = qMain.GetQuestsData(player, Zdobich.QuestName, (int)zdobich_quests.Stage11) + totalmoney;

                        if (sessionData.WorkData.PointsCount >= 500)
                        {
                            qMain.UpdateQuestsStage(player, Zdobich.QuestName, (int)zdobich_quests.Stage11, 1, isUpdateHud: true);
                            qMain.UpdateQuestsComplete(player, Zdobich.QuestName, (int)zdobich_quests.Stage11, true);
                            Trigger.SendChatMessage(player, "!{#fc0}" + LangFunc.GetText(LangType.Ru, DataName.QuestPartComplete));
                        }
                        else
                        {
                            qMain.UpdateQuestsData(player, Zdobich.QuestName, (int)zdobich_quests.Stage11, sessionData.WorkData.PointsCount.ToString());
                            //todo translate (было DataName.PointsQuestGot)
                            Trigger.SendChatMessage(player, LangFunc.GetText(LangType.Ru, DataName.YouEarnedJob, sessionData.WorkData.PointsCount, 500 - sessionData.WorkData.PointsCount));
                        }
                    }
                }
                else Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NotEnoughRes), 3000);
            }
            catch (Exception e)
            {
                Log.Write($"PlayerSellOres Exception: {e.ToString()}");
            }
        }
    }
}