﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonogramApp.Models
{
    internal class users
    {

    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
    }

    public static class Session
    {
        public static User CurrentUser { get; set; }
    }
}
