using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoot  {

    void Fire();
    bool ShouldFire();
    void SetTarget(Transform toTarget);
}
