using System;
using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Character;
using Redage.SDK;
using NeptuneEvo.Accounts;
using NeptuneEvo.Players;
using NeptuneEvo.Core;
using NeptuneEvo.Fractions.Player;
using Database;
using GTANetworkMethods;
using Localization;
using System.Text.RegularExpressions;
using LinqToDB;
using System.Linq;
using System.Collections.Generic;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Houses;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms;
using Google.Protobuf.WellKnownTypes;
using LinqToDB.Common;
using System.Threading.Tasks;
using System.Numerics;
using NeptuneEvo.Database.Models;
using NeptuneEvo.Fractions.Models;
using System.Reflection;
using NeptuneEvo.Chars;
using static NeptuneEvo.Core.ReportSys;
using Ubiety.Dns.Core;
using Org.BouncyCastle.Asn1.X509;
using System.Security.Principal;

namespace NeptuneEvo.AleSystems
{
    class AdminConsole : Script
    {
        public static readonly nLog Log = new nLog(nameof(AdminConsole));

        [ServerEvent(Event.ResourceStart)]
        public void onResourceStart()
        {
            try
            {
                Log.Write("Админ консоль загружена ");
            }
            catch (Exception e)
            {
                Log.Write($"onResourceStart Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("server.adminConsole.try_getPlayer")]
        public async void TryGetPlayerData(ExtPlayer player, string playerIdOrNick)
        {
            try
            {
                if (!int.TryParse(playerIdOrNick, out var playerId))
                {
                    if (!Main.PlayerUUIDs.TryGetValue(playerIdOrNick, out playerId))
                    {
                        SendPlayerData(player, null);
                        return;
                    }
                }

                var targetData = await GetData(playerId);
                SendPlayerData(player, targetData);
            }
            catch (Exception ex) { Log.Write(nameof(TryGetPlayerData) + ": " + ex.ToString()); }
        }

        public async Task<object> GetData(int uuid)
        {
            var target = Main.GetPlayerByUUID(uuid);

            await using var db = new ServerBD("MainDB");
            var characterData = db.Characters.Find(uuid);
            if (characterData == null)
                return null;

            var login = Main.GetLoginFromUUID(uuid);
            if (login.IsNullOrEmpty())
                return null;

            var accountData = db.Accounts.Find(login);
            if (accountData is null)
                return null;

            var ban = await db.Banned
                .Where(v => v.Uuid == characterData.Uuid)
                .Where(v => v.Until > DateTime.Now)
                .FirstOrDefaultAsync();

            var nickname = $"{characterData.Firstname}_{characterData.Lastname}";

            var organizationData = Organizations.Manager.GetOrganizationMemberData(uuid);
            var fractionData = Fractions.Manager.GetFractionMemberData(uuid);

            var house = HouseManager.Houses.Find(x => x.Owner == nickname);

            string numberInfo = characterData.Sim == -1 ? "Нет" : characterData.Sim.ToString();

            string fraction = fractionData is null ? "Нет" : $"{Fractions.Manager.FractionNames[fractionData.Id]} ({fractionData.Id})";

            var warnInfo = target is null ? JsonConvert.DeserializeObject<WarnInfo>(characterData.Warninfo) : target.CharacterData.WarnInfo;
            var warns = target is null ? (int)characterData.Warns : target.CharacterData.Warns;

            var models = VehicleManager.GetVehiclesCarModelToPlayer(nickname);
            var numbers = VehicleManager.GetVehiclesCarNumberToPlayer(nickname);
            var vehs = RAGE.Entities.Vehicles.All.Cast<ExtVehicle>();
            int? id = null;
            foreach (ExtVehicle veh in vehs)
            {
                if (veh.NumberPlate != null || numbers.Contains(veh.NumberPlate)) continue;
                id = veh.Id;
                break;
            }

            var vehiclesArray = models.Select((model, index) => new object[]
            {
                model,
                numbers[index]
            }).ToArray();

            return new
            {
                gId = target is null ? -1 : target.Value,
                cId = characterData.Uuid,
                nick = nickname,
                phone = numberInfo,
                level = target is null ? characterData.Lvl : target.CharacterData.LVL,
                cash = target is null ? (long)characterData.Money : target.CharacterData.Money,
                bank = MoneySystem.Bank.GetBalance((int)characterData.Bank),
                faction = fraction,
                family = organizationData is null ? -1 : organizationData.Id,
                houseId = house is null ? -1 : house.ID,
                apartmentId = "Нет",
                bizList = target is null ? JsonConvert.DeserializeObject<List<int>>(characterData.Biz) : target.CharacterData.BizIDs,
                warnNow = warns == 0 ? "Нет" : warnInfo.Reason[warns - 1],
                warnCount = warns,
                banStatus = await Ban.GetBanToUUID(db, uuid) is null ? "Нет" : $"Активен ( До {ban.Until.ToString("dd")}.{ban.Until.ToString("MM")}.{ban.Until.ToString("yy")} {ban.Until.ToString("HH")}:{ban.Until.ToString("mm")}:{ban.Until.ToString("ss")} | H: {ban.Ishard} | {ban.Reason} - {ban.Byadmin} )",
                prisonTime = target is null ? characterData.Demorgan : target.CharacterData.DemorganTime,
                jailTime = target is null ? characterData.Arrest : target.CharacterData.ArrestTime,
                login = login,
                socialClub = accountData.Socialclub,
                vehicles = vehiclesArray
            };
        }

        public void SendPlayerData(ExtPlayer player, object data)
        {
            Trigger.ClientEvent(player, "client.adminConsole.setPlayerData", JsonConvert.SerializeObject(data));
        }
        [RemoteEvent("server.adminConsole.try_getReportList")]
        public async void TryGetReportList(ExtPlayer player)
        {
            try
            {
                var targetData = await GetData();
                SendReportList(player, targetData);
            }
            catch (Exception ex)
            {
                Log.Write(nameof(TryGetReportList) + ": " + ex.ToString());
            }
        }

        public async Task<object> GetData()
        {
            await using ServerBD db = new ServerBD("MainDB");

            var results = new List<object>();

            foreach (Report report in Reports.Values)
            {
                try
                {
                    ExtPlayer author = (ExtPlayer)NAPI.Player.GetPlayerFromName(report.Author);
                    if (author != null)
                    {
                        var playerId = author.Value;
                        var playerUuid = author.GetUUID();
                        var IsMedia = false;
                        var IsLeader = false;
                        if (Main.Media.Contains(playerUuid)) IsMedia = true;
                        if (author.HasData("leader")) IsLeader = true;

                        results.Add(new
                        {
                            number = report.ID,
                            id = playerId,
                            cId = playerUuid,
                            hasAns = report.BlockedBy.Length > 1,
                            media = IsMedia,
                            leader = IsLeader
                        });
                    }
                    else
                    {
                        var playerId = "Не в сети";
                        var playerUuid = Main.PlayerUUIDs[report.Author];
                        var IsMedia = false;
                        var IsLeader = false;
                        if (Main.Media.Contains(playerUuid)) IsMedia = true;

                        results.Add(new
                        {
                            number = report.ID,
                            id = playerId,
                            cId = playerUuid,
                            hasAns = report.BlockedBy.Length > 1,
                            media = IsMedia,
                            leader = IsLeader
                        });
                    }
                }
                catch (Exception e)
                {
                    Log.Write($"GetData Foreach Exception: {e}");
                }
            }

            return results;
        }

        public void SendReportList(ExtPlayer player, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            Trigger.ClientEvent(player, "client.adminConsole.setReportList", jsonData);
        }

        [RemoteEvent("server.adminConsole.try_getReportData")]
        public async void TryGetReportData(ExtPlayer player, int reportNumber)
        {
            try
            {
                var targetData = await GetReportData(reportNumber);
                SendReportData(player, targetData);
            }
            catch (Exception ex) { Log.Write(nameof(TryGetReportData) + ": " + ex.ToString()); }
        }

        public async Task<object> GetReportData(int reportNumber)
        {
            await using ServerBD db = new ServerBD("MainDB");

            var results = new List<object>();

            if (Reports.TryGetValue(reportNumber, out Report report))
            {
                try
                {
                    ExtPlayer author = (ExtPlayer)NAPI.Player.GetPlayerFromName(report.Author);
                    if (author != null)
                    {
                        var playerId = author.Value;
                        var playerUuid = author.GetUUID();
                        var IsMedia = false;
                        var IsLeader = false;
                        if (Main.Media.Contains(playerUuid)) IsMedia = true;
                        if (author.HasData("leader")) IsLeader = true;

                        results.Add(new
                        {
                            number = report.ID,
                            id = playerId,
                            cId = playerUuid,
                            hasAns = report.BlockedBy.Length > 1,
                            media = IsMedia,
                            leader = IsLeader
                        });
                    }
                    else
                    {
                        var playerId = "Не в сети";
                        var playerUuid = Main.PlayerUUIDs[report.Author];
                        var IsMedia = false;
                        var IsLeader = false;
                        if (Main.Media.Contains(playerUuid)) IsMedia = true;

                        results.Add(new
                        {
                            number = report.ID,
                            id = playerId,
                            cId = playerUuid,
                            hasAns = report.BlockedBy.Length > 1,
                            media = IsMedia,
                            leader = IsLeader
                        });
                    }
                }
                catch (Exception e)
                {
                    Log.Write($"GetReportData Foreach Exception: {e}");
                }
            }

            return results;
        }

        public void SendReportData(ExtPlayer player, object data)
        {
            foreach (Report report in Reports.Values)
            {
                try
                {
                    if (report.BlockedBy.Length > 1)
                    {
                        Trigger.ClientEvent(player, "console_notifyError", $"Этот репорт уже взят другим администратором [{report.BlockedBy}].");
                        return;
                    }
                    else if (report.BlockedBy.Length < 1)
                    {
                        var jsonData = JsonConvert.SerializeObject(data);
                        Trigger.ClientEvent(player, "client.adminConsole.setReportData", jsonData);

                        ExtPlayer author = (ExtPlayer)NAPI.Player.GetPlayerFromName(report.Author);
                        if (author != null)
                        {
                            Trigger.ClientEvent(player, "c_admin_report", report.ID, $"{report.Author}[{author.Value}]", report.Question);
                        }
                        else
                        {
                            Trigger.ClientEvent(player, "c_admin_report", report.ID, $"{report.Author}[Не в сети]", report.Question);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write($"ReportMessage Foreach Exception: {ex}");
                }
            }
        }
        [RemoteEvent("server_reportSystem_deleteReport")]
        private void server_reportSystem_deleteReport(ExtPlayer player, string e)
        {
            try
            {
                Log.Write("server_reportSystem_deleteReport call good");
            }
            catch (Exception ex)
            {
                Log.Write($"server_reportSystem_deleteReport Exception: {ex}");
            }
        }
        [RemoteEvent("server:AdminPassEnter")]
        private void server_AdminPassEnter(ExtPlayer player, string pass)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                var accountData = player.GetAccountData();
                if (accountData == null) return;

                var hashedPass = Accounts.Repository.GetSha256(pass);
                if (characterData.AdminLVL < 1) return;
                if (characterData.AdminPass == null || characterData.AdminPass == "null")
                {
                    Regex rg = new Regex(@"^[a-z0-9]+$", RegexOptions.IgnoreCase);
                    if (!rg.IsMatch(pass))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.PassRestrcSymbols), 3000);
                        return;
                    }
                    if (accountData.Password == hashedPass)
                    {
                        Trigger.ClientEvent(player, "console_notifyError", "Нельзя использовать пароль как и от игрового аккаунта.");
                        return;
                    }
                    Trigger.SetTask(async () =>
                    {
                        try
                        {
                            await using var db = new ServerBD("MainDB");

                            await db.Characters
                            .Where(v => v.Uuid == characterData.UUID)
                            .Set(v => v.AdminPass, hashedPass)
                            .UpdateAsync();
                        }
                        catch (Exception e)
                        {
                            Debugs.Repository.Exception(e);
                        }
                    });
                    Trigger.ClientEvent(player, "server:AdminPassResponse", true);
                }
                else
                {
                    if (characterData.AdminPass != hashedPass)
                    {
                        Trigger.ClientEvent(player, "console_notifyError", "Неправильный пароль.");
                        return;
                    }
                    Trigger.SetTask(async () =>
                    {
                        try
                        {
                            await using var db = new ServerBD("MainDB");

                            await db.Characters
                            .Where(v => v.Uuid == characterData.UUID)
                            .Set(v => v.AdminPass, hashedPass)
                            .UpdateAsync();
                        }
                        catch (Exception e)
                        {
                            Debugs.Repository.Exception(e);
                        }
                    });
                    Trigger.ClientEvent(player, "server:AdminPassResponse", true);
                }
            }
            catch (Exception ex)
            {
                Log.Write($"server_AdminPassEnter Exception: {ex}");
            }
        }
        [RemoteEvent("server_adminConsole_chat_sendText")]
        private void ClientEvent_AdminChat(ExtPlayer player, string e)
        {
            try
            {
                var dateTime = DateTime.Now;
                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                string time = $"{dateTime:HH:mm:ss}";
                string a = $"[{time}] {player.Name}[{player.Value}]: {e}";

                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {
                    var foreachCharacterData = foreachPlayer.GetCharacterData();
                    if (foreachCharacterData == null) continue;
                    if (foreachCharacterData.AdminLVL >= 1) Trigger.ClientEvent(foreachPlayer, "c_admin_chat", a);
                }
            }
            catch (Exception ex)
            {
                Log.Write($"ClientEvent_AdminChat Exception: {ex}");
            }
        }
        public static void AntiCheatLog(ExtPlayer player, string trigger)
        {
            try
            {
                var dateTime = DateTime.Now;
                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                string time = $"{dateTime:HH:mm:ss}";
                string a = $"[{time}] {player.Name} | ID: {player.Value} | #{characterData.UUID} - {trigger}";

                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {
                    var foreachCharacterData = foreachPlayer.GetCharacterData();
                    if (foreachCharacterData == null) continue;
                    if (foreachCharacterData.AdminLVL >= 4) Trigger.ClientEvent(foreachPlayer, "c_admin_cheatl", a);
                }
            }
            catch (Exception ex)
            {
                Log.Write($"AntiCheatLog Exception: {ex}");
            }
        }
        public static void PlayerLog(ExtPlayer player)
        {
            try
            {
                var dateTime = DateTime.Now;
                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                var sessionData = player.GetSessionData();
                if (sessionData == null) return;

                string time = $"{dateTime:HH:mm:ss}";
                string a = $"[{time}] {player.Name} | UUID: {characterData.UUID} | ID: {player.Value} | SC: {sessionData.SocialClubName} | IP: {sessionData.Address} зашёл на сервер.";

                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {
                    var foreachCharacterData = foreachPlayer.GetCharacterData();
                    if (foreachCharacterData == null) continue;
                    if (foreachCharacterData.AdminLVL >= 4) Trigger.ClientEvent(foreachPlayer, "c_admin_alog", a);
                }
            }
            catch (Exception ex)
            {
                Log.Write($"PlayerLog Exception: {ex}");
            }
        }
        public static void AdminCLog(string Admin, string Action, string Player)
        {
            try
            {
                var dateTime = DateTime.Now;

                string time = $"{dateTime:HH:mm:ss}";
                string a = $"[{time}] {Admin} использует команду {Action} [{Player}]";

                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {
                    var foreachCharacterData = foreachPlayer.GetCharacterData();
                    if (foreachCharacterData == null) continue;
                    if (foreachCharacterData.AdminLVL >= 4) Trigger.ClientEvent(foreachPlayer, "c_admin_clog", a);
                }
            }
            catch (Exception ex)
            {
                Log.Write($"AdminCLog Exception: {ex}");
            }
        }
        public static void KillLog(string a, int b, string c, int d, uint e)
        {
            try
            {
                var dateTime = DateTime.Now;

                string time = $"{dateTime:HH:mm:ss}";
              if (Fractions.Ems.weaponHashToString.TryGetValue(e, out string ee))
              {
                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {
                    var foreachCharacterData = foreachPlayer.GetCharacterData();
                    if (foreachCharacterData == null) continue;
                    string g = $"[{time}] {a}[{b}] убил(а) {c}[{d}] - {ee}";
                    if (foreachCharacterData.AdminLVL >= 3) Trigger.ClientEvent(foreachPlayer, "c_admin_klog", g);
                }
              }
            }
            catch (Exception ex)
            {
                Log.Write($"KillLog Exception: {ex}");
            }
        }
        [RemoteEvent("loadbackhud")]
        private void ServerEvent_loadbackhud(ExtPlayer player)
        {
            try
            {
                NeptuneEvo.Trigger.ClientEvent(player, "client.init",
                Main.ServerSettings.ServerId,
                Main.ServerSettings.ServerName,
                Main.DonateSettings.Multiplier,
                Main.DonateSettings.Convert,
                Main.ServerSettings.IsMerger);
            }
            catch (Exception e)
            {
                Log.Write($"ServerEvent_loadbackhud Exception: {e}");
            }
        }
        [Command("alogin13")]
        private static void CMD_alogin13(ExtPlayer player)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                if (characterData.AdminLVL < 1) return;

                NeptuneEvo.Trigger.ClientEvent(player, "client_adminConsole_ALogin");
            }
            catch (Exception e)
            {
                Log.Write($"CMD_alogin13 Exception: {e.ToString()}");
            }
        }
        [RemoteEvent("server_adminConsole_command")]
        private void ClientEvent_UseCommand(ExtPlayer player, string e, int id, int time, string reason)
        {
            try
            {
                if (e == "ban")
                {
                    Admin.banPlayer(player, Main.GetPlayerByID(id), time, reason);
                }
                else if (e == "jail")
                {
                    Admin.sendPlayerToDemorgan(player, Main.GetPlayerByID(id), time, reason);
                }
                else if(e == "history")
                {
                    Commands.CMD_NickHistory(player, id);
                }
            }
            catch (Exception ex)
            {
                Log.Write($"ClientEvent_UseCommand Exception: {ex}");
            }
        }
    }
}