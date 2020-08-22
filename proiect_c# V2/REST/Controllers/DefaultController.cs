using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace REST.Controllers
{
    public class DefaultController : ApiController
    {
    // GET: Default
    public Class1[] GetAll()
    {
      return new Class1[] {
        new Class1
          {
            Id = 1,
            Name="test1"
          },
          new Class1
          {
            Id=2,
            Name="test2"
          }
        };
      }
    }
}