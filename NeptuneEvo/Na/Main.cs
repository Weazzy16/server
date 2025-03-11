using GTANetworkAPI;
using NeptuneEvo.Handles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Data;
using Redage.SDK;
using MySqlConnector;
using System.Text.RegularExpressions;
using System.Threading;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Chars;
using NeptuneEvo.Fractions;
using NeptuneEvo.Functions;
using NeptuneEvo.Accounts;
using NeptuneEvo.Players.Models;
using NeptuneEvo.Players;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using Database;
using LinqToDB;
using System.Threading.Tasks;
using Localization;
using NeptuneEvo.Fractions.Models;
using NeptuneEvo.Fractions.Player;
using NeptuneEvo.Players.Phone.Messages.Models;
using NeptuneEvo.VehicleData.Data;
using NeptuneEvo.VehicleData.LocalData;
using NeptuneEvo.VehicleData.LocalData.Models;
using Newtonsoft.Json;
using GTANetworkMethods;
using NeptuneEvo.Database.Models;
using NeptuneEvo.MoneySystem;
using static LinqToDB.Reflection.Methods.LinqToDB;
using static LinqToDB.Sql;
using System.Numerics;
using System.Xml.Linq;
using System.Reflection;
using NeptuneEvo.Core;
using static NeptuneEvo.Na.Na;
using NeptuneEvo.Fractions.Table.Tasks.Models;
using System.Diagnostics;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Linq;


namespace NeptuneEvo.Na
{
    public class CraftingRecipe
    {
        public Dictionary<ItemId, int> ResultItems { get; set; }
        public Dictionary<ItemId, int> Ingredients { get; set; }
        public double CraftShance { get; set; }
        public int CraftTime { get; set; }

        public CraftingRecipe(Dictionary<ItemId, int> resultItems, Dictionary<ItemId, int> ingredients, double craftshance, int craftTime)
        {
            ResultItems = resultItems;
            Ingredients = ingredients;
            CraftShance = craftshance;
            CraftTime = craftTime;
        }
    }

    public class Na : Script
    {

        public static Dictionary<ItemId, CraftingRecipe> craftingRecipes = new Dictionary<ItemId, CraftingRecipe>();

