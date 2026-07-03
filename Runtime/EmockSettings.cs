using Basis.Scripts.Settings;
using Basis;
using Basis.BasisUI;
using UnityEngine;

namespace jp.lilxyzw.emock
{
    internal static class EmockSettings
    {
        public static BasisSettingsBinding<bool> ChangeByTrackpad = new("change by trackpad", new BasisPlatformDefault<bool>(true));
        public static BasisSettingsBinding<bool> ResetUponMoving = new("reset upon moving", new BasisPlatformDefault<bool>(false));
        public static BasisSettingsBinding<float> StopDistance = new("stop distance", new BasisPlatformDefault<float>(50));

        public static void LoadAll()
        {
            ChangeByTrackpad.LoadBindingValue();
            ResetUponMoving.LoadBindingValue();
            StopDistance.LoadBindingValue();
        }

        private static PanelTabPage EmockTab(PanelTabGroup tabGroup)
        {
            PanelTabPage tab = PanelTabPage.CreateVertical(tabGroup.Descriptor.ContentParent);
            PanelElementDescriptor descriptor = tab.Descriptor;
            descriptor.SetTitle("Emock");

            RectTransform container = descriptor.ContentParent;

            PanelElementDescriptor group = PanelElementDescriptor.CreateNew(PanelElementDescriptor.ElementStyles.Group, container);
            group.SetTitle("Emock");

            PanelToggle toggleChangeByTrackpad = PanelToggle.CreateNewEntry(group);
            toggleChangeByTrackpad.Descriptor.SetTitle("Change By Trackpad");
            toggleChangeByTrackpad.Descriptor.SetTooltip("Enable facial expression changes using the trackpad.");
            toggleChangeByTrackpad.AssignBinding(ChangeByTrackpad);

            PanelToggle toggleResetUponMoving = PanelToggle.CreateNewEntry(group);
            toggleResetUponMoving.Descriptor.SetTitle("Reset Upon Moving");
            toggleResetUponMoving.Descriptor.SetTooltip("Resets the facial expression when moving. This setting is primarily for Vive Controllers.");
            toggleResetUponMoving.AssignBinding(ResetUponMoving);

            PanelSlider sliderStopDistance = PanelSlider.CreateEntryAndBind(
                group,
                PanelSlider.SliderSettings.Distance("Stop Distance", 100),
                StopDistance
            );
            sliderStopDistance.Descriptor.SetTooltip("The animation stops if the avatar is further away than this distance.");

            return tab;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            SettingsProvider.ExternalTabs.Add(("Emock", EmockTab));
        }
    }
}
