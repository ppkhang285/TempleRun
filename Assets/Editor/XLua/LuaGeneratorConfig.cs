using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using XLua;
using GameUtils;

public static class LuaGeneratorConfig 
{
    public static List<Type> LuaCallCSharp = new List<Type>()
    {
        typeof(BoxCollider),
        typeof(InputBindingSetting.InputBinding),
        typeof(InputBindingSetting),
        typeof(Queue),
    };

    public static List<Type>  CSharpCallLua = new List<Type>();

    public static List<Type>  GCOptimizeList = new List<Type>();

    public static Dictionary<Type, List<string>> AdditionalProperties = new Dictionary<Type, List<string>>();

    public static List<Type> ReflectionUse = new List<Type>();

    public static List<List<string>> BlackList = new List<List<string>>()
    {
        new List<string>() { "UnityEngine.Light", "SetLightDirty" },
        new List<string>() { "UnityEngine.Light", "shadowRadius" },
        new List<string>() { "UnityEngine.Light", "shadowAngle" },
    };

    public static Dictionary<Type, HotfixFlag> HotfixCfg = new Dictionary<Type, HotfixFlag>();

    public static Dictionary<Type, OptimizeFlag> OptimizeCfg = new Dictionary<Type, OptimizeFlag>();

    public static Dictionary<Type, HashSet<string>> DoNotGen = new Dictionary<Type, HashSet<string>>();
}
