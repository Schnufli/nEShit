﻿using AuraModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nEShit
{
    public partial class MainInterface : Form
    {
        public MainInterface()
        {
            InitializeComponent();
        }

        private void b_debug_Click(object sender, EventArgs e)
        {

        }

        private void MainInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Call Game Crash Msg lul
            Hook.UnsetHook();
        }

        private void loop_Tick(object sender, EventArgs e)
        {
            if (!cb_loop.Checked) return;

            if (!AuraModule.Utils.IsInGame() || !(AuraModule.Utils.locPlayer.GetEntityInfo.level >= 1))
                return;
            // Set Movement Value in Float
            if(speedhack.Checked)
                AuraModule.Utils.locPlayer.SetMovementValue((float)trackBar1.Value);
            // Auto Resurrection if currentLife is 0
            if(resurrection.Checked)
                AuraModule.Utils.locPlayer.FullResurrection();
            // Auto Eidolon Link without Inventory Check
            if (eal.Checked)
                WindowManager.GetEudemonExtendWindow.UpdateEudemons(false);
            // Auto Blue Range Hack assist for Fishing
            if (cb_fishHelper.Checked)
                WindowManager.GetFishingWindow.setBlueRangeHack();
            //Fishing Assist
            if (cb_fishingAssist.Checked)
                WindowManager.GetFishingWindow.Update();

            AuraModule.Fishing.GetUnwandtedFishType();
            fishType.Text = AuraModule.Fishing.FishTypeSTR;

            //HotKey Feature
            HotKeyFeature();

        }
        private void cb_loop_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_loop.Checked)
                loop.Enabled = true;
            else loop.Enabled = false;
        }

        private void btn_teleportInterface_Click(object sender, EventArgs e)
        {
            AuraModule.Utils.locPlayer.TeleportInterface();
        }

        private void btn_Resurrection_Click(object sender, EventArgs e)
        {
            AuraModule.Utils.locPlayer.FullResurrection();
        }

        private void HotKeyFeature()
        {
            if (Pinvoke.ForegroundWindow())
            {
                var handle = Pinvoke.GetConsoleWindow();

                if (Hotkey.KeyPress(System.Windows.Forms.Keys.F10))
                    AuraModule.Utils.locPlayer.TeleportInterface();
                if (Hotkey.KeyPress(System.Windows.Forms.Keys.F9))
                    AuraModule.Utils.locPlayer.FullResurrection();
                if (Hotkey.KeyPress(System.Windows.Forms.Keys.F11))
                    Pinvoke.ShowWindow(handle, 0);
                if (Hotkey.KeyPress(System.Windows.Forms.Keys.F12))
                    Pinvoke.ShowWindow(handle, 1);
            }
        }
    }
}
