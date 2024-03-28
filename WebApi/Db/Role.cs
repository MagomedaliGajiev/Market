﻿namespace WebApi.Db
{
    public class Role
    {
        public RoleId RoleId { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
