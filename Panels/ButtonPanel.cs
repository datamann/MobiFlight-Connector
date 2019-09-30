﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MobiFlight;
using MobiFlight.InputConfig;
using MobiFlight.Panels.Group;

namespace MobiFlight.Panels
{
    public partial class ButtonPanel : UserControl
    {
        InputConfig.ButtonInputConfig _config;

        public ButtonPanel()
        {
            InitializeComponent();
            onPressActionTypePanel.ActionTypeChanged += new ActionTypePanel.ActionTypePanelSelectHandler(onPressActionTypePanel_ActionTypeChanged);
            onReleaseActionTypePanel.ActionTypeChanged += new ActionTypePanel.ActionTypePanelSelectHandler(onPressActionTypePanel_ActionTypeChanged);
        }

        // On Press Action
        private void onPressActionTypePanel_ActionTypeChanged(object sender, String value)
        {
            Control panel = null;
            Panel owner = onPressActionConfigPanel;
            bool isOnPress = (sender as ActionTypePanel) == onPressActionTypePanel;

            if (!isOnPress) owner = onReleaseActionConfigPanel;

            owner.Controls.Clear();
            switch (value)
            {
                case "FSUIPC Offset":
                    panel = new Panels.Group.FsuipcConfigPanel();
                    (panel as Panels.Group.FsuipcConfigPanel).setMode(false);


                    if (isOnPress && _config != null && _config.onPress != null) 
                        (panel as Panels.Group.FsuipcConfigPanel).syncFromConfig(_config.onPress as FsuipcOffsetInputAction);
                    else if (!isOnPress && _config != null && _config.onRelease != null)
                        (panel as Panels.Group.FsuipcConfigPanel).syncFromConfig(_config.onRelease as FsuipcOffsetInputAction);

                    break;

                case "Key":
                    panel = new KeyboardInputPanel();
                    if (isOnPress && _config != null && _config.onPress != null)
                        (panel as Panels.KeyboardInputPanel).syncFromConfig(_config.onPress as KeyInputAction);
                    else if (!isOnPress && _config != null && _config.onRelease != null)
                        (panel as Panels.KeyboardInputPanel).syncFromConfig(_config.onRelease as KeyInputAction);

                    break;

                case "Event ID":
                    panel = new EventIdInputPanel();
                    if (isOnPress && _config != null && _config.onPress != null)
                        (panel as Panels.EventIdInputPanel).syncFromConfig(_config.onPress as EventIdInputAction);
                    else if (!isOnPress && _config != null && _config.onRelease != null)
                        (panel as Panels.EventIdInputPanel).syncFromConfig(_config.onRelease as EventIdInputAction);
  
                    break;


                case MobiFlight.InputConfig.PmdgEventIdInputAction.Label:
                    panel = new PmdgEventIdInputPanel(PmdgEventIdInputPanel.AircraftType.B737);
                    if (isOnPress && _config != null && _config.onPress != null)
                        (panel as Panels.PmdgEventIdInputPanel).syncFromConfig(_config.onPress as PmdgEventIdInputAction);
                    else if (!isOnPress && _config != null && _config.onRelease != null)
                        (panel as Panels.PmdgEventIdInputPanel).syncFromConfig(_config.onRelease as PmdgEventIdInputAction);

                    break;

                case "Jeehell DataPipe":
                    panel = new JeehellInputPanel();
                    if (isOnPress && _config != null && _config.onPress != null)
                        (panel as Panels.JeehellInputPanel).syncFromConfig(_config.onPress as JeehellInputAction);
                    else if (!isOnPress && _config != null && _config.onRelease != null)
                        (panel as Panels.JeehellInputPanel).syncFromConfig(_config.onRelease as JeehellInputAction);

                    break;

                case "vJoy virtual Joystick":
                    panel = new VJoyInputPanel();
                    if (isOnPress && _config != null && _config.onPress != null)
                        (panel as Panels.VJoyInputPanel).syncFromConfig(_config.onPress as VJoyInputAction);
                    else if (!isOnPress && _config != null && _config.onRelease != null)
                        (panel as Panels.VJoyInputPanel).syncFromConfig(_config.onRelease as VJoyInputAction);
                    break;

                case MobiFlight.InputConfig.LuaMacroInputAction.Label:
                    panel = new LuaMacroInputPanel();
                    if (isOnPress && _config != null && _config.onPress != null)
                        (panel as Panels.LuaMacroInputPanel).syncFromConfig(_config.onPress as LuaMacroInputAction);
                    else if (!isOnPress && _config != null && _config.onRelease != null)
                        (panel as Panels.LuaMacroInputPanel).syncFromConfig(_config.onRelease as LuaMacroInputAction);

                    break;

                case MobiFlight.InputConfig.RetriggerInputAction.Label:
                    panel = new RetriggerInputPanel();
                    if (isOnPress && _config != null && _config.onPress != null)
                        (panel as Panels.RetriggerInputPanel).syncFromConfig(_config.onPress as RetriggerInputAction);
                    else if (!isOnPress && _config != null && _config.onRelease != null)
                        (panel as Panels.RetriggerInputPanel).syncFromConfig(_config.onRelease as RetriggerInputAction);

                    break;
            }

            if (panel != null)
            {
                panel.Padding = new Padding(0, 0, 0, 0);
                panel.Width = owner.Width;
                owner.Controls.Add(panel);
                panel.Height = owner.Height - 3;
            }
        }

