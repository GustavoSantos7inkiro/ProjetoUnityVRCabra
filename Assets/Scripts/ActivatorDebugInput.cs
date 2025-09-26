using UnityEngine;

using System.Collections.Generic;

using UnityEngine.XR; 

public class ActivatorDebugInput : MonoBehaviour
{
    [Header("Alvo que implementa IActivatable (DoorController, DrawerController, etc.)")]
    public MonoBehaviour target; 

    [Header("Entradas ativas")]
    public bool listenKeyboard = true;       // O / P
    public bool listenQuestButtons = true;   // A / B (controle direito)

    IActivatable _act;

    // Controle direito (Quest): primary = A, secondary = B
    InputDevice _rightHand;
    bool _prevPrimary, _prevSecondary;

    void Awake()
    {
        _act = target as IActivatable;
        if (_act == null && target != null)
            Debug.LogWarning($"[{nameof(ActivatorDebugInput)}] 'target' não implementa IActivatable.");
    }

    void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        FindRightHand();
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
    }

    void Update()
    {
        if (_act == null) return;

        // Teclado
        if (listenKeyboard)
        {
            if (Input.GetKeyDown(KeyCode.O)) _act.Activate();
            if (Input.GetKeyDown(KeyCode.P)) _act.Deactivate();
        }

        // Controle direito (Quest)
        if (listenQuestButtons && _rightHand.isValid)
        {
            bool primary = false, secondary = false;

            _rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out primary);     // A
            _rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out secondary); // B

            // borda de subida (apertou agora)
            if (primary && !_prevPrimary)   _act.Activate();
            if (secondary && !_prevSecondary) _act.Deactivate();

            _prevPrimary = primary;
            _prevSecondary = secondary;
        }
    }

    void OnDeviceConnected(InputDevice dev)
    {
        // pega o controle direito quando conectar
        var right = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        if ((dev.characteristics & right) == right)
            _rightHand = dev;
    }

    void FindRightHand()
    {
        var list = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, list);
        if (list.Count > 0) _rightHand = list[0];
    }
}