﻿namespace WebApi.AuthorizationModel
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; } = new UserRole();
    }
}
