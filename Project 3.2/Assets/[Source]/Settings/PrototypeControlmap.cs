// GENERATED AUTOMATICALLY FROM 'Assets/[Source]/Settings/PrototypeControlmap.inputactions'

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;


[Serializable]
public class PrototypeControlmap : InputActionAssetReference
{
    public PrototypeControlmap()
    {
    }
    public PrototypeControlmap(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // gameplay
        m_gameplay = asset.GetActionMap("gameplay");
        m_gameplay_PrimaryInteract = m_gameplay.GetAction("PrimaryInteract");
        if (m_gameplayPrimaryInteractActionStarted != null)
            m_gameplay_PrimaryInteract.started += m_gameplayPrimaryInteractActionStarted.Invoke;
        if (m_gameplayPrimaryInteractActionPerformed != null)
            m_gameplay_PrimaryInteract.performed += m_gameplayPrimaryInteractActionPerformed.Invoke;
        if (m_gameplayPrimaryInteractActionCancelled != null)
            m_gameplay_PrimaryInteract.cancelled += m_gameplayPrimaryInteractActionCancelled.Invoke;
        m_gameplay_SecondaryInteract = m_gameplay.GetAction("SecondaryInteract");
        if (m_gameplaySecondaryInteractActionStarted != null)
            m_gameplay_SecondaryInteract.started += m_gameplaySecondaryInteractActionStarted.Invoke;
        if (m_gameplaySecondaryInteractActionPerformed != null)
            m_gameplay_SecondaryInteract.performed += m_gameplaySecondaryInteractActionPerformed.Invoke;
        if (m_gameplaySecondaryInteractActionCancelled != null)
            m_gameplay_SecondaryInteract.cancelled += m_gameplaySecondaryInteractActionCancelled.Invoke;
        m_gameplay_Move = m_gameplay.GetAction("Move");
        if (m_gameplayMoveActionStarted != null)
            m_gameplay_Move.started += m_gameplayMoveActionStarted.Invoke;
        if (m_gameplayMoveActionPerformed != null)
            m_gameplay_Move.performed += m_gameplayMoveActionPerformed.Invoke;
        if (m_gameplayMoveActionCancelled != null)
            m_gameplay_Move.cancelled += m_gameplayMoveActionCancelled.Invoke;
        m_gameplay_Menu = m_gameplay.GetAction("Menu");
        if (m_gameplayMenuActionStarted != null)
            m_gameplay_Menu.started += m_gameplayMenuActionStarted.Invoke;
        if (m_gameplayMenuActionPerformed != null)
            m_gameplay_Menu.performed += m_gameplayMenuActionPerformed.Invoke;
        if (m_gameplayMenuActionCancelled != null)
            m_gameplay_Menu.cancelled += m_gameplayMenuActionCancelled.Invoke;
        m_gameplay_Zoom = m_gameplay.GetAction("Zoom");
        if (m_gameplayZoomActionStarted != null)
            m_gameplay_Zoom.started += m_gameplayZoomActionStarted.Invoke;
        if (m_gameplayZoomActionPerformed != null)
            m_gameplay_Zoom.performed += m_gameplayZoomActionPerformed.Invoke;
        if (m_gameplayZoomActionCancelled != null)
            m_gameplay_Zoom.cancelled += m_gameplayZoomActionCancelled.Invoke;
        m_gameplay_ToggleMouseRotation = m_gameplay.GetAction("ToggleMouseRotation");
        if (m_gameplayToggleMouseRotationActionStarted != null)
            m_gameplay_ToggleMouseRotation.started += m_gameplayToggleMouseRotationActionStarted.Invoke;
        if (m_gameplayToggleMouseRotationActionPerformed != null)
            m_gameplay_ToggleMouseRotation.performed += m_gameplayToggleMouseRotationActionPerformed.Invoke;
        if (m_gameplayToggleMouseRotationActionCancelled != null)
            m_gameplay_ToggleMouseRotation.cancelled += m_gameplayToggleMouseRotationActionCancelled.Invoke;
        m_gameplay_RotateYaw = m_gameplay.GetAction("RotateYaw");
        if (m_gameplayRotateYawActionStarted != null)
            m_gameplay_RotateYaw.started += m_gameplayRotateYawActionStarted.Invoke;
        if (m_gameplayRotateYawActionPerformed != null)
            m_gameplay_RotateYaw.performed += m_gameplayRotateYawActionPerformed.Invoke;
        if (m_gameplayRotateYawActionCancelled != null)
            m_gameplay_RotateYaw.cancelled += m_gameplayRotateYawActionCancelled.Invoke;
        // menu
        m_menu = asset.GetActionMap("menu");
        m_menu_navigate = m_menu.GetAction("navigate");
        if (m_menuNavigateActionStarted != null)
            m_menu_navigate.started += m_menuNavigateActionStarted.Invoke;
        if (m_menuNavigateActionPerformed != null)
            m_menu_navigate.performed += m_menuNavigateActionPerformed.Invoke;
        if (m_menuNavigateActionCancelled != null)
            m_menu_navigate.cancelled += m_menuNavigateActionCancelled.Invoke;
        m_menu_click = m_menu.GetAction("click");
        if (m_menuClickActionStarted != null)
            m_menu_click.started += m_menuClickActionStarted.Invoke;
        if (m_menuClickActionPerformed != null)
            m_menu_click.performed += m_menuClickActionPerformed.Invoke;
        if (m_menuClickActionCancelled != null)
            m_menu_click.cancelled += m_menuClickActionCancelled.Invoke;
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        if (m_GameplayActionsCallbackInterface != null)
        {
            gameplay.SetCallbacks(null);
        }
        m_gameplay = null;
        m_gameplay_PrimaryInteract = null;
        if (m_gameplayPrimaryInteractActionStarted != null)
            m_gameplay_PrimaryInteract.started -= m_gameplayPrimaryInteractActionStarted.Invoke;
        if (m_gameplayPrimaryInteractActionPerformed != null)
            m_gameplay_PrimaryInteract.performed -= m_gameplayPrimaryInteractActionPerformed.Invoke;
        if (m_gameplayPrimaryInteractActionCancelled != null)
            m_gameplay_PrimaryInteract.cancelled -= m_gameplayPrimaryInteractActionCancelled.Invoke;
        m_gameplay_SecondaryInteract = null;
        if (m_gameplaySecondaryInteractActionStarted != null)
            m_gameplay_SecondaryInteract.started -= m_gameplaySecondaryInteractActionStarted.Invoke;
        if (m_gameplaySecondaryInteractActionPerformed != null)
            m_gameplay_SecondaryInteract.performed -= m_gameplaySecondaryInteractActionPerformed.Invoke;
        if (m_gameplaySecondaryInteractActionCancelled != null)
            m_gameplay_SecondaryInteract.cancelled -= m_gameplaySecondaryInteractActionCancelled.Invoke;
        m_gameplay_Move = null;
        if (m_gameplayMoveActionStarted != null)
            m_gameplay_Move.started -= m_gameplayMoveActionStarted.Invoke;
        if (m_gameplayMoveActionPerformed != null)
            m_gameplay_Move.performed -= m_gameplayMoveActionPerformed.Invoke;
        if (m_gameplayMoveActionCancelled != null)
            m_gameplay_Move.cancelled -= m_gameplayMoveActionCancelled.Invoke;
        m_gameplay_Menu = null;
        if (m_gameplayMenuActionStarted != null)
            m_gameplay_Menu.started -= m_gameplayMenuActionStarted.Invoke;
        if (m_gameplayMenuActionPerformed != null)
            m_gameplay_Menu.performed -= m_gameplayMenuActionPerformed.Invoke;
        if (m_gameplayMenuActionCancelled != null)
            m_gameplay_Menu.cancelled -= m_gameplayMenuActionCancelled.Invoke;
        m_gameplay_Zoom = null;
        if (m_gameplayZoomActionStarted != null)
            m_gameplay_Zoom.started -= m_gameplayZoomActionStarted.Invoke;
        if (m_gameplayZoomActionPerformed != null)
            m_gameplay_Zoom.performed -= m_gameplayZoomActionPerformed.Invoke;
        if (m_gameplayZoomActionCancelled != null)
            m_gameplay_Zoom.cancelled -= m_gameplayZoomActionCancelled.Invoke;
        m_gameplay_ToggleMouseRotation = null;
        if (m_gameplayToggleMouseRotationActionStarted != null)
            m_gameplay_ToggleMouseRotation.started -= m_gameplayToggleMouseRotationActionStarted.Invoke;
        if (m_gameplayToggleMouseRotationActionPerformed != null)
            m_gameplay_ToggleMouseRotation.performed -= m_gameplayToggleMouseRotationActionPerformed.Invoke;
        if (m_gameplayToggleMouseRotationActionCancelled != null)
            m_gameplay_ToggleMouseRotation.cancelled -= m_gameplayToggleMouseRotationActionCancelled.Invoke;
        m_gameplay_RotateYaw = null;
        if (m_gameplayRotateYawActionStarted != null)
            m_gameplay_RotateYaw.started -= m_gameplayRotateYawActionStarted.Invoke;
        if (m_gameplayRotateYawActionPerformed != null)
            m_gameplay_RotateYaw.performed -= m_gameplayRotateYawActionPerformed.Invoke;
        if (m_gameplayRotateYawActionCancelled != null)
            m_gameplay_RotateYaw.cancelled -= m_gameplayRotateYawActionCancelled.Invoke;
        if (m_MenuActionsCallbackInterface != null)
        {
            menu.SetCallbacks(null);
        }
        m_menu = null;
        m_menu_navigate = null;
        if (m_menuNavigateActionStarted != null)
            m_menu_navigate.started -= m_menuNavigateActionStarted.Invoke;
        if (m_menuNavigateActionPerformed != null)
            m_menu_navigate.performed -= m_menuNavigateActionPerformed.Invoke;
        if (m_menuNavigateActionCancelled != null)
            m_menu_navigate.cancelled -= m_menuNavigateActionCancelled.Invoke;
        m_menu_click = null;
        if (m_menuClickActionStarted != null)
            m_menu_click.started -= m_menuClickActionStarted.Invoke;
        if (m_menuClickActionPerformed != null)
            m_menu_click.performed -= m_menuClickActionPerformed.Invoke;
        if (m_menuClickActionCancelled != null)
            m_menu_click.cancelled -= m_menuClickActionCancelled.Invoke;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        var gameplayCallbacks = m_GameplayActionsCallbackInterface;
        var menuCallbacks = m_MenuActionsCallbackInterface;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
        gameplay.SetCallbacks(gameplayCallbacks);
        menu.SetCallbacks(menuCallbacks);
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // gameplay
    private InputActionMap m_gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private InputAction m_gameplay_PrimaryInteract;
    [SerializeField] private ActionEvent m_gameplayPrimaryInteractActionStarted;
    [SerializeField] private ActionEvent m_gameplayPrimaryInteractActionPerformed;
    [SerializeField] private ActionEvent m_gameplayPrimaryInteractActionCancelled;
    private InputAction m_gameplay_SecondaryInteract;
    [SerializeField] private ActionEvent m_gameplaySecondaryInteractActionStarted;
    [SerializeField] private ActionEvent m_gameplaySecondaryInteractActionPerformed;
    [SerializeField] private ActionEvent m_gameplaySecondaryInteractActionCancelled;
    private InputAction m_gameplay_Move;
    [SerializeField] private ActionEvent m_gameplayMoveActionStarted;
    [SerializeField] private ActionEvent m_gameplayMoveActionPerformed;
    [SerializeField] private ActionEvent m_gameplayMoveActionCancelled;
    private InputAction m_gameplay_Menu;
    [SerializeField] private ActionEvent m_gameplayMenuActionStarted;
    [SerializeField] private ActionEvent m_gameplayMenuActionPerformed;
    [SerializeField] private ActionEvent m_gameplayMenuActionCancelled;
    private InputAction m_gameplay_Zoom;
    [SerializeField] private ActionEvent m_gameplayZoomActionStarted;
    [SerializeField] private ActionEvent m_gameplayZoomActionPerformed;
    [SerializeField] private ActionEvent m_gameplayZoomActionCancelled;
    private InputAction m_gameplay_ToggleMouseRotation;
    [SerializeField] private ActionEvent m_gameplayToggleMouseRotationActionStarted;
    [SerializeField] private ActionEvent m_gameplayToggleMouseRotationActionPerformed;
    [SerializeField] private ActionEvent m_gameplayToggleMouseRotationActionCancelled;
    private InputAction m_gameplay_RotateYaw;
    [SerializeField] private ActionEvent m_gameplayRotateYawActionStarted;
    [SerializeField] private ActionEvent m_gameplayRotateYawActionPerformed;
    [SerializeField] private ActionEvent m_gameplayRotateYawActionCancelled;
    public struct GameplayActions
    {
        private PrototypeControlmap m_Wrapper;
        public GameplayActions(PrototypeControlmap wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryInteract { get { return m_Wrapper.m_gameplay_PrimaryInteract; } }
        public ActionEvent PrimaryInteractStarted { get { return m_Wrapper.m_gameplayPrimaryInteractActionStarted; } }
        public ActionEvent PrimaryInteractPerformed { get { return m_Wrapper.m_gameplayPrimaryInteractActionPerformed; } }
        public ActionEvent PrimaryInteractCancelled { get { return m_Wrapper.m_gameplayPrimaryInteractActionCancelled; } }
        public InputAction @SecondaryInteract { get { return m_Wrapper.m_gameplay_SecondaryInteract; } }
        public ActionEvent SecondaryInteractStarted { get { return m_Wrapper.m_gameplaySecondaryInteractActionStarted; } }
        public ActionEvent SecondaryInteractPerformed { get { return m_Wrapper.m_gameplaySecondaryInteractActionPerformed; } }
        public ActionEvent SecondaryInteractCancelled { get { return m_Wrapper.m_gameplaySecondaryInteractActionCancelled; } }
        public InputAction @Move { get { return m_Wrapper.m_gameplay_Move; } }
        public ActionEvent MoveStarted { get { return m_Wrapper.m_gameplayMoveActionStarted; } }
        public ActionEvent MovePerformed { get { return m_Wrapper.m_gameplayMoveActionPerformed; } }
        public ActionEvent MoveCancelled { get { return m_Wrapper.m_gameplayMoveActionCancelled; } }
        public InputAction @Menu { get { return m_Wrapper.m_gameplay_Menu; } }
        public ActionEvent MenuStarted { get { return m_Wrapper.m_gameplayMenuActionStarted; } }
        public ActionEvent MenuPerformed { get { return m_Wrapper.m_gameplayMenuActionPerformed; } }
        public ActionEvent MenuCancelled { get { return m_Wrapper.m_gameplayMenuActionCancelled; } }
        public InputAction @Zoom { get { return m_Wrapper.m_gameplay_Zoom; } }
        public ActionEvent ZoomStarted { get { return m_Wrapper.m_gameplayZoomActionStarted; } }
        public ActionEvent ZoomPerformed { get { return m_Wrapper.m_gameplayZoomActionPerformed; } }
        public ActionEvent ZoomCancelled { get { return m_Wrapper.m_gameplayZoomActionCancelled; } }
        public InputAction @ToggleMouseRotation { get { return m_Wrapper.m_gameplay_ToggleMouseRotation; } }
        public ActionEvent ToggleMouseRotationStarted { get { return m_Wrapper.m_gameplayToggleMouseRotationActionStarted; } }
        public ActionEvent ToggleMouseRotationPerformed { get { return m_Wrapper.m_gameplayToggleMouseRotationActionPerformed; } }
        public ActionEvent ToggleMouseRotationCancelled { get { return m_Wrapper.m_gameplayToggleMouseRotationActionCancelled; } }
        public InputAction @RotateYaw { get { return m_Wrapper.m_gameplay_RotateYaw; } }
        public ActionEvent RotateYawStarted { get { return m_Wrapper.m_gameplayRotateYawActionStarted; } }
        public ActionEvent RotateYawPerformed { get { return m_Wrapper.m_gameplayRotateYawActionPerformed; } }
        public ActionEvent RotateYawCancelled { get { return m_Wrapper.m_gameplayRotateYawActionCancelled; } }
        public InputActionMap Get() { return m_Wrapper.m_gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                PrimaryInteract.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimaryInteract;
                PrimaryInteract.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimaryInteract;
                PrimaryInteract.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimaryInteract;
                SecondaryInteract.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondaryInteract;
                SecondaryInteract.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondaryInteract;
                SecondaryInteract.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondaryInteract;
                Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Menu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
                Menu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
                Menu.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
                Zoom.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                Zoom.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                Zoom.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                ToggleMouseRotation.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleMouseRotation;
                ToggleMouseRotation.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleMouseRotation;
                ToggleMouseRotation.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleMouseRotation;
                RotateYaw.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateYaw;
                RotateYaw.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateYaw;
                RotateYaw.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateYaw;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                PrimaryInteract.started += instance.OnPrimaryInteract;
                PrimaryInteract.performed += instance.OnPrimaryInteract;
                PrimaryInteract.cancelled += instance.OnPrimaryInteract;
                SecondaryInteract.started += instance.OnSecondaryInteract;
                SecondaryInteract.performed += instance.OnSecondaryInteract;
                SecondaryInteract.cancelled += instance.OnSecondaryInteract;
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.cancelled += instance.OnMove;
                Menu.started += instance.OnMenu;
                Menu.performed += instance.OnMenu;
                Menu.cancelled += instance.OnMenu;
                Zoom.started += instance.OnZoom;
                Zoom.performed += instance.OnZoom;
                Zoom.cancelled += instance.OnZoom;
                ToggleMouseRotation.started += instance.OnToggleMouseRotation;
                ToggleMouseRotation.performed += instance.OnToggleMouseRotation;
                ToggleMouseRotation.cancelled += instance.OnToggleMouseRotation;
                RotateYaw.started += instance.OnRotateYaw;
                RotateYaw.performed += instance.OnRotateYaw;
                RotateYaw.cancelled += instance.OnRotateYaw;
            }
        }
    }
    public GameplayActions @gameplay
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new GameplayActions(this);
        }
    }
    // menu
    private InputActionMap m_menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private InputAction m_menu_navigate;
    [SerializeField] private ActionEvent m_menuNavigateActionStarted;
    [SerializeField] private ActionEvent m_menuNavigateActionPerformed;
    [SerializeField] private ActionEvent m_menuNavigateActionCancelled;
    private InputAction m_menu_click;
    [SerializeField] private ActionEvent m_menuClickActionStarted;
    [SerializeField] private ActionEvent m_menuClickActionPerformed;
    [SerializeField] private ActionEvent m_menuClickActionCancelled;
    public struct MenuActions
    {
        private PrototypeControlmap m_Wrapper;
        public MenuActions(PrototypeControlmap wrapper) { m_Wrapper = wrapper; }
        public InputAction @navigate { get { return m_Wrapper.m_menu_navigate; } }
        public ActionEvent navigateStarted { get { return m_Wrapper.m_menuNavigateActionStarted; } }
        public ActionEvent navigatePerformed { get { return m_Wrapper.m_menuNavigateActionPerformed; } }
        public ActionEvent navigateCancelled { get { return m_Wrapper.m_menuNavigateActionCancelled; } }
        public InputAction @click { get { return m_Wrapper.m_menu_click; } }
        public ActionEvent clickStarted { get { return m_Wrapper.m_menuClickActionStarted; } }
        public ActionEvent clickPerformed { get { return m_Wrapper.m_menuClickActionPerformed; } }
        public ActionEvent clickCancelled { get { return m_Wrapper.m_menuClickActionCancelled; } }
        public InputActionMap Get() { return m_Wrapper.m_menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                navigate.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                navigate.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                navigate.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                click.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnClick;
                click.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnClick;
                click.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                navigate.started += instance.OnNavigate;
                navigate.performed += instance.OnNavigate;
                navigate.cancelled += instance.OnNavigate;
                click.started += instance.OnClick;
                click.performed += instance.OnClick;
                click.cancelled += instance.OnClick;
            }
        }
    }
    public MenuActions @menu
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new MenuActions(this);
        }
    }
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get

        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get

        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_SteamSchemeIndex = -1;
    public InputControlScheme SteamScheme
    {
        get

        {
            if (m_SteamSchemeIndex == -1) m_SteamSchemeIndex = asset.GetControlSchemeIndex("Steam");
            return asset.controlSchemes[m_SteamSchemeIndex];
        }
    }
    private int m_VRSchemeIndex = -1;
    public InputControlScheme VRScheme
    {
        get

        {
            if (m_VRSchemeIndex == -1) m_VRSchemeIndex = asset.GetControlSchemeIndex("VR");
            return asset.controlSchemes[m_VRSchemeIndex];
        }
    }
    [Serializable]
    public class ActionEvent : UnityEvent<InputAction.CallbackContext>
    {
    }
}
public interface IGameplayActions
{
    void OnPrimaryInteract(InputAction.CallbackContext context);
    void OnSecondaryInteract(InputAction.CallbackContext context);
    void OnMove(InputAction.CallbackContext context);
    void OnMenu(InputAction.CallbackContext context);
    void OnZoom(InputAction.CallbackContext context);
    void OnToggleMouseRotation(InputAction.CallbackContext context);
    void OnRotateYaw(InputAction.CallbackContext context);
}
public interface IMenuActions
{
    void OnNavigate(InputAction.CallbackContext context);
    void OnClick(InputAction.CallbackContext context);
}
