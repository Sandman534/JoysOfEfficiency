﻿using JoysOfEfficiency.Utils;
using StardewModdingAPI;
using StardewValley;

namespace JoysOfEfficiency.Core
{
    /// <summary>
    /// This class holds mod and config instance and exposes some useful methods.
    /// </summary>
    internal class InstanceHolder
    {
        private static ModEntry ModInstance { get; set; }

        public static Config Config { get; private set; }
        private static IModHelper Helper => ModInstance.Helper;
        public static ITranslationHelper Translation => Helper.Translation;
        public static IReflectionHelper Reflection => Helper.Reflection;
        public static Multiplayer Multiplayer => Reflection.GetField<Multiplayer>(typeof(Game1), "multiplayer").GetValue();

        public static InputState Input => Reflection.GetField<InputState>(typeof(Game1), "input").GetValue();

        public static CustomAnimalConfigHolder CustomAnimalTool;

        /// <summary>
        /// Sets mod's entry　point instance. 
        /// </summary>
        /// <param name="modInstance">the mod instance</param>
        
        public static void Init(ModEntry modInstance)
        {
            ModInstance = modInstance;
            Config = LoadConfig();
            CustomAnimalTool = new CustomAnimalConfigHolder(modInstance.GetFilePath("customAnimalTools.json"));
        }
        
        /// <summary>
        /// Writes settings to '(ModFolder)/config.json'.
        /// </summary>

        public static void WriteConfig()
        {
            Helper?.WriteConfig(Config);
        }

        public static Config LoadConfig()
        {
            return Helper != null ? Config = Helper.ReadConfig<Config>() : new Config();
        }
    }
}
