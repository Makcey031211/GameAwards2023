using UnityEngine;

/*
 ===================
 §ìFûü´
 TvFIuWFNgÌ®ðÇ·éXNvg
 ===================
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
        Clockwise,        // ½vñè
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

        //* Oûü®Ú *//
        startPosition = transform.position;
        endPosition   = startPosition + Vector3.right * MoveDistance;

        //* O_Ô®Ú *//
        points[0] = StartPoint;    // zñÌ0ÔÚÉn_ðãü
        points[1] = HalfwayPoint;  // zñÌ1ÔÚÉÔ_ðãü
        points[2] = EndPoint;      // zñÌ2ÔÚÉI_ðãü

        //* ~®Ú *//
        currentTime += StartTime; // JnÉÔð¸ç·
    }

    void Update()
    {
        //- Ið·é®Ì^CvÉ¶Äðªò
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
        //- ððSÄ½µÄ¢éAÈºðµÈ¢
        if (StopMove && fireworks && fireworks.IsExploded) return;

        //- oßÔðvZ·é
        timeElapsed += Time.deltaTime;

        //- Ú®ÌðvZ·éi0©ç1ÜÅÌlj
        float t = Mathf.Clamp01(timeElapsed / TravelTime);

        //- Ú®ûüÉí¹ÄÊuðÏX·é
        if (!reverse)
        {
            //- Ú®ûüÉ¶ÄÊuðâÔ·é
            switch (MoveDirection)
            {
                case E_MoveDirection.Horizontal: // ½
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    break;
                case E_MoveDirection.Vertical:   // ¼
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + Vector3.up * MoveDistance, t);
                    break;
                case E_MoveDirection.Diagonal:   // Îpü
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + new Vector3(MoveDistance, MoveDistance, 0), t);
                    break;
            }
        }
        else
        {
            //- tûüÉÚ®·éê
            switch (MoveDirection)
            {
                case E_MoveDirection.Horizontal:  // ½
                    transform.position = Vector3.Lerp(endPosition, startPosition, t);
                    break;
                case E_MoveDirection.Vertical:    // ¼
                    transform.position = Vector3.Lerp(
                        startPosition + Vector3.up * MoveDistance, startPosition, t);
                    break;
                case E_MoveDirection.Diagonal:    // Îpü
                    transform.position = Vector3.Lerp(
                        startPosition + new Vector3(MoveDistance, MoveDistance, 0), startPosition, t);
                    break;
            }
        }

        //- Ú®ª®¹µ½çoßÔðZbg·é
        if (t == 1.0f)
        {
            timeElapsed = 0.0f; // oßÔÌZbg
            reverse = !reverse; // Ú®ûüð½]
        }
    }

    /// <summary>
    /// O_Ô®
    /// </summary>
    private void ThreePointMove()
    {
        //- ððSÄ½µÄ¢éAÈºðµÈ¢
        if (StopMove && fireworks && fireworks.IsExploded) return;

        //- IuWFNgªÒ@©Ç¤©
        if (isWaiting)
        {
            //- oßÔÉîÃ¢ÄÒ@Ôð¸­³¹é
            waitingTimer -= Time.deltaTime;

            //- Ò@Ôª0ÈºÉÈÁ½ê
            if (waitingTimer <= 0)
            {
                //- Ò@óÔðI¹·é
                isWaiting = false;
                //- »ÝÌûüÉîÃ¢ÄÌ|CgÉÚ®·é
                currentPoint += currentDirection;
                //- »ÝÌ|Cgª|CgÌÍÍOÅ é
                if (currentPoint >= points.Length || currentPoint < 0)
                {
                    //- Ú®ûüð½]³¹é
                    currentDirection *= -1;
                    //- ½]µ½ûüÌÌ|CgÉÚ®·é
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

            //- àµ»ÝÊuÆÌ|CgÆÌ£ªêèl¢Å êÎAÌ|CgÉBµ½Æ»f·é
            if (Vector3.Distance(transform.position, points[currentPoint]) < 0.01f)
            {
                //- àµ»ÝÌ|CgªI_Å êÎ
                if (currentPoint == points.Length - 1)
                {
                    //- Ò@óÔðLøÉ·é
                    isWaiting    = true;
                    //- wèµ½Ò@ÔªAÒ@·é
                    waitingTimer = EndWaitTime;
                }
                else
                {
                    //- »ÝÌûüÉîÃ¢ÄÌ|CgÉÚ®·é
                    currentPoint += currentDirection;
                    //- àµ»ÝÌ|Cgª|CgÌÍÍOÅ é
                    if (currentPoint >= points.Length || currentPoint < 0)
                    {
                        //- Ú®ûüð½]³¹é
                        currentDirection *= -1;
                        //- ½]µ½ûüÌÌ|CgÉÚ®·é
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
        //- ððSÄ½µÄ¢éAÈºðµÈ¢
        if (StopMove && fireworks && fireworks.IsExploded) return;

        //- IuWFNgªÒ@©Ç¤©
        if (isWaiting)
        {
            //- oßÔÉîÃ¢ÄÒ@Ôð¸­³¹é
            waitingTimer -= Time.deltaTime;

            //- Ò@Ôª0ÈºÉÈÁ½ê
            if (waitingTimer <= 0.0f)
            {
                //- Ò@óÔðI¹·é
                isWaiting = false;
                //- »ÝÌûüÉîÃ¢ÄÌ|CgÉÚ®·é
                currentPoint += currentDirection;
                //- »ÝÌ|Cgª|CgÌÍÍOÅ é
                if (currentPoint >= points.Length || currentPoint < 0)
                {
                    //- »ÝÌûüð½]³¹é
                    currentDirection *= -1;
                    //- ½]µ½ûüÌÌ|CgÉÚ®·é
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

            //- àµ»ÝÊuÆÌ|CgÆÌ£ªêèl¢Å êÎAÌ|CgÉBµ½Æ»f·é
            if (Vector3.Distance(transform.position, points[currentPoint]) < 0.01f)
            {
                //- àµ»ÝÌ|Cgªn_Ü½ÍÔ_Ü½ÍI_Å êÎ
                if (currentPoint == points.Length - 3 || currentPoint == points.Length - 2 
                    || currentPoint == points.Length - 1)
                {
                    //- Ò@óÔðLøÉ·é
                    isWaiting    = true;
                    //- wèµ½Ò@ÔªAÒ@·é
                    waitingTimer = WaitTime;
                }
                else
                {
                    //- »ÝÌûüÉîÃ¢ÄÌ|CgÉÚ®·é
                    currentPoint += currentDirection;
                    //- àµ»ÝÌ|Cgª|CgÌÍÍOÅ é
                    if (currentPoint >= points.Length || currentPoint < 0)
                    {
                        //- Ú®ûüð½]³¹é
                        currentDirection *= -1;
                        //- ½]µ½ûüÌÌ|CgÉÚ®·é
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
        //- ððSÄ½µÄ¢éAÈºðµÈ¢
        if (StopMove && fireworks && fireworks.IsExploded) return;

        //- TransformIuWFNgÌQÆðæ¾
        var trans = transform;

        //- »ÝÌpxÆ²ÉîÃ¢ÄNI[^jIð¶¬
        var angleAxis = Quaternion.AngleAxis(currentAngle, Axis);

        //- ¼aÉÎ·éxNgðì¬µAñ]²ÉÁÄñ]³¹é
        var radiusVec = angleAxis * (Vector3.up * Radius);

        //- S_É¼aÉÎ·éxNgðÁZµÄÊuðvZ·é
        var pos = Center + radiusVec;

        //- ÊuðXV·é
        trans.position = pos;

        //- ü«ðXV·é
        if (UpdateRotation)
        {  trans.rotation = Quaternion.LookRotation(Center - pos, Vector3.up);  }

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

    /// <summary>
    /// ç©È~^®
    /// </summary>
    private void SmoothCircularMove()
    {
        //- ððSÄ½µÄ¢éAÈºðµÈ¢
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

        //- transformðAÏtransÉi[·é
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
        {  trans.rotation = Quaternion.LookRotation(Center - pos, Vector3.up);  }

        //- »ÝÌñ]pxðXV·é
        currentTime += Time.deltaTime;
    }

    /// <summary>
    /// ®ªâ~µÄ¢é©»èðæéÖ
    /// </summary>
    /// <param name="moveFlag"></param>
    public void SetStopFrag(bool moveFlag)
    { StopMove = moveFlag; }
}