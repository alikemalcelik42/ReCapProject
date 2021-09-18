﻿using Core.DataAcces.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EntityRepositoryBase<Color, CarRentalContext>, IColorDal
    {
    }
}