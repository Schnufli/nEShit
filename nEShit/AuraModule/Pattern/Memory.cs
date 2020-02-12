﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MemoryStore
{
    public static IntPtr GET_LOCAL_PLAYER = IntPtr.Zero;
    public static IntPtr INVENTORY_ACCESS_FUNCTION = IntPtr.Zero;
    public static IntPtr TARGETING_COLLECTIONS_BASE = IntPtr.Zero;
    public static IntPtr WND_INTERFACE_BASE = IntPtr.Zero;
    public static IntPtr EUDEMON_GETEUDEMON_FUNCTION = IntPtr.Zero;
    public static IntPtr EUDEMON_SENDCOMMAND_FUNCTION = IntPtr.Zero;
    public static IntPtr EUDEMON_SELECT_FUNCTION = IntPtr.Zero;
    public static IntPtr EUDEMON_ISMEDITATING_FUNCTION = IntPtr.Zero;
    public static IntPtr EUDEMON_HASGIFT_FUNCTION = IntPtr.Zero;
    public static IntPtr CURRENT_MAP_BASE = IntPtr.Zero;
    public static IntPtr DETOUR_MAIN_LOOP_OFFSET = IntPtr.Zero;

    public static IntPtr PLAYER_Resurrection = IntPtr.Zero;
    public static IntPtr PLAYER_DoUIAction = IntPtr.Zero;
}
public class Load_Pattern
{
    public static void SetPatterns()
    {
        System.Diagnostics.ProcessModule gameproc = Minimem.FindProcessModule("game.bin", false);
        MemoryStore.DETOUR_MAIN_LOOP_OFFSET = PatternManager.FindPattern(gameproc, "55 8b ec 83 ec ? 80 3d ? ? ? ? ? 74 ? 8b 0d");

        MemoryStore.GET_LOCAL_PLAYER = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 8B F8 0F BF 86 ? ? ? ?", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.INVENTORY_ACCESS_FUNCTION = PatternManager.FindPattern(gameproc, "55 8B EC 8B 55 08 33 C0 83 FA 0D");
        MemoryStore.TARGETING_COLLECTIONS_BASE = PatternManager.FindPatternAlain(gameproc, "8B 0D ? ? ? ? E8 ? ? ? ? 8B F8 0F BF 86 ? ? ? ?", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);
        MemoryStore.WND_INTERFACE_BASE = PatternManager.FindPatternAlain(gameproc, "89 35 00 ? ? ? 5E C3", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);
        MemoryStore.EUDEMON_GETEUDEMON_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 85 C0 74 C2", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 0F B7 86 ? ? ? ? 83 C4 10 ", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_SELECT_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 6A 01 8B CF E8 ? ? ? ? 8B CF E8 ? ? ? ? 8B CF 5F 5B E9 ? ? ? ?", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_ISMEDITATING_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 84 C0 74 33 8B CE", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.EUDEMON_HASGIFT_FUNCTION = PatternManager.FindPatternAlain(gameproc, "E8 ? ? ? ? 84 C0 74 3A 8B CF E8 ? ? ? ? C7 45 ? ? ? ? ?", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.CURRENT_MAP_BASE = PatternManager.FindPatternAlain(gameproc, "8B 0D ? ? ? ? E8 ? ? ? ? A1 ? ? ? ? 85 C0 75 05 E8 ? ? ? ? ", 1, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES_RAW);
        
        MemoryStore.PLAYER_Resurrection = PatternManager.FindPatternAlain(gameproc, "e8 ? ? ? ? 83 c4 ? 83 7d ? ? 72 ? ff 75 ? e8 ? ? ? ? 83 c4 ? 8b 4d ? b0 ? 64 89 0d ? ? ? ? 8b e5 5d c2 ? ? 8d 49", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);
        MemoryStore.PLAYER_DoUIAction = PatternManager.FindPatternAlain(gameproc, "e8 ? ? ? ? 83 c4 ? b0 ? c3 cc cc cc cc 55 8b ec 56", 0, 1, PatternManager.MemoryType.RT_READNEXT4_BYTES);

    }

    public static bool RetrieveAddresses(uint gamePID)
    {
        Attach.PID = gamePID;
        if (Attach.PID == 0) return false;
        Attach.OpenProcess();
        SetPatterns();//Load Patterns
#if DEBUG
        Debug.WriteLine($"----- Debug Pattern -----");
        Debug.WriteLine($"DETOUR_MAIN_LOOP_OFFSET 0x{MemoryStore.DETOUR_MAIN_LOOP_OFFSET.ToString("X")}\n");
        Debug.WriteLine($"GET_LOCAL_PLAYER 0x{MemoryStore.GET_LOCAL_PLAYER.ToString("X")}\n");
        Debug.WriteLine($"INVENTORY_ACCESS_FUNCTION 0x{MemoryStore.INVENTORY_ACCESS_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"TARGETING_COLLECTIONS_BASE 0x{MemoryStore.TARGETING_COLLECTIONS_BASE.ToString("X")}\n");
        Debug.WriteLine($"WND_INTERFACE_BASE 0x{MemoryStore.WND_INTERFACE_BASE.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_GETEUDEMON_FUNCTION 0x{MemoryStore.EUDEMON_GETEUDEMON_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_SENDCOMMAND_FUNCTION 0x{MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_SELECT_FUNCTION 0x{MemoryStore.EUDEMON_SELECT_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_ISMEDITATING_FUNCTION 0x{MemoryStore.EUDEMON_ISMEDITATING_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"EUDEMON_HASGIFT_FUNCTION 0x{MemoryStore.EUDEMON_HASGIFT_FUNCTION.ToString("X")}\n");
        Debug.WriteLine($"CURRENT_MAP_BASE 0x{MemoryStore.CURRENT_MAP_BASE.ToString("X")}\n");
        Debug.WriteLine($"----- End -----");
#endif
        if
            (
            MemoryStore.DETOUR_MAIN_LOOP_OFFSET == IntPtr.Zero ||
            MemoryStore.GET_LOCAL_PLAYER == IntPtr.Zero ||
            MemoryStore.INVENTORY_ACCESS_FUNCTION == IntPtr.Zero ||
            MemoryStore.TARGETING_COLLECTIONS_BASE == IntPtr.Zero ||
            MemoryStore.WND_INTERFACE_BASE == IntPtr.Zero ||
            MemoryStore.EUDEMON_GETEUDEMON_FUNCTION == IntPtr.Zero ||
            MemoryStore.EUDEMON_SENDCOMMAND_FUNCTION == IntPtr.Zero ||
            MemoryStore.EUDEMON_SELECT_FUNCTION == IntPtr.Zero ||
            MemoryStore.EUDEMON_ISMEDITATING_FUNCTION == IntPtr.Zero ||
            MemoryStore.EUDEMON_HASGIFT_FUNCTION == IntPtr.Zero ||
            MemoryStore.CURRENT_MAP_BASE == IntPtr.Zero ||

            MemoryStore.PLAYER_DoUIAction == IntPtr.Zero ||
            MemoryStore.PLAYER_Resurrection == IntPtr.Zero
            )
            return true;
        else return false;
    }
}
