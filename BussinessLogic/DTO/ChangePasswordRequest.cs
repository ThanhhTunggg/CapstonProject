﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.DTO
{
    public class ChangePasswordRequest
    {
        public int UserId { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string ReNewPass { get; set; }
    }
}
