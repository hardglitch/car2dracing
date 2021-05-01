// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""PlayerControllers"",
            ""id"": ""9c32d5db-77eb-425d-93fd-d3083b9e466e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b55c3c34-0d2e-4f53-8d57-7670eb9f9ca1"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""33240d51-2882-4cae-9e67-161de1bd99a3"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6e92c8b0-e667-4552-954e-80fd3fb6a3c8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CPU"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d844b77b-3a35-49bd-ac60-c83f19e2a968"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CPU"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""CPU"",
            ""bindingGroup"": ""CPU"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Phone"",
            ""bindingGroup"": ""Phone"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControllers
        m_PlayerControllers = asset.FindActionMap("PlayerControllers", throwIfNotFound: true);
        m_PlayerControllers_Move = m_PlayerControllers.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerControllers
    private readonly InputActionMap m_PlayerControllers;
    private IPlayerControllersActions m_PlayerControllersActionsCallbackInterface;
    private readonly InputAction m_PlayerControllers_Move;
    public struct PlayerControllersActions
    {
        private @PlayerController m_Wrapper;
        public PlayerControllersActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControllers_Move;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControllers; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControllersActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControllersActions instance)
        {
            if (m_Wrapper.m_PlayerControllersActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControllersActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControllersActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControllersActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerControllersActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerControllersActions @PlayerControllers => new PlayerControllersActions(this);
    private int m_CPUSchemeIndex = -1;
    public InputControlScheme CPUScheme
    {
        get
        {
            if (m_CPUSchemeIndex == -1) m_CPUSchemeIndex = asset.FindControlSchemeIndex("CPU");
            return asset.controlSchemes[m_CPUSchemeIndex];
        }
    }
    private int m_PhoneSchemeIndex = -1;
    public InputControlScheme PhoneScheme
    {
        get
        {
            if (m_PhoneSchemeIndex == -1) m_PhoneSchemeIndex = asset.FindControlSchemeIndex("Phone");
            return asset.controlSchemes[m_PhoneSchemeIndex];
        }
    }
    public interface IPlayerControllersActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
