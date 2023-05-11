using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// §ìFûü´
/*
 * IuWFNgÌ®ðÇ·éNX
 */
public class MovementManager : MonoBehaviour
{
    //--- ñÌè`(^Cv)
    public enum E_MovementType
    {
        ThreewayBehaviour,       // Oûü®
        ThreepointBehaviour,     // O_Ô®
        ThreepointWaitBehaviour, // O_ÔÒ@®
        CicrleBehaviour,         // ~®
        SmoothCircularBehaviour, // ç©È~®
    }

    //--- ñÌè`(ñ])
    public enum E_RotaDirection
    {
        Clockwise,        // ¼vüè
        CounterClockwise, // vñè
    }

    //--- ñÌè`(Ú®)
    public enum E_MoveDirection
    {
        Horizontal, // ¡Ú®
        Vertical,   // cÚ®
        Diagonal,   // ÎßÚ®
    }


    //* ¤ÊÖA *//
    //- CXyN^[É\¦
    [SerializeField, Header("®ÌíÞ")]
    public E_MovementType _type = E_MovementType.ThreewayBehaviour;
    [SerializeField, Header("®â~")]
    private bool StopMove = false;
    //- CXyN^[©çñ\¦
    FireworksModule fireworks;
    public E_MovementType Type => _type;

    //* Oûü®ÖA *//
    //- CXyN^[É\¦
    [SerializeField, HideInInspector]
    public E_MoveDirection _moveDirection = E_MoveDirection.Horizontal; // Ú®ûü
    [SerializeField, HideInInspector]
    public float _moveDistance = 5.0f; // Ú®£
    [SerializeField, HideInInspector]
    public float _travelTime   = 1.0f; // Ú®Ô
    //- CXyN^[©çñ\¦
    private Vector3 startPosition;   // JnÊu
    private Vector3 endPosition;     // I¹Êu
    private float   timeElapsed;     // oßÔ
    private bool    reverse = false; // Ú®Ìûü]·p
    //- O©çÌlæ¾p
    public E_MoveDirection MoveDirection => _moveDirection;
    public float MoveDistance => _moveDistance;
    public float TravelTime => _travelTime;

    //* O_Ô® *//
    //- CXyN^[É\¦
    [SerializeField, HideInInspector]
    public Vector3 _startPoint;   // n_
    [SerializeField, HideInInspector]
    public Vector3 _halfwayPoint; // Ô_
    [SerializeField, HideInInspector]
    public Vector3 _endPoint;     // I_
    [SerializeField, HideInInspector]
    public float _moveSpeed   = 1.0f;   // Ú®¬x
    [SerializeField, HideInInspector]
    public float _endWaitTime = 1.0f;   // I_BÌÒ@Ô
    [SerializeField, HideInInspector]
    public float _waitTime = 1.0f;      // e|CgBÌÒ@Ô
    //- CXyN^[©çñ\¦
    private Vector3[] points = new Vector3[3]; // zñÌÂªi[
    private int   currentPoint     = 0;     // »ÝÌÊu
    private int   currentDirection = 1;     // »ÝÌûü
    private float waitingTimer     = 0.0f;  // Ò@Ô
    private bool  isWaiting        = false; // Ò@µÄ¢é©µÄ¢È¢©
    //- O©çÌlæ¾p
    public Vector3 StartPoint => _startPoint;
    public Vector3 HalfwayPoint => _halfwayPoint;
    public Vector3 EndPoint => _endPoint;
    public float MoveSpeed => _moveSpeed;
    public float EndWaitTime => _endWaitTime;
    public float WaitTime => _waitTime;

    //* ~®ÖA *//
    //- CXyN^[É\¦
    [SerializeField, HideInInspector]
    public E_RotaDirection _rotaDirection = E_RotaDirection.Clockwise; // ñ]ûü
    [SerializeField, HideInInspector]
    public Vector3 _center = Vector3.zero;    // S_
    [SerializeField, HideInInspector]
    public Vector3 _axis   = Vector3.forward; // ñ]²
    [SerializeField, HideInInspector]
    public float _radius     = 1.0f; // ¼aÌå«³
    [SerializeField, HideInInspector]
    public float _startTime  = 1.0f; // JnÉ¸ç·Ô(b)
    [SerializeField, HideInInspector]
    public float _periodTime = 2.0f; // êüñéÌÉ©©éÔ(b)
    [SerializeField, HideInInspector]
    public bool _updateRotation = false; // ü«ðXV·é©Ç¤©
    //- CXyN^[©çñ\¦
    private float currentTime;  // »ÝÌÔ
    private float currentAngle; // »ÝÌñ]px
    private float angle = 360f; // êüªÌpx
    //- O©çÌlæ¾p
    public E_RotaDirection RotaDirection => _rotaDirection;
    public Vector3 Center => _center;
    public Vector3 Axis => _axis;
    public float Radius => _radius;
    public float StartTime => _startTime;
    public float PeriodTime => _periodTime;
    public bool UpdateRotation => _updateRotation;


