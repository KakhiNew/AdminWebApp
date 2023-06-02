﻿namespace AdminWebApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime RegistrationTime { get; set; }
        public bool Blocked { get; set; }
    }
}
