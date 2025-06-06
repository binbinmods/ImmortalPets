using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
// using static Obeliskial_Essentials.Essentials;
using System;
using static ImmortalPets.CustomFunctions;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;
using static ImmortalPets.Plugin;
// using static MatchManager;

// Make sure your namespace is the same everywhere
namespace ImmortalPets
{

    [HarmonyPatch] //DO NOT REMOVE/CHANGE

    public class ImmortalPetsPatches
    {
        public static int i = 1;

        // [HarmonyReversePatch]
        // [HarmonyPatch(typeof(MatchManager), "CreateNPC")]
        // public static void CreateNPCReversePatch(NPCData _npcData,
        //     string effectTarget = "",
        //     int _position = -1,
        //     bool generateFromReload = false,
        //     string internalId = "",
        //     CardData _cardActive = null) =>
        //     //This is intentionally a stub
        //     throw new NotImplementedException("Reverse patch has not been executed.");
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MatchManager), nameof(MatchManager.CastCardAction))]
        public static void CastCardActionPrefix(
            ref CardData _cardActive,
            Transform targetTransformCast,
            CardItem theCardItem,
            string _uniqueCastId,
            bool _automatic,
            CardData _card,
            int _cardIterationTotal,
            int _cardSpecialValueGlobal)
        {
            if (_cardActive == null)
            {
                LogDebug($"CastCardActionPrefix - _cardActive is null, skipping patch.");
                return;
            }
            _cardActive.KillPet = false;

        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Globals), nameof(Globals.CreateGameContent))]
        public static void CreateGameContentPostfix(ref Globals __instance, ref Dictionary<string, CardData> ____CardsSource)
        {
            string cardToChange = "twilightslaughter";
            if (____CardsSource.TryGetValue(cardToChange, out CardData twilightslaughter))
            {
                LogDebug($"CreateGameContentPostfix - Preventing twilight slaughter from killing pets");
                twilightslaughter.KillPet = false;
                ____CardsSource[cardToChange] = twilightslaughter;
            }
            else
            {
                LogDebug($"CreateGameContentPostfix - Twilight Slaughter not found in CardsSource");
            }
            cardToChange = "twilightslaughtera";
            if (____CardsSource.TryGetValue(cardToChange, out CardData twilightslaughtera))
            {
                LogDebug($"CreateGameContentPostfix - Preventing twilight slaughter from killing pets");
                twilightslaughtera.KillPet = false;
                ____CardsSource[cardToChange] = twilightslaughtera;
            }
            {
                LogDebug($"CreateGameContentPostfix - TwilightSlaughterA not found in CardsSource");
            }
        }

    }

}
