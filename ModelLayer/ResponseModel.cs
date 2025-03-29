﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }

        public string Messege { get; set; }

        public T Data { get; set; }


    }
}
