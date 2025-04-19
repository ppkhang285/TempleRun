using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using XLua;

public class LuaManager
{

    private static LuaManager _instance;

    private LuaEnv _luaEnv;
    private Dictionary<string, byte[]> luaFiles;

    public static LuaManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LuaManager();
            }
            return _instance;
        }
    }


    public LuaManager()
    {
        
    }

    public void Init()
    {
        luaFiles = new Dictionary<string, byte[]>();
        LoadLuaBundle();
    }

    private void LoadLuaBundle()
    {
        AssetBundle luaBundle = null;
        if (luaBundle == null)
        {
            string path = System.IO.Path.Combine(Application.streamingAssetsPath, "Lua/lua_bundle");
            luaBundle = AssetBundle.LoadFromFile(path);
            if (luaBundle == null)
            {
                Debug.LogError("Failed to load Lua AssetBundle.");
            }
            Debug.Log("Lua AssetBundle loaded successfully.");

            TextAsset[] luaAssets = luaBundle.LoadAllAssets<TextAsset>();
            foreach (TextAsset luaAsset in luaAssets)
            {
                string fileName = luaAsset.name;
                luaFiles.Add(fileName, System.Text.Encoding.UTF8.GetBytes(luaAsset.text));
                
            }

            luaBundle.Unload(false);

        }
    }


    public LuaEnv LuaEnv
    {
        get
        {
            if (_luaEnv == null)
            {
                _luaEnv = new LuaEnv();
                _luaEnv.AddLoader((ref string filename) =>
                {
                    string fullFileName = filename + ".lua";

                    if (luaFiles.ContainsKey(fullFileName))
                    {
                        return luaFiles[fullFileName];
                    }
                    Debug.LogError("Cannot find Lua file: " + filename);
                    return null;
                });
            }
            return _luaEnv;
        }
    }

    public LuaTable LoadScript(string scriptName)
    {

        LuaTable script = LuaEnv.DoString($"return require '{scriptName}'")[0] as LuaTable;
        if (script == null)
        {
            Debug.LogError("Failed to load Lua script: " + scriptName);
        }
        return script;
    }

    public void OnDestroy()
    {
        if (_luaEnv != null)
        {
            _luaEnv.Dispose();
            _luaEnv = null;
        }
    }





}
