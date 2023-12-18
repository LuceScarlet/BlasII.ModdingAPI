﻿using HarmonyLib;
using Il2CppTGK.Game.Components.Misc;
using Il2CppTGK.Game.Components.UI;

namespace BlasII.ModdingAPI.Menus
{
    /// <summary>
    /// When starting a new game, open menus instead
    /// After menus are finished, call NewGame event
    /// </summary>
    [HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.NewGame))]
    class Menu_NewGame_Patch
    {
        public static bool Prefix(int slot)
        {
            if (Main.ModdingAPI.MenuHandler.AllowGameStart)
                return true;

            Main.ModdingAPI.MenuHandler.OnTryStartGame(slot, true);
            return false;
        }

        public static void Postfix()
        {
            if (Main.ModdingAPI.MenuHandler.AllowGameStart)
                Main.ModLoader.ProcessModFunction(mod => mod.OnNewGame());
        }
    }

    /// <summary>
    /// When loading an existing game, open menus instead
    /// After menus are finished, call LoadGame event
    /// </summary>
    [HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.LoadGame))]
    class Menu_LoadGame_Patch
    {
        public static bool Prefix(int slot)
        {
            if (Main.ModdingAPI.MenuHandler.AllowGameStart)
                return true;

            Main.ModdingAPI.MenuHandler.OnTryStartGame(slot, false);
            return false;
        }

        public static void Postfix()
        {
            if (Main.ModdingAPI.MenuHandler.AllowGameStart)
                Main.ModLoader.ProcessModFunction(mod => mod.OnLoadGame());
        }
    }

    /// <summary>
    /// Prevent the main menu from canceling when the settings menu is active
    /// </summary>
    [HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnBackPressed))]
    class Menu_Cancel_Patch
    {
        public static bool Prefix() => !Main.ModdingAPI.MenuHandler.IsMenuActive;
    }

    /// <summary>
    /// Prevent this class from hiding the cursor, even if cursor.visible = true
    /// </summary>
    [HarmonyPatch(typeof(MouseCursorVisibilityController), nameof(MouseCursorVisibilityController.Update))]
    class Mouse_Update_Patch
    {
        public static bool Prefix() => false;
    }
    [HarmonyPatch(typeof(MouseCursorVisibilityController), nameof(MouseCursorVisibilityController.Awake))]
    class Mouse_Awake_Patch
    {
        public static bool Prefix() => false;
    }
}
