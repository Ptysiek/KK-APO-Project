﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    abstract public class IPopupBuilder
    {
        abstract public IPopup GetResult();
    }
}
