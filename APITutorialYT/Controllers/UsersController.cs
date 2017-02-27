using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APITutorialYT.Controllers
{
    [EnableCorsAttribute ("*", "*", "*")]
    [Authorize]
    public class UsersController : ApiController
    {
        //[DisableCors]
        [HttpGet]
        //[BasicAuthentication]
        public HttpResponseMessage LoadAllUsers(string userName="All")
        {
            //string username = Thread.CurrentPrincipal.Identity.Name; //get the value saved in BasicAuthenticationAttribute class

            FINANCIERAAPP_testEntities userContext = new FINANCIERAAPP_testEntities();
            switch (userName.ToLower())
            {
                case "all":
                    return Request.CreateResponse(HttpStatusCode.OK, userContext.PaymentsUsers.ToList());
                case "sramirez":
                    return Request.CreateResponse(HttpStatusCode.OK,
                        userContext.PaymentsUsers.Where(e => e.UserName.ToLower() == "sramirez").ToList());
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The username was not found");
            }
        }

        public HttpResponseMessage Get(int id)
        {
            FINANCIERAAPP_testEntities userContext = new FINANCIERAAPP_testEntities();
            var entity = userContext.PaymentsUsers.FirstOrDefault( e => e.IDUser == id);

            if(entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id = " + id.ToString() + " was not found");
            }
        }

        public HttpResponseMessage Post([FromBody] PaymentsUser user)
        {
            try
            {
                FINANCIERAAPP_testEntities userContext = new FINANCIERAAPP_testEntities();
                userContext.PaymentsUsers.Add(user);
                userContext.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created, user);
                message.Headers.Location = new Uri(Request.RequestUri + user.IDUser.ToString());
                return message;
            } catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                FINANCIERAAPP_testEntities userContext = new FINANCIERAAPP_testEntities();
                var entity = userContext.PaymentsUsers.FirstOrDefault(e => e.IDUser == id);
                if (entity == null) //user not found
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id = " + id.ToString() + " was not found");
                }
                else
                {
                    userContext.PaymentsUsers.Remove(entity);
                    userContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            } catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody]int id, [FromUri]PaymentsUser user)
        {
            try
            {
                FINANCIERAAPP_testEntities userContext = new FINANCIERAAPP_testEntities();
                PaymentsUser entity = userContext.PaymentsUsers.FirstOrDefault(e => e.IDUser == id);
                if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id = " + id.ToString() + " was not found");
                }
                else
                {
                    entity.UserName = user.UserName;
                    entity.Password = user.Password;
                    entity.Name = user.Name;

                    userContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                

            } catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
