using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Commands.DeleteLanguage;
using Application.Features.Languages.Commands.UpdateLanguage;
using Application.Features.Languages.Dtos.Languages;
using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetByIdLanguage;
using Application.Features.Languages.Queries.GetListLanguage;
using Core.Application.Requests;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLanguageCommand createLanguageCommand)
        {
            CreatedLanguageDto result = await Mediator.Send(createLanguageCommand);
            return Created("", result);
        }



        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListLanguageQuery getListLanguageQuery = new() { PageRequest = pageRequest };
            LanguageListModel result = await Mediator.Send(getListLanguageQuery);

            return Ok(result);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdLanguageQuery getByIdLanguageQuery)
        {
            GetByIdLanguageDto getByIdLanguageDto = await Mediator.Send(getByIdLanguageQuery);
            return Ok(getByIdLanguageDto);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete([FromBody] DeleteLanguageCommand deleteLanguageCommand)
        {
            DeletedLanguageDto result = await Mediator.Send(deleteLanguageCommand);

            return Ok(result); 
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLanguageCommand updateLanguageCommand)
        {
            UpdatedLanguageDto result = await Mediator.Send(updateLanguageCommand);

            return Ok(result);
        }


    }
}