        public void syncFromConfig(InputConfig.ButtonInputConfig config)
        {
            if (config == null) return;

            _config = config;

            if (_config.onPress != null)
            {
                onPressActionTypePanel.syncFromConfig(_config.onPress);
            }
            
            if (_config.onRelease != null)
            {
                onReleaseActionTypePanel.syncFromConfig(_config.onRelease);
            }
        }

        public void ToConfig(InputConfig.ButtonInputConfig config)
        {
            // for on press check the action type
            if (onPressActionTypePanel.ActionTypeComboBox.SelectedItem != null)
            {
                switch (onPressActionTypePanel.ActionTypeComboBox.SelectedItem.ToString())
                {
                    case "FSUIPC Offset":
                        config.onPress = (onPressActionConfigPanel.Controls[0] as FsuipcConfigPanel).ToConfig();
                        break;

                    case "Key":
                        config.onPress = (onPressActionConfigPanel.Controls[0] as KeyboardInputPanel).ToConfig();
                        break;

                    case "Event ID":
                        config.onPress = (onPressActionConfigPanel.Controls[0] as EventIdInputPanel).ToConfig();
                        break;

                    case MobiFlight.InputConfig.PmdgEventIdInputAction.Label:
                        config.onPress = (onPressActionConfigPanel.Controls[0] as PmdgEventIdInputPanel).ToConfig();
                        break;

                    case "Jeehell DataPipe":
                        config.onPress = (onPressActionConfigPanel.Controls[0] as JeehellInputPanel).ToConfig();
                        break;

                    case "vJoy virtual Joystick":
                        config.onPress = (onPressActionConfigPanel.Controls[0] as VJoyInputPanel).ToConfig();
                        break;

                    case MobiFlight.InputConfig.LuaMacroInputAction.Label:
                        config.onPress = (onPressActionConfigPanel.Controls[0] as LuaMacroInputPanel).ToConfig();
                        break;

                    case MobiFlight.InputConfig.RetriggerInputAction.Label:
                        config.onPress = (onPressActionConfigPanel.Controls[0] as RetriggerInputPanel).ToConfig();
                        break;

                    default:
                        config.onPress = null;
                        break;
                }
            }

            if (onReleaseActionTypePanel.ActionTypeComboBox.SelectedItem != null)
            {
                switch (onReleaseActionTypePanel.ActionTypeComboBox.SelectedItem.ToString())
                {
                    case "FSUIPC Offset":
                        config.onRelease = (onReleaseActionConfigPanel.Controls[0] as FsuipcConfigPanel).ToConfig();
                        break;

                    case "Key":
                        config.onRelease = (onReleaseActionConfigPanel.Controls[0] as KeyboardInputPanel).ToConfig();
                        break;

                    case "Event ID":
                        config.onRelease = (onReleaseActionConfigPanel.Controls[0] as EventIdInputPanel).ToConfig();
                        break;

                    case MobiFlight.InputConfig.PmdgEventIdInputAction.Label:
                        config.onRelease = (onReleaseActionConfigPanel.Controls[0] as PmdgEventIdInputPanel).ToConfig();
                        break;

                    case "Jeehell DataPipe":
                        config.onRelease = (onReleaseActionConfigPanel.Controls[0] as JeehellInputPanel).ToConfig();
                        break;

                    case "vJoy virtual Joystick":
                        config.onRelease = (onReleaseActionConfigPanel.Controls[0] as VJoyInputPanel).ToConfig();
                        break;

                    case MobiFlight.InputConfig.LuaMacroInputAction.Label:
                        config.onRelease = (onReleaseActionConfigPanel.Controls[0] as LuaMacroInputPanel).ToConfig();
                        break;

                    case MobiFlight.InputConfig.RetriggerInputAction.Label:
                        config.onRelease = (onReleaseActionConfigPanel.Controls[0] as RetriggerInputPanel).ToConfig();
                        break;

                    default:
                        config.onRelease = null;
                        break;
                }
            }
        }
    }
}
