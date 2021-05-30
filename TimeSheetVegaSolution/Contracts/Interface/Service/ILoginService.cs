using Model.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interface.Service
{
    public interface ILoginService
    {
        public UserModel AuthenticateUser(UserModel login);
        public string GenerateJSONWebToken(UserModel userInfo);
    }
}
