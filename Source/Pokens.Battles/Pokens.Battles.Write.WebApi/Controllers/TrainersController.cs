﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pokens.Battles.Business;
using Pokens.Battles.Write.WebApi.Models;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Http;

namespace Pokens.Battles.Write.WebApi.Controllers
{
    [Route("api/v1/trainers")]
    public sealed class TrainersController : ControllerBase
    {
        private readonly IIdentifiedUser user;
        private readonly IMediator mediator;

        public TrainersController(IIdentifiedUser user, IMediator mediator)
        {
            this.user = user;
            this.mediator = mediator;
        }

        [HttpPatch("me/battles/{battleId:Guid}")]
        public async Task<IActionResult> UseAbility([FromRoute] Guid battleId, [FromBody] UseAbilityModel model)
        {
            var command = new UseAbilityCommand(battleId, new Guid(user.Property("Id").Value), model.AbilityId);
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }

        [HttpPatch("me/auto")]
        public async Task<IActionResult> ToggleAutoMode()
        {
            var command = new ToggleAutoModeCommand(new Guid(user.Property("Id").Value));
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }
    }
}