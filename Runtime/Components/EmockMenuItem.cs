using System.Collections.Generic;
using Basis.Scripts.BasisSdk;
using Basis.Scripts.BasisSdk.Players;
using HVR.Basis.Comms;
using UnityEngine;

namespace jp.lilxyzw.emock
{
    [AddComponentMenu("Emock/Emock Menu Item")]
    public class EmockMenuItem : MonoBehaviour, IEmockAvatarLoad
    {
        public string title;
        public string description;
        public string parameter;
        public List<string> selections;
        public EmockController controller;
        public SaveType saveType;
        public float defaultValue;
        public Sprite icon;

        private float value;

        public void OnAvatarReady(BasisAvatar avatar, bool IsOwner)
        {
            if (IsOwner)
            {
                if (!controller)
                {
                    Debug.LogError("EmockMenuItem: EmockController not found.");
                    Destroy(this);
                }
                this.value = defaultValue;
                var key = GetKey();
                if (string.IsNullOrEmpty(key) || !HVRVixxyPersistentStore.TryGet($"emock.grobal:{parameter}", out var value)) return;
                this.value = value;
                if (controller) controller.SetParameter(parameter, value);
            }
            else Destroy(this);
        }

        public void Reset() => ApplyValue(defaultValue);

        public void ApplyValue(float value)
        {
            this.value = value;
            if (controller) controller.SetParameter(parameter, value);
            var key = GetKey();
            if (string.IsNullOrEmpty(key)) return;
            HVRVixxyPersistentStore.Set(key, value, defaultValue);
        }

        public void ApplyValue(string choice) => ApplyValue(selections.IndexOf(choice));

        public void ApplyValue(bool value) => ApplyValue(value ? 1f : 0f);

        public float GetValue() => value;

        private string GetKey()
        {
            if (saveType == SaveType.None)
            {
                return null;
            }
            else if (saveType == SaveType.Avatar)
            {
                if (string.IsNullOrEmpty(BasisLocalPlayer.CurrentAvatarUniqueID)) return null;
                return $"emock.parameter:{BasisLocalPlayer.CurrentAvatarUniqueID}|{parameter}";
            }
            else
            {
                return $"emock.parameter:{parameter}";
            }
        }
    }

    public enum SaveType
    {
        None,
        Avatar,
        Global
    }
}