    void Start()
    {
        //* ¤ÊÚ *//
        fireworks = this.gameObject.GetComponent<FireworksModule>();
        currentTime += StartTime; // JnÉÔð¸ç·

        //* Oûü®Ú *//
        startPosition = transform.position;
        endPosition   = startPosition + Vector3.right * MoveDistance;

        //* O_Ô®Ú *//
        points[0] = StartPoint;
        points[1] = HalfwayPoint;
        points[2] = EndPoint;
    }

    void Update()
    {
        //- Ið·é^CvÉ¶Äðªò
        switch (Type)
        {
            case E_MovementType.ThreewayBehaviour:
                ThreewayMove();
                break;
            case E_MovementType.ThreepointBehaviour:
                ThreePointMove();
                break;
            case E_MovementType.ThreepointWaitBehaviour:
                ThreePointWaitMove();
                break;
            case E_MovementType.CicrleBehaviour:
                CicrleMove();
                break;
            case E_MovementType.SmoothCircularBehaviour:
                SmoothCircularMove();
                break;
        }
    }

    /// <summary>
    /// Oûü®
    /// </summary>
    private void ThreewayMove()
    {
        //- null`FbN
        if (StopMove && fireworks && fireworks.IsExploded) return;

        //- oßÔðvZ·é
        timeElapsed += Time.deltaTime;

        //- Ú®ÌðvZ·éi0©ç1ÜÅÌlj
        float t = Mathf.Clamp01(timeElapsed / TravelTime);

        //- Ú®ûüÉí¹ÄÊuðÏX·é
        if (!reverse)
        {
            switch (MoveDirection)
            {
                case E_MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    break;
                case E_MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + Vector3.up * MoveDistance, t);
                    break;
                case E_MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + new Vector3(MoveDistance, MoveDistance, 0), t);
                    break;
            }
        }
        else
        {
            switch (MoveDirection)
            {
                case E_MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(endPosition, startPosition, t);
                    break;
                case E_MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition + Vector3.up * MoveDistance, startPosition, t);
                    break;
                case E_MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition + new Vector3(MoveDistance, MoveDistance, 0), startPosition, t);
                    break;
            }
        }

        //- Ú®ª®¹µ½çoßÔðZbg·é
        if (t == 1.0f)
        {
            timeElapsed = 0.0f;
            reverse = !reverse; // Ú®ûüð½]
        }
    }

    /// <summary>
    /// O_Ô®
    /// </summary>
    private void ThreePointMove()
    {
        //- null`FbN
        if (StopMove && fireworks && fireworks.IsExploded) return;

        if (isWaiting)
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer <= 0.0f)
            {
                isWaiting = false;
                currentPoint += currentDirection;
                if (currentPoint >= points.Length || currentPoint < 0)
                {
                    currentDirection *= -1;
                    currentPoint += currentDirection;
                }
            }
        }
        else
        {
            //- ÌÊuÉÚ®·é½ßÌûüxNgðvZ·é
            Vector3 directionVector = (points[currentPoint] - transform.position).normalized;

            //- ÌÊuÉÚ®·é½ßÌ£ðvZ·é
            float distanceToMove = MoveSpeed * Time.deltaTime;

            //- ÌÊuÉÚ®·é
            transform.position += directionVector * distanceToMove;

            //- Ì|CgÉBµ½çûüðtÉ·é
            if (Vector3.Distance(transform.position, points[currentPoint]) < 0.01f)
            {
                //- ÅãÌ|CgÉBµ½ç
                if (currentPoint == points.Length - 1)
                {
                    isWaiting    = true;
                    waitingTimer = EndWaitTime;
                }
                else
                {
                    currentPoint += currentDirection;
                    if (currentPoint >= points.Length || currentPoint < 0)
                    {
                        currentDirection *= -1;
                        currentPoint += currentDirection;
                    }
                }
            }
        }
    }

    /// <summary>
    /// O_Ô®(e|CgÒ@)
    /// </summary>
    private void ThreePointWaitMove()
    {
        //- null`FbN
        if (StopMove && fireworks && fireworks.IsExploded) return;

        if (isWaiting)
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer <= 0.0f)
            {
                isWaiting = false;
                currentPoint += currentDirection;
                if (currentPoint >= points.Length || currentPoint < 0)
                {
                    currentDirection *= -1;
                    currentPoint += currentDirection;
                }
            }
        }
        else
        {
            //- ÌÊuÉÚ®·é½ßÌûüxNgðvZ·é
            Vector3 directionVector = (points[currentPoint] - transform.position).normalized;

            //- ÌÊuÉÚ®·é½ßÌ£ðvZ·é
            float distanceToMove = MoveSpeed * Time.deltaTime;

            //- ÌÊuÉÚ®·é
            transform.position += directionVector * distanceToMove;

            //- Ì|CgÉBµ½çûüðtÉ·é
            if (Vector3.Distance(transform.position, points[currentPoint]) < 0.01f)
            {
                //- n_Ü½ÍÔ_Ü½ÍI_ÉBµ½ç
                if (currentPoint == points.Length - 3 || currentPoint == points.Length - 2 
                    || currentPoint == points.Length - 1)
                {
                    isWaiting    = true;
                    waitingTimer = WaitTime;
                }
                else
                {
                    currentPoint += currentDirection;
                    if (currentPoint >= points.Length || currentPoint < 0)
                    {
                        currentDirection *= -1;
                        currentPoint += currentDirection;
                    }
                }
            }
        }
    }

    /// <summary>
    /// ~^®
    /// </summary>
    private void CicrleMove()
    {
        //- null`FbN
        if (StopMove && fireworks && fireworks.IsExploded) return;

        var trans = transform;

        //- ñ]ÌNH[^jIì¬
        var angleAxis = Quaternion.AngleAxis(currentAngle, Axis);

        //- ¼aÉÎ·éxNgðì¬µAñ]²ÉÁÄñ]³¹é
        var radiusVec = angleAxis * (Vector3.up * Radius);

        //- S_É¼aÉÎ·éxNgðÁZµÄÊuðvZ·é
        var pos = Center + radiusVec;

        //- ÊuðXV·é
        trans.position = pos;

        //- ü«ðXV·é
        if (UpdateRotation)
        {
            trans.rotation = Quaternion.LookRotation(Center - pos, Vector3.up);
        }

        //- »ÝÌñ]pxðXV·é
        currentTime += Time.deltaTime;

        //- ñ]ûüÉ¶Äðªò
        switch (RotaDirection)
        {
            case E_RotaDirection.Clockwise:
                currentAngle = (currentTime % PeriodTime) / PeriodTime * angle;
                break;
            case E_RotaDirection.CounterClockwise:
                currentAngle = angle - ((currentTime % PeriodTime) / PeriodTime * angle);
                break;
        }
    }

    private void SmoothCircularMove()
    {
        //- null`FbN
        if (StopMove && fireworks && fireworks.IsExploded) return;

        //- ñ]ûüÉ¶Äðªò
        switch (RotaDirection)
        {
            case E_RotaDirection.Clockwise:
                currentAngle = (currentTime % PeriodTime) / PeriodTime * angle;
                break;
            case E_RotaDirection.CounterClockwise:
                currentAngle = angle - ((currentTime % PeriodTime) / PeriodTime * angle);
                break;
        }

        var trans = transform;

        //- ñ]ÌNH[^jIì¬
        var angleAxis = Quaternion.AngleAxis(currentAngle, Axis);

        //- ¼aÉÎ·éxNgðì¬µAñ]²ÉÁÄñ]³¹é
        var radiusVec = angleAxis * (Vector3.down * Radius);

        //- S_É¼aÉÎ·éxNgðÁZµÄÊuðvZ·é
        var pos = Center + radiusVec;

        //- ÊuðXV·é
        trans.position = pos;

        //- ü«ðXV·é
        if (UpdateRotation)
        {
            trans.rotation = Quaternion.LookRotation(Center - pos, Vector3.up);
        }

        //- »ÝÌñ]pxðXV·é
        currentTime += Time.deltaTime;
    }
}