﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Entities
{
    internal interface IEntity<T>
    {

        public T Id { get; set; }
    }
}
