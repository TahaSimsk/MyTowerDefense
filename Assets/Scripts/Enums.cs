using System;

[Flags]
public enum Immunity
{
    None = 0,
    Freeze = 1,
    AllTraps = 2,
    Pierce = 4,
    StickyTrap = 8,
    Poison = 16,
}

public enum EnemyAttackType
{
    Melee,
    Ranged,
}

public enum GatherState
{
    Gatherable,
    Gathering,
    Gathered,
}



