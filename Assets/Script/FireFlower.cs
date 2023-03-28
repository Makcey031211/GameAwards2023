using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    //- 他のスクリプトから爆破するためにこの変数をtrueにする
    //- このスクリプトを持っているオブジェクトの花火スクリプトが
    //- この変数を参照しており、trueになると花火挙動が始まる
    public bool isExploded = false; // 爆発したかどうか
}
