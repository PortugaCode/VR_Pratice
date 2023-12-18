using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================== VR
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
//====================================

public class TeleportModi : MonoBehaviour
{
    [SerializeField] private XRRayInteractor ray;
    [SerializeField] private XRInteractorLineVisual lineRenderer;

    [SerializeField] private TeleportationProvider teleportationProvider;

    [Header("Layer - Teleport Area")]
    [SerializeField] private InteractionLayerMask Teleport_area;

    [Header("Layer - Teleport Anchor")]
    [SerializeField] private InteractionLayerMask Teleport_anchor;


    [SerializeField] private InputActionProperty TelePortModeActive;
    [SerializeField] private InputActionProperty TelePortModeCancel;
    [SerializeField] private InputActionProperty GripmodeActive;
    [SerializeField] private InputActionProperty Trigger;

    public bool isRayActive = false;
    private bool isActive;
    private InteractionLayerMask interactionLayer;
    private List<IXRInteractable> interactables = new List<IXRInteractable>();

    private void Start()
    {
        // <������ ���� ����ϱ� ���� ����>
        // Action.Enable -> ������ ���� ��(Performed) �̺�Ʈ �߰�

        // Action Ȱ��ȭ
        TelePortModeActive.action.Enable();
        TelePortModeCancel.action.Enable();

        // Action �̺�Ʈ �߰�
        TelePortModeActive.action.performed += OnTeleportActive;
        TelePortModeCancel.action.performed += OnTelePortCancel;

        interactionLayer = ray.interactionLayers;
        lineRenderer = ray.gameObject.GetComponent<XRInteractorLineVisual>();

        if(!isRayActive)
        {
            lineRenderer.enabled = isRayActive;
        }
    }

    private void Update()
    {
        if (!isActive) return;

        if(Trigger.action.IsPressed() || TelePortModeActive.action.IsPressed())
        {
            if(!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            return;
        }

        ray.GetValidTargets(interactables);
        if(interactables.Count <= 0)
        {
            TurnOff_Teleport();
            return;
        }
        ray.TryGetCurrent3DRaycastHit(out RaycastHit hit);

        TeleportRequest request = new TeleportRequest();
        if(interactables[0].interactionLayers.Equals(Teleport_area)) // area�� ������ ���Ѵ�
        {
            //teleport Area
            Vector3 pos = new Vector3(hit.point.x, request.destinationPosition.y, hit.point.z);
            request.destinationPosition = pos;

            lineRenderer.enabled = false;
        }
        else if(interactables[0].interactionLayers.Equals(Teleport_anchor)) //��Ŀ�� ������ �� �� ��ġ���� �� �� �ִ� ��
        {
            request.destinationPosition = hit.transform.GetChild(0).transform.position;
            lineRenderer.enabled = false;
        }

        teleportationProvider.QueueTeleportRequest(request);
        TurnOff_Teleport();
    }

    private void OnTeleportActive(InputAction.CallbackContext obj)
    {
        //
        if(GripmodeActive.action.phase != InputActionPhase.Performed ||
            TelePortModeCancel.action.phase != InputActionPhase.Performed)
        {
            isActive = true;
            ray.lineType = XRRayInteractor.LineType.ProjectileCurve;
            ray.interactionLayers = Teleport_area;
            ray.interactionLayers |= Teleport_anchor; // Layer Mix ���� ���
        }
    }

    private void OnTelePortCancel(InputAction.CallbackContext obj)
    {
        TurnOff_Teleport();
    }

    private void TurnOff_Teleport()
    {
        isActive = false;
        ray.lineType = XRRayInteractor.LineType.StraightLine;
        ray.interactionLayers = interactionLayer;
    }



}