using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern.User
{
    interface IUserSystem
    {
        bool CheckUser(int userId);
        void Login(int userId);
    }
}
