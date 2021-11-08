//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.1
//     from Assets/InputSystem/PlateControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlateControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlateControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlateControls"",
    ""maps"": [
        {
            ""name"": ""PlateActions"",
            ""id"": ""fcf22413-6d43-447f-af5a-7dc89563668b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c040082c-ce3b-4577-b60a-49352f249c29"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""8e16c20c-c9d6-46bc-b350-32fcaa5d2d92"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""8918f16e-fbd5-49ab-bc45-fdc2d9894c3a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""bcc3be96-6ee7-4f94-b154-128ace00fc9d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9e9c120d-564e-4de6-88a3-85da83a71b9b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""20a414d0-6498-416a-8891-0799dfd21ae4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c52e4551-442d-4163-aba0-c9589252f562"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""338ced84-2870-4e84-bcb5-372ab3896f83"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlateActions
        m_PlateActions = asset.FindActionMap("PlateActions", throwIfNotFound: true);
        m_PlateActions_Movement = m_PlateActions.FindAction("Movement", throwIfNotFound: true);
        m_PlateActions_Rotation = m_PlateActions.FindAction("Rotation", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlateActions
    private readonly InputActionMap m_PlateActions;
    private IPlateActionsActions m_PlateActionsActionsCallbackInterface;
    private readonly InputAction m_PlateActions_Movement;
    private readonly InputAction m_PlateActions_Rotation;
    public struct PlateActionsActions
    {
        private @PlateControls m_Wrapper;
        public PlateActionsActions(@PlateControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlateActions_Movement;
        public InputAction @Rotation => m_Wrapper.m_PlateActions_Rotation;
        public InputActionMap Get() { return m_Wrapper.m_PlateActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlateActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlateActionsActions instance)
        {
            if (m_Wrapper.m_PlateActionsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlateActionsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlateActionsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlateActionsActionsCallbackInterface.OnMovement;
                @Rotation.started -= m_Wrapper.m_PlateActionsActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlateActionsActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlateActionsActionsCallbackInterface.OnRotation;
            }
            m_Wrapper.m_PlateActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
            }
        }
    }
    public PlateActionsActions @PlateActions => new PlateActionsActions(this);
    public interface IPlateActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
    }
}
