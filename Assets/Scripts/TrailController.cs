using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MT_MeshTrail;

public class TrailController : MonoBehaviour
{
    public ParticleSystem splashParticles, trailParticle;
    public Vector3 scale;

    [Space(10)]
    public MT_MeshTrailRenderer trailRenderer;
    private MT_SnowMeshTrailUpdater trailUpdater;

    PlayerController controller => RefManager.I.playerController;

    public Vector3 edgeOffset;
    public Vector3 trailScaleOffset;

    [Range(0.15f, 1)]
    public float tempRange;
    public float tempTilt;

    [Space(10)]
    public bool isMidair;
    private bool instantiateSnowPuff;

    [Space(10)]
    public float arcTrail = 2;

    public Transform board;
    // Start is called before the first frame update
    void Start()
    {
        scale = trailParticle.transform.localScale;
        // controller = GetComponent<PlayerController>();
        trailUpdater = trailRenderer.GetComponent<MT_SnowMeshTrailUpdater>();
    }

    // Update is called once per frame
    void Update()
    {
        trailUpdater.IsDrawing = controller.isGrounded;
        trailUpdater.offset.x = (board.localPosition.x + edgeOffset.x) * controller.tilt;
        trailUpdater.offset.y = (edgeOffset.y);
        trailUpdater.offset.z = (edgeOffset.z);

        var difference = Mathf.Abs(controller.tilt) - tempTilt;
        tempRange = (1 - difference);

        trailScaleOffset.x = Mathf.Clamp(0.15f * tempRange, 0.025f, 0.15f);
        trailScaleOffset.y = Mathf.Clamp(0.5f * tempRange, 0.1f, 0.5f);
        trailScaleOffset.z = trailRenderer.Scale.z;

        trailRenderer.Scale = trailScaleOffset;

        if (controller.isGrounded)
            trailParticle.enableEmission = true;
        else
            trailParticle.enableEmission = false;

        var h = ControlFreak2.CF2Input.GetAxis("Horizontal");
        var v = ControlFreak2.CF2Input.GetAxis("Vertical");
        //trailParticle.transform.localScale = (scale * (Mathf.Abs(h) > 0 ? 2.5f : 1)) * (Mathf.Abs(h) > 0 ? Mathf.Abs(h) : 1);

        var pMain = trailParticle.main;
        var curve = pMain.startSize;

        curve.constantMax = Mathf.Clamp(pMain.startSize.constantMax * 3 * Mathf.Abs(h), 3, 9);
        curve.constantMin = Mathf.Clamp(pMain.startSize.constantMin * 3 * Mathf.Abs(h), 5, 15);
        pMain.startSize = curve;

        var vot = trailParticle.velocityOverLifetime;
        vot.orbitalY = -arcTrail * h * v;


        //trailParticle.transform.localPosition = new Vector3(0, 0, 1 * h);

        if (isMidair && controller.isGrounded)
        {
            ParticleSystem p = Instantiate(splashParticles, transform.position, transform.rotation);
            p.Play(true);
            Destroy(p.gameObject, 3);
            isMidair = false;
        }
        else if (!controller.isGrounded)
            isMidair = true;
    }
}
