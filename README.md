# Shardpunk - RandomParty

## Description

This mod adds a new button in the party selection menu to select a random party among all unlocked units.

## Installation

This mod was made with `BepInEx` which is why you need to install it first.

### Installing BepInEx

- Download the version `5.4.22` of [BepInEx](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.22).
- Unzip it in the `Shardpunk` folder.

### Installing RandomParty

- Download the latest release of [RandomParty](https://github.com/remyCases/Shardpunk-RandomParty/releases). 
- Unzip it, you should have 2 files: `Random.dll` and `base.txt`.
- Move `Random.dll` in the `Shardpunk/BepInEx/plugins` folder.
- Move `base.txt` in the `Data\Languages\en` folder.

You can now play the game with the modded version !

## Troubleshooting

If you encountered some troubles while playing with this mod, you can contact me on [Discord](https://discord.com/users/200330865522376704), and send me the log file `LogOutput` found in the folder `Shardpunk/BepInEx`.

## For anyone who wants to change this mod and build it themself

> [!CAUTION]
> The current repo can not be built directly as I didn't include the Assembly dll of Shardpunk (for obvious reasons).
> You need to create a `lib` folder first and to move the `Assembly-CSharp.dll` and the `UnityEngine.UI.dll` from the `Shardpunk/Shardpunk_Data/Managed` folder to the `lib` folder.

This mod was made using `HarmonyX`, a fork of `Harmony2` from `BepInEx`. I strongly advise anyone who wants to change the mod to check the documentation of [Harnomy2](https://harmony.pardeike.net/articles/intro.html) and to check the difference between [Harmony2 and HarmonyX](https://github.com/BepInEx/HarmonyX/wiki/Difference-between-Harmony-and-HarmonyX) first.

## See also

Other mods I've made:
- Shardpunk:
    - [Shardpunk-Faster](https://github.com/remyCases/Shardpunk-Faster)
    - [Shardpunk-MoreSkillLevels](https://github.com/remyCases/Shardpunk-MoreSkillLevels)
    - [Shardpunk-BiggerTeam](https://github.com/remyCases/Shardpunk-BiggerTeam)

- Stoneshard:
    - [Character Creation](https://github.com/remyCases/CharacterCreator)
    - [Defeat Scenarios](https://github.com/remyCases/Stoneshard-DefeatScenarios)
    - [More Save Slots](https://github.com/remyCases/Stoneshard-MoreSaveSlots)
    - [Pelt Durability](https://github.com/remyCases/Stoneshard-PeltDurability)
    - [Speedshard_Core](https://github.com/remyCases/SpeedshardCore)
    - [Speedshard_Sprint](https://github.com/remyCases/SpeedshardSprint)
    - [Speedshard_Backpack](https://github.com/remyCases/SpeedshardBackpack)
    - [Speedshard_Skinning](https://github.com/remyCases/SpeedshardSkinning)
    - [Speedshard_MoneyDungeon](https://github.com/remyCases/SpeedshardMoneyDungeon)
    - [Speedshard_Stances](https://github.com/remyCases/SpeedshardStances)

- Airship Kingdom Adrift:
    - [ProductionPanel](https://github.com/remyCases/AKAMod_ProdPanel)