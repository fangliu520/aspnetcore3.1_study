using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern.User
{
    public class UserSystem : IUserSystem
    {
        public bool CheckUser(int userId)
        {
            return userId > 10;
        }

        public void Login(int userId)
        {
            throw new NotImplementedException();
        }

    }
}
