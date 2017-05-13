using System;
using GameServer;
using GameServer.Interfaces;
using ServiceLayer;
using UserManagement;

namespace AcceptanceTests.Bridge
{
    
    class Driver
    {

        public static IBridge GetBridge()
        {
            //chenge to real when the real instance is ready
            Proxy proxy = new Proxy();
         //   IUserDataBase db = new UserDataBase();
            UserDataBase db = UserDataBase.Instance;

            IUserEnforcer uh = new UserEnforcer();
            IGameCenter gc = GameCenter.Instance;
            
            Real real = new Real(new UserController(uh,db),new GameController(gc));
            //chenge to real on connection
          //  proxy.SetRealBridge(real);
            return real;
        }
    }
}