        [ServerEvent(Event.ResourceStart)]
        public static void onResourceStart()
        {

            // Создаем рецепты крафта
            craftingRecipes.Add(ItemId.GasCan, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.GasCan, 1 } }, new Dictionary<ItemId, int> { { ItemId.Material, 2 } }, 0.8, 8));
            // Добавьте здесь другие рецепты
            craftingRecipes.Add(ItemId.RPG, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.RPG, 1 } }, new Dictionary<ItemId, int> { { ItemId.Material, 20 } }, 0.8, 8));
            craftingRecipes.Add(ItemId.Radio, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.Radio, 1 } }, new Dictionary<ItemId, int> { { ItemId.Material, 20 }, { ItemId.Cuffs, 1 } }, 0.01, 8));
            craftingRecipes.Add(ItemId.Lockpick, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.Lockpick, 1 } }, new Dictionary<ItemId, int> { { ItemId.Material, 20 } }, 0.5, 8));
            craftingRecipes.Add(ItemId.HealthKit, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.HealthKit, 1 } }, new Dictionary<ItemId, int> { { ItemId.Drugs, 20 }, { ItemId.Burger, 1 } }, 0.1, 8));
            craftingRecipes.Add(ItemId.RusDrink1, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.RusDrink1, 1 } }, new Dictionary<ItemId, int> { { ItemId.Sprunk, 1 }, { ItemId.Beer, 1 }, { ItemId.Drugs, 1 } }, 0.2, 8));
            craftingRecipes.Add(ItemId.Pizza, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.Pizza, 1 } }, new Dictionary<ItemId, int> { { ItemId.Material, 20 } }, 0.8, 8));
            craftingRecipes.Add(ItemId.HotDog, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.HotDog, 1 } }, new Dictionary<ItemId, int> { { ItemId.Material, 20 }, { ItemId.Cuffs, 1 } }, 0.01, 8));
            craftingRecipes.Add(ItemId.BagWithMoney, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.BagWithMoney, 1 } }, new Dictionary<ItemId, int> { { ItemId.Material, 20 } }, 0.5, 8));
            craftingRecipes.Add(ItemId.Drugs, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.Drugs, 1 } }, new Dictionary<ItemId, int> { { ItemId.Drugs, 20 }, { ItemId.Burger, 1 } }, 0.1, 8));
            craftingRecipes.Add(ItemId.YakDrink1, new CraftingRecipe(new Dictionary<ItemId, int> { { ItemId.YakDrink1, 1 } }, new Dictionary<ItemId, int> { { ItemId.Sprunk, 1 }, { ItemId.Beer, 1 }, { ItemId.Drugs, 1 } }, 0.2, 8));
            //getCraftList();
        }

        public static void getCraftList(ExtPlayer player)
        {
            List<Dictionary<ItemId, int>> resultItemsList = craftingRecipes.Values.Select(recipe => recipe.ResultItems).ToList();
            List<object> resultItemsArray = new List<object>();
            foreach (var resultItem in resultItemsList)
            {
                foreach (var item in resultItem)
                {
                    resultItemsArray.Add(new { Item = item.Key, Count = item.Value });
                }
            }
            //Console.WriteLine("=======================================craftList:");
            foreach (var item in resultItemsArray)
            {
                var itemObj = (dynamic)item;
                //Console.WriteLine($"Item: {itemObj.Item}, Count: {itemObj.Count}");
            }
            Trigger.ClientEvent(player, "loadStupidInfo", resultItemsArray);
            GetMoreInfoInfo(player, 0);
        }

        [RemoteEvent("server.gta5devcraft.getinfo")]
        public static void GetMoreInfoInfo(ExtPlayer player, int index)
        {
            CraftingRecipe CraftingRecipe = craftingRecipes.ElementAtOrDefault(index).Value;

            // Проверяем, что данные первого крафта не равны null
            if (CraftingRecipe != null)
            {
                List<object> ingredientsList = new List<object>();
                foreach (var ingredient in CraftingRecipe.Ingredients)
                {
                    ingredientsList.Add(new { Item = ingredient.Key, Count = ingredient.Value });
                }

                Trigger.ClientEvent(player, "loadMoreInfoInfo", GetFirstResultItemKeyName(CraftingRecipe), ingredientsList, CraftingRecipe.CraftShance, CraftingRecipe.CraftTime);
                // Отправляем данные первого крафта клиенту
            }
        }

        public static ItemId GetFirstResultItemKeyName(CraftingRecipe recipe)
        {
            if (recipe.ResultItems != null && recipe.ResultItems.Count > 0)
            {
                ItemId firstKey = recipe.ResultItems.Keys.FirstOrDefault();
                return firstKey; // Возвращаем название первого ключа
            }
            return ItemId.Debug; // Возвращаем null, если словарь пуст
        }

        [RemoteEvent("server.gta5devcraft.startcraft")]
        public static void checkCraft(ExtPlayer player, int itemId, int value, bool info)
        {
            ItemId itemToCraft = (ItemId)System.Enum.Parse(typeof(ItemId), itemId.ToString());

            if (craftingRecipes.ContainsKey(itemToCraft))
            {
                CraftingRecipe recipe = craftingRecipes[itemToCraft];
                //Console.WriteLine($"Рецепт крафта для {itemToCraft}:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    //Console.WriteLine($"- {ingredient.Value} шт. {ingredient.Key}");
                }
                //Console.WriteLine($"Время крафта: {recipe.CraftTime} секунд");
                //Console.WriteLine("Результаты крафта:");
                foreach (var resultItem in recipe.ResultItems)
                {
                    Console.WriteLine($"- {resultItem.Value} шт. {resultItem.Key}");
                }
                StartCraft(player, itemToCraft, value, info);
            }
            else
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Рецепт крафта для {itemToCraft} не найден.", 3000);
                //Console.WriteLine($"Рецепт крафта для {itemToCraft} не найден.");
            }
        }

        public static void StartCraft(ExtPlayer player, ItemId itemToCraft, int value, bool info)
        {
            CraftingRecipe recipe = craftingRecipes[itemToCraft];

            foreach (var ingredient in recipe.Ingredients)
            {
                int count = getCountInv(player, ingredient.Key);
                if (count < ingredient.Value * value)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно ресурсов", 3000);
                    return;
                }
            }

            foreach (var resultItem in recipe.ResultItems)
            {
                int var = Chars.Repository.isFreeSlots(player, resultItem.Key, resultItem.Value * value);
                Console.WriteLine($"====================================================={var}");
                if (var > 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NoSpaceInventory), 3000);
                    return;
                }
            }

            if (info == false)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, "Забираем ресурсы", 3000);
                    removeItemCount(player, ingredient.Key, ingredient.Value * value);
                }

                Random random = new Random();
                double rnd = random.NextDouble();
                //Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"рандом {rnd}", 3000);

                if (rnd < recipe.CraftShance)
                {
                    foreach (var resultItem in recipe.ResultItems)
                    {
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "Вы успешно скрафтили предмет ", 3000);
                        addCraftedItemToInv(player, resultItem.Key, resultItem.Value * value);
                    }
                }
                else
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "Увы вам не удалось скрафтить предмет", 3000);
                }
            }
            else
            {
                Trigger.ClientEvent(player, "clien.gta5devcraft.allowcraft", itemToCraft, recipe.CraftTime, value);
            }
        }

        public static void addCraftedItemToInv(ExtPlayer player, ItemId item, int count)
        {
            Chars.Repository.AddNewItem(player, $"char_{player.CharacterData.UUID}", "inventory", item, count);
        }

        public static int getCountInv(ExtPlayer player, ItemId item)
        {
            int count = Chars.Repository.getCountItem($"char_{player.CharacterData.UUID}", item);
            return count;
        }

        public static void removeItemCount(ExtPlayer player, ItemId item, int count)
        {
            Chars.Repository.Remove(player, $"char_{player.CharacterData.UUID}", "inventory", item, count);
        }
    }
}
