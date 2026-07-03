Emock
====

Emotion + Mock

Mock animator controller for controlling the avatar's facial expressions.

## Note

Except for EmockMenuItem, it is not recommended for users to configure this plugin's components directly. Instead, use external tools to configure them automatically.

- [lilEmo](https://github.com/lilxyzw/lilEmo)

## Design

- Complex conditional branching and input operations that determine which animation to play are handled exclusively by the avatar owner. (`EmockController`)
- Once the animation to be played is determined, the index of that animation is sent to the network. (`EmockNetwork`)
- Receives the index sent by the owner and switches the animation. (`EmockNetwork`)
- The component plays a specific single animation based on the index. (`EmockAnimator`)
- Animation interpolation and fading are performed using Burst for each avatar, and the determined values ​​are applied to the avatar. (`EmockInterpolator`)

## Editing the framework

### `com.basis.sdk/Settings/AvatarContentPoliceSelector.asset`

- jp.lilxyzw.emock.EmockAnimator
- jp.lilxyzw.emock.EmockController
- jp.lilxyzw.emock.EmockMenuItem
- jp.lilxyzw.emock.EmockNetwork

### `com.basis.eventdriver/BasisEventDriver.asmdef`

```json
{
    ...
    "references": [
        ...
        "jp.lilxyzw.emock"
    ],
    ...
}
```

### `com.basis.eventdriver/BasisEventDriver.cs`

```C#
        private void UpdateBody()
        {
			...

			// Add this
            jp.lilxyzw.emock.EmockManager.Update();

			...
		}

        private void LateUpdateBody()
        {
			...

			// Add this
            jp.lilxyzw.emock.EmockManager.LateUpdate();

			...
		}
```

## Make Emock compatible with Cilbox

### `com.basis.shim/BasisShims.asmdef`

```json
{
    ...
    "references": [
        ...
        "jp.lilxyzw.emock"
    ],
    ...
}
```

### `com.basis.shim/Shims/CilboxBasisCommon.cs`

```C#
		protected static readonly HashSet<string> commonWhiteListType = new HashSet<string>(){
			...

			// Add this
			"jp.lilxyzw.emock.EmockController",
			"jp.lilxyzw.emock.EmockNetwork",
		};

		protected static readonly Dictionary<Type, HashSet<string>> commonMethodWhitelist = new Dictionary<Type, HashSet<string>>()
		{
			...

			// Add this
			{ typeof(jp.lilxyzw.emock.EmockController), new HashSet<string>{ "SetParameter" } },
			{ typeof(jp.lilxyzw.emock.EmockNetwork),   new HashSet<string>{ "SetIndex" } },
		};
```

### `com.basis.shim/Shims/link.xml`

```xml
<linker>
    ...
	<assembly fullname="jp.lilxyzw.emock">
		<type fullname="jp.lilxyzw.emock.EmockController" preserve="all"/>
		<type fullname="jp.lilxyzw.emock.EmockNetwork" preserve="all"/>
	</assembly>
</linker>
```

### Client settings

Installing this plugin adds a new item to the settings.

- Change By Trackpad: Whether to enable changing facial expressions using the trackpad.
- Reset Upon Moving: Resets the avatar's facial expression when moving. This setting is primarily for Vive Controllers.
- Stop Distance: The animation stops if the avatar is further away than this distance.
