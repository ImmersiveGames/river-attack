using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable { }

public interface IHasPool
{
    void StartMyPool(bool isPersistent = false);
}
