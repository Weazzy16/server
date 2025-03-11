using Redage.SDK;
using GTANetworkAPI;
using System;
using NeptuneEvo.Core;
using System.Collections.Generic;
using System.Text;
using NeptuneEvo.Core;
using NeptuneEvo;
using Redage.SDK;
using NeptuneEvo.Handles;
using NeptuneEvo.Character;
using NeptuneEvo.Fractions.Player;
using Localization;

namespace NeptuneEvo.Fractions
{
    class CodeRing : Script
    {

        #region Инициализация
        private static nLog RLog = new nLog("fmark");
        private static Random rnd = new Random();
        //
        private static GTANetworkAPI.Blip _markBlip;
        //
        public static bool _isStart = false;
        //
        #endregion

        #region Methods
        public static void SpawnAnCode(ExtPlayer player, int code)
        {
            var memberFractionData = player.GetFractionMemberData();
            if (memberFractionData == null)
                return;

            var fractionData = Manager.GetFractionData(memberFractionData.Id);
            if (fractionData == null)
                return;
            if (fractionData.Id == 7 || fractionData.Id == 9 || fractionData.Id == 6 || fractionData.Id == 14 || fractionData.Id == 18)
            {
                _markBlip = NAPI.Blip.CreateBlip(767, player.Position, 1, 1, Main.StringToU16($"Сигнал Код {code}"), 255, 0, true, 0, 0);
            }
            Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Вы отправили сигнал бедствия", 9000);
            _isStart = true;
        }
        public static void DestroyCode(ExtPlayer player)
        {
            NAPI.Task.Run(() =>
            {
                _markBlip.Delete();
            });
            _isStart = false;
        }
        #endregion

        [Command("code")]
        public static void CMD_SpawnBox(ExtPlayer player, int code)
        {
            try
            {
                var memberFractionData = player.GetFractionMemberData();
                if (memberFractionData == null)
                    return;

                var fractionData = Manager.GetFractionData(memberFractionData.Id);
                if (fractionData == null)
                    return;
                if (fractionData.Id == 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Вы не состоите во фракции!", 3000);
                    return;
                }
                if (fractionData.Id >= 1 && fractionData.Id <= 5)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Команда для гос сотрудников", 3000);
                    return;
                }
                if (fractionData.Id >= 10 && fractionData.Id <= 13)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Команда для гос сотрудников", 3000);
                    return;
                }
                if (_isStart)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Сигнал уже создан!", 3000);
                    return;
                }
                SpawnAnCode(player, code);
                Commands.RPChat("me", player, $"Достал планшет и отправил сигнал бедствия");
                Manager.sendFractionMessage(6, $"~b~[Департамент]{player.Name} отправил сигнал Код {code}!!!");
                Manager.sendFractionMessage(7, $"~b~[Департамент]{player.Name} отправил сигнал Код {code}!!!");
                Manager.sendFractionMessage(9, $"~b~[Департамент]{player.Name} отправил сигнал Код {code}!!!");
                Manager.sendFractionMessage(14, $"~b~[Департамент]{player.Name} отправил сигнал Код {code}!!!");
                Manager.sendFractionMessage(18, $"~b~[Департамент]{player.Name} отправил сигнал! Код {code}!!");
            }
            catch (Exception e) { RLog.Write("code: " + e.Message, nLog.Type.Error); }
        }
        [Command("destroycode")]
        public static void CMD_DelSpawnBox(ExtPlayer player)
        {
            try
            {
                var memberFractionData = player.GetFractionMemberData();
                if (memberFractionData == null)
                    return;

                var fractionData = Manager.GetFractionData(memberFractionData.Id);
                if (fractionData == null)
                    return;
                if (fractionData.Id >= 1 && fractionData.Id <= 5)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Команда для гос сотрудников", 3000);
                    return;
                }
                if (fractionData.Id >= 10 && fractionData.Id <= 13)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Команда для гос сотрудников", 3000);
                    return;
                }
                DestroyCode(player);
                Commands.RPChat("me", player, $"Достал планшет и отключил сигнал бедствия");
            }
            catch (Exception e) { RLog.Write("destroycode: " + e.Message, nLog.Type.Error); }
        }
    }
}
