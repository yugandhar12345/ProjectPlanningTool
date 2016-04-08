﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TeamBins.Common;
using TeamBins6.Infrastrucutre.Services;


namespace TeamBins6.Controllers.Web
{
    public class UsersController : BaseController
    {
        readonly ITeamManager teamManager;
        public UsersController(ITeamManager teamManager)
        {
            this.teamManager = teamManager;
        }
        public async Task<ActionResult> Index()
        {
            var teamVm = await teamManager.GetTeamInoWithMembers();
            return View(teamVm);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddTeamMemberRequestVM model)
        {
            try
            {
                //if (ModelState.IsValid)
                {
                    model.SiteBaseUrl = AppBaseUrl;
                    await teamManager.AddNewTeamMember(model);
                    return Json(new { Status = "Success", Message = "Successfully added user to team" });
                }
                ////else
                //{
                //    var errors = ViewData.ModelState.Values.SelectMany(s => s.Errors.Select(g => g.ErrorMessage));
                //    return Json(new { Status = "Error", Message = "Error adding user to team", Errors= errors });
                //}
            }
            catch (Exception ex)
            {
                //Log
            }

            return Json(new { Status = "Error", Message = "Error adding user to team" });  
        }

    }
}