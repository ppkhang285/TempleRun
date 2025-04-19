#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class GameUtilsConstantsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameUtils.Constants);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 9, 0, 0);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CHARACTER_VERTICAL_VELOCITY", GameUtils.Constants.CHARACTER_VERTICAL_VELOCITY);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GRAVITY", GameUtils.Constants.GRAVITY);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DESTROY_DISTANCE", GameUtils.Constants.DESTROY_DISTANCE);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SCORE_PER_COIN", GameUtils.Constants.SCORE_PER_COIN);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SCORE_PER_ITEM", GameUtils.Constants.SCORE_PER_ITEM);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MAX_POWERUP_LEVEL", GameUtils.Constants.MAX_POWERUP_LEVEL);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DIRECTION_VECTOR", GameUtils.Constants.DIRECTION_VECTOR);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ROTATION_VECTOR", GameUtils.Constants.ROTATION_VECTOR);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "GameUtils.Constants does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        
        
        
        
        
		
		
		
		
    }
}
